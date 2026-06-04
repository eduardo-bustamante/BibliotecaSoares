using BibliotecaSoares.Models;
using BibliotecaSoares.Models.DTO;
using BibliotecaSoares.Repositories;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Timers;


namespace BibliotecaSoares
{
    public partial class CadastroLivros : Form
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IEmprestimoRepository _emprestimoRepository;
        private int _idSelecionado = 0;
        private Relatorios _telaRelatorio; // Variável para armazenar a instância da tela de relatório
        private List<LivroGridDTO> _listaLivrosNaTela;
        private bool _ordemCrescenteLivros = true;

        // Variável para garantir que o backup só rode UMA vez por dia às 16h
        private bool _backupRealizadoHoje = false;
        private System.Windows.Forms.Timer _timerBackup;
        private string _nomeArquivoCapa = "";
        public CadastroLivros()
        {
            InitializeComponent();
            _livroRepository = new LivroRepository();
            _emprestimoRepository = new EmprestimoRepository(); ConfigurarTimerBackup();
        }

        private void ConfigurarTimerBackup()
        {
            _timerBackup = new System.Windows.Forms.Timer(); _timerBackup.Interval = 60000; // Checa o relógio a cada 1 minuto (60.000 milissegundos)
            _timerBackup.Tick += TimerBackup_Tick;
            _timerBackup.Start();
        }

