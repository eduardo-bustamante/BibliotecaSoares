using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BibliotecaSoares
{
    public partial class FormSplash : Form
    {
        public FormSplash()
        {
            InitializeComponent();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 1. O sistema "congela" a tela do splash por 3 segundos (3000 milissegundos)
            await Task.Delay(3000);

            // 2. Após os 3 segundos, a tela se autodestrói
            this.Close();
        }
    }
}
