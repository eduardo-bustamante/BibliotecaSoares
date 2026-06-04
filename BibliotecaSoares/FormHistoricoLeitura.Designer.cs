namespace BibliotecaSoares
{
    partial class FormHistoricoLeitura
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHistoricoLeitura));
            lblNomeAluno = new Label();
            dgvHistorico = new DataGridView();
            btnImprimirHistoricoLeitura = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvHistorico).BeginInit();
            SuspendLayout();
            // 
            // lblNomeAluno
            // 
            lblNomeAluno.AutoSize = true;
            lblNomeAluno.Font = new Font("JetBrains Mono NL SemiBold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNomeAluno.Location = new Point(50, 10);
            lblNomeAluno.Name = "lblNomeAluno";
            lblNomeAluno.Size = new Size(56, 17);
            lblNomeAluno.TabIndex = 0;
            lblNomeAluno.Text = "label1";
            // 
            // dgvHistorico
            // 
            dgvHistorico.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvHistorico.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistorico.Location = new Point(4, 44);
            dgvHistorico.Name = "dgvHistorico";
            dgvHistorico.Size = new Size(1170, 430);
            dgvHistorico.TabIndex = 1;
            // 
            // btnImprimirHistoricoLeitura
            // 
            btnImprimirHistoricoLeitura.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            btnImprimirHistoricoLeitura.Font = new Font("JetBrains Mono SemiBold", 8.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnImprimirHistoricoLeitura.Image = (Image)resources.GetObject("btnImprimirHistoricoLeitura.Image");
            btnImprimirHistoricoLeitura.ImageAlign = ContentAlignment.TopCenter;
            btnImprimirHistoricoLeitura.Location = new Point(50, 494);
            btnImprimirHistoricoLeitura.Name = "btnImprimirHistoricoLeitura";
            btnImprimirHistoricoLeitura.Size = new Size(89, 55);
            btnImprimirHistoricoLeitura.TabIndex = 5;
            btnImprimirHistoricoLeitura.Text = "Imprimir";
            btnImprimirHistoricoLeitura.TextAlign = ContentAlignment.BottomCenter;
            btnImprimirHistoricoLeitura.UseVisualStyleBackColor = true;
            btnImprimirHistoricoLeitura.Click += btnImprimirHistoricoLeitura_Click;
            // 
            // FormHistoricoLeitura
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 561);
            Controls.Add(btnImprimirHistoricoLeitura);
            Controls.Add(dgvHistorico);
            Controls.Add(lblNomeAluno);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormHistoricoLeitura";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormHistoricoLeitura";
            ((System.ComponentModel.ISupportInitialize)dgvHistorico).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblNomeAluno;
        private DataGridView dgvHistorico;
        private Button btnImprimirHistoricoLeitura;
    }
}