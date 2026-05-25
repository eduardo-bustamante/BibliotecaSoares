namespace BibliotecaSoares
{
    partial class CadastroUsuarios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CadastroUsuarios));
            txt_turma = new TextBox();
            label6 = new Label();
            label4 = new Label();
            txt_nome = new TextBox();
            label3 = new Label();
            panel2 = new Panel();
            btnCancelar = new Button();
            btn_deletar = new Button();
            btn_salvar = new Button();
            dgvUsuarios = new DataGridView();
            label1 = new Label();
            gbcadastro = new GroupBox();
            txt_telefone = new TextBox();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            gbcadastro.SuspendLayout();
            SuspendLayout();
            // 
            // txt_turma
            // 
            txt_turma.BackColor = SystemColors.MenuBar;
            txt_turma.Font = new Font("JetBrains Mono NL", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_turma.Location = new Point(7, 247);
            txt_turma.Name = "txt_turma";
            txt_turma.Size = new Size(183, 34);
            txt_turma.TabIndex = 3;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("JetBrains Mono NL SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(7, 208);
            label6.Name = "label6";
            label6.Size = new Size(72, 26);
            label6.TabIndex = 4;
            label6.Text = "Turma";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("JetBrains Mono NL SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(7, 128);
            label4.Name = "label4";
            label4.Size = new Size(108, 26);
            label4.TabIndex = 5;
            label4.Text = "Telefone";
            // 
            // txt_nome
            // 
            txt_nome.BackColor = SystemColors.MenuBar;
            txt_nome.Font = new Font("JetBrains Mono NL", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_nome.Location = new Point(7, 83);
            txt_nome.Name = "txt_nome";
            txt_nome.Size = new Size(449, 34);
            txt_nome.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("JetBrains Mono NL SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(7, 45);
            label3.Name = "label3";
            label3.Size = new Size(168, 26);
            label3.TabIndex = 6;
            label3.Text = "Nome Completo";
            // 
            // panel2
            // 
            panel2.Controls.Add(btnCancelar);
            panel2.Controls.Add(btn_deletar);
            panel2.Controls.Add(btn_salvar);
            panel2.Location = new Point(14, 381);
            panel2.Name = "panel2";
            panel2.Size = new Size(463, 111);
            panel2.TabIndex = 10;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(0, 86, 179);
            btnCancelar.Font = new Font("JetBrains Mono NL", 11.25F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(320, 23);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(137, 80);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btn_deletar
            // 
            btn_deletar.BackColor = Color.FromArgb(0, 86, 179);
            btn_deletar.Font = new Font("JetBrains Mono NL", 11.25F, FontStyle.Bold);
            btn_deletar.ForeColor = Color.White;
            btn_deletar.Location = new Point(163, 23);
            btn_deletar.Name = "btn_deletar";
            btn_deletar.Size = new Size(137, 80);
            btn_deletar.TabIndex = 2;
            btn_deletar.Text = "Deletar";
            btn_deletar.UseVisualStyleBackColor = false;
            btn_deletar.Click += btn_deletar_Click;
            // 
            // btn_salvar
            // 
            btn_salvar.BackColor = Color.FromArgb(0, 86, 179);
            btn_salvar.Font = new Font("JetBrains Mono NL", 11.25F, FontStyle.Bold);
            btn_salvar.ForeColor = Color.White;
            btn_salvar.Location = new Point(7, 23);
            btn_salvar.Name = "btn_salvar";
            btn_salvar.Size = new Size(137, 80);
            btn_salvar.TabIndex = 1;
            btn_salvar.Text = "Salvar";
            btn_salvar.UseVisualStyleBackColor = false;
            btn_salvar.Click += btnSalvarUsuario_ClickAsync;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.AllowUserToOrderColumns = true;
            dgvUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvUsuarios.BackgroundColor = Color.WhiteSmoke;
            dgvUsuarios.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("JetBrains Mono", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvUsuarios.DefaultCellStyle = dataGridViewCellStyle1;
            dgvUsuarios.GridColor = SystemColors.ScrollBar;
            dgvUsuarios.Location = new Point(483, 68);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvUsuarios.RowHeadersWidth = 51;
            dgvUsuarios.RowTemplate.Height = 30;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.Size = new Size(663, 889);
            dgvUsuarios.TabIndex = 11;
            dgvUsuarios.CellClick += dgvUsuarios_CellClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("JetBrains Mono", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(523, 13);
            label1.Name = "label1";
            label1.Size = new Size(134, 31);
            label1.TabIndex = 12;
            label1.Text = "Usuários";
            // 
            // gbcadastro
            // 
            gbcadastro.Controls.Add(label3);
            gbcadastro.Controls.Add(txt_telefone);
            gbcadastro.Controls.Add(txt_nome);
            gbcadastro.Controls.Add(label4);
            gbcadastro.Controls.Add(label6);
            gbcadastro.Controls.Add(txt_turma);
            gbcadastro.Location = new Point(14, 68);
            gbcadastro.Margin = new Padding(3, 4, 3, 4);
            gbcadastro.Name = "gbcadastro";
            gbcadastro.Padding = new Padding(3, 4, 3, 4);
            gbcadastro.Size = new Size(463, 307);
            gbcadastro.TabIndex = 13;
            gbcadastro.TabStop = false;
            gbcadastro.Text = "Cadastro";
            // 
            // txt_telefone
            // 
            txt_telefone.BackColor = SystemColors.MenuBar;
            txt_telefone.Font = new Font("JetBrains Mono NL", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_telefone.Location = new Point(7, 167);
            txt_telefone.Name = "txt_telefone";
            txt_telefone.Size = new Size(449, 34);
            txt_telefone.TabIndex = 2;
            txt_telefone.TextChanged += txt_telefone_TextChanged;
            // 
            // CadastroUsuarios
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1152, 972);
            Controls.Add(gbcadastro);
            Controls.Add(label1);
            Controls.Add(dgvUsuarios);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(1168, 1008);
            Name = "CadastroUsuarios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cadastrar usuários";
            Load += CadastroUsuarios_Load;
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            gbcadastro.ResumeLayout(false);
            gbcadastro.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txt_turma;
        private Label label6;
        private Label label4;
        private TextBox txt_nome;
        private Label label3;
        private Panel panel2;
        private Button btnCancelar;
        private Button btn_deletar;
        private Button btn_salvar;
        private DataGridView dgvUsuarios;
        private Label label1;
        private GroupBox gbcadastro;
        private TextBox txt_telefone;
    }
}