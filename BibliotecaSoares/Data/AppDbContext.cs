using BibliotecaSoares.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaSoares.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Busca a string de conexão configurada no arquivo externo App.config
                string connectionString = ConfigurationManager.ConnectionStrings["BibliotecaDB"]?.ConnectionString;

                // Se por acaso o arquivo estiver vazio, usa um padrão local de contingência
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = @"Server=(localdb)\mssqllocaldb;Database=BibliotecaOdolfoSoares;Trusted_Connection=True;";
                }

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração opcional de segurança: Garante as regras de relacionamento se o EF se perder
            modelBuilder.Entity<Emprestimo>()
                .HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.UsuarioId);

            modelBuilder.Entity<Emprestimo>()
                .HasOne(e => e.Livro)
                .WithMany()
                .HasForeignKey(e => e.LivroId);
        }
    }
}
