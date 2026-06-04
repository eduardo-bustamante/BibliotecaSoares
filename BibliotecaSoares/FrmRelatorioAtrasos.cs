using BibliotecaSoares.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;


namespace BibliotecaSoares
{
    public partial class FrmRelatorioAtrasos : Form
    {

        private readonly AppDbContext _context;
        private string _nomeFiltroInicial = "";
        // Variável para controlar em qual linha a impressora está
        private int _linhaAtualImpressao = 0;
        private bool filtroAtivo = false;

        public FrmRelatorioAtrasos(string nomeFiltro = "")
        {
            InitializeComponent();
            _context = new AppDbContext();

            // Guarda o nome recebido na variável
            _nomeFiltroInicial = nomeFiltro;
        }

        private async void FrmRelatorioAtrasos_Load(object sender, EventArgs e)
        {

            txt_NovoValorMulta.Visible = false; // Habilita a caixa de texto para o usuário digitar o value
            lblDinheiro.Visible = false; // Exibe o "R$" ao lado da caixa de texto
            btnOk.Visible = false; // Exibe o botão de confirmação



            if (!string.IsNullOrWhiteSpace(_nomeFiltroInicial))
            {
                txt_nomeBusca.Text = _nomeFiltroInicial;
            }

            lblTarifaAtual.Text = $"Tarifa de atraso atual: {Properties.Settings.Default.ValorDiariaMulta:C} / dia";
            // 4. Manda carregar a grade já passando o nome!
            CarregarGridAtrasados(_nomeFiltroInicial);
        }
        private async void CarregarGridAtrasados(string nomeBusca = "")
        {
            try
            {
                // 1. Declaração das variáveis
                DateTime dataHoje = DateTime.Now.Date;
                // Lê o valor diretamente das configurações do sistema
                decimal valorMultaDiaria = Properties.Settings.Default.ValorDiariaMulta;
                // 2. Prepara a busca base (Atrasados OU com Dívida pendente)
                var query = _context.Emprestimos
                    .Include(emp => emp.Usuario)
                    .Include(emp => emp.Livro)
                    .Where(emp =>
                        (emp.Devolvido == false && emp.DataPrevistaDevolucao.Date < dataHoje) ||
                        (emp.Devolvido == true && emp.ValorMultaPaga > 0)
                    );

                // 3. A MÁGICA DA BUSCA: Se a pessoa digitou um nome, adiciona o filtro!
                if (!string.IsNullOrWhiteSpace(nomeBusca))
                {
                    query = query.Where(emp => emp.Usuario.Nome.Contains(nomeBusca));
                }

                // 4. Executa a busca final no banco de dados
                var emprestimosAtrasados = await query.ToListAsync();

                // 5. Monta a lista calculando os valores exatos
                var listaParaExibicao = emprestimosAtrasados.Select(emp =>
                {
                    decimal dividaAtual = 0;
                    int diasAtraso = 0;
                    string statusDoLivro = "";

                    if (emp.Devolvido == true)
                    {
                        statusDoLivro = "Devolvido";
                        dividaAtual = emp.ValorMultaPaga;
                        diasAtraso = (emp.DataDevolucaoReal.Value.Date - emp.DataPrevistaDevolucao.Date).Days;
                    }
                    else
                    {
                        statusDoLivro = "Pendente";
                        diasAtraso = (dataHoje - emp.DataPrevistaDevolucao.Date).Days;
                        dividaAtual = diasAtraso * valorMultaDiaria;
                    }

                    return new
                    {
                        EmprestimoID = emp.Id,
                        Aluno = emp.Usuario.Nome,
                        Turma = emp.Usuario.Turma,
                        Livro = emp.Livro.Titulo,
                        Status = statusDoLivro,
                        DataPrevista = emp.DataPrevistaDevolucao.ToShortDateString(),
                        Dias = diasAtraso,
                        MultaAcumulada = dividaAtual.ToString("C")
                    };
                }).ToList();

                // Joga os dados formatados na grade
                dgvAtrasados.DataSource = null;
                dgvAtrasados.DataSource = listaParaExibicao;

                // =========================================================================
                // 6. ATUALIZAÇÃO DINÂMICA DAS LABELS (O NOVO BLOCO AQUI!)
                // =========================================================================

                // A quantidade é simplesmente o total de itens da lista filtrada
                int totalLivrosAtrasados = listaParaExibicao.Count;

                // Fazemos a soma matemática exata usando os valores numéricos
                decimal somaTotalDividas = 0;
                foreach (var emp in emprestimosAtrasados)
                {
                    if (emp.Devolvido == true)
                    {
                        somaTotalDividas += emp.ValorMultaPaga;
                    }
                    else
                    {
                        int dias = (dataHoje - emp.DataPrevistaDevolucao.Date).Days;
                        somaTotalDividas += (dias * valorMultaDiaria);
                    }
                }

                // IMPORTANTE: Substitua 'lblQuantidade' e 'lblValorTotal' pelos nomes exatos das suas Labels!
                lblQuantidade.Text = totalLivrosAtrasados.ToString();
                lblValorTotal.Text = somaTotalDividas.ToString("C"); // Exibe formatado em R$

                // =========================================================================

                if (dgvAtrasados.Columns.Count > 0)
                {
                    dgvAtrasados.Columns["EmprestimoID"].HeaderText = "Cód.";
                    dgvAtrasados.Columns["Status"].HeaderText = "Situação";
                    dgvAtrasados.Columns["DataPrevista"].HeaderText = "Vencimento";
                    dgvAtrasados.Columns["Dias"].HeaderText = "Dias Atraso";
                    dgvAtrasados.Columns["MultaAcumulada"].HeaderText = "Dívida Atual";

                    // >>> A MÁGICA DO TAMANHO AUTOMÁTICO ENTRA AQUI <<<
                    dgvAtrasados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar o relatório de atrasos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnPagarMulta_ClickAsync(object sender, EventArgs e)
        {
            // 1. Validação de segurança: Verifica se tem alguma linha selecionada
            if (dgvAtrasados.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecione um aluno na grade para registrar o pagamento.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Resgata o ID do Empréstimo da linha clicada
                // ATENÇÃO: Verifique se o nome da coluna é "EmprestimoID" mesmo, como definimos no carregamento
                int idEmprestimo = Convert.ToInt32(dgvAtrasados.SelectedRows[0].Cells["EmprestimoID"].Value);

                // 3. Busca o registro fresquinho direto do banco de dados
                var emprestimo = await _context.Emprestimos.FindAsync(idEmprestimo);

                if (emprestimo == null) return;

                // 4. Pede uma confirmação rápida para evitar cliques acidentais
                var confirmacao = MessageBox.Show(
                    "Deseja confirmar a quitação desta pendência?\n\nIsso irá zerar a dívida do aluno no sistema.",
                    "Confirmar Pagamento",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmacao == DialogResult.No) return;

                // 5. A LÓGICA DE DECISÃO BIFURCADA
                if (emprestimo.Devolvido == true)
                {
                    // CENÁRIO A: O livro já estava na prateleira, ele só veio pagar o que devia.
                    // Apenas zera a conta.
                    emprestimo.ValorMultaPaga = 0;
                }
                else
                {
                    // CENÁRIO B: Ele trouxe o livro atrasado hoje e já pagou a multa na hora.
                    // Encerra o empréstimo E zera a conta.
                    emprestimo.Devolvido = true;
                    emprestimo.DataDevolucaoReal = DateTime.Now;
                    emprestimo.ValorMultaPaga = 0;
                }

                // 6. Avisa o banco de dados e salva fisicamente
                _context.Emprestimos.Update(emprestimo);
                await _context.SaveChangesAsync();

                // 7. Feedback de sucesso e atualização visual
                MessageBox.Show("Pagamento processado e conta zerada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Como o registro agora tem Devolvido = true e Multa = 0, 
                // ao recarregar a grade, ele vai sumir automaticamente da tela de devedores!
                CarregarGridAtrasados();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao processar o pagamento: {ex.Message}", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnBuscarNomeAtrasado_ClickAsync(object sender, EventArgs e)
        {
            filtroAtivo = true;
            // Troque 'txt_nomeBusca' pelo nome da sua caixa de texto
            string nomeDigitado = txt_nomeBusca.Text.Trim();
            CarregarGridAtrasados(nomeDigitado);
        }

        private async void btnPagarTudo_ClickAsync(object sender, EventArgs e)
        {
            // 1. Verifica se a grade está vazia para não rodar código à toa
            if (dgvAtrasados.Rows.Count == 0) return;

            // 2. Confirmação de segurança (muito importante para ações em massa!)
            var confirmacao = MessageBox.Show(
                "Deseja realmente QUITAR e RESOLVER todas as pendências listadas na tela agora?\n\n" +
                "Isso fará a devolução de todos os livros listados e zerará todas as dívidas.",
                "Quitação em Massa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation);

            if (confirmacao == DialogResult.No) return;

            try
            {
                // 3. Cria uma lista vazia e guarda todos os IDs que estão aparecendo na tela
                List<int> idsParaAtualizar = new List<int>();

                foreach (DataGridViewRow linha in dgvAtrasados.Rows)
                {
                    // Pega o ID oculto de cada linha
                    int id = Convert.ToInt32(linha.Cells["EmprestimoID"].Value);
                    idsParaAtualizar.Add(id);
                }

                // 4. Busca todos esses empréstimos no banco de dados em um comando só (muito rápido!)
                var emprestimosParaQuitar = await _context.Emprestimos
                    .Where(emp => idsParaAtualizar.Contains(emp.Id))
                    .ToListAsync();

                // 5. Aplica a regra de ouro para cada um dos registros encontrados
                foreach (var emp in emprestimosParaQuitar)
                {
                    if (emp.Devolvido == false)
                    {
                        // Se ainda estava com o aluno, faz a devolução
                        emp.Devolvido = true;
                        emp.DataDevolucaoReal = DateTime.Now;
                    }

                    // Independente se devolveu agora ou já tinha devolvido, a dívida morre aqui
                    emp.ValorMultaPaga = 0;

                    // Avisa o Entity Framework que este registro sofreu alteração
                    _context.Emprestimos.Update(emp);
                }

                // 6. Salva todas as alterações no SQL Server de uma única vez!
                await _context.SaveChangesAsync();

                MessageBox.Show("Todas as pendências listadas foram resolvidas com sucesso!", "Dívida Quitada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 7. Limpa a busca e atualiza a grade (ela deve ficar vazia para esse aluno agora)
                txt_nomeBusca.Clear();
                CarregarGridAtrasados();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao tentar processar o pagamento em massa: {ex.Message}", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvAtrasados.Rows.Count == 0)
            {
                MessageBox.Show("Não há registros na tela para serem impressos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _linhaAtualImpressao = 0;

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.DesenharPaginaRelatorio);

            // Deixamos apenas UMA instância do visualizador
            PrintPreviewDialog visualizador = new PrintPreviewDialog();

            visualizador.Document = pd;
            visualizador.WindowState = FormWindowState.Maximized;

            // --- A CIRURGIA NA BARRA DE FERRAMENTAS ---
            // --- A CIRURGIA NA BARRA DE FERRAMENTAS ---
            ToolStrip barraFerramentas = null;

            foreach (Control controle in visualizador.Controls)
            {
                if (controle is ToolStrip)
                {
                    barraFerramentas = (ToolStrip)controle;
                    break;
                }
            }

            if (barraFerramentas != null)
            {
                // 1. Pega o botão original
                ToolStripItem botaoOriginal = barraFerramentas.Items[0];

                // 2. SALVA A IMAGEM ANTES DE REMOVER O BOTÃO (O grande segredo!)
                Image iconeOriginal = botaoOriginal.Image;

                // 3. Remove o botão original
                barraFerramentas.Items.Remove(botaoOriginal);

                // 4. Cria o nosso novo botão
                ToolStripButton meuBotaoImprimir = new ToolStripButton();

                // 5. Aplica a imagem salva e FORÇA ele a mostrar o ícone
                meuBotaoImprimir.Image = iconeOriginal;
                meuBotaoImprimir.DisplayStyle = ToolStripItemDisplayStyle.Image; // A linha que faz o ícone aparecer!
                meuBotaoImprimir.ToolTipText = "Imprimir ou Salvar em PDF";

                // 6. Diz o que o botão vai fazer ao ser clicado
                meuBotaoImprimir.Click += (s, args) =>
                {
                    using (PrintDialog telaDeImpressoras = new PrintDialog())
                    {
                        telaDeImpressoras.Document = pd;
                        telaDeImpressoras.UseEXDialog = true;

                        if (telaDeImpressoras.ShowDialog() == DialogResult.OK)
                        {
                            pd.Print();
                        }
                    }
                };

                // 7. Coloca o botão na barra
                barraFerramentas.Items.Insert(0, meuBotaoImprimir);
            }
            // =========================================================================
            // BUSCAMOS NA TELA CERTA (visualizador)
            foreach (Control controle in visualizador.Controls)
            {
                if (controle is ToolStrip)
                {
                    barraFerramentas = (ToolStrip)controle;
                    break;
                }
            }

            if (barraFerramentas != null)
            {
                // Pega o botão original (que é sempre o primeiro item, índice 0)
                ToolStripItem botaoOriginal = barraFerramentas.Items[0];

                // Remove o botão original
                barraFerramentas.Items.Remove(botaoOriginal);

                // Cria um novo botão com o mesmo desenho (ícone) da impressora
                ToolStripButton meuBotaoImprimir = new ToolStripButton();
                meuBotaoImprimir.Image = botaoOriginal.Image;
                meuBotaoImprimir.ToolTipText = "Imprimir ou Salvar em PDF";

                // Diz o que o botão vai fazer ao ser clicado
                meuBotaoImprimir.Click += (s, args) =>
                {
                    using (PrintDialog telaDeImpressoras = new PrintDialog())
                    {
                        telaDeImpressoras.Document = pd;
                        telaDeImpressoras.UseEXDialog = true; // Usa a tela moderna de impressoras do Windows

                        // Abre a tela. Se a pessoa escolher a impressora e clicar em OK, ele imprime!
                        if (telaDeImpressoras.ShowDialog() == DialogResult.OK)
                        {
                            pd.Print();
                        }
                    }
                };

                // Coloca o nosso botão novo na mesma posição (0) do antigo
                barraFerramentas.Items.Insert(0, meuBotaoImprimir);
            }
            // =========================================================================

            // Finalmente, exibe a tela na frente de tudo
            visualizador.ShowDialog();
        }
        private void DesenharPaginaRelatorio(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            var nomeAluno = (dgvAtrasados.Rows.Count > 0) ? dgvAtrasados.Rows[0].Cells["Aluno"].Value.ToString() : "Todos os Alunos";

            // 1. Configuração de Fontes e Margens
            Font fonteTitulo = new Font("Arial", 16, FontStyle.Bold);
            Font fonteSubtitulo = new Font("Arial", 10, FontStyle.Regular);
            Font fonteCabecalhoGrid = new Font("Arial", 10, FontStyle.Bold);
            Font fonteLinhas = new Font("Arial", 9, FontStyle.Regular);

            int margemEsquerda = 50;
            int posicaoY = 50; // Começa no topo da página

            // 2. Desenhando o Cabeçalho do Relatório
            e.Graphics.DrawString("Colégio Odolfo Soares", fonteTitulo, Brushes.Black, margemEsquerda, posicaoY);
            posicaoY += 30;
            e.Graphics.DrawString($"Relatório de Pendências da Biblioteca - Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm}", fonteSubtitulo, Brushes.Black, margemEsquerda, posicaoY);
            posicaoY += 40;
            if (!string.IsNullOrWhiteSpace(txt_nomeBusca.Text) && filtroAtivo)
            {
                e.Graphics.DrawString($"Aluno: {nomeAluno} ", fonteSubtitulo, Brushes.Black, margemEsquerda, posicaoY);
                posicaoY += 50;
                //filtroAtivo = false; // Reseta o filtro para a próxima impressão, que deve ser geral se a pessoa clicar de novo sem buscar um nome específico
            }
            else
            {
                e.Graphics.DrawString($"Todos os Alunos", fonteSubtitulo, Brushes.Black, margemEsquerda, posicaoY);
                posicaoY += 50;
            }

            // 3. Posição horizontal (X) de cada coluna no papel
            int[] colX = { 50, 250, 500, 650 };

            // Desenhando os Títulos das Colunas
            e.Graphics.DrawString("ALUNO", fonteCabecalhoGrid, Brushes.Black, colX[0], posicaoY);
            e.Graphics.DrawString("LIVRO", fonteCabecalhoGrid, Brushes.Black, colX[1], posicaoY);
            e.Graphics.DrawString("VENCIMENTO", fonteCabecalhoGrid, Brushes.Black, colX[2], posicaoY);
            e.Graphics.DrawString("DÍVIDA", fonteCabecalhoGrid, Brushes.Black, colX[3], posicaoY);

            posicaoY += 25;
            e.Graphics.DrawLine(Pens.Black, margemEsquerda, posicaoY, 750, posicaoY); // Linha divisória
            posicaoY += 15;

            // 4. O LAÇO DE REPETIÇÃO: Vai desenhando os dados da grade
            while (_linhaAtualImpressao < dgvAtrasados.Rows.Count)
            {
                DataGridViewRow linha = dgvAtrasados.Rows[_linhaAtualImpressao];

                // Resgata os textos das células da grade
                string aluno = linha.Cells["Aluno"].Value?.ToString() ?? "";
                string livro = linha.Cells["Livro"].Value?.ToString() ?? "";
                string vencimento = linha.Cells["DataPrevista"].Value?.ToString() ?? "";
                string divida = linha.Cells["MultaAcumulada"].Value?.ToString() ?? "";

                // Limita o tamanho do nome do aluno e do livro para não encavalar o texto no papel
                if (aluno.Length > 25) aluno = aluno.Substring(0, 25) + "...";
                if (livro.Length > 30) livro = livro.Substring(0, 30) + "...";

                // Imprime a linha
                e.Graphics.DrawString(aluno, fonteLinhas, Brushes.Black, colX[0], posicaoY);
                e.Graphics.DrawString(livro, fonteLinhas, Brushes.Black, colX[1], posicaoY);
                e.Graphics.DrawString(vencimento, fonteLinhas, Brushes.Black, colX[2], posicaoY);
                e.Graphics.DrawString(divida, fonteLinhas, Brushes.Black, colX[3], posicaoY);

                posicaoY += 25; // Pula para a próxima linha
                _linhaAtualImpressao++; // Registra que essa linha já foi impressa

                // 5. QUEBRA DE PÁGINA: Verifica se o papel acabou
                if (posicaoY >= e.MarginBounds.Bottom)
                {
                    // O papel encheu! Avisa o documento que tem mais páginas
                    e.HasMorePages = true;
                    return; // Ele sai do método, troca a folha, e entra no método de novo onde parou
                }
            }
            // <-- Esta é a chave que fecha o seu "while (_linhaAtualImpressao < dgvAtrasados.Rows.Count)"

            // =========================================================================
            // 6. IMPRESSÃO DO RODAPÉ (TOTALIZADOR)
            // Se o código chegou até aqui, é porque todas as linhas foram impressas!
            // =========================================================================

            posicaoY += 20; // Pula um espaço após o último aluno

            // Desenha uma linha separadora mais grossa para destacar o total
            e.Graphics.DrawLine(new Pen(Color.Black, 2), margemEsquerda, posicaoY, 750, posicaoY);
            posicaoY += 10;

            // A Matemática: Somar os valores da grade
            decimal somaTotal = 0;
            foreach (DataGridViewRow r in dgvAtrasados.Rows)
            {
                string textoMoeda = r.Cells["MultaAcumulada"].Value?.ToString() ?? "0";

                // O C# não sabe somar letras, então nós "limpamos" o R$ antes da conta
                textoMoeda = textoMoeda.Replace("R$", "").Trim();

                if (decimal.TryParse(textoMoeda, out decimal valorLinha))
                {
                    somaTotal += valorLinha;
                }
            }

            // Desenha o texto do Total
            Font fonteTotal = new Font("Arial", 12, FontStyle.Bold);

            // Escreve a palavra "TOTAL:" alinhada um pouco antes do valor
            e.Graphics.DrawString("TOTAL:", fonteTotal, Brushes.Black, colX[2], posicaoY);

            // Escreve o valor em dinheiro, alinhado exatamente na mesma coluna das dívidas (colX[3])
            e.Graphics.DrawString(somaTotal.ToString("C"), fonteTotal, Brushes.Black, colX[3], posicaoY);

            // =========================================================================

            // Avisa que o documento acabou
            e.HasMorePages = false;
        }

        private void btnMudarValorPadrao_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja alterar o valor padrão da multa por atraso? Esta ação afetará o cálculo de todas as pendências futuras, mas não irá alterar as dívidas já acumuladas dos alunos. Se quiser alterar o valor apenas para um aluno específico, basta pedir para ele pagar a dívida atual e depois atualizar o valor para os próximos casos.", "Confirmação de Alteração", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                txt_NovoValorMulta.Visible = true; // Habilita a caixa de texto para o usuário digitar o valor
                txt_NovoValorMulta.Focus(); // Coloca o cursor dentro da caixa de texto para facilitar a digitação
                txt_NovoValorMulta.Text = Properties.Settings.Default.ValorDiariaMulta.ToString("F2"); // Preenche a caixa com o valor atual para facilitar a edição
                lblDinheiro.Visible = true; // Exibe o "R$" ao lado da caixa de texto
                btnOk.Visible = true; // Exibe o botão de confirmação
            }
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Verifica se a pessoa digitou alguma coisa
                if (string.IsNullOrWhiteSpace(txt_NovoValorMulta.Text))
                {
                    MessageBox.Show("Por favor, digite o novo valor da multa.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Converte o texto digitado para dinheiro (decimal)
                // O C# limpa um possível "R$" que a pessoa tenha digitado sem querer
                string textoLimpo = txt_NovoValorMulta.Text.Replace("R$", "").Trim();

                if (decimal.TryParse(textoLimpo, out decimal novoValor))
                {
                    // 3. A MÁGICA: Salva o valor na memória do computador
                    Properties.Settings.Default.ValorDiariaMulta = novoValor;
                    Properties.Settings.Default.Save(); // Grava fisicamente no Windows!

                    MessageBox.Show($"Valor padrão atualizado com sucesso para {novoValor:C}!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 4. Limpa a caixinha e recarrega a grade para refazer as contas de todo mundo com o preço novo
                    txt_NovoValorMulta.Clear();
                    lblTarifaAtual.Text = $"Tarifa de atraso atual: {Properties.Settings.Default.ValorDiariaMulta:C} / dia";

                    txt_NovoValorMulta.Visible = false; // Habilita a caixa de texto para o usuário digitar o value
                    lblDinheiro.Visible = false; // Exibe o "R$" ao lado da caixa de texto
                    btnOk.Visible = false; // Exibe o botão de confirmação

                    CarregarGridAtrasados(txt_nomeBusca.Text);
                }
                else
                {
                    MessageBox.Show("Valor inválido. Digite um número correto (ex: 2,50).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao tentar alterar o valor: {ex.Message}");
            }

        }

        private void btnLimparBuscar_Click(object sender, EventArgs e)
        {
            filtroAtivo = false;
            CarregarGridAtrasados(); // Recarrega a grade sem nenhum filtro, mostrando todos os atrasados
        }
    }
}
