namespace BibliotecaSoares
{
    partial class FormHistoricoLivro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHistoricoLivro));
            dgvHistoricoLeitores = new DataGridView();
            lblTituloLivro = new Label();
            btnImprimirHistoricoLivro = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvHistoricoLeitores).BeginInit();
            SuspendLayout();
            // 
            // dgvHistoricoLeitores
            // 
            dgvHistoricoLeitores.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvHistoricoLeitores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistoricoLeitores.Location = new Point(4, 44);
            dgvHistoricoLeitores.Name = "dgvHistoricoLeitores";
            dgvHistoricoLeitores.Size = new Size(1170, 430);
            dgvHistoricoLeitores.TabIndex = 3;
            // 
            // lblTituloLivro
            // 
            lblTituloLivro.AutoSize = true;
            lblTituloLivro.Font = new Font("JetBrains Mono NL SemiBold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloLivro.Location = new Point(50, 10);
            lblTituloLivro.Name = "lblTituloLivro";
            lblTituloLivro.Size = new Size(56, 17);
            lblTituloLivro.TabIndex = 2;
            lblTituloLivro.Text = "label1";
            // 
            // btnImprimirHistoricoLivro
            // 
            btnImprimirHistoricoLivro.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnImprimirHistoricoLivro.Font = new Font("JetBrains Mono SemiBold", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnImprimirHistoricoLivro.Image = (Image)resources.GetObject("btnImprimirHistoricoLivro.Image");
            btnImprimirHistoricoLivro.ImageAlign = ContentAlignment.TopCenter;
            btnImprimirHistoricoLivro.Location = new Point(93, 494);
            btnImprimirHistoricoLivro.Name = "btnImprimirHistoricoLivro";
            btnImprimirHistoricoLivro.Size = new Size(89, 55);
            btnImprimirHistoricoLivro.TabIndex = 4;
            btnImprimirHistoricoLivro.Text = "Imprimir";
            btnImprimirHistoricoLivro.TextAlign = ContentAlignment.BottomCenter;
            btnImprimirHistoricoLivro.UseVisualStyleBackColor = true;
            btnImprimirHistoricoLivro.Click += btnImprimirHistoricoLivro_Click;
            // 
            // FormHistoricoLivro
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 561);
            Controls.Add(btnImprimirHistoricoLivro);
            Controls.Add(dgvHistoricoLeitores);
            Controls.Add(lblTituloLivro);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormHistoricoLivro";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormHistoricoLivro";
            Load += FormHistoricoLivro_Load;
            ((System.ComponentModel.ISupportInitialize)dgvHistoricoLeitores).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvHistoricoLeitores;
        private Label lblTituloLivro;
        private Button btnImprimirHistoricoLivro;
    }
}