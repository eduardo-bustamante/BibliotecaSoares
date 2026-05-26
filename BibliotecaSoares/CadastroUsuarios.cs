using BibliotecaSoares.Models;
using BibliotecaSoares.Models.DTO;
using BibliotecaSoares.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BibliotecaSoares
{
    public partial class CadastroUsuarios : Form
    {
        private int _idSelecionadoUsuario = 0;
        private readonly IUsuarioRepository _usuarioRepository;
        private bool _ordemCrescenteUsuarios = false;
        public CadastroUsuarios()
        {
            InitializeComponent();
            _usuarioRepository = new UsuarioRepository();
        }


        private async void btnSalvarUsuario_ClickAsync(object sender, EventArgs e)
        {
            // 1. Validação Básica
            if (string.IsNullOrWhiteSpace(txt_nome.Text) || string.IsNullOrWhiteSpace(txt_turma.Text))
            {
                MessageBox.Show("Os campos Nome e Turma são obrigatórios!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Para a execução aqui se estiver vazio
            }

            try
            {
                // 2. Montar o objeto Usuário com os dados da tela
                var usuarioParaSalvar = new Usuario
                {
                    Id = _idSelecionadoUsuario, // Se for 0, o banco entende que é novo. Se tiver número, ele atualiza.
                    Nome = txt_nome.Text.Trim(),
                    Telefone = txt_telefone.Text.Trim(),
                    Turma = txt_turma.Text.Trim()
                    // A DataCadastro é preenchida automaticamente pela classe!
                };

                // 3. Decidir se é Inserção (Create) ou Atualização (Update)
                if (_idSelecionadoUsuario == 0)
                {
                    await _usuarioRepository.AdicionarAsync(usuarioParaSalvar);
                    MessageBox.Show("Aluno cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await _usuarioRepository.AtualizarAsync(usuarioParaSalvar);
                    MessageBox.Show("Cadastro do aluno atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reseta o ID para 0 após atualizar, para voltar ao modo de "Novo Cadastro"
                    _idSelecionadoUsuario = 0;
                }

                // 4. Limpar a tela para o próximo cadastro
                txt_nome.Clear();
                txt_telefone.Clear();
                txt_turma.Clear();
                txt_nome.Focus(); // Coloca o cursor piscando no campo nome

                btn_salvar.Text = "Salvar Usuário";

                // 5. Atualizar as listas da tela
                await CarregarGridUsuariosAsync(); // Atualiza a grade de alunos
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar o aluno: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cole este código DENTRO da sua partial class Form1 : Form, mas FORA dos eventos de clique

        private async Task CarregarGridUsuariosAsync()
        {
            try
            {
                // Vai no banco e busca todos os alunos
                var listaUsuarios = await _usuarioRepository.ObterTodosAsync();

                // Substitua 'dgvUsuarios' pelo nome exato do DataGridView que você criou para os usuários
                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = listaUsuarios.OrderBy(u => u.Nome).ToList();

                // --- PERSONALIZAÇÃO DOS CABEÇALHOS ---
                dgvUsuarios.Columns["Nome"].HeaderText = "Nome do Aluno";
                dgvUsuarios.Columns["Telefone"].HeaderText = "Telefone";
                dgvUsuarios.Columns["Turma"].HeaderText = "Turma";
                dgvUsuarios.Columns["DataCadastro"].HeaderText = "Cadastrado em";

                // --- ESCONDER COLUNAS INDESEJADAS ---
                // Escondemos o ID (pois é controle interno) e o NomeComTurma (que é só para o ComboBox)
                if (dgvUsuarios.Columns["Id"] != null)
                {
                    dgvUsuarios.Columns["Id"].Visible = false;
                }

                if (dgvUsuarios.Columns["NomeComTurma"] != null)
                {
                    dgvUsuarios.Columns["NomeComTurma"].Visible = false;
                }

                if (dgvUsuarios.Columns["DataCadastro"] != null)
                {
                    dgvUsuarios.Columns["DataCadastro"].Visible = false;
                }

                // --- TAMANHO DAS COLUNAS ---
                // 1. As informações mais curtas encolhem para abraçar o texto
                dgvUsuarios.Columns["Telefone"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvUsuarios.Columns["Turma"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvUsuarios.Columns["DataCadastro"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                // 2. O Nome do aluno estica e ganha todo o espaço que sobrar na tela
                dgvUsuarios.Columns["Nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar a grade de alunos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CadastroUsuarios_Load(object sender, EventArgs e)
        {
            await CarregarGridUsuariosAsync();
        }

        private void txt_telefone_TextChanged(object sender, EventArgs e)
        {
            // 1. Desliga o evento temporariamente para evitar um loop infinito
            txt_telefone.TextChanged -= txt_telefone_TextChanged;

            // 2. Guarda a posição do cursor para ele não pular para o final sozinho
            int posicaoCursor = txt_telefone.SelectionStart;
            int tamanhoAntes = txt_telefone.Text.Length;

            // 3. Extrai apenas os números do que o usuário digitou
            string apenasNumeros = "";
            foreach (char c in txt_telefone.Text)
            {
                if (char.IsDigit(c)) apenasNumeros += c;
            }

            // 4. Limita a 11 dígitos (DDD + 9 números do celular)
            if (apenasNumeros.Length > 11)
                apenasNumeros = apenasNumeros.Substring(0, 11);

            // 5. Aplica a máscara dinamicamente dependendo da quantidade de números
            string textoFormatado = "";

            if (apenasNumeros.Length == 0)
                textoFormatado = "";
            else if (apenasNumeros.Length <= 2)
                textoFormatado = $"({apenasNumeros}";
            else if (apenasNumeros.Length <= 6)
                textoFormatado = $"({apenasNumeros.Substring(0, 2)}) {apenasNumeros.Substring(2)}";
            else if (apenasNumeros.Length <= 10) // Telefone Fixo (8 dígitos)
                textoFormatado = $"({apenasNumeros.Substring(0, 2)}) {apenasNumeros.Substring(2, 4)}-{apenasNumeros.Substring(6)}";
            else // Celular (9 dígitos)
                textoFormatado = $"({apenasNumeros.Substring(0, 2)}) {apenasNumeros.Substring(2, 5)}-{apenasNumeros.Substring(7)}";

            // 6. Atualiza a tela com o texto formatado
            txt_telefone.Text = textoFormatado;

            // 7. Calcula e devolve o cursor para o lugar certo
            int diferenca = txt_telefone.Text.Length - tamanhoAntes;
            int novaPosicao = posicaoCursor + diferenca;

            if (novaPosicao < 0) novaPosicao = 0;
            txt_telefone.SelectionStart = novaPosicao;

            // 8. Religa o evento
            txt_telefone.TextChanged += txt_telefone_TextChanged;
        }


        private async void btn_deletar_Click(object sender, EventArgs e)
        {
            // 1. Verifica se tem alguém selecionado para excluir
            if (_idSelecionadoUsuario == 0)
            {
                MessageBox.Show("Por favor, selecione um aluno na lista (dando um duplo clique) antes de clicar em excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Pede confirmação (Segurança em primeiro lugar!)
            var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este cadastro? Esta ação não pode ser desfeita.",
                                              "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    // 3. Chama o repositório para deletar no banco de dados
                    await _usuarioRepository.ExcluirAsync(_idSelecionadoUsuario); // Ou ExcluirAsync, dependendo de como você nomeou

                    MessageBox.Show("Cadastro excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 4. Limpa a tela e reseta a variável
                    _idSelecionadoUsuario = 0;
                    txt_nome.Clear();
                    txt_telefone.Clear();
                    txt_turma.Clear();

                    // Se você mudou o texto do botão salvar no duplo clique, volte ao normal aqui
                    btn_salvar.Text = "Salvar Usuário";

                    // 5. Atualiza a grade e os seletores
                    await CarregarGridUsuariosAsync();
                }
                catch (Exception ex)
                {
                    // Tratamento de erro crucial para banco de dados!
                    MessageBox.Show($"Não foi possível excluir o aluno.\n\nMotivo técnico: {ex.Message}\n\nDica: Verifique se este aluno possui histórico de empréstimos registrados no sistema. O banco de dados bloqueia a exclusão para não perder o histórico de livros.",
                                    "Erro na Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // 1. Reseta a variável de controle para sair do "Modo de Edição"
            _idSelecionadoUsuario = 0;

            // 2. Limpa todos os campos da tela
            txt_nome.Clear();
            txt_telefone.Clear();
            txt_turma.Clear();

            // 3. Devolve o texto original do botão de salvar (caso você tenha clicado em editar antes)
            btn_salvar.Text = "Salvar Usuário";

            // 4. Coloca o cursor do mouse piscando no primeiro campo, pronto para começar de novo
            txt_nome.Focus();
        }

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Prevenção de erros: Ignora se o usuário der duplo clique no cabeçalho (que é a linha -1)
            if (e.RowIndex >= 0)
            {
                // 2. Pega exatamente a linha que o usuário clicou
                DataGridViewRow linhaClicada = dgvUsuarios.Rows[e.RowIndex];

                // 3. Alimenta a nossa variável de controle com o ID do aluno
                // Mesmo a coluna "Id" estando invisível, o dado continua lá nos bastidores!
                _idSelecionadoUsuario = Convert.ToInt32(linhaClicada.Cells["Id"].Value);

                // 4. Devolve os dados para os campos da tela
                txt_nome.Text = linhaClicada.Cells["Nome"].Value.ToString();
                txt_turma.Text = linhaClicada.Cells["Turma"].Value?.ToString();

                // O telefone tem a nossa lógica especial, então passamos o texto cru e ela se vira
                txt_telefone.Text = linhaClicada.Cells["Telefone"].Value?.ToString();

                // 5. Opcional: Avisa visualmente que o sistema entrou em "Modo de Edição"
                btn_salvar.Text = "Atualizar Cadastro";
            }
        }

        private async void txtBuscaUsuarios_TextChangedAsync(object sender, EventArgs e)
        {
            try
            {
                string termoBusca = txtBuscaUsuarios.Text.Trim().ToLower();

                // 1. Busca todos os usuários do banco (ou use seu método existente)
                var todosUsuarios = await _usuarioRepository.ObterTodosAsync();

                // 2. Filtra e transforma no DTO
                var listaFiltrada = todosUsuarios
                    .Where(u =>
                        (u.Nome != null && u.Nome.ToLower().Contains(termoBusca)) ||
                        (u.Turma != null && u.Turma.ToLower().Contains(termoBusca))
                    )
                    .Select(u => new UsuarioGridDTO
                    {
                        Id = u.Id,
                        Nome = u.Nome,
                        Telefone = u.Telefone,
                        Turma = u.Turma
                    }).OrderBy(u => u.Nome)
                    .ToList();

                // 3. Alimenta a grade apenas com as colunas do DTO
                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = listaFiltrada;

                // 4. Reaplica a formatação (tamanhos e esconder o ID)
                FormatarGridUsuarios();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na busca de usuários: {ex.Message}");
            }
        }
        private void FormatarGridUsuarios()
        {
            if (dgvUsuarios.Columns.Count == 0) return;

            // Tradução e Maquiagem
            if (dgvUsuarios.Columns["Nome"] != null) dgvUsuarios.Columns["Nome"].HeaderText = "Nome Completo";
            if (dgvUsuarios.Columns["Telefone"] != null) dgvUsuarios.Columns["Telefone"].HeaderText = "Telefone";
            if (dgvUsuarios.Columns["Turma"] != null) dgvUsuarios.Columns["Turma"].HeaderText = "Turma";

            // Oculta o ID
            if (dgvUsuarios.Columns["Id"] != null) dgvUsuarios.Columns["Id"].Visible = false;

            // Tamanhos
            if (dgvUsuarios.Columns["Telefone"] != null) dgvUsuarios.Columns["Telefone"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            if (dgvUsuarios.Columns["Turma"] != null) dgvUsuarios.Columns["Turma"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            if (dgvUsuarios.Columns["Nome"] != null) dgvUsuarios.Columns["Nome"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dgvUsuarios_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            // 1. Resgata a lista atual que está na tela (Usando o DTO que você criou!)
            var listaAtual = dgvUsuarios.DataSource as List<Usuario>;

            // Se a lista estiver vazia ou houver erro no cast, não faz nada
            if (listaAtual == null || listaAtual.Count == 0) return;

            // 2. Descobre qual foi a coluna exata que recebeu o clique
            string nomeColunaClicada = dgvUsuarios.Columns[e.ColumnIndex].DataPropertyName;

            // 3. Aplica a ordenação
            if (nomeColunaClicada == "Nome")
            {
                listaAtual = _ordemCrescenteUsuarios
                    ? listaAtual.OrderBy(u => u.Nome).ToList()
                    : listaAtual.OrderByDescending(u => u.Nome).ToList();
            }
            else if (nomeColunaClicada == "Turma")
            {
                // Muito útil para agrupar todos os alunos da mesma turma com um clique
                listaAtual = _ordemCrescenteUsuarios
                    ? listaAtual.OrderBy(u => u.Turma).ToList()
                    : listaAtual.OrderByDescending(u => u.Turma).ToList();
            }
            else if (nomeColunaClicada == "Telefone")
            {
                // Muito útil para agrupar todos os alunos da mesma turma com um clique
                listaAtual = _ordemCrescenteUsuarios
                    ? listaAtual.OrderBy(u => u.Telefone).ToList()
                    : listaAtual.OrderByDescending(u => u.Telefone).ToList();
            }
            // (Opcional) Pode adicionar um else if para "Telefone" se achar útil

            // 4. Inverte a chave para o próximo clique
            _ordemCrescenteUsuarios = !_ordemCrescenteUsuarios;

            // 5. Devolve a lista organizada para a grade
            dgvUsuarios.DataSource = listaAtual;
        }
    }
    
}
