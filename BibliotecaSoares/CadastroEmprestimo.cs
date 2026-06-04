using BibliotecaSoares.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BibliotecaSoares
{
    public partial class CadastroEmprestimo : Form
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILivroRepository _livroRepository;
        private readonly IEmprestimoRepository _emprestimoRepository;


        public CadastroEmprestimo()
        {
            InitializeComponent();
            _usuarioRepository = new UsuarioRepository();
            _livroRepository = new LivroRepository();
            _emprestimoRepository = new EmprestimoRepository();
        }
        private async void CarregarDadosSelectoresAsync()
        {
            try
            {
                // 1. Busca os alunos no banco
                var alunos = await _usuarioRepository.ObterTodosAsync();

                // 2. Prepara os dados juntando Nome e Turma em um texto só
                var alunosFormatados = alunos
                    .Select(u => new
                    {
                        Id = u.Id,
                        // O símbolo $ permite colocar as variáveis dentro das chaves { }
                        NomeComTurma = $"{u.Nome} - {u.Turma}"
                    })
                    .OrderBy(u => u.NomeComTurma)
                    .ToList();

                // 3. Joga no ComboBox
                cmbUsuarios.DataSource = alunosFormatados;

                // O que o bibliotecário vê na tela (Agora é a nossa propriedade combinada!)
                cmbUsuarios.DisplayMember = "NomeComTurma";

                // O ID secreto continua protegido e funcionando perfeitamente para o banco de dados
                cmbUsuarios.ValueMember = "Id";

                // 2. Faz a mesma coisa para os Livros
                var livros = await _livroRepository.ListarTodosAsync();
                cmbLivros.DataSource = livros.OrderBy(l => l.Titulo).ToList();

                cmbLivros.DisplayMember = "Titulo"; // Pode ser TituloLivro dependendo da sua classe
                cmbLivros.ValueMember = "Id";

                // Opcional: Iniciar com os campos limpos (sem nenhum selecionado automaticamente)
                cmbUsuarios.SelectedIndex = -1;
                cmbLivros.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao preencher os seletores: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Emprestimo_Load(object sender, EventArgs e)
        {
            CarregarDadosSelectoresAsync();
        }

        private async void btn_confirmar_ClickAsync(object sender, EventArgs e)
        {
            // 1. Validação: Impede o sistema de tentar salvar se o usuário não escolheu nada
            if (cmbUsuarios.SelectedIndex == -1 || cmbLivros.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, selecione um Aluno e um Livro para realizar o empréstimo.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nudDias.Value <= 0)
            {
                MessageBox.Show("O prazo de empréstimo deve ser de pelo menos 1 dia.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Extrair os IDs da tela
                // Usamos Convert.ToInt32 porque o SelectedValue vem como um tipo "object" genérico
                int idAluno = Convert.ToInt32(cmbUsuarios.SelectedValue);
                int idLivro = Convert.ToInt32(cmbLivros.SelectedValue);

                // Se você adicionou um NumericUpDown para os dias, use: Convert.ToInt32(nudDias.Value);
                // Se não adicionou, podemos fixar em 7 dias como padrão:
                int diasEmprestimo = Convert.ToInt32(nudDias.Value);

                // 3. Chamar o repositório para salvar no banco
                await _emprestimoRepository.RegistrarEmprestimoAsync(idAluno, idLivro, diasEmprestimo);

                // 4. Mensagem de Sucesso
                MessageBox.Show("Empréstimo registrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 5. Limpar a tela para o próximo empréstimo
                cmbUsuarios.SelectedIndex = -1;
                cmbLivros.SelectedIndex = -1;

                // Se você tiver uma grade (DataGridView) mostrando os livros ou empréstimos na mesma tela, 
                // chame o método de atualizar a grade aqui (ex: await CarregarGridLivrosAsync
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                // Se o repositório disparar o erro de "Livro sem estoque", ele vai aparecer exatamente aqui!
                MessageBox.Show(ex.Message, "Não foi possível emprestar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
