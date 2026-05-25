using BibliotecaSoares.Models;
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

                dgv_livros.DataSource = null;

                dgv_livros.DataSource = listaLivros;

                FormatarGridLivros();

                dgv_livros.Columns["Id"].HeaderText = "ID";
                dgv_livros.Columns["Titulo"].HeaderText = "Livro"; // Muda de 'Titulo' para 'Livro' na tela
                dgv_livros.Columns["Autor"].HeaderText = "Autor";
                dgv_livros.Columns["QuantidadeTotal"].HeaderText = "Qtd Total";
                dgv_livros.Columns["QuantidadeEmprestada"].HeaderText = "Emprestados";
                dgv_livros.Columns["QuantidadeDisponivel"].HeaderText = "Disponível";

                if (dgv_livros.Columns["Ano"] != null)
                {
                    dgv_livros.Columns["Ano"].Visible = false;
                }
                if (dgv_livros.Columns["Genero"] != null)
                {
                    dgv_livros.Columns["Genero"].Visible = false;
                }

                if (dgv_livros.Columns["Editora"] != null)
                {
                    dgv_livros.Columns["Editora"].Visible = false;
                }
                if (dgv_livros.Columns["Idioma"] != null)
                {
                    dgv_livros.Columns["Idioma"].Visible = false;
                }

                // --- TAMANHO DAS COLUNAS ---

                // 1. O ID e as Quantidades ficam no modo "AllCells" (Eles encolhem para ocupar 
                // apenas a largura exata do texto que está dentro deles)
                dgv_livros.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv_livros.Columns["QuantidadeTotal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv_livros.Columns["QuantidadeEmprestada"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv_livros.Columns["QuantidadeDisponivel"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                // 2. O Título do Livro e o Autor ficam no modo "Fill" (Eles vão esticar e 
                // dividir entre si todo o espaço vazio que sobrar na tela)
                dgv_livros.Columns["Titulo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv_livros.Columns["Autor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

        private void dgv_livros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se o clique foi em uma linha válida (ignora clique no cabeçalho)
            if (e.RowIndex >= 0)
            {
                // Pega a linha que foi clicada
                DataGridViewRow linhaClicada = dgv_livros.Rows[e.RowIndex];

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
                    dgv_livros.DataSource = null;
                    dgv_livros.DataSource = listaFiltrada;
                    FormatarGridLivros();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao pesquisar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                { }
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
                // 1. Busca no banco apenas os empréstimos onde Devolvido == false
                var emprestimosAtivos = await _emprestimoRepository.ObterTodosAtivosAsync();

                // 2. Prepara os dados especificamente para ficarem bonitos na tela
                var dadosParaGrade = emprestimosAtivos.Select(e => new
                {
                    Id = e.Id, // Precisamos do ID do empréstimo (escondido ou visível) para poder devolver
                    Aluno = e.Usuario.Nome,
                    Turma = e.Usuario.Turma,
                    Livro = e.Livro.Titulo,
                    DataSaida = e.DataEmprestimo.ToShortDateString(),
                    Vencimento = e.DataPrevistaDevolucao.ToShortDateString()
                }).ToList();

                // 3. Joga na grade
                dgvEmprestimosAtivos.DataSource = null;
                dgvEmprestimosAtivos.DataSource = dadosParaGrade;

                // --- PERSONALIZAÇÃO DOS CABEÇALHOS ---
                dgvEmprestimosAtivos.Columns["Aluno"].HeaderText = "Nome do Usuario";
                dgvEmprestimosAtivos.Columns["Turma"].HeaderText = "Turma";
                dgvEmprestimosAtivos.Columns["Livro"].HeaderText = "Livro Emprestado";
                dgvEmprestimosAtivos.Columns["DataSaida"].HeaderText = "Data de Saída";
                dgvEmprestimosAtivos.Columns["Vencimento"].HeaderText = "Prazo Final";

                // --- ESCONDER A COLUNA ID ---
                // Como o ID serve só para o banco de dados, podemos escondê-lo do usuário
                if (dgvEmprestimosAtivos.Columns["Id"] != null)
                {
                    dgvEmprestimosAtivos.Columns["Id"].Visible = false;
                }

                // --- TAMANHO DAS COLUNAS ---
                // 1. Turma e Datas encolhem para ocupar apenas o espaço do texto
                dgvEmprestimosAtivos.Columns["Turma"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvEmprestimosAtivos.Columns["DataSaida"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvEmprestimosAtivos.Columns["Vencimento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                // 2. Aluno e Livro esticam e dividem todo o espaço vazio da tela entre si
                dgvEmprestimosAtivos.Columns["Aluno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvEmprestimosAtivos.Columns["Livro"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                // Opcional: Esconder a coluna do ID para não confundir o usuário
                if (dgvEmprestimosAtivos.Columns["Id"] != null)
                {
                    dgvEmprestimosAtivos.Columns["Id"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar devoluções pendentes: {ex.Message}");
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
                    DateTime dataVencimento = Convert.ToDateTime(linha.Cells["Vencimento"].Value);

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
            if (dgv_livros.CurrentRow == null)
            {
                MessageBox.Show("Por favor, selecione um livro na lista primeiro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Pega o ID do livro da linha que o usuário clicou
            int idLivroSelecionado = Convert.ToInt32(dgv_livros.CurrentRow.Cells["Id"].Value);

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
            if (dgv_livros.Columns.Count == 0) return;

            dgv_livros.Columns["Id"].HeaderText = "ID";
            dgv_livros.Columns["Titulo"].HeaderText = "Livro"; // Muda de 'Titulo' para 'Livro' na tela
            dgv_livros.Columns["Autor"].HeaderText = "Autor";
            dgv_livros.Columns["QuantidadeTotal"].HeaderText = "Qtd Total";
            dgv_livros.Columns["QuantidadeEmprestada"].HeaderText = "Emprestados";
            dgv_livros.Columns["QuantidadeDisponivel"].HeaderText = "Disponível";

            if (dgv_livros.Columns["Ano"] != null)
            {
                dgv_livros.Columns["Ano"].Visible = false;
            }
            if (dgv_livros.Columns["Genero"] != null)
            {
                dgv_livros.Columns["Genero"].Visible = false;
            }

            if (dgv_livros.Columns["Editora"] != null)
            {
                dgv_livros.Columns["Editora"].Visible = false;
            }
            if (dgv_livros.Columns["Idioma"] != null)
            {
                dgv_livros.Columns["Idioma"].Visible = false;
            }

            // --- TAMANHO DAS COLUNAS ---

            // 1. O ID e as Quantidades ficam no modo "AllCells" (Eles encolhem para ocupar 
            // apenas a largura exata do texto que está dentro deles)
            dgv_livros.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_livros.Columns["QuantidadeTotal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_livros.Columns["QuantidadeEmprestada"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_livros.Columns["QuantidadeDisponivel"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // 2. O Título do Livro e o Autor ficam no modo "Fill" (Eles vão esticar e 
            // dividir entre si todo o espaço vazio que sobrar na tela)
            dgv_livros.Columns["Titulo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv_livros.Columns["Autor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

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

        private void dgv_livros_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 1. Resgata a lista atual que está aparecendo na tela
            var listaAtual = dgv_livros.DataSource as List<Livro>;
            if (listaAtual == null || listaAtual.Count == 0) return;

            // 2. Descobre qual foi a coluna exata que o usuário clicou (ex: "Titulo", "Autor")
            string nomeColunaClicada = dgv_livros.Columns[e.ColumnIndex].DataPropertyName;

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
            dgv_livros.DataSource = listaAtual;
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
    }
}