        private async void TimerBackup_Tick(object sender, EventArgs e)
        {
            DateTime agora = DateTime.Now;

            // Se passar das 16h, libera a trava para o dia seguinte
            if (agora.Hour != 16)
            {
                _backupRealizadoHoje = false;
            }

            // Se for exatamente 16h (e zero minutos) e ainda não rodou hoje
            if (agora.Hour == 16 && agora.Minute == 00 && !_backupRealizadoHoje)
            {
                _backupRealizadoHoje = true; // Trava para não rodar de novo no próximo segundo
                await ExecutarBackupAutomaticoSilenciosoAsync();
            }
        }
        private async Task ExecutarBackupAutomaticoSilenciosoAsync()
        {
            try
            {
                // 1. Define a pasta na raiz do disco C:
                string pastaRaizBackup = @"C:\BackupsBiblioteca";

                // Se a pasta não existir na raiz, o C# cria ela automaticamente
                if (!Directory.Exists(pastaRaizBackup))
                {
                    Directory.CreateDirectory(pastaRaizBackup);
                }

                // 2. Monta o nome do arquivo com a data de hoje
                string nomeArquivo = $"Backup_Automatico_{DateTime.Now.ToString("dd_MM_yyyy")}.bak";
                string caminhoFinalCompleto = Path.Combine(pastaRaizBackup, nomeArquivo);

                // 3. String de conexão e comando oficial do SQL Server
                string connectionString = @"Server=.\SQLEXPRESS;Database=OdolfoBiblioteca;Trusted_Connection=True;TrustServerCertificate=True;";
                string comandoSql = $@"BACKUP DATABASE [OdolfoBiblioteca] TO DISK = '{caminhoFinalCompleto}' WITH FORMAT, NAME = 'Backup Automatico Diario';";

                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    using (SqlCommand comando = new SqlCommand(comandoSql, conexao))
                    {
                        await conexao.OpenAsync();
                        await comando.ExecuteNonQueryAsync();
                    }
                }

                // (Opcional) Salva um aviso no console do sistema apenas para registro do desenvolvedor
                Console.WriteLine($"[BACKUP] Cópia automatizada gerada com sucesso às {DateTime.Now}");
            }
            catch (Exception ex)
            {
                // Se der erro (ex: falta de permissão), avisa o usuário porque é uma falha crítica
                MessageBox.Show($"O backup automático das 16:00 falhou!\n\nErro: {ex.Message}", "Alerta de Segurança", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CarregarGridAsync()
        {

            try
            {

                var listaLivros = await _livroRepository.ListarTodosAsync();

                _listaLivrosNaTela = listaLivros.Select(l => new LivroGridDTO
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Autor = l.Autor,
                    Editora = l.Editora,
                    Ano = l.Ano,
                    Genero = l.Genero,
                    Idioma = l.Idioma,
                    CaminhoCapa = l.CaminhoCapa,
                    QuantidadeTotal = l.QuantidadeTotal,
                    QuantidadeEmprestada = l.QuantidadeEmprestada,
                    QuantidadeDisponivel = l.QuantidadeDisponivel
                }).OrderBy(l => l.Id).ToList();

                // Define a altura padrão para todas as NOVAS linhas que vão entrar na grade
                dgvLivros.RowTemplate.Height = 18;

                dgvLivros.DataSource = null;

                dgvLivros.DataSource = _listaLivrosNaTela;

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
            // 1. Limpa os espaços em branco
            string tituloDigitado = txt_titulo.Text.Trim();
            string autorDigitado = txt_autor.Text.Trim();
            int quantidadeDigitada = Convert.ToInt32(nud_quantidade.Value);

            // 2. Busca todos os livros cadastrados
            var todosLivros = await _livroRepository.ListarTodosAsync();

            // 3. Tenta encontrar (capturar) o livro gêmeo no banco de dados
            var livroExistente = todosLivros.FirstOrDefault(l =>
                l.Titulo.Equals(tituloDigitado, StringComparison.OrdinalIgnoreCase) &&
                l.Autor.Equals(autorDigitado, StringComparison.OrdinalIgnoreCase) &&
                l.Id != _idSelecionado // Ignora o próprio livro no modo edição
            );

            // 4. Se o livro foi encontrado...
            if (livroExistente != null)
            {
                // Faz a pergunta para a bibliotecária
                var resposta = MessageBox.Show(
                    $"O livro '{tituloDigitado}' já está no sistema com {livroExistente.QuantidadeTotal} exemplar(es).\n\nDeseja somar os {quantidadeDigitada} novos exemplares ao registro existente?",
                    "Livro Duplicado Encontrado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                // Se ela clicar em SIM
                if (resposta == DialogResult.Yes)
                {
                    try
                    {
                        // Soma a quantidade nova com a que já estava no banco
                        livroExistente.QuantidadeTotal += quantidadeDigitada;

                        // Salva a atualização no banco de dados (Use o seu método de Atualizar/Update aqui)
                        await _livroRepository.AtualizarAsync(livroExistente);

                        MessageBox.Show("Exemplares adicionados com sucesso ao livro existente!", "Estoque Atualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Limpa a tela e recarrega a grade para mostrar a nova quantidade
                        // LimparTela(); 
                        CarregarGridAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao atualizar estoque: {ex.Message}");
                    }
                }

                // Retorna (cancela) em ambos os casos: 
                // Se ela disse SIM, já salvou e não precisa continuar. 
                // Se ela disse NÃO, a tela para, permitindo que ela mude o título ou autor se digitou errado.
                return;
            }

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
                    CaminhoCapa = _nomeArquivoCapa
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
                pbCapa.Image = null;
                _nomeArquivoCapa = "";


                await CarregarGridAsync();
                await AtualizarDashboardAsync(); // Atualiza o painel de estatísticas após salvar ou atualizar um livro
            }
            catch (Exception ex)
            {
                // Pega a mensagem genérica inicial
                string mensagemErro = ex.Message;

                // A MÁGICA: Vai cavando para encontrar o erro real que o banco de dados enviou
                Exception erroInterno = ex.InnerException;
                while (erroInterno != null)
                {
                    mensagemErro += "\n\nDetalhe exato do erro:\n" + erroInterno.Message;
                    erroInterno = erroInterno.InnerException;
                }

                // Mostra o erro completo na tela
                MessageBox.Show(mensagemErro, "Falha ao Salvar", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // --- CORREÇÃO DA CAPA DAQUI EM DIANTE ---

                // 1. Limpa o PictureBox e a variável de controle antes de carregar o novo
                pbCapa.ImageLocation = null;
                _nomeArquivoCapa = "";

                // 2. Resgata o nome do arquivo que veio da linha da grade
                var nomeArquivo = linhaClicada.Cells["CaminhoCapa"].Value?.ToString();

                // 3. Se houver um nome de arquivo salvo para este livro
                if (!string.IsNullOrEmpty(nomeArquivo))
                {
                    // Monta o caminho completo juntando a pasta do sistema com o nome do arquivo
                    string pastaCapas = Path.Combine(Application.StartupPath, "CapasLivros");
                    string caminhoCompletoDaFoto = Path.Combine(pastaCapas, nomeArquivo);

                    // 4. Se a foto realmente existir na pasta física, carrega ela com segurança
                    if (File.Exists(caminhoCompletoDaFoto))
                    {
                        // Carrega usando ImageLocation (não tranca o arquivo no Windows!)
                        pbCapa.ImageLocation = caminhoCompletoDaFoto;

                        // GUARDA O NOME NA VARIÁVEL GLOBAL! (Fundamental para quando você clicar em Salvar/Editar)
                        _nomeArquivoCapa = nomeArquivo;
                    }
                }
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
                string termoBusca = txt_pesquisa.Text.Trim().ToLower();


                if (string.IsNullOrEmpty(termoBusca))
                {
                    await CarregarGridAsync(); // Recarrega a grade com todos os livros se o campo de pesquisa estiver vazio
                    return;
                }
                else
                {
                    // 1. Busca todos os livros do banco (ou use sua lista já em memória)
                    var todosLivros = await _livroRepository.ListarTodosAsync
                        ();

                    // 2. O SEGREDO: O filtro agora olha para o Título OU (||) para o Autor
                    var listaFiltrada = todosLivros
                        .Where(l =>
                            (l.Titulo != null && l.Titulo.ToLower().Contains(termoBusca)) ||
                            (l.Autor != null && l.Autor.ToLower().Contains(termoBusca))
                        )
                        .Select(l => new LivroGridDTO // Lembre-se de usar o seu DTO se tiver um!
                        {
                            Id = l.Id,
                            Titulo = l.Titulo,
                            Autor = l.Autor,
                            QuantidadeTotal = l.QuantidadeTotal,
                            QuantidadeEmprestada = l.QuantidadeEmprestada,
                            QuantidadeDisponivel = l.QuantidadeDisponivel - l.QuantidadeEmprestada

                            // ... outras colunas que você exibe na grade
                        })
                        .OrderBy(l => l.Titulo) // Mantém a ordem alfabética padrão
                        .ToList();

                    // 3. Atualiza a grade
                    dgvLivros.DataSource = null;
                    dgvLivros.DataSource = listaFiltrada;
                    FormatarGridLivros();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na busca de livros: {ex.Message}");
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
            pbCapa.Image = null;
            _nomeArquivoCapa = "";

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
            // 1. Validação de segurança: Verifica se existe alguma linha selecionada na grade de empréstimos
            if (dgvEmprestimosAtivos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecione um empréstimo na grade para realizar a devolução.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Resgata o ID do empréstimo da linha selecionada
                int idEmprestimo = Convert.ToInt32(dgvEmprestimosAtivos.SelectedRows[0].Cells["Id"].Value);

                // 3. Busca o registro completo direto do banco de dados
                var emprestimo = await _emprestimoRepository.ObterPorIdAsync(idEmprestimo);

                if (emprestimo == null) return;

                // 4. Configuração das variáveis de tempo e valores
                decimal valorMultaDiaria = 1.50m; // Defina aqui o valor da diária de atraso
                decimal multaCalculada = 0;
                DateTime dataHoje = DateTime.Now.Date;
                DateTime dataPrazo = emprestimo.DataPrevistaDevolucao.Date;

                // 5. CALCULA A MULTA NA HORA (ON THE FLY)
                if (dataHoje > dataPrazo)
                {
                    TimeSpan diferencaTempo = dataHoje - dataPrazo;
                    int diasAtrasados = diferencaTempo.Days;
                    multaCalculada = diasAtrasados * valorMultaDiaria;

                    // Pergunta ao bibliotecário se a multa foi paga ou se ele deseja prosseguir
                    var resultadoMulta = MessageBox.Show(
                        $"Atenção! Esta devolução está ATRASADA em {diasAtrasados} dias.\n\n" +
                        $"Valor da multa gerada: R$ {multaCalculada:F2}\n\n" +
                        "O aluno realizou o pagamento da multa agora?",
                        "Cobrança de Multa",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Warning);

                    // Se o bibliotecário clicar em Cancelar, interrompe a devolução inteira
                    if (resultadoMulta == DialogResult.Cancel) return;

                    // Se ele clicar em Não (o aluno não pagou agora), você pode escolher se barra ou se zera
                    if (resultadoMulta == DialogResult.No)
                    {
                        multaCalculada = diasAtrasados * valorMultaDiaria;
                    }
                    if (resultadoMulta == DialogResult.Yes)
                    {
                        multaCalculada = 0; // Zera a multa porque o aluno pagou
                    }

                }

                // 6. ATUALIZA O REGISTRO DO EMPRÉSTIMO
                emprestimo.DataDevolucaoReal = DateTime.Now;
                emprestimo.Devolvido = true;
                emprestimo.ValorMultaPaga = multaCalculada;

                // >>> A LINHA QUE FALTA AQUI: <<<
                await _emprestimoRepository.AtualizarAsync(emprestimo);

                // 8. FEEDBACK PARA O USUÁRIO E ATUALIZAÇÃO DA TELA
                MessageBox.Show("Devolução realizada e estoque atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarGridEmprestimosAtivosAsync(); // Recarrega a grade de empréstimos ativos para remover o que acabou de ser devolvido
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao processar a devolução: {ex.Message}", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dgvLivros.Columns["CaminhoCapa"] != null)
            {
                dgvLivros.Columns["CaminhoCapa"].Visible = false;
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
            // 1. Desliga a sanfona automática do Windows Forms
            dgvLivros.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // 2. Trava a altura do cabeçalho em um tamanho fixo, confortável e elegante (35 ou 40 são ótimos)
            dgvLivros.ColumnHeadersHeight = 35;

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
            // 1. Resgata a lista atual que está na tela (seja ela completa ou já filtrada pela busca)
            // ATENÇÃO: Use o nome exato do DTO que você está usando para a grade de livros!
            var listaAtual = dgvLivros.DataSource as List<LivroGridDTO>;

            // Se a lista estiver vazia ou houver erro na conversão, aborta para não travar
            if (listaAtual == null || listaAtual.Count == 0) return;

            // 2. Descobre o nome exato da coluna que recebeu o clique
            string nomeColunaClicada = dgvLivros.Columns[e.ColumnIndex].DataPropertyName;

            // 3. Aplica a ordenação em cima da lista atual
            if (nomeColunaClicada == "Titulo")
            {
                listaAtual = _ordemCrescenteLivros
                    ? listaAtual.OrderBy(l => l.Titulo).ToList()
                    : listaAtual.OrderByDescending(l => l.Titulo).ToList();
            }
            else if (nomeColunaClicada == "Autor")
            {
                listaAtual = _ordemCrescenteLivros
                    ? listaAtual.OrderBy(l => l.Autor).ToList()
                    : listaAtual.OrderByDescending(l => l.Autor).ToList();
            }
            else if (nomeColunaClicada == "Id")
            {
                // Ordenação numérica perfeita
                listaAtual = _ordemCrescenteLivros
                    ? listaAtual.OrderBy(l => l.Id).ToList()
                    : listaAtual.OrderByDescending(l => l.Id).ToList();
            }

            // 4. Inverte a chave para o próximo clique
            _ordemCrescenteLivros = !_ordemCrescenteLivros;

            // 5. DEVOLVE A LISTA ORGANIZADA PARA A GRADE (Limpando antes!)
            dgvLivros.DataSource = null; // Zera a grade primeiro para forçar o Windows a piscar e atualizar
            dgvLivros.DataSource = listaAtual;
            FormatarGridLivros(); // Reaplica a formatação para garantir que tudo fique bonito
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

        private async void btnBackup_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog caixaDeDialogo = new SaveFileDialog())
                {
                    caixaDeDialogo.Title = "Salvar Backup da Biblioteca";
                    caixaDeDialogo.FileName = $"Backup_OdolfoBiblioteca_{DateTime.Now.ToString("dd_MM_yyyy_HHmm")}.bak";
                    caixaDeDialogo.Filter = "Backup do SQL Server (*.bak)|*.bak";

                    if (caixaDeDialogo.ShowDialog() == DialogResult.OK)
                    {
                        // A sua string de conexão exata!
                        string connectionString = ConfigurationManager.ConnectionStrings["BibliotecaDB"]?.ConnectionString; ;

                        string caminhoEscolhido = caixaDeDialogo.FileName;

                        // O comando oficial do SQL Server para gerar o backup
                        string comandoSql = $@"BACKUP DATABASE [OdolfoBiblioteca] TO DISK = '{caminhoEscolhido}' WITH FORMAT, MEDIANAME = 'BackupBiblioteca', NAME = 'Backup Completo';";

                        using (SqlConnection conexao = new SqlConnection(connectionString))
                        {
                            using (SqlCommand comando = new SqlCommand(comandoSql, conexao))
                            {
                                await conexao.OpenAsync();
                                await comando.ExecuteNonQueryAsync();
                            }
                        }

                        MessageBox.Show("Backup realizado com sucesso e salvo no seu Pen Drive!", "Backup Concluído", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao tentar fazer o backup.\n\nDetalhe técnico: {ex.Message}", "Falha no Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRestaurarBackup_ClickAsync(object sender, EventArgs e)
        {
            // 1. Confirmação de segurança dupla (nunca é demais quando se trata de apagar o banco atual)
            var confirmacao = MessageBox.Show(
                "ATENÇÃO: Restaurar um backup vai apagar todos os dados atuais e voltar o sistema para a data do arquivo escolhido.\n\nTem certeza que deseja continuar?",
                "Aviso Crítico",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmacao != DialogResult.Yes) return;

            try
            {
                using (OpenFileDialog caixaDeBusca = new OpenFileDialog())
                {
                    caixaDeBusca.Title = "Selecione o arquivo de Backup";
                    caixaDeBusca.Filter = "Backup do SQL Server (*.bak)|*.bak";

                    if (caixaDeBusca.ShowDialog() == DialogResult.OK)
                    {
                        string caminhoBackup = caixaDeBusca.FileName;

                        // O TRUQUE: Mudamos a conexão para o banco "master" em vez do "OdolfoBiblioteca"
                        string connectionStringMaster = @"Server=.\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";

                        // O Script SQL que expulsa todo mundo, restaura e religa
                        string comandoSql = $@"
                    -- 1. Coloca o banco em modo de usuário único (derruba as conexões ativas)
                    ALTER DATABASE [OdolfoBiblioteca] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    
                    -- 2. Restaura o backup por cima (REPLACE)
                    RESTORE DATABASE [OdolfoBiblioteca] FROM DISK = '{caminhoBackup}' WITH REPLACE;
                    
                    -- 3. Devolve o banco ao estado normal para múltiplos usuários
                    ALTER DATABASE [OdolfoBiblioteca] SET MULTI_USER;";

                        using (SqlConnection conexao = new SqlConnection(connectionStringMaster))
                        {
                            using (SqlCommand comando = new SqlCommand(comandoSql, conexao))
                            {
                                await conexao.OpenAsync();
                                await comando.ExecuteNonQueryAsync();
                            }
                        }

                        MessageBox.Show("Restauração concluída com sucesso! O sistema foi revertido para a versão do backup.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Reinicia a aplicação para recarregar todos os dados limpos
                        Application.Restart();
                        Environment.Exit(0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falha ao tentar restaurar o banco de dados.\n\nDetalhe técnico: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void históricoDeEmpréstimosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLivros.SelectedRows.Count > 0)
            {
                // 1. Captura o ID e o Título do livro selecionado na linha da grade
                int idLivro = Convert.ToInt32(dgvLivros.SelectedRows[0].Cells["Id"].Value);
                string tituloLivro = dgvLivros.SelectedRows[0].Cells["Titulo"].Value.ToString();

                // 2. Instancia a tela de histórico injetando os dados do livro
                FormHistoricoLivro telaHistorico = new FormHistoricoLivro(idLivro, tituloLivro);

                // 3. Exibe a janela de forma centralizada
                telaHistorico.ShowDialog();
            }
        }

        private void btnEscolherCapa_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog caixaBusca = new OpenFileDialog())
            {
                caixaBusca.Title = "Selecione a Capa do Livro";
                // Filtra para mostrar apenas imagens
                caixaBusca.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png";

                if (caixaBusca.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // 1. Define a pasta 'CapasLivros' na raiz do seu executável
                        string pastaCapas = Path.Combine(Application.StartupPath, "CapasLivros");

                        // Se a pasta não existir ainda, o C# cria ela na hora
                        if (!Directory.Exists(pastaCapas))
                        {
                            Directory.CreateDirectory(pastaCapas);
                        }

                        // 2. Cria um nome ÚNICO para a imagem (Evita que um livro de Matemática apague a capa de outro de Matemática)
                        // Usamos um Guid (código aleatório) + a extensão original do arquivo
                        string extensao = Path.GetExtension(caixaBusca.FileName);
                        _nomeArquivoCapa = Guid.NewGuid().ToString() + extensao;

                        // 3. Monta o caminho final onde a foto vai morar
                        string caminhoFinal = Path.Combine(pastaCapas, _nomeArquivoCapa);

                        // 4. Faz a cópia da imagem do pendrive/computador para a pasta do sistema
                        File.Copy(caixaBusca.FileName, caminhoFinal, true);

                        // 5. Mostra a imagem na tela para o usuário ver que deu certo!
                        pbCapa.ImageLocation = caminhoFinal;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao processar a imagem: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLimparCapa_Click(object sender, EventArgs e)
        {
            pbCapa.Image = null;
            _nomeArquivoCapa = "";
        }

        private void btnVersao_Click(object sender, EventArgs e)
        {
            FormVersao telaVersao = new FormVersao();
            telaVersao.ShowDialog();
        }

        private void btnMultas_Click(object sender, EventArgs e)
        {
            FrmRelatorioAtrasos telaAtrasos = new FrmRelatorioAtrasos();

            // 2. A MÁGICA: Usa ShowDialog em vez de Show!
            // Isso faz a tela principal "congelar" e esperar a tela de atrasos ser fechada.
            telaAtrasos.ShowDialog();

            // 3. ATUALIZAÇÃO: Esta linha só vai ser executada no exato milissegundo 
            // em que o bibliotecário fechar a tela de atrasos no "X"!

            // Chame aqui o seu método que recarrega a grade da tela principal
            CarregarGridEmprestimosAtivosAsync();
        }
    }
}

