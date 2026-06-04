namespace BibliotecaSoares
{
    partial class FormVersao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVersao));
            label5 = new Label();
            label3 = new Label();
            label4 = new Label();
            label2 = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("JetBrains Mono NL", 8.999999F);
            label5.ForeColor = SystemColors.ButtonFace;
            label5.Location = new Point(161, 272);
            label5.Name = "label5";
            label5.Size = new Size(252, 16);
            label5.TabIndex = 7;
            label5.Text = "Desenvolvido por Eduardo Bustamante";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("JetBrains Mono NL", 8.999999F);
            label3.ForeColor = SystemColors.ButtonFace;
            label3.Location = new Point(336, 289);
            label3.Name = "label3";
            label3.Size = new Size(77, 16);
            label3.TabIndex = 8;
            label3.Text = "versão 1.5";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("JetBrains Mono NL ExtraBold", 17.9999981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.ButtonHighlight;
            label4.Location = new Point(22, 176);
            label4.Name = "label4";
            label4.Size = new Size(378, 31);
            label4.TabIndex = 4;
            label4.Text = "Colégio Est. Odolfo Soares";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("JetBrains Mono NL SemiBold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(30, 1);
            label2.Name = "label2";
            label2.Size = new Size(363, 27);
            label2.TabIndex = 5;
            label2.Text = "Sistema de Gerenciamento de";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("JetBrains Mono NL", 27.7499962F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(90, 32);
            label1.Name = "label1";
            label1.Size = new Size(242, 49);
            label1.TabIndex = 6;
            label1.Text = "Biblioteca";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(111, 90);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(200, 123);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // FormVersao
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 91, 150);
            ClientSize = new Size(434, 311);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FormVersao";
            Text = "Versão do Sistema";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label5;
        private Label label3;
        private Label label4;
        private Label label2;
        private Label label1;
        private PictureBox pictureBox1;
    }
}