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
    public partial class FrmVisualizarLivros : Form
    {
        private readonly LivroRepository _livroRepository;
        private int _idLivroRecebido;

        // 1. Modificamos o construtor para receber o ID
        public FrmVisualizarLivros(int idLivro)
        {
            InitializeComponent();

            _livroRepository = new LivroRepository();
            _idLivroRecebido = idLivro; // Guarda o ID que veio da tela principal

            this.Load += FrmVisualizarLivros_Load;
        }

        // 2. Quando a tela carregar, ela busca o livro no banco e preenche os campos
        private async void FrmVisualizarLivros_Load(object sender, EventArgs e)
        {
            try
            {
                // Busca o livro fresco direto do banco de dados
                var livro = await _livroRepository.ObterPorIdAsync(_idLivroRecebido);

                if (livro != null)
                {
                    // Joga as informações para os seus Labels ou TextBoxes (somente leitura) na tela
                    lbCodigo.Text = livro.Id.ToString();
                    lbTitulo.Text = livro.Titulo;
                    lbAutor.Text = livro.Autor;
                    lbEditora.Text = livro.Editora;
                    lbAno.Text = livro.Ano.ToString();
                    lbGenero.Text = livro.Genero;
                    lbQuantidade.Text = livro.QuantidadeTotal.ToString();

                    // Se você tiver outros campos como Editora, Ano, Gênero, coloque aqui!
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar os detalhes do livro: {ex.Message}");
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
