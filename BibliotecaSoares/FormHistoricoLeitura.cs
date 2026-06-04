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
    public partial class FormHistoricoLeitura : Form
    {
        private readonly int _idUsuario;
        private readonly string _nomeUsuario;
        private readonly string _turmaUsuario; // Se quiser mostrar a turma também, pode adicionar essa variável e passá-la no construtor

        // Modificamos o construtor para exigir o ID e o Nome do aluno ao abrir
        public FormHistoricoLeitura(int idUsuario, string nomeUsuario, string turmaUsuario)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
            _nomeUsuario = nomeUsuario;
            _turmaUsuario = turmaUsuario; // Se quiser mostrar a turma, adicione um parâmetro para isso e atribua aqui

            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Histórico de Leituras";
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lblNomeAluno.Text = $"Histórico de Leituras de: {_nomeUsuario}";
            await CarregarHistoricoLeiturasAsync();
        }

        private async Task CarregarHistoricoLeiturasAsync()
        {
            try
            {
                // Substitua 'AppDbContext' pelo nome do seu contexto do Entity Framework
                using (var context = new AppDbContext())
                {
                    // Busca todos os empréstimos (ativos ou já devolvidos) deste usuário específico
                    var dados = await context.Emprestimos
                        .Include(e => e.Livro)
                        .Where(e => e.UsuarioId == _idUsuario)
                        .OrderByDescending(e => e.DataEmprestimo) // Mais recentes primeiro
                        .Select(e => new HistorialLeituraDTO
                        {
                            Livro = e.Livro.Titulo,
                            Autor = e.Livro.Autor,
                            DataEmprestimo = e.DataEmprestimo,
                            PrazoDevolucao = e.DataPrevistaDevolucao,
                            // Se a DataDevolucaoReal for nula, o livro ainda não foi devolvido
                            Situacao = e.DataDevolucaoReal == null
                            ? "🔴 PENDENTE (Com o aluno)"
                            : $"🟢 Devolvido em {e.DataDevolucaoReal.Value.ToString("dd/MM/yyyy")}"
                        })
                    .ToListAsync();

                    dgvHistorico.DataSource = null;
                    dgvHistorico.DataSource = dados;

                    FormatarGridHistorico();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar histórico: {ex.Message}");
            }
        }

        private void FormatarGridHistorico()
        {
            if (dgvHistorico.Columns.Count == 0) return;

            // Títulos das colunas
            dgvHistorico.Columns["Livro"].HeaderText = "Título do Livro";
            dgvHistorico.Columns["Autor"].HeaderText = "Autor";
            dgvHistorico.Columns["DataEmprestimo"].HeaderText = "Data do Empréstimo";
            dgvHistorico.Columns["PrazoDevolucao"].HeaderText = "Prazo de Devolução";
            dgvHistorico.Columns["Situacao"].HeaderText = "Situação / Status";

            // Esconder horas das colunas de data
            dgvHistorico.Columns["DataEmprestimo"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvHistorico.Columns["PrazoDevolucao"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Ditadura da Fonte (Tamanho 9 para ficar elegante)
            Font fontePadrao = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgvHistorico.Font = fontePadrao;
            dgvHistorico.DefaultCellStyle.Font = fontePadrao;
            dgvHistorico.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvHistorico.RowTemplate.Height = 28;

            // Tamanhos das colunas
            dgvHistorico.Columns["DataEmprestimo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvHistorico.Columns["PrazoDevolucao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvHistorico.Columns["Situacao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvHistorico.Columns["Autor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvHistorico.Columns["Livro"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }


        private void btnImprimirHistoricoLeitura_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.DesenharRelatorioPage);
            pd.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);

            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = pd;
            previewDialog.WindowState = FormWindowState.Maximized;
            previewDialog.Text = "Visualizar Impressão - Histórico do Aluno"; // Já com a correção do 'Text'!

            // --- A CIRURGIA NA BARRA DE FERRAMENTAS ---
            ToolStrip barraDeFerramentas = null;

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
                ToolStripItem botaoOriginal = barraDeFerramentas.Items[0];

                ToolStripButton novoBotaoImprimir = new ToolStripButton();
                novoBotaoImprimir.Image = botaoOriginal.Image;
                novoBotaoImprimir.ToolTipText = "Escolher Impressora ou Salvar PDF";

                novoBotaoImprimir.Click += (s, args) =>
                {
                    using (PrintDialog caixaDeImpressao = new PrintDialog())
                    {
                        caixaDeImpressao.Document = pd;
                        caixaDeImpressao.UseEXDialog = true;

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

                barraDeFerramentas.Items.Remove(botaoOriginal);
                barraDeFerramentas.Items.Insert(0, novoBotaoImprimir);
            }

            previewDialog.ShowDialog();
        }

        private void DesenharRelatorioPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            Font fonteTitulo = new Font("Segoe UI", 18, FontStyle.Bold);
            Font fonteSubtitulo = new Font("Segoe UI", 10, FontStyle.Italic);
            Font fonteCabecalhoTabela = new Font("Segoe UI", 10, FontStyle.Bold);
            Font fonteLinhasTabela = new Font("Segoe UI", 9, FontStyle.Regular);
            Brush pincelPreto = Brushes.Black;

            float inicioX = e.MarginBounds.Left;
            float inicioY = e.MarginBounds.Top;
            float fimX = e.MarginBounds.Right;

            // --- CABEÇALHO DO RELATÓRIO ---
            g.DrawString("COLÉGIO ODOLFO SOARES", fonteSubtitulo, pincelPreto, inicioX, inicioY);
            inicioY += 20;
            g.DrawString("Histórico de Leituras do Aluno", fonteTitulo, pincelPreto, inicioX, inicioY);
            inicioY += 35;

            // Nome do Aluno em destaque (Usando a sua variável _nomeUsuario)
            g.DrawString($"Aluno(a): {_nomeUsuario}       Turma: {_turmaUsuario}", new Font("Segoe UI", 12, FontStyle.Bold), pincelPreto, inicioX, inicioY);
            inicioY += 20;
            g.DrawString($"Data de Emissão: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}", fonteSubtitulo, pincelPreto, inicioX, inicioY);
            inicioY += 40;

            // --- CABEÇALHO DA TABELA ---
            // Ajustei as larguras para o Título do Livro ter mais espaço
            float colLivroX = inicioX;
            float colAutorX = inicioX + 220;
            float colDataX = inicioX + 350;
            float colPrazoX = inicioX + 450;
            float colStatusX = inicioX + 550;

            g.DrawString("Título do Livro", fonteCabecalhoTabela, pincelPreto, colLivroX, inicioY);
            g.DrawString("Autor", fonteCabecalhoTabela, pincelPreto, colAutorX, inicioY);
            g.DrawString("Empréstimo", fonteCabecalhoTabela, pincelPreto, colDataX, inicioY);
            g.DrawString("Prazo", fonteCabecalhoTabela, pincelPreto, colPrazoX, inicioY);
            g.DrawString("Situação", fonteCabecalhoTabela, pincelPreto, colStatusX, inicioY);

            inicioY += 20;
            g.DrawLine(Pens.Black, inicioX, inicioY, fimX, inicioY);
            inicioY += 10;

            // --- LINHAS DA TABELA (DADOS) ---
            // Lendo a grade dgvHistorico
            foreach (DataGridViewRow linha in dgvHistorico.Rows)
            {
                if (linha.IsNewRow) continue;

                string livro = linha.Cells["Livro"].Value?.ToString() ?? "";
                string autor = linha.Cells["Autor"].Value?.ToString() ?? "";
                string dataEmp = Convert.ToDateTime(linha.Cells["DataEmprestimo"].Value).ToString("dd/MM/yyyy");
                string prazo = Convert.ToDateTime(linha.Cells["PrazoDevolucao"].Value).ToString("dd/MM/yyyy");
                string situacao = linha.Cells["Situacao"].Value?.ToString() ?? "";

                // Remove os emojis para a impressão
                situacao = situacao.Replace("🔴 ", "").Replace("🟢 ", "");

                // Limita o tamanho dos textos para não encavalar
                if (livro.Length > 28) livro = livro.Substring(0, 25) + "...";
                if (autor.Length > 16) autor = autor.Substring(0, 14) + "...";

                // Desenha os textos
                g.DrawString(livro, fonteLinhasTabela, pincelPreto, colLivroX, inicioY);
                g.DrawString(autor, fonteLinhasTabela, pincelPreto, colAutorX, inicioY);
                g.DrawString(dataEmp, fonteLinhasTabela, pincelPreto, colDataX, inicioY);
                g.DrawString(prazo, fonteLinhasTabela, pincelPreto, colPrazoX, inicioY);
                g.DrawString(situacao, fonteLinhasTabela, pincelPreto, colStatusX, inicioY);

                inicioY += 18;
                g.DrawLine(Pens.LightGray, inicioX, inicioY, fimX, inicioY);
                inicioY += 7;

                if (inicioY > e.MarginBounds.Bottom - 20)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            e.HasMorePages = false;
        }
    }
}
