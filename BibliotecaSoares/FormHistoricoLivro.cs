using BibliotecaSoares.Data;
using BibliotecaSoares.Models.DTO;
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
    public partial class FormHistoricoLivro : Form
    {
        private readonly int _idLivro;
        private readonly string _tituloLivro;

        // O construtor agora exige o ID e o Título do livro ao ser aberto
        public FormHistoricoLivro(int idLivro, string tituloLivro)
        {
            InitializeComponent();
            _idLivro = idLivro;
            _tituloLivro = tituloLivro;

            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Histórico do Livro";
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lblTituloLivro.Text = $"Leitores do Livro: {_tituloLivro}";
            await CarregarHistoricoLeitoresAsync();
        }

        private async Task CarregarHistoricoLeitoresAsync()
        {
            try
            {
                using (var context = new AppDbContext()) // Substitua pelo seu contexto
                {
                    // Busca os empréstimos filtrando por este LIVRO específico
                    var dados = await context.Emprestimos
                        .Include(e => e.Usuario) // OBRIGATÓRIO: Carrega os dados do aluno que pegou
                        .Where(e => e.LivroId == _idLivro)
                        .OrderByDescending(e => e.DataEmprestimo) // Empréstimos mais recentes primeiro
                        .Select(e => new HistorialLeitoresDTO
                        {
                            Aluno = e.Usuario.Nome,
                            Turma = e.Usuario.Turma, // Se Turma estiver direto no empréstimo ou e.Usuario.Turma
                            DataEmprestimo = e.DataEmprestimo,
                            PrazoDevolucao = e.DataPrevistaDevolucao,
                            Situacao = e.DataDevolucaoReal == null
                            ? "🔴 PENDENTE (Com o aluno)"
                            : $"🟢 Devolvido em {e.DataDevolucaoReal.Value.ToString("dd/MM/yyyy")}"
                        })
                    .ToListAsync();

                    dgvHistoricoLeitores.DataSource = null;
                    dgvHistoricoLeitores.DataSource = dados;

                    FormatarGridHistoricoLeitores();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar leitores: {ex.Message}");
            }
        }

        private void FormatarGridHistoricoLeitores()
        {
            if (dgvHistoricoLeitores.Columns.Count == 0) return;

            // Cabeçalhos
            dgvHistoricoLeitores.Columns["Aluno"].HeaderText = "Nome do Aluno";
            dgvHistoricoLeitores.Columns["Turma"].HeaderText = "Turma";
            dgvHistoricoLeitores.Columns["DataEmprestimo"].HeaderText = "Data de Saída";
            dgvHistoricoLeitores.Columns["PrazoDevolucao"].HeaderText = "Prazo para Entrega";
            dgvHistoricoLeitores.Columns["Situacao"].HeaderText = "Situação atual";

            // Formato das Datas
            dgvHistoricoLeitores.Columns["DataEmprestimo"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvHistoricoLeitores.Columns["PrazoDevolucao"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Padronização Visual (Letras uniformes tamanho 9)
            Font fontePadrao = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgvHistoricoLeitores.Font = fontePadrao;
            dgvHistoricoLeitores.DefaultCellStyle.Font = fontePadrao;
            dgvHistoricoLeitores.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvHistoricoLeitores.RowTemplate.Height = 28;

            // Tamanhos das Colunas
            dgvHistoricoLeitores.Columns["Turma"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvHistoricoLeitores.Columns["DataEmprestimo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvHistoricoLeitores.Columns["PrazoDevolucao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvHistoricoLeitores.Columns["Situacao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvHistoricoLeitores.Columns["Aluno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void FormHistoricoLivro_Load(object sender, EventArgs e)
        {

        }

        private void btnImprimirHistoricoLivro_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.DesenharRelatorioPage);
            pd.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);

            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = pd;
            previewDialog.WindowState = FormWindowState.Maximized;
            previewDialog.Text = "Visualizar Impressão - Histórico do Livro";

            // --- A CIRURGIA NA BARRA DE FERRAMENTAS ---
            ToolStrip barraDeFerramentas = null;

            // Procura a barra de ferramentas dentro da janela de preview
            foreach (Control controle in previewDialog.Controls)
            {
                if (controle is ToolStrip)
                {
                    barraDeFerramentas = (ToolStrip)controle;
                    break;
                }
            }

            if (barraDeFerramentas != null)
            {
                // Pega o botão de imprimir original (que é o primeiro item, índice 0)
                ToolStripItem botaoOriginal = barraDeFerramentas.Items[0];

                // Cria o nosso novo botão
                ToolStripButton novoBotaoImprimir = new ToolStripButton();
                novoBotaoImprimir.Image = botaoOriginal.Image; // Copia a imagem original para ninguém perceber a troca!
                novoBotaoImprimir.ToolTipText = "Escolher Impressora ou Salvar PDF";

                // Ensina o nosso botão a abrir a caixa de impressoras em vez de imprimir direto
                novoBotaoImprimir.Click += (s, args) =>
                {
                    using (PrintDialog caixaDeImpressao = new PrintDialog())
                    {
                        caixaDeImpressao.Document = pd;
                        caixaDeImpressao.UseEXDialog = true; // Interface moderna do Windows

                        if (caixaDeImpressao.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                pd.Print();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Erro na impressão: {ex.Message}");
                            }
                        }
                    }
                };

                // Troca os botões
                barraDeFerramentas.Items.Remove(botaoOriginal);
                barraDeFerramentas.Items.Insert(0, novoBotaoImprimir);
            }

            // Por fim, exibe a tela de pré-visualização (agora com o nosso botão "infiltrado")
            previewDialog.ShowDialog();
        }

        private void DesenharRelatorioPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            // Definição da nossa "Ditadura da Fonte" para o papel
            Font fonteTitulo = new Font("Segoe UI", 18, FontStyle.Bold);
            Font fonteSubtitulo = new Font("Segoe UI", 10, FontStyle.Italic);
            Font fonteCabecalhoTabela = new Font("Segoe UI", 10, FontStyle.Bold);
            Font fonteLinhasTabela = new Font("Segoe UI", 9, FontStyle.Regular);
            Brush pincelPreto = Brushes.Black;

            // Pega as margens da página
            float inicioX = e.MarginBounds.Left;
            float inicioY = e.MarginBounds.Top;
            float fimX = e.MarginBounds.Right;

            // --- CABEÇALHO DO RELATÓRIO ---
            g.DrawString("COLÉGIO ESTADUAL ODOLFO SOARES", fonteSubtitulo, pincelPreto, inicioX, inicioY);
            inicioY += 20;
            g.DrawString("Relatório de Histórico de Empréstimos", fonteTitulo, pincelPreto, inicioX, inicioY);
            inicioY += 35;

            // Nome do Livro em destaque
            g.DrawString($"Livro: {_tituloLivro}", new Font("Segoe UI", 12, FontStyle.Bold), pincelPreto, inicioX, inicioY);
            inicioY += 20;
            g.DrawString($"Data de Emissão: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}", fonteSubtitulo, pincelPreto, inicioX, inicioY);
            inicioY += 40; // Dá um espaço para começar a tabela

            // --- CABEÇALHO DA TABELA ---
            // Definimos a posição X de cada coluna para não encavalarem
            float colAlunoX = inicioX;
            float colTurmaX = inicioX + 220;
            float colDataX = inicioX + 340;
            float colPrazoX = inicioX + 440;
            float colStatusX = inicioX + 540;

            g.DrawString("Aluno", fonteCabecalhoTabela, pincelPreto, colAlunoX, inicioY);
            g.DrawString("Turma", fonteCabecalhoTabela, pincelPreto, colTurmaX, inicioY);
            g.DrawString("Empréstimo", fonteCabecalhoTabela, pincelPreto, colDataX, inicioY);
            g.DrawString("Prazo", fonteCabecalhoTabela, pincelPreto, colPrazoX, inicioY);
            g.DrawString("Situação", fonteCabecalhoTabela, pincelPreto, colStatusX, inicioY);

            // Desenha uma linha preta estilosa abaixo do cabeçalho da tabela
            inicioY += 20;
            g.DrawLine(Pens.Black, inicioX, inicioY, fimX, inicioY);
            inicioY += 10;

            // --- LINHAS DA TABELA (DADOS) ---
            foreach (DataGridViewRow linha in dgvHistoricoLeitores.Rows)
            {
                // Ignora a linha em branco de segurança do Grid
                if (linha.IsNewRow) continue;

                // Puxa os dados das células (use os nomes exatos do seu HistorialLeitoresDTO)
                string aluno = linha.Cells["Aluno"].Value?.ToString() ?? "";
                string turma = linha.Cells["Turma"].Value?.ToString() ?? "";
                string dataEmp = Convert.ToDateTime(linha.Cells["DataEmprestimo"].Value).ToString("dd/MM/yyyy");
                string prazo = Convert.ToDateTime(linha.Cells["PrazoDevolucao"].Value).ToString("dd/MM/yyyy");
                string situacao = linha.Cells["Situacao"].Value?.ToString() ?? "";

                // TRUQUE VISUAL: Remove os círculos coloridos da tela para o papel não sair com caracteres estranhos
                situacao = situacao.Replace("🔴 ", "").Replace("🟢 ", "");

                // Limita o nome do aluno se ele for comprido demais para não invadir a coluna da Turma
                if (aluno.Length > 28) aluno = aluno.Substring(0, 25) + "...";

                // Desenha os textos da linha atual
                g.DrawString(aluno, fonteLinhasTabela, pincelPreto, colAlunoX, inicioY);
                g.DrawString(turma, fonteLinhasTabela, pincelPreto, colTurmaX, inicioY);
                g.DrawString(dataEmp, fonteLinhasTabela, pincelPreto, colDataX, inicioY);
                g.DrawString(prazo, fonteLinhasTabela, pincelPreto, colPrazoX, inicioY);
                g.DrawString(situacao, fonteLinhasTabela, pincelPreto, colStatusX, inicioY);

                // Desenha uma linha cinza bem discreta separando os registros (estilo caderno)
                inicioY += 18;
                g.DrawLine(Pens.LightGray, inicioX, inicioY, fimX, inicioY);
                inicioY += 7;

                // Verifica se chegou ao fim da página (Garante que se o livro tiver dezenas de leitores, ele cria a página 2 automaticamente)
                if (inicioY > e.MarginBounds.Bottom - 20)
                {
                    e.HasMorePages = true;
                    return; // Para o desenho desta página e pula para a próxima
                }
            }

            // Se chegou aqui, renderizou tudo e não precisa de mais páginas
            e.HasMorePages = false;
        }
    }
}
