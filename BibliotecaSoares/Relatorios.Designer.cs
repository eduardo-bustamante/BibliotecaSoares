namespace BibliotecaSoares
{
    partial class Relatorios
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
            panelRelatorio = new Panel();
            dgvRelatorio = new DataGridView();
            panelRelatorio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRelatorio).BeginInit();
            SuspendLayout();
            // 
            // panelRelatorio
            // 
            panelRelatorio.Controls.Add(dgvRelatorio);
            panelRelatorio.Location = new Point(10, 32);
            panelRelatorio.Name = "panelRelatorio";
            panelRelatorio.Size = new Size(542, 488);
            panelRelatorio.TabIndex = 6;
            // 
            // dgvRelatorio
            // 
            dgvRelatorio.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRelatorio.Location = new Point(3, 0);
            dgvRelatorio.Margin = new Padding(3, 2, 3, 2);
            dgvRelatorio.Name = "dgvRelatorio";
            dgvRelatorio.RowHeadersWidth = 51;
            dgvRelatorio.Size = new Size(528, 478);
            dgvRelatorio.TabIndex = 6;
            dgvRelatorio.ColumnHeaderMouseClick += dgvRelatorio_ColumnHeaderMouseClick;
            // 
            // Relatorios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(597, 565);
            Controls.Add(panelRelatorio);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Relatorios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Relatorios";
            panelRelatorio.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRelatorio).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelRelatorio;
        private DataGridView dgvRelatorio;
    }
}