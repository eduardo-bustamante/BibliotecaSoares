namespace BibliotecaSoares
{
    partial class CadastroEmprestimo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CadastroEmprestimo));
            label3 = new Label();
            cmbUsuarios = new ComboBox();
            label1 = new Label();
            cmbLivros = new ComboBox();
            btn_confirmar = new Button();
            label2 = new Label();
            label4 = new Label();
            nudDias = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)nudDias).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("JetBrains Mono NL SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(51, 51, 51);
            label3.Location = new Point(14, 144);
            label3.Name = "label3";
            label3.Size = new Size(96, 26);
            label3.TabIndex = 7;
            label3.Text = "Usuário";
            // 
            // cmbUsuarios
            // 
            cmbUsuarios.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbUsuarios.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbUsuarios.BackColor = SystemColors.Menu;
            cmbUsuarios.Font = new Font("JetBrains Mono NL SemiBold", 12F, FontStyle.Bold);
            cmbUsuarios.FormattingEnabled = true;
            cmbUsuarios.Location = new Point(14, 176);
            cmbUsuarios.Margin = new Padding(3, 4, 3, 4);
            cmbUsuarios.Name = "cmbUsuarios";
            cmbUsuarios.Size = new Size(662, 34);
            cmbUsuarios.Sorted = true;
            cmbUsuarios.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("JetBrains Mono NL SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(51, 51, 51);
            label1.Location = new Point(14, 69);
            label1.Name = "label1";
            label1.Size = new Size(72, 26);
            label1.TabIndex = 7;
            label1.Text = "Livro";
            // 
            // cmbLivros
            // 
            cmbLivros.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbLivros.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbLivros.BackColor = SystemColors.Menu;
            cmbLivros.Font = new Font("JetBrains Mono NL SemiBold", 12F, FontStyle.Bold);
            cmbLivros.FormattingEnabled = true;
            cmbLivros.Location = new Point(14, 101);
            cmbLivros.Margin = new Padding(3, 4, 3, 4);
            cmbLivros.Name = "cmbLivros";
            cmbLivros.Size = new Size(662, 34);
            cmbLivros.Sorted = true;
            cmbLivros.TabIndex = 1;
            // 
            // btn_confirmar
            // 
            btn_confirmar.BackColor = Color.FromArgb(0, 86, 179);
            btn_confirmar.Font = new Font("JetBrains Mono NL", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btn_confirmar.ForeColor = SystemColors.ControlLightLight;
            btn_confirmar.Location = new Point(271, 307);
            btn_confirmar.Name = "btn_confirmar";
            btn_confirmar.Size = new Size(166, 80);
            btn_confirmar.TabIndex = 9;
            btn_confirmar.Text = "Emprestar";
            btn_confirmar.UseVisualStyleBackColor = false;
            btn_confirmar.Click += btn_confirmar_ClickAsync;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("JetBrains Mono NL SemiBold", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(0, 86, 179);
            label2.Location = new Point(234, 12);
            label2.Name = "label2";
            label2.Size = new Size(255, 36);
            label2.TabIndex = 7;
            label2.Text = "Emprestar Livro";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("JetBrains Mono NL SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(51, 51, 51);
            label4.Location = new Point(301, 235);
            label4.Name = "label4";
            label4.Size = new Size(228, 26);
            label4.TabIndex = 7;
            label4.Text = "Quantidade de dias";
            // 
            // nudDias
            // 
            nudDias.BackColor = SystemColors.Menu;
            nudDias.Font = new Font("JetBrains Mono NL", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nudDias.Location = new Point(574, 224);
            nudDias.Margin = new Padding(3, 4, 3, 4);
            nudDias.Maximum = new decimal(new int[] { 366, 0, 0, 0 });
            nudDias.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudDias.Name = "nudDias";
            nudDias.Size = new Size(103, 34);
            nudDias.TabIndex = 3;
            nudDias.Value = new decimal(new int[] { 7, 0, 0, 0 });
            // 
            // CadastroEmprestimo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(690, 407);
            Controls.Add(nudDias);
            Controls.Add(btn_confirmar);
            Controls.Add(cmbLivros);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cmbUsuarios);
            Controls.Add(label4);
            Controls.Add(label3);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CadastroEmprestimo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Empréstimos";
            Load += Emprestimo_Load;
            ((System.ComponentModel.ISupportInitialize)nudDias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private ComboBox cmbUsuarios;
        private Label label1;
        private ComboBox cmbLivros;
        private Button btn_confirmar;
        private Label label2;
        private Label label4;
        private NumericUpDown nudDias;
    }
}