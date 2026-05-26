namespace BibliotecaSoares
{
    partial class CadastroLivros
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CadastroLivros));
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            dgvLivros = new DataGridView();
            label1 = new Label();
            panel1 = new Panel();
            nud_quantidade = new NumericUpDown();
            btnVizualizar = new Button();
            btn_cadastroUsuario = new Button();
            nud_ano = new NumericUpDown();
            cb_idioma = new ComboBox();
            txt_genero = new TextBox();
            txt_editora = new TextBox();
            label7 = new Label();
            txt_autor = new TextBox();
            label9 = new Label();
            label8 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            txt_titulo = new TextBox();
            label3 = new Label();
            txt_codigo = new TextBox();
            label2 = new Label();
            panel2 = new Panel();
            btn_cancelar = new Button();
            btn_deletar = new Button();
            btn_salvar = new Button();
            buscar = new Label();
            txt_pesquisa = new TextBox();
            btnDevolver = new Button();
            btn_emprestar = new Button();
            dgvEmprestimosAtivos = new DataGridView();
            groupBox1 = new GroupBox();
            btnRelatorio = new Button();
            groupBox2 = new GroupBox();
            lblTotalExemplares = new Label();
            lblTotalTitulos = new Label();
            label11 = new Label();
            label10 = new Label();
            txtBuscarEmprestimos = new TextBox();
            label12 = new Label();
            menuLivros = new ContextMenuStrip(components);
            editarToolStripMenuItem = new ToolStripMenuItem();
            excluirToolStripMenuItem = new ToolStripMenuItem();
            visualizarToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dgvLivros).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nud_quantidade).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_ano).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEmprestimosAtivos).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            menuLivros.SuspendLayout();
            SuspendLayout();
            // 
            // dgvLivros
            // 
            dgvLivros.AllowUserToDeleteRows = false;
            dgvLivros.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Font = new Font("JetBrains Mono NL", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvLivros.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvLivros.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvLivros.BackgroundColor = Color.WhiteSmoke;
            dgvLivros.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("JetBrains Mono", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvLivros.DefaultCellStyle = dataGridViewCellStyle2;
            dgvLivros.GridColor = SystemColors.ScrollBar;
            dgvLivros.Location = new Point(437, 66);
            dgvLivros.Margin = new Padding(3, 2, 3, 2);
            dgvLivros.Name = "dgvLivros";
            dgvLivros.ReadOnly = true;
            dgvLivros.RowHeadersWidth = 51;
            dgvLivros.RowTemplate.Height = 30;
            dgvLivros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLivros.Size = new Size(732, 352);
            dgvLivros.TabIndex = 0;
            dgvLivros.CellDoubleClick += dgvLivros_CellDoubleClick;
            dgvLivros.CellMouseDown += dgvLivros_CellMouseDown;
            dgvLivros.ColumnHeaderMouseClick += dgvLivros_ColumnHeaderMouseClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("JetBrains Mono", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(0, 86, 179);
            label1.Location = new Point(534, 10);
            label1.Name = "label1";
            label1.Size = new Size(300, 25);
            label1.TabIndex = 1;
            label1.Text = "Biblioteca Odolfo Soares";
            // 
            // panel1
            // 
            panel1.Controls.Add(nud_quantidade);
            panel1.Controls.Add(btnVizualizar);
            panel1.Controls.Add(btn_cadastroUsuario);
            panel1.Controls.Add(nud_ano);
            panel1.Controls.Add(cb_idioma);
            panel1.Controls.Add(txt_genero);
            panel1.Controls.Add(txt_editora);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(txt_autor);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(txt_titulo);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txt_codigo);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(20, 53);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(405, 330);
            panel1.TabIndex = 2;
            // 
            // nud_quantidade
            // 
            nud_quantidade.BackColor = SystemColors.Menu;
            nud_quantidade.Font = new Font("JetBrains Mono NL", 10F);
            nud_quantidade.Location = new Point(269, 239);
            nud_quantidade.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            nud_quantidade.Name = "nud_quantidade";
            nud_quantidade.Size = new Size(133, 25);
            nud_quantidade.TabIndex = 6;
            nud_quantidade.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnVizualizar
            // 
            btnVizualizar.BackColor = SystemColors.ButtonFace;
            btnVizualizar.FlatStyle = FlatStyle.Popup;
            btnVizualizar.Font = new Font("JetBrains Mono SemiBold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVizualizar.ForeColor = SystemColors.WindowText;
            btnVizualizar.Image = (Image)resources.GetObject("btnVizualizar.Image");
            btnVizualizar.ImageAlign = ContentAlignment.MiddleLeft;
            btnVizualizar.Location = new Point(269, 270);
            btnVizualizar.Name = "btnVizualizar";
            btnVizualizar.Padding = new Padding(4, 3, 4, 3);
            btnVizualizar.Size = new Size(133, 47);
            btnVizualizar.TabIndex = 11;
            btnVizualizar.Text = "Visualizar";
            btnVizualizar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnVizualizar.UseVisualStyleBackColor = false;
            btnVizualizar.Click += btnVizualizar_Click;
            // 
            // btn_cadastroUsuario
            // 
            btn_cadastroUsuario.Font = new Font("JetBrains Mono SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_cadastroUsuario.Image = (Image)resources.GetObject("btn_cadastroUsuario.Image");
            btn_cadastroUsuario.ImageAlign = ContentAlignment.MiddleLeft;
            btn_cadastroUsuario.Location = new Point(250, 8);
            btn_cadastroUsuario.Name = "btn_cadastroUsuario";
            btn_cadastroUsuario.Size = new Size(152, 57);
            btn_cadastroUsuario.TabIndex = 12;
            btn_cadastroUsuario.Text = "Cadastrar Usuário";
            btn_cadastroUsuario.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_cadastroUsuario.UseVisualStyleBackColor = true;
            btn_cadastroUsuario.Click += btn_cadastroUsuario_Click;
            // 
            // nud_ano
            // 
            nud_ano.BackColor = SystemColors.Menu;
            nud_ano.Font = new Font("JetBrains Mono NL", 10F);
            nud_ano.Location = new Point(269, 186);
            nud_ano.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            nud_ano.Minimum = new decimal(new int[] { 1700, 0, 0, 0 });
            nud_ano.Name = "nud_ano";
            nud_ano.Size = new Size(133, 25);
            nud_ano.TabIndex = 4;
            nud_ano.Value = new decimal(new int[] { 2026, 0, 0, 0 });
            // 
            // cb_idioma
            // 
            cb_idioma.BackColor = SystemColors.Menu;
            cb_idioma.Font = new Font("JetBrains Mono NL", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cb_idioma.FormattingEnabled = true;
            cb_idioma.Items.AddRange(new object[] { "Português", "Espanhol", "Inglês" });
            cb_idioma.Location = new Point(6, 292);
            cb_idioma.Margin = new Padding(3, 2, 3, 2);
            cb_idioma.Name = "cb_idioma";
            cb_idioma.Size = new Size(247, 25);
            cb_idioma.TabIndex = 7;
            // 
            // txt_genero
            // 
            txt_genero.BackColor = SystemColors.Menu;
            txt_genero.Font = new Font("JetBrains Mono NL", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_genero.Location = new Point(6, 239);
            txt_genero.Margin = new Padding(3, 2, 3, 2);
            txt_genero.Name = "txt_genero";
            txt_genero.Size = new Size(247, 25);
            txt_genero.TabIndex = 5;
            // 
            // txt_editora
            // 
            txt_editora.BackColor = SystemColors.Menu;
            txt_editora.Font = new Font("JetBrains Mono NL", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_editora.Location = new Point(6, 186);
            txt_editora.Margin = new Padding(3, 2, 3, 2);
            txt_editora.Name = "txt_editora";
            txt_editora.Size = new Size(247, 25);
            txt_editora.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("JetBrains Mono NL SemiBold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.FromArgb(51, 51, 51);
            label7.Location = new Point(269, 216);
            label7.Name = "label7";
            label7.Size = new Size(88, 18);
            label7.TabIndex = 0;
            label7.Text = "Quantidade";
            // 
            // txt_autor
            // 
            txt_autor.BackColor = SystemColors.Menu;
            txt_autor.Font = new Font("JetBrains Mono NL", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_autor.Location = new Point(6, 133);
            txt_autor.Margin = new Padding(3, 2, 3, 2);
            txt_autor.Name = "txt_autor";
            txt_autor.Size = new Size(396, 25);
            txt_autor.TabIndex = 2;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("JetBrains Mono NL SemiBold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.FromArgb(51, 51, 51);
            label9.Location = new Point(6, 269);
            label9.Name = "label9";
            label9.Size = new Size(56, 18);
            label9.TabIndex = 0;
            label9.Text = "Idioma";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("JetBrains Mono NL SemiBold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(51, 51, 51);
            label8.Location = new Point(6, 216);
            label8.Name = "label8";
            label8.Size = new Size(56, 18);
            label8.TabIndex = 0;
            label8.Text = "Gênero";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("JetBrains Mono NL SemiBold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(51, 51, 51);
            label6.Location = new Point(6, 163);
            label6.Name = "label6";
            label6.Size = new Size(64, 18);
            label6.TabIndex = 0;
            label6.Text = "Editora";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("JetBrains Mono NL SemiBold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(51, 51, 51);
            label5.Location = new Point(269, 163);
            label5.Name = "label5";
            label5.Size = new Size(32, 18);
            label5.TabIndex = 0;
            label5.Text = "Ano";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("JetBrains Mono NL SemiBold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(51, 51, 51);
            label4.Location = new Point(6, 110);
            label4.Name = "label4";
            label4.Size = new Size(48, 18);
            label4.TabIndex = 0;
            label4.Text = "Autor";
            // 
            // txt_titulo
            // 
            txt_titulo.BackColor = SystemColors.Menu;
            txt_titulo.Font = new Font("JetBrains Mono NL", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_titulo.Location = new Point(6, 80);
            txt_titulo.Margin = new Padding(3, 2, 3, 2);
            txt_titulo.Name = "txt_titulo";
            txt_titulo.Size = new Size(396, 25);
            txt_titulo.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("JetBrains Mono NL SemiBold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(51, 51, 51);
            label3.Location = new Point(6, 57);
            label3.Name = "label3";
            label3.Size = new Size(128, 18);
            label3.TabIndex = 0;
            label3.Text = "Título do Livro";
            // 
            // txt_codigo
            // 
            txt_codigo.BackColor = SystemColors.ActiveBorder;
            txt_codigo.Font = new Font("JetBrains Mono NL", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_codigo.Location = new Point(6, 28);
            txt_codigo.Margin = new Padding(3, 2, 3, 2);
            txt_codigo.Name = "txt_codigo";
            txt_codigo.ReadOnly = true;
            txt_codigo.Size = new Size(128, 25);
            txt_codigo.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("JetBrains Mono NL SemiBold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(51, 51, 51);
            label2.Location = new Point(6, 8);
            label2.Name = "label2";
            label2.Size = new Size(128, 18);
            label2.TabIndex = 0;
            label2.Text = "Código do Livro";
            // 
            // panel2
            // 
            panel2.Controls.Add(btn_cancelar);
            panel2.Controls.Add(btn_deletar);
            panel2.Controls.Add(btn_salvar);
            panel2.Location = new Point(20, 387);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(405, 55);
            panel2.TabIndex = 3;
            // 
            // btn_cancelar
            // 
            btn_cancelar.BackColor = Color.FromArgb(0, 86, 179);
            btn_cancelar.Font = new Font("JetBrains Mono NL", 10F, FontStyle.Bold | FontStyle.Italic);
            btn_cancelar.ForeColor = Color.White;
            btn_cancelar.Location = new Point(281, 2);
            btn_cancelar.Margin = new Padding(3, 2, 3, 2);
            btn_cancelar.Name = "btn_cancelar";
            btn_cancelar.Size = new Size(104, 50);
            btn_cancelar.TabIndex = 3;
            btn_cancelar.Text = "Cancelar";
            btn_cancelar.UseVisualStyleBackColor = false;
            btn_cancelar.Click += btn_cancelar_Click;
            // 
            // btn_deletar
            // 
            btn_deletar.BackColor = Color.FromArgb(0, 86, 179);
            btn_deletar.Font = new Font("JetBrains Mono NL", 10F, FontStyle.Bold | FontStyle.Italic);
            btn_deletar.ForeColor = Color.White;
            btn_deletar.Location = new Point(149, 2);
            btn_deletar.Margin = new Padding(3, 2, 3, 2);
            btn_deletar.Name = "btn_deletar";
            btn_deletar.Size = new Size(104, 50);
            btn_deletar.TabIndex = 2;
            btn_deletar.Text = "Deletar";
            btn_deletar.UseVisualStyleBackColor = false;
            btn_deletar.Click += btn_deletar_ClickAsync;
            // 
            // btn_salvar
            // 
            btn_salvar.BackColor = Color.FromArgb(0, 86, 179);
            btn_salvar.Font = new Font("JetBrains Mono NL", 10F, FontStyle.Bold | FontStyle.Italic);
            btn_salvar.ForeColor = Color.White;
            btn_salvar.Location = new Point(15, 2);
            btn_salvar.Margin = new Padding(3, 2, 3, 2);
            btn_salvar.Name = "btn_salvar";
            btn_salvar.Size = new Size(104, 50);
            btn_salvar.TabIndex = 1;
            btn_salvar.Text = "Salvar";
            btn_salvar.UseVisualStyleBackColor = false;
            btn_salvar.Click += btn_salvar_ClickAsync;
            // 
            // buscar
            // 
            buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buscar.AutoSize = true;
            buscar.Font = new Font("JetBrains Mono NL SemiBold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buscar.Location = new Point(731, 41);
            buscar.Name = "buscar";
            buscar.Size = new Size(56, 18);
            buscar.TabIndex = 0;
            buscar.Text = "Buscar";
            // 
            // txt_pesquisa
            // 
            txt_pesquisa.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_pesquisa.BackColor = SystemColors.Control;
            txt_pesquisa.Font = new Font("JetBrains Mono NL", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_pesquisa.Location = new Point(792, 38);
            txt_pesquisa.Margin = new Padding(3, 2, 3, 2);
            txt_pesquisa.Name = "txt_pesquisa";
            txt_pesquisa.PlaceholderText = "Digite o nome do livro";
            txt_pesquisa.Size = new Size(377, 25);
            txt_pesquisa.TabIndex = 1;
            txt_pesquisa.TextChanged += txt_pesquisa_TextChangedAsync;
            // 
            // btnDevolver
            // 
            btnDevolver.BackColor = Color.FromArgb(0, 86, 179);
            btnDevolver.Font = new Font("JetBrains Mono NL", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnDevolver.ForeColor = Color.White;
            btnDevolver.Location = new Point(32, 146);
            btnDevolver.Margin = new Padding(3, 2, 3, 2);
            btnDevolver.Name = "btnDevolver";
            btnDevolver.Size = new Size(160, 100);
            btnDevolver.TabIndex = 2;
            btnDevolver.Text = "Devolver";
            btnDevolver.UseVisualStyleBackColor = false;
            btnDevolver.Click += btnDevolver_ClickAsync;
            // 
            // btn_emprestar
            // 
            btn_emprestar.BackColor = Color.FromArgb(0, 86, 179);
            btn_emprestar.Font = new Font("JetBrains Mono NL", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btn_emprestar.ForeColor = Color.White;
            btn_emprestar.Location = new Point(32, 27);
            btn_emprestar.Margin = new Padding(3, 2, 3, 2);
            btn_emprestar.Name = "btn_emprestar";
            btn_emprestar.Size = new Size(160, 100);
            btn_emprestar.TabIndex = 1;
            btn_emprestar.Text = "Emprestar";
            btn_emprestar.UseVisualStyleBackColor = false;
            btn_emprestar.Click += btn_emprestar_ClickAsync;
            // 
            // dgvEmprestimosAtivos
            // 
            dgvEmprestimosAtivos.AllowUserToDeleteRows = false;
            dgvEmprestimosAtivos.AllowUserToOrderColumns = true;
            dataGridViewCellStyle3.Font = new Font("JetBrains Mono NL", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgvEmprestimosAtivos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            dgvEmprestimosAtivos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvEmprestimosAtivos.BackgroundColor = Color.WhiteSmoke;
            dgvEmprestimosAtivos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("JetBrains Mono", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvEmprestimosAtivos.DefaultCellStyle = dataGridViewCellStyle4;
            dgvEmprestimosAtivos.GridColor = SystemColors.ScrollBar;
            dgvEmprestimosAtivos.Location = new Point(207, 10);
            dgvEmprestimosAtivos.Margin = new Padding(3, 2, 3, 2);
            dgvEmprestimosAtivos.Name = "dgvEmprestimosAtivos";
            dgvEmprestimosAtivos.ReadOnly = true;
            dgvEmprestimosAtivos.RowHeadersWidth = 51;
            dgvEmprestimosAtivos.RowTemplate.Height = 20;
            dgvEmprestimosAtivos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmprestimosAtivos.Size = new Size(732, 252);
            dgvEmprestimosAtivos.TabIndex = 0;
            dgvEmprestimosAtivos.CellDoubleClick += dgvEmprestimosAtivos_CellDoubleClick;
            dgvEmprestimosAtivos.DataBindingComplete += dgvEmprestimosAtivos_DataBindingComplete;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.BackColor = Color.White;
            groupBox1.Controls.Add(btnDevolver);
            groupBox1.Controls.Add(dgvEmprestimosAtivos);
            groupBox1.Controls.Add(btn_emprestar);
            groupBox1.Font = new Font("JetBrains Mono NL SemiBold", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(230, 447);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(946, 270);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Empréstimos";
            // 
            // btnRelatorio
            // 
            btnRelatorio.BackColor = Color.FromArgb(0, 86, 179);
            btnRelatorio.Font = new Font("JetBrains Mono NL", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRelatorio.ForeColor = Color.White;
            btnRelatorio.Location = new Point(23, 27);
            btnRelatorio.Margin = new Padding(3, 2, 3, 2);
            btnRelatorio.Name = "btnRelatorio";
            btnRelatorio.Size = new Size(160, 100);
            btnRelatorio.TabIndex = 3;
            btnRelatorio.Text = "Relatórios";
            btnRelatorio.UseVisualStyleBackColor = false;
            btnRelatorio.Click += btnRelatorio_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox2.BackColor = Color.WhiteSmoke;
            groupBox2.Controls.Add(btnRelatorio);
            groupBox2.Controls.Add(lblTotalExemplares);
            groupBox2.Controls.Add(lblTotalTitulos);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(label10);
            groupBox2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(12, 449);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(212, 268);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Relatórios";
            // 
            // lblTotalExemplares
            // 
            lblTotalExemplares.AutoSize = true;
            lblTotalExemplares.Font = new Font("JetBrains Mono NL SemiBold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalExemplares.ForeColor = Color.FromArgb(51, 51, 51);
            lblTotalExemplares.Location = new Point(111, 174);
            lblTotalExemplares.Name = "lblTotalExemplares";
            lblTotalExemplares.Size = new Size(16, 18);
            lblTotalExemplares.TabIndex = 0;
            lblTotalExemplares.Text = "X";
            // 
            // lblTotalTitulos
            // 
            lblTotalTitulos.AutoSize = true;
            lblTotalTitulos.Font = new Font("JetBrains Mono NL SemiBold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalTitulos.ForeColor = Color.FromArgb(51, 51, 51);
            lblTotalTitulos.Location = new Point(167, 153);
            lblTotalTitulos.Name = "lblTotalTitulos";
            lblTotalTitulos.Size = new Size(16, 18);
            lblTotalTitulos.TabIndex = 0;
            lblTotalTitulos.Text = "X";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("JetBrains Mono NL SemiBold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.FromArgb(51, 51, 51);
            label11.Location = new Point(8, 174);
            label11.Name = "label11";
            label11.Size = new Size(88, 18);
            label11.TabIndex = 0;
            label11.Text = "Qtd Livros";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("JetBrains Mono NL SemiBold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.FromArgb(51, 51, 51);
            label10.Location = new Point(6, 153);
            label10.Name = "label10";
            label10.Size = new Size(160, 18);
            label10.TabIndex = 0;
            label10.Text = "Livros Cadastrados:";
            // 
            // txtBuscarEmprestimos
            // 
            txtBuscarEmprestimos.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            txtBuscarEmprestimos.BackColor = SystemColors.Control;
            txtBuscarEmprestimos.Font = new Font("JetBrains Mono NL", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscarEmprestimos.Location = new Point(792, 422);
            txtBuscarEmprestimos.Margin = new Padding(3, 2, 3, 2);
            txtBuscarEmprestimos.Name = "txtBuscarEmprestimos";
            txtBuscarEmprestimos.PlaceholderText = "Digite o nome do Aluno";
            txtBuscarEmprestimos.Size = new Size(377, 25);
            txtBuscarEmprestimos.TabIndex = 1;
            txtBuscarEmprestimos.TextChanged += txtBuscarEmprestimo_TextChanged;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label12.AutoSize = true;
            label12.Font = new Font("JetBrains Mono NL SemiBold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.Location = new Point(731, 425);
            label12.Name = "label12";
            label12.Size = new Size(56, 18);
            label12.TabIndex = 0;
            label12.Text = "Buscar";
            // 
            // menuLivros
            // 
            menuLivros.Font = new Font("JetBrains Mono", 8.999999F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuLivros.Items.AddRange(new ToolStripItem[] { editarToolStripMenuItem, excluirToolStripMenuItem, visualizarToolStripMenuItem });
            menuLivros.Name = "contextMenuStrip1";
            menuLivros.Size = new Size(145, 70);
            // 
            // editarToolStripMenuItem
            // 
            editarToolStripMenuItem.Image = (Image)resources.GetObject("editarToolStripMenuItem.Image");
            editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            editarToolStripMenuItem.Size = new Size(144, 22);
            editarToolStripMenuItem.Text = "Editar";
            editarToolStripMenuItem.Click += editarToolStripMenuItem_Click;
            // 
            // excluirToolStripMenuItem
            // 
            excluirToolStripMenuItem.Image = (Image)resources.GetObject("excluirToolStripMenuItem.Image");
            excluirToolStripMenuItem.Name = "excluirToolStripMenuItem";
            excluirToolStripMenuItem.Size = new Size(144, 22);
            excluirToolStripMenuItem.Text = "Excluir";
            excluirToolStripMenuItem.Click += excluirToolStripMenuItem_ClickAsync;
            // 
            // visualizarToolStripMenuItem
            // 
            visualizarToolStripMenuItem.Image = (Image)resources.GetObject("visualizarToolStripMenuItem.Image");
            visualizarToolStripMenuItem.Name = "visualizarToolStripMenuItem";
            visualizarToolStripMenuItem.Size = new Size(144, 22);
            visualizarToolStripMenuItem.Text = "Visualizar";
            visualizarToolStripMenuItem.Click += visualizarToolStripMenuItem_Click;
            // 
            // CadastroLivros
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1181, 725);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(dgvLivros);
            Controls.Add(label12);
            Controls.Add(txtBuscarEmprestimos);
            Controls.Add(buscar);
            Controls.Add(txt_pesquisa);
            ForeColor = SystemColors.ControlText;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(1024, 764);
            Name = "CadastroLivros";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Biblioteca Odolfo Soares";
            Load += CadastroLivros_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLivros).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nud_quantidade).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_ano).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvEmprestimosAtivos).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            menuLivros.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvLivros;
        private Label label1;
        private Panel panel1;
        private TextBox txt_titulo;
        private Label label3;
        private TextBox txt_codigo;
        private Label label2;
        private Panel panel2;
        private TextBox txt_editora;
        private Label label7;
        private TextBox txt_autor;
        private Label label6;
        private Label label5;
        private Label label4;
        private TextBox txt_genero;
        private Label label8;
        private Label label9;
        private ComboBox cb_idioma;
        private Button btn_cancelar;
        private Button btn_deletar;
        private Button btn_salvar;
        private Label buscar;
        private TextBox txt_pesquisa;
        private NumericUpDown nud_ano;
        private NumericUpDown nud_quantidade;
        private Button btnDevolver;
        private Button btn_emprestar;
        private Button btn_cadastroUsuario;
        private DataGridView dgvEmprestimosAtivos;
        private GroupBox groupBox1;
        private Button btnVizualizar;
        private Button btnRelatorio;
        private GroupBox groupBox2;
        private Label lblTotalExemplares;
        private Label lblTotalTitulos;
        private Label label11;
        private Label label10;
        private TextBox txtBuscarEmprestimos;
        private Label label12;
        private ContextMenuStrip menuLivros;
        private ToolStripMenuItem editarToolStripMenuItem;
        private ToolStripMenuItem excluirToolStripMenuItem;
        private ToolStripMenuItem visualizarToolStripMenuItem;
    }
}
