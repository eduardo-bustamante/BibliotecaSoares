namespace BibliotecaSoares
{
    partial class FrmRelatorioAtrasos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRelatorioAtrasos));
            dgvAtrasados = new DataGridView();
            btnPagarMulta = new Button();
            buscar = new Label();
            txt_nomeBusca = new TextBox();
            btnPagarTudo = new Button();
            btnBuscarNomeAtrasado = new Button();
            btnImprimir = new Button();
            lblQuantidade = new Label();
            lblValorTotal = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnMudarValorPadrao = new Button();
            txt_NovoValorMulta = new TextBox();
            lblDinheiro = new Label();
            lblTarifaAtual = new Label();
            btnOk = new Button();
            btnLimparBuscar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAtrasados).BeginInit();
            SuspendLayout();
            // 
            // dgvAtrasados
            // 
            dgvAtrasados.BackgroundColor = SystemColors.ButtonHighlight;
            dgvAtrasados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAtrasados.Location = new Point(12, 86);
            dgvAtrasados.Name = "dgvAtrasados";
            dgvAtrasados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAtrasados.Size = new Size(972, 583);
            dgvAtrasados.TabIndex = 0;
            // 
            // btnPagarMulta
            // 
            btnPagarMulta.BackColor = Color.FromArgb(0, 86, 179);
            btnPagarMulta.Font = new Font("JetBrains Mono NL", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnPagarMulta.ForeColor = Color.White;
            btnPagarMulta.Location = new Point(990, 86);
            btnPagarMulta.Margin = new Padding(3, 2, 3, 2);
            btnPagarMulta.Name = "btnPagarMulta";
            btnPagarMulta.Size = new Size(160, 100);
            btnPagarMulta.TabIndex = 2;
            btnPagarMulta.Text = "Pagar";
            btnPagarMulta.UseVisualStyleBackColor = false;
            btnPagarMulta.Click += btnPagarMulta_ClickAsync;
            // 
            // buscar
            // 
            buscar.AutoSize = true;
            buscar.Font = new Font("JetBrains Mono NL SemiBold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buscar.Location = new Point(26, 54);
            buscar.Name = "buscar";
            buscar.Size = new Size(56, 18);
            buscar.TabIndex = 3;
            buscar.Text = "Buscar";
            // 
            // txt_nomeBusca
            // 
            txt_nomeBusca.BackColor = SystemColors.ButtonHighlight;
            txt_nomeBusca.Font = new Font("JetBrains Mono NL", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_nomeBusca.Location = new Point(87, 51);
            txt_nomeBusca.Margin = new Padding(3, 2, 3, 2);
            txt_nomeBusca.Name = "txt_nomeBusca";
            txt_nomeBusca.PlaceholderText = "Digite o nome do Aluno";
            txt_nomeBusca.Size = new Size(338, 25);
            txt_nomeBusca.TabIndex = 4;
            // 
            // btnPagarTudo
            // 
            btnPagarTudo.BackColor = Color.FromArgb(0, 86, 179);
            btnPagarTudo.Font = new Font("JetBrains Mono NL", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnPagarTudo.ForeColor = Color.White;
            btnPagarTudo.Location = new Point(990, 190);
            btnPagarTudo.Margin = new Padding(3, 2, 3, 2);
            btnPagarTudo.Name = "btnPagarTudo";
            btnPagarTudo.Size = new Size(160, 100);
            btnPagarTudo.TabIndex = 2;
            btnPagarTudo.Text = "Pagar Tudo";
            btnPagarTudo.UseVisualStyleBackColor = false;
            btnPagarTudo.Click += btnPagarTudo_ClickAsync;
            // 
            // btnBuscarNomeAtrasado
            // 
            btnBuscarNomeAtrasado.BackColor = SystemColors.ActiveCaption;
            btnBuscarNomeAtrasado.FlatStyle = FlatStyle.Flat;
            btnBuscarNomeAtrasado.ForeColor = SystemColors.ActiveCaptionText;
            btnBuscarNomeAtrasado.Image = (Image)resources.GetObject("btnBuscarNomeAtrasado.Image");
            btnBuscarNomeAtrasado.Location = new Point(431, 43);
            btnBuscarNomeAtrasado.Name = "btnBuscarNomeAtrasado";
            btnBuscarNomeAtrasado.Size = new Size(40, 40);
            btnBuscarNomeAtrasado.TabIndex = 5;
            btnBuscarNomeAtrasado.UseVisualStyleBackColor = false;
            btnBuscarNomeAtrasado.Click += btnBuscarNomeAtrasado_ClickAsync;
            // 
            // btnImprimir
            // 
            btnImprimir.BackColor = Color.SlateGray;
            btnImprimir.FlatStyle = FlatStyle.Popup;
            btnImprimir.Font = new Font("JetBrains Mono NL", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnImprimir.ForeColor = Color.White;
            btnImprimir.Image = (Image)resources.GetObject("btnImprimir.Image");
            btnImprimir.ImageAlign = ContentAlignment.TopCenter;
            btnImprimir.Location = new Point(990, 327);
            btnImprimir.Margin = new Padding(3, 2, 3, 2);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(160, 61);
            btnImprimir.TabIndex = 2;
            btnImprimir.Text = "Imprimir";
            btnImprimir.TextAlign = ContentAlignment.BottomCenter;
            btnImprimir.UseVisualStyleBackColor = false;
            btnImprimir.Click += btnImprimir_Click;
            // 
            // lblQuantidade
            // 
            lblQuantidade.AutoSize = true;
            lblQuantidade.Font = new Font("JetBrains Mono SemiBold", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblQuantidade.Location = new Point(990, 451);
            lblQuantidade.Name = "lblQuantidade";
            lblQuantidade.Size = new Size(20, 21);
            lblQuantidade.TabIndex = 6;
            lblQuantidade.Text = "7";
            lblQuantidade.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblValorTotal
            // 
            lblValorTotal.AutoSize = true;
            lblValorTotal.Font = new Font("JetBrains Mono SemiBold", 11.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblValorTotal.Location = new Point(990, 503);
            lblValorTotal.Name = "lblValorTotal";
            lblValorTotal.Size = new Size(30, 21);
            lblValorTotal.TabIndex = 6;
            lblValorTotal.Text = "35";
            lblValorTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("JetBrains Mono", 8.999999F);
            label1.Location = new Point(990, 427);
            label1.Name = "label1";
            label1.Size = new Size(119, 16);
            label1.TabIndex = 7;
            label1.Text = "Livros Atrasados";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("JetBrains Mono", 8.999999F);
            label2.Location = new Point(990, 488);
            label2.Name = "label2";
            label2.Size = new Size(84, 16);
            label2.TabIndex = 7;
            label2.Text = "Valor Total";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("JetBrains Mono", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(470, 9);
            label3.Name = "label3";
            label3.Size = new Size(84, 25);
            label3.TabIndex = 13;
            label3.Text = "Multas";
            // 
            // btnMudarValorPadrao
            // 
            btnMudarValorPadrao.Font = new Font("JetBrains Mono SemiBold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMudarValorPadrao.Image = (Image)resources.GetObject("btnMudarValorPadrao.Image");
            btnMudarValorPadrao.ImageAlign = ContentAlignment.MiddleLeft;
            btnMudarValorPadrao.Location = new Point(990, 541);
            btnMudarValorPadrao.Name = "btnMudarValorPadrao";
            btnMudarValorPadrao.Size = new Size(160, 84);
            btnMudarValorPadrao.TabIndex = 14;
            btnMudarValorPadrao.Text = "Valor da Multa Diaria";
            btnMudarValorPadrao.UseVisualStyleBackColor = true;
            btnMudarValorPadrao.Click += btnMudarValorPadrao_Click;
            // 
            // txt_NovoValorMulta
            // 
            txt_NovoValorMulta.Font = new Font("JetBrains Mono", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_NovoValorMulta.Location = new Point(1020, 640);
            txt_NovoValorMulta.Name = "txt_NovoValorMulta";
            txt_NovoValorMulta.Size = new Size(89, 25);
            txt_NovoValorMulta.TabIndex = 15;
            // 
            // lblDinheiro
            // 
            lblDinheiro.AutoSize = true;
            lblDinheiro.Font = new Font("JetBrains Mono", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDinheiro.Location = new Point(990, 644);
            lblDinheiro.Name = "lblDinheiro";
            lblDinheiro.Size = new Size(24, 17);
            lblDinheiro.TabIndex = 16;
            lblDinheiro.Text = "R$";
            // 
            // lblTarifaAtual
            // 
            lblTarifaAtual.AutoSize = true;
            lblTarifaAtual.Font = new Font("JetBrains Mono", 8.999999F);
            lblTarifaAtual.Location = new Point(552, 55);
            lblTarifaAtual.Name = "lblTarifaAtual";
            lblTarifaAtual.Size = new Size(119, 16);
            lblTarifaAtual.TabIndex = 7;
            lblTarifaAtual.Text = "Livros Atrasados";
            // 
            // btnOk
            // 
            btnOk.Location = new Point(1115, 635);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(35, 35);
            btnOk.TabIndex = 17;
            btnOk.Text = "ok";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // btnLimparBuscar
            // 
            btnLimparBuscar.BackColor = SystemColors.ActiveCaption;
            btnLimparBuscar.FlatStyle = FlatStyle.Flat;
            btnLimparBuscar.ForeColor = SystemColors.ActiveCaptionText;
            btnLimparBuscar.Image = (Image)resources.GetObject("btnLimparBuscar.Image");
            btnLimparBuscar.Location = new Point(477, 43);
            btnLimparBuscar.Name = "btnLimparBuscar";
            btnLimparBuscar.Size = new Size(40, 40);
            btnLimparBuscar.TabIndex = 5;
            btnLimparBuscar.UseVisualStyleBackColor = false;
            btnLimparBuscar.Click += btnLimparBuscar_Click;
            // 
            // FrmRelatorioAtrasos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1157, 681);
            Controls.Add(btnOk);
            Controls.Add(lblDinheiro);
            Controls.Add(txt_NovoValorMulta);
            Controls.Add(btnMudarValorPadrao);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(lblTarifaAtual);
            Controls.Add(label1);
            Controls.Add(lblValorTotal);
            Controls.Add(lblQuantidade);
            Controls.Add(btnLimparBuscar);
            Controls.Add(btnBuscarNomeAtrasado);
            Controls.Add(buscar);
            Controls.Add(txt_nomeBusca);
            Controls.Add(btnImprimir);
            Controls.Add(btnPagarTudo);
            Controls.Add(btnPagarMulta);
            Controls.Add(dgvAtrasados);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmRelatorioAtrasos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Historico de Multas";
            Load += FrmRelatorioAtrasos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAtrasados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvAtrasados;
        private Button btnPagarMulta;
        private Label buscar;
        private TextBox txt_nomeBusca;
        private Button btnPagarTudo;
        private Button btnBuscarNomeAtrasado;
        private Button btnImprimir;
        private Label lblQuantidade;
        private Label lblValorTotal;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnMudarValorPadrao;
        private TextBox txt_NovoValorMulta;
        private Label lblDinheiro;
        private Label lblTarifaAtual;
        private Button btnOk;
        private Button btnLimparBuscar;
    }
}