using BibliotecaSoares.Models.DTO;
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
    public partial class Relatorios : Form
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private bool _ordemCrescente = true;

        public Relatorios()
        {
            InitializeComponent();
            _emprestimoRepository = new EmprestimoRepository();
            FormatarGridRelatorio();
            CarregarRelatorioAsync();

        }

        private void FormatarGridRelatorio()
        {
            // Se a grade não tiver carregado nada ainda, cancela a formatação para evitar erros
            if (dgvRelatorio.Columns.Count == 0) return;

            // 1. TAMANHO DO CABEÇALHO
            dgvRelatorio.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvRelatorio.ColumnHeadersHeight = 35;

            // 2. SELEÇÃO E COMPORTAMENTO DA LINHA
            dgvRelatorio.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRelatorio.RowHeadersVisible = false;
            dgvRelatorio.MultiSelect = false;
            dgvRelatorio.AllowUserToAddRows = false;
            dgvRelatorio.AllowUserToDeleteRows = false;
            dgvRelatorio.ReadOnly = true;

            // 3. NOMES DOS CABEÇALHOS (A tradução para o bibliotecário)
            if (dgvRelatorio.Columns["NomeAluno"] != null)
            {
                dgvRelatorio.Columns["NomeAluno"].HeaderText = "Nome do Aluno";
            }

            if (dgvRelatorio.Columns["QuantidadeLivrosEmprestados"] != null)
            {
                dgvRelatorio.Columns["QuantidadeLivrosEmprestados"].HeaderText = "Livros Retirados";
            }

            // 4. LARGURA AUTOMÁTICA DAS COLUNAS E ALINHAMENTO
            // A coluna de quantidade de livros é um número curto, então ela encolhe e fica centralizada
            if (dgvRelatorio.Columns["QuantidadeLivrosEmprestados"] != null)
            {
                dgvRelatorio.Columns["QuantidadeLivrosEmprestados"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvRelatorio.Columns["QuantidadeLivrosEmprestados"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // A coluna com o nome do aluno estica como um elástico para preencher o resto da janela
            if (dgvRelatorio.Columns["NomeAluno"] != null)
            {
                dgvRelatorio.Columns["NomeAluno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private async void CarregarRelatorioAsync()
        {
            var dadosRelatorio = await _emprestimoRepository.ObterRelatorioQuantidadePorAlunoAsync();

            dgvRelatorio.DataSource = dadosRelatorio;
            FormatarGridRelatorio();
        }

        private void Relatorio_Load(object sender, EventArgs e)
        {
            CarregarRelatorioAsync();
        }

        private void dgvRelatorio_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 1. Resgata a lista atual que o relatório está mostrando
            var listaAtual = dgvRelatorio.DataSource as List<RelatorioLivrosPorAlunoDTO>;

            // Se a lista estiver vazia, não faz nada
            if (listaAtual == null || listaAtual.Count == 0) return;

            // 2. Descobre qual coluna recebeu o clique do mouse
            string nomeColunaClicada = dgvRelatorio.Columns[e.ColumnIndex].DataPropertyName;

            // 3. Aplica a ordenação
            if (nomeColunaClicada == "NomeAluno") // Clique no cabeçalho do Nome
            {
                if (_ordemCrescente)
                    listaAtual = listaAtual.OrderBy(r => r.NomeAluno).ToList(); // A-Z
                else
                    listaAtual = listaAtual.OrderByDescending(r => r.NomeAluno).ToList(); // Z-A
            }
            else if (nomeColunaClicada == "Turma") // Clique no cabeçalho da Turma
            {
                if (_ordemCrescente)
                    listaAtual = listaAtual.OrderBy(r => r.Turma).ToList(); // A-Z
                else
                    listaAtual = listaAtual.OrderByDescending(r => r.QuantidadeLivrosEmprestados).ToList(); // Maior para Menor
            }
            else if (nomeColunaClicada == "QuantidadeLivrosEmprestados") // Clique no cabeçalho da Quantidade
            {
                if (_ordemCrescente)
                    listaAtual = listaAtual.OrderBy(r => r.QuantidadeLivrosEmprestados).ToList(); // Menor para Maior
                else
                    listaAtual = listaAtual.OrderByDescending(r => r.QuantidadeLivrosEmprestados).ToList(); // Maior para Menor
            }

            // Inverte a chave para o próximo clique
            _ordemCrescente = !_ordemCrescente;

            // 4. Devolve a lista reorganizada para a grade
            dgvRelatorio.DataSource = listaAtual;
        }
    }
}
