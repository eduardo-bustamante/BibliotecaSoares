using BibliotecaSoares.Data;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaSoares
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // --- AUTOMAÇÃO DO BANCO DE DADOS ---
            try
            {
                using (var context = new AppDbContext())
                {
                    // Verifica se há alguma migration criada no seu código que ainda não 
                    // existe no banco da máquina. Se o banco não existir, ele CRIA o banco 
                    // e todas as tabelas no exato segundo em que o sistema abre.
                    context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falha grave ao conectar ou criar o banco de dados.\n\n" +
                                $"Verifique se o SQL Server está instalado nesta máquina e se a linha de conexão está correta.\n\n" +
                                $"Detalhes do erro: {ex.Message}",
                                "Erro de Inicialização", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Opcional: fechar o programa caso o banco de dados falhe catastroficamente
                return;
            }

            // Abre o formulário principal normalmente
            Application.Run(new CadastroLivros());
        }
    }
}