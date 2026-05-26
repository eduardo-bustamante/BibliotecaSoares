using BibliotecaSoares.Models;
using BibliotecaSoares.Models.DTO;
using BibliotecaSoares.Repositories;

namespace BibliotecaSoares
{
    public partial class CadastroLivros : Form
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IEmprestimoRepository _emprestimoRepository;
        private int _idSelecionado = 0;
        private Relatorios _telaRelatorio; // Variável para armazenar a instância da tela de relatório
                                           // Variável para lembrar se o último clique foi de A-Z ou de Z-A
        private bool _ordemCrescente = true;

        public CadastroLivros()
        {
            InitializeComponent();
            _livroRepository = new LivroRepository();
            _emprestimoRepository = new EmprestimoRepository();
        }

        private async Task CarregarGridAsync()
        {

            try
            {

                var listaLivros = await _livroRepository.ListarTodosAsync();

                // Define a altura padrão para todas as NOVAS linhas que vão entrar na grade
                dgvLivros.RowTemplate.Height = 18;

                dgvLivros.DataSource = null;

                dgvLivros.DataSource = listaLivros;

                FormatarGridLivros();

                dgvLivros.Columns["Id"].HeaderText = "ID";
                dgvLivros.Columns["Titulo"].HeaderText = "Livro"; // Muda de 'Titulo' para 'Livro' na tela
                dgvLivros.Columns["Autor"].HeaderText = "Autor";
                dgvLivros.Columns["QuantidadeTotal"].HeaderText = "Qtd Total";
                dgvLivros.Columns["QuantidadeEmprestada"].HeaderText = "Emprestados";
                dgvLivros.Columns["QuantidadeDisponivel"].HeaderText = "Disponível";

                if (dgvLivros.Columns["Ano"] != null)
                {
                    dgvLivros.Columns["Ano"].Visible = false;
                }
                if (dgvLivros.Columns["Genero"] != null)
                {
                    dgvLivros.Columns["Genero"].Visible = false;
                }

                if (dgvLivros.Columns["Editora"] != null)
                {
                    dgvLivros.Columns["Editora"].Visible = false;
                }
                if (dgvLivros.Columns["Idioma"] != null)
                {
                    dgvLivros.Columns["Idioma"].Visible = false;
                }

                // --- TAMANHO DAS COLUNAS ---

                // 1. O ID e as Quantidades ficam no modo "AllCells" (Eles encolhem para ocupar 
                // apenas a largura exata do texto que está dentro deles)
                dgvLivros.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvLivros.Columns["QuantidadeTotal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvLivros.Columns["QuantidadeEmprestada"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvLivros.Columns["QuantidadeDisponivel"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                // 2. O Título do Livro e o Autor ficam no modo "Fill" (Eles vão esticar e 
                // dividir entre si todo o espaço vazio que sobrar na tela)
                dgvLivros.Columns["Titulo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvLivros.Columns["Autor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                // 1. Cria a fonte oficial que você deseja usar
                Font fontePadrao = new Font("JetBrains Mono", 9F, FontStyle.Regular);
                Font fonteCabecalho = new Font("JetBrains Mono", 9F, FontStyle.Bold);

                // 2. Aplica na raiz do controle
                dgvLivros.Font = fontePadrao;

                // 3. Aplica nas linhas normais e alternadas (Zebra)
                dgvLivros.DefaultCellStyle.Font = fontePadrao;
                dgvLivros.AlternatingRowsDefaultCellStyle.Font = fontePadrao;

                // 4. Aplica nos cabeçalhos
                dgvLivros.ColumnHeadersDefaultCellStyle.Font = fonteCabecalho;

                // 5. O PULO DO GATO: Percorre todas as colunas e apaga qualquer estilo rebelde
                foreach (DataGridViewColumn coluna in dgvLivros.Columns)
                {
                    coluna.DefaultCellStyle.Font = fontePadrao;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show($"Erro ao carregar dados: {ex.Message}");
            }
        }
        private async void btn_salvar_ClickAsync(object sender, EventArgs e)
        {
            // 1. Validação do Título
            if (string.IsNullOrWhiteSpace(txt_titulo.Text))
            {
                MessageBox.Show("O campo 'Título do Livro' não pode ficar em branco. Por favor, preencha-o.",
                                "Aviso de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txt_titulo.Focus(); // Coloca o cursor piscando dentro do campo vazio
                return; // O "return" cancela a execução e impede o sistema de continuar salvando
            }

            // 2. Validação do Autor
            if (string.IsNullOrWhiteSpace(txt_autor.Text))
            {
                MessageBox.Show("O campo 'Autor' não pode ficar em branco. Por favor, preencha-o.",
                                "Aviso de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txt_autor.Focus();
                return;
            }

            try
            {
                var novoLivro = new Livro
                {
                    Id = _idSelecionado,
                    Titulo = txt_titulo.Text,
                    Autor = txt_autor.Text,
                    Editora = txt_editora.Text,
                    Ano = Convert.ToInt32(nud_ano.Value),
                    Genero = txt_genero.Text,
                    QuantidadeTotal = Convert.ToInt32(nud_quantidade.Value),
                    Idioma = cb_idioma.Text,

                };

                if (_idSelecionado == 0)
                {
                    await _livroRepository.AdicionarAsync(novoLivro);
                    MessageBox.Show("Livro salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var confirmacao = MessageBox.Show("Tem certeza que deseja editar este livro?", "Confirmar Edição", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmacao == DialogResult.No)
                    {
                        return; // Se o usuário cancelar a edição, o método é encerrado aqui
                    }
                    await _livroRepository.AtualizarAsync(novoLivro);
                    MessageBox.Show("Livro atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _idSelecionado = 0; // Reseta o ID selecionado após a atualização
                }

                txt_codigo.Clear();
                txt_titulo.Clear();
                txt_autor.Clear();
                txt_editora.Clear();
                nud_ano.Value = 2026;
                txt_genero.Clear();
                nud_quantidade.Value = 1;
                cb_idioma.SelectedIndex = -1;


                await CarregarGridAsync();
                await AtualizarDashboardAsync(); // Atualiza o painel de estatísticas após salvar ou atualizar um livro
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvLivros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se o clique foi em uma linha válida (ignora clique no cabeçalho)
            if (e.RowIndex >= 0)
            {
                // Pega a linha que foi clicada
                DataGridViewRow linhaClicada = dgvLivros.Rows[e.RowIndex];

                _idSelecionado = Convert.ToInt32(linhaClicada.Cells["Id"].Value);
                txt_codigo.Text = linhaClicada.Cells["Id"].Value.ToString();
                txt_titulo.Text = linhaClicada.Cells["Titulo"].Value.ToString();
                txt_autor.Text = linhaClicada.Cells["Autor"].Value.ToString();
                txt_editora.Text = linhaClicada.Cells["Editora"].Value.ToString();
                nud_ano.Value = Convert.ToDecimal(linhaClicada.Cells["Ano"].Value);
                txt_genero.Text = linhaClicada.Cells["Genero"].Value.ToString();
                nud_quantidade.Value = Convert.ToDecimal(linhaClicada.Cells["QuantidadeTotal"].Value);
                cb_idioma.Text = linhaClicada.Cells["Idioma"].Value.ToString();
            }
        }

        private async void btn_deletar_ClickAsync(object sender, EventArgs e)
        {
            if (_idSelecionado == 0)
            {
                MessageBox.Show("Por favor, dê um duplo clique em um usuário na grade para selecioná-lo antes de excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Para a execução do código aqui
            }

            var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este usuário permanentemente?", "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    await _livroRepository.ExcluirAsync(_idSelecionado);
                    MessageBox.Show("Livro excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _idSelecionado = 0; // Reseta o ID selecionado após a exclusão
                    await CarregarGridAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao excluir: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void txt_pesquisa_TextChangedAsync(object sender, EventArgs e)
        {
            try
            {
                string termoPesquisa = txt_pesquisa.Text.Trim();

                if (string.IsNullOrEmpty(termoPesquisa))
                {
                    await CarregarGridAsync(); // Recarrega a grade com todos os livros se o campo de pesquisa estiver vazio
                    return;
                }
                else
                {
                    var listaFiltrada = await _livroRepository.ListarPortituloAsync(txt_pesquisa.Text);
                    dgvLivros.DataSource = null;
                    dgvLivros.DataSource = listaFiltrada;
                    FormatarGridLivros();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao pesquisar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CadastroLivros_Load(object sender, EventArgs e)
        {
            await CarregarGridAsync();
            await CarregarGridEmprestimosAtivosAsync(); // Carrega a grade de empréstimos ativos ao iniciar a tela de livros
            await AtualizarDashboardAsync();
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            _idSelecionado = 0; // Reseta o ID selecionado para evitar confusões futuras
            txt_codigo.Clear();
            txt_titulo.Clear();
            txt_autor.Clear();
            txt_editora.Clear();
            nud_ano.Value = 2026;
            txt_genero.Clear();
            nud_quantidade.Value = 1;
            cb_idioma.SelectedIndex = -1;

        }

        private void btn_cadastroUsuario_Click(object sender, EventArgs e)
        {
            CadastroUsuarios cadastrar = new CadastroUsuarios();
            cadastrar.Show();
        }

        private async void btn_emprestar_ClickAsync(object sender, EventArgs e)
        {
            // Instancia a janela de empréstimos
            using (var telaEmprestimo = new CadastroEmprestimo())
            {
                // O ShowDialog() abre a tela e TRAVA a tela principal até a de empréstimo ser fechada
                // O "if" verifica se a tela foi fechada com aquele sinal de sucesso (DialogResult.OK)
                if (telaEmprestimo.ShowDialog() == DialogResult.OK)
                {
                    // Se chegou aqui, é porque o empréstimo deu certo e a janela fechou!
                    // Então, chamamos o método para recarregar a grade de livros com os novos números
                    await CarregarGridAsync(); // Use o nome do seu método que atualiza a grid
                    await CarregarGridEmprestimosAtivosAsync(); // E também recarrega a grade de empréstimos ativos, para mostrar o novo empréstimo
                }
            }
        }

        private async Task CarregarGridEmprestimosAtivosAsync()
        {
            try
            {
                // 1. Busca os empréstimos ativos no repositório (com os Includes instalados)
                var emprestimosAtivos = await _emprestimoRepository.ObterTodosAtivosAsync();

                // 2. Converte de forma segura para a lista do DTO com os nomes corretos
                var dadosParaGrade = emprestimosAtivos.Select(e => new EmprestimoAtivoDTO
                {
                    Id = e.Id,
                    // Se e.Livro for nulo, ele usa e.TituloLivro (campo direto). Se ambos forem nulos, usa "Sem Título"
                    Livro = e.Livro?.Titulo,
                    Autor = e.Livro?.Autor,
                    Aluno = e.Usuario?.Nome,
                    Turma = e.Usuario?.Turma,
                    DataEmprestimo = e.DataEmprestimo,
                    DataDevolucaoPrevista = e.DataPrevistaDevolucao
                }).ToList();

                // 3. Alimenta a grade
                dgvEmprestimosAtivos.DataSource = null;
                dgvEmprestimosAtivos.DataSource = dadosParaGrade;

                // 4. Aplica as formatações visuais
                FormatarGridEmprestimosAtivos();
            }
            catch (Exception ex)
            {
                // Se der qualquer erro no banco ou no mapeamento, este aviso vai te dizer o motivo exato
                MessageBox.Show($"Erro ao carregar devoluções pendentes: {ex.Message}\n\nDetalhes: {ex.InnerException?.Message}");
            }
        }

        private void FormatarGridEmprestimosAtivos()
        {


            if (dgvEmprestimosAtivos.Columns.Count == 0) return;

            dgvEmprestimosAtivos.RowTemplate.Height = 18;
            // --- TAMANHO E ESTILO DA FONTE ---

            // Tradução dos Cabeçalhos para o Bibliotecário
            if (dgvEmprestimosAtivos.Columns["Aluno"] != null) dgvEmprestimosAtivos.Columns["Aluno"].HeaderText = "Aluno";
            if (dgvEmprestimosAtivos.Columns["Livro"] != null) dgvEmprestimosAtivos.Columns["Livro"].HeaderText = "Livro";
            if (dgvEmprestimosAtivos.Columns["Autor"] != null) dgvEmprestimosAtivos.Columns["Autor"].HeaderText = "Autor";
            if (dgvEmprestimosAtivos.Columns["DataEmprestimo"] != null) dgvEmprestimosAtivos.Columns["DataEmprestimo"].HeaderText = "Empréstimo";
            if (dgvEmprestimosAtivos.Columns["DataDevolucaoPrevista"] != null) dgvEmprestimosAtivos.Columns["DataDevolucaoPrevista"].HeaderText = "Devolução";

            // ---FORMATAÇÃO DE DATA(Esconde as horas) ---
            if (dgvEmprestimosAtivos.Columns["DataEmprestimo"] != null)
            {
                // O formato "d" ou "dd/MM/yyyy" força a grade a mostrar apenas a data curta
                dgvEmprestimosAtivos.Columns["DataEmprestimo"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dgvEmprestimosAtivos.Columns["DataDevolucaoPrevista"] != null)
            {
                dgvEmprestimosAtivos.Columns["DataDevolucaoPrevista"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            // Ocultar coluna ID
            if (dgvEmprestimosAtivos.Columns["Id"] != null) dgvEmprestimosAtivos.Columns["Id"].Visible = false;

            // Tamanhos automáticos
            if (dgvEmprestimosAtivos.Columns["Turma"] != null) dgvEmprestimosAtivos.Columns["Turma"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            if (dgvEmprestimosAtivos.Columns["DataEmprestimo"] != null) dgvEmprestimosAtivos.Columns["DataEmprestimo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            if (dgvEmprestimosAtivos.Columns["DataDevolucaoPrevista"] != null) dgvEmprestimosAtivos.Columns["DataDevolucaoPrevista"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            if (dgvEmprestimosAtivos.Columns["Aluno"] != null) dgvEmprestimosAtivos.Columns["Aluno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            if (dgvEmprestimosAtivos.Columns["Livro"] != null) dgvEmprestimosAtivos.Columns["Livro"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            if (dgvEmprestimosAtivos.Columns["Autor"] != null) dgvEmprestimosAtivos.Columns["Autor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            // 1. Cria a fonte oficial que você deseja usar
            Font fontePadrao = new Font("JetBrains Mono", 9F, FontStyle.Regular);
            Font fonteCabecalho = new Font("JetBrains Mono", 9F, FontStyle.Bold);

            // 2. Aplica na raiz do controle
            dgvEmprestimosAtivos.Font = fontePadrao;

            // 3. Aplica nas linhas normais e alternadas (Zebra)
            dgvEmprestimosAtivos.DefaultCellStyle.Font = fontePadrao;
            dgvEmprestimosAtivos.AlternatingRowsDefaultCellStyle.Font = fontePadrao;

            // 4. Aplica nos cabeçalhos
            dgvEmprestimosAtivos.ColumnHeadersDefaultCellStyle.Font = fonteCabecalho;

            // 5. O PULO DO GATO: Percorre todas as colunas e apaga qualquer estilo rebelde
            foreach (DataGridViewColumn coluna in dgvEmprestimosAtivos.Columns)
            {
                coluna.DefaultCellStyle.Font = fontePadrao;
            }
        }

        private async void btnDevolver_ClickAsync(object sender, EventArgs e)
        {
            // 1. Verifica se tem alguma linha selecionada na grade
            if (dgvEmprestimosAtivos.CurrentRow == null)
            {
                MessageBox.Show("Por favor, selecione um empréstimo na lista para devolver.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Pega o ID do empréstimo que está na linha que o usuário clicou
                int idEmprestimoSelecionado = Convert.ToInt32(dgvEmprestimosAtivos.CurrentRow.Cells["Id"].Value);

                // Confirmação de segurança
                var confirmacao = MessageBox.Show("Confirmar a devolução deste livro?", "Devolução", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacao == DialogResult.Yes)
                {
                    // 3. Chama a regra de negócio! (Isso muda o status para devolvido e soma +1 no estoque)
                    await _emprestimoRepository.RegistrarDevolucaoAsync(idEmprestimoSelecionado);

                    MessageBox.Show("Livro devolvido com sucesso e retornado ao estoque!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 4. Atualiza as duas grades (para o empréstimo sumir dessa lista, e o estoque do livro atualizar na outra aba)
                    await CarregarGridEmprestimosAtivosAsync();
                    await CarregarGridAsync(); // Chame o seu método que recarrega os livros aqui
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro na Devolução", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvEmprestimosAtivos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow linha in dgvEmprestimosAtivos.Rows)
            {
                // Ignora a última linha em branco (se a grade permitir adicionar novas linhas)
                if (!linha.IsNewRow)
                {
                    // Pega a data que está na coluna "Vencimento" que criamos no passo anterior
                    DateTime dataVencimento = Convert.ToDateTime(linha.Cells["DataDevolucaoPrevista"].Value);

                    // Se a data de vencimento for MENOR que o dia de hoje (meia-noite), está atrasado!
                    if (dataVencimento.Date < DateTime.Now.Date)
                    {
                        // Pinta o fundo da linha de vermelho coral
                        linha.DefaultCellStyle.BackColor = Color.LightCoral;

                        // Muda a cor do texto para um vermelho escuro para dar contraste
                        linha.DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else
                    {
                        // Se não estiver atrasado, garante que a cor volta ao normal 
                        // (Isso previne bugs visuais quando a tabela é recarregada)
                        linha.DefaultCellStyle.BackColor = Color.White;
                        linha.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }

            // Tira a seleção azul automática da primeira linha para não esconder a cor
            dgvEmprestimosAtivos.ClearSelection();
        }

        private void btnVizualizar_Click(object sender, EventArgs e)
        {
            // 1. Verifica se tem alguma linha selecionada na grade
            if (dgvLivros.CurrentRow == null)
            {
                MessageBox.Show("Por favor, selecione um livro na lista primeiro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Pega o ID do livro da linha que o usuário clicou
            int idLivroSelecionado = Convert.ToInt32(dgvLivros.CurrentRow.Cells["Id"].Value);

            // 3. Abre a nova janela, ENTREGANDO o ID para ela
            using (var telaVisualizar = new FrmVisualizarLivros(idLivroSelecionado))
            {
                // Usamos ShowDialog para que a janela abra como um Pop-up focado
                telaVisualizar.ShowDialog();
            }
        }

        private void FormatarGridLivros()
        {
            // Se a grade estiver vazia, não faz nada para evitar erros
            if (dgvLivros.Columns.Count == 0) return;

            dgvLivros.Columns["Id"].HeaderText = "ID";
            dgvLivros.Columns["Titulo"].HeaderText = "Livro"; // Muda de 'Titulo' para 'Livro' na tela
            dgvLivros.Columns["Autor"].HeaderText = "Autor";
            dgvLivros.Columns["QuantidadeTotal"].HeaderText = "Qtd Total";
            dgvLivros.Columns["QuantidadeEmprestada"].HeaderText = "Emprestados";
            dgvLivros.Columns["QuantidadeDisponivel"].HeaderText = "Disponível";

            if (dgvLivros.Columns["Ano"] != null)
            {
                dgvLivros.Columns["Ano"].Visible = false;
            }
            if (dgvLivros.Columns["Genero"] != null)
            {
                dgvLivros.Columns["Genero"].Visible = false;
            }

            if (dgvLivros.Columns["Editora"] != null)
            {
                dgvLivros.Columns["Editora"].Visible = false;
            }
            if (dgvLivros.Columns["Idioma"] != null)
            {
                dgvLivros.Columns["Idioma"].Visible = false;
            }

            // --- TAMANHO DAS COLUNAS ---
            // 1. O ID e as Quantidades ficam no modo "AllCells" (Eles encolhem para ocupar 
            // apenas a largura exata do texto que está dentro deles)
            dgvLivros.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvLivros.Columns["QuantidadeTotal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvLivros.Columns["QuantidadeEmprestada"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvLivros.Columns["QuantidadeDisponivel"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // 2. O Título do Livro e o Autor ficam no modo "Fill" (Eles vão esticar e 
            // dividir entre si todo o espaço vazio que sobrar na tela)
            dgvLivros.Columns["Titulo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvLivros.Columns["Autor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            // Verifica se a janela de relatório já está aberta
            if (_telaRelatorio == null || _telaRelatorio.IsDisposed)
            {
                // Se não estiver aberta, cria uma nova instância e mostra a janela
                _telaRelatorio = new Relatorios();
                _telaRelatorio.Show();
            }
            else
            {
                // Se já estiver aberta, traz a janela para o primeiro plano
                _telaRelatorio.BringToFront();
            }
        }

        private void dgvLivros_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 1. Resgata a lista atual que está aparecendo na tela
            var listaAtual = dgvLivros.DataSource as List<Livro>;
            if (listaAtual == null || listaAtual.Count == 0) return;

            // 2. Descobre qual foi a coluna exata que o usuário clicou (ex: "Titulo", "Autor")
            string nomeColunaClicada = dgvLivros.Columns[e.ColumnIndex].DataPropertyName;

            // 3. Aplica a ordenação dependendo da coluna clicada
            if (nomeColunaClicada == "Titulo")
            {
                if (_ordemCrescente)
                    listaAtual = listaAtual.OrderBy(l => l.Titulo).ToList(); // A-Z
                else
                    listaAtual = listaAtual.OrderByDescending(l => l.Titulo).ToList(); // Z-A

                _ordemCrescente = !_ordemCrescente; // Inverte a chave para o próximo clique
            }
            else if (nomeColunaClicada == "Autor")
            {
                if (_ordemCrescente)
                    listaAtual = listaAtual.OrderBy(l => l.Autor).ToList();
                else
                    listaAtual = listaAtual.OrderByDescending(l => l.Autor).ToList();

                _ordemCrescente = !_ordemCrescente;
            }
            else if (nomeColunaClicada == "Id")
            {
                if (_ordemCrescente)
                    listaAtual = listaAtual.OrderBy(l => l.Id).ToList();
                else
                    listaAtual = listaAtual.OrderByDescending(l => l.Id).ToList();

                _ordemCrescente = !_ordemCrescente;
            }

            // 4. Devolve a lista organizada para a grade
            dgvLivros.DataSource = listaAtual;
        }

        // Método exclusivo para atualizar o painel
        private async Task AtualizarDashboardAsync()
        {
            try
            {
                // Busca os dois números no banco de dados
                int totalTitulos = await _livroRepository.ObterTotalTitulosAsync();
                int totalExemplares = await _livroRepository.ObterTotalExemplaresAsync();

                // Joga os números nos Labels da tela
                lblTotalTitulos.Text = totalTitulos.ToString();
                lblTotalExemplares.Text = totalExemplares.ToString();
            }
            catch (Exception ex)
            {
                // Tratamento silencioso ou exibir mensagem de erro de conexão
                lblTotalTitulos.Text = "-";
                lblTotalExemplares.Text = "-";
            }
        }

        private async void txtBuscarEmprestimo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string termoBusca = txtBuscarEmprestimos.Text.Trim().ToLower();
                var todosAtivos = await _emprestimoRepository.ObterTodosAtivosAsync();

                var listaFiltrada = todosAtivos
                    .Where(emp =>
                        (emp.Livro != null && emp.Livro.Titulo.ToLower().Contains(termoBusca)) ||
                        (emp.Usuario != null && emp.Usuario.Nome.ToLower().Contains(termoBusca))
                    )
                    .Select(e => new EmprestimoAtivoDTO
                    {
                        Id = e.Id,
                        Livro = e.Livro?.Titulo ?? e.Livro.Titulo,
                        Autor = e.Livro?.Autor ?? "Não informado", // <-- Mantém o Autor na busca
                        Aluno = e.Usuario?.Nome ?? e.Usuario.Nome,
                        Turma = e.Usuario?.Turma ?? "Não informado",
                        DataEmprestimo = e.DataEmprestimo,
                        DataDevolucaoPrevista = e.DataPrevistaDevolucao
                    }).ToList();

                dgvEmprestimosAtivos.DataSource = null;
                dgvEmprestimosAtivos.DataSource = listaFiltrada;

                FormatarGridEmprestimosAtivos();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na busca: {ex.Message}");
            }
        }

        private void dgvEmprestimosAtivos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnDevolver_ClickAsync(sender, e);
        }

        private void dgvLivros_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 1. Verifica se foi o botão DIREITO do mouse e se o clique foi em uma linha válida (não no cabeçalho)
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // 2. Limpa qualquer outra linha que estivesse selecionada antes
                dgvLivros.ClearSelection();

                // 3. Força a seleção da linha exata onde o mouse clicou
                dgvLivros.Rows[e.RowIndex].Selected = true;

                // 4. Exibe o menu exatamente na ponta da setinha do mouse
                menuLivros.Show(Cursor.Position);
            }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvLivros_CellDoubleClick(sender, new DataGridViewCellEventArgs(0, dgvLivros.SelectedRows[0].Index));

        }

        private async void excluirToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            if (dgvLivros.SelectedRows.Count > 0)
            {
                // Resgata o Título e o ID para confirmar com o usuário
                int idLivro = Convert.ToInt32(dgvLivros.SelectedRows[0].Cells["Id"].Value);
                string tituloLivro = dgvLivros.SelectedRows[0].Cells["Titulo"].Value.ToString();

                // Pede confirmação antes de excluir (Segurança em primeiro lugar!)
                var confirmacao = MessageBox.Show(
                    $"Tem certeza que deseja excluir o livro '{tituloLivro}' da biblioteca?",
                    "Confirmar Exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        // Aqui você chama o repositório para excluir
                        await _livroRepository.ExcluirAsync(idLivro);

                        MessageBox.Show("Livro excluído com sucesso!");

                        txt_codigo.Clear();
                        txt_titulo.Clear();
                        txt_autor.Clear();
                        txt_editora.Clear();
                        nud_ano.Value = 2026;
                        txt_genero.Clear();
                        nud_quantidade.Value = 1;
                        cb_idioma.SelectedIndex = -1;

                        // Recarrega a grade para o livro sumir da tela
                        await CarregarGridAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir: {ex.Message}");
                    }
                }
            }
        }

        private void visualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Verifica se tem realmente uma linha selecionada por segurança
            if (dgvLivros.SelectedRows.Count > 0)
            {
                // Resgata o ID da linha selecionada (Lembre-se de usar o nome exato da sua coluna de Id)
                int idLivroSelecionado = Convert.ToInt32(dgvLivros.SelectedRows[0].Cells["Id"].Value);
                using (var telaVisualizar = new FrmVisualizarLivros(idLivroSelecionado))
                {
                    // Usamos ShowDialog para que a janela abra como um Pop-up focado
                    telaVisualizar.ShowDialog();
                }
            }
        }
    }
}

