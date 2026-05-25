using BibliotecaSoares.Data;
using BibliotecaSoares.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaSoares.Repositories
{
   

    public class LivroRepository : ILivroRepository
    {
        public async Task AdicionarAsync(Livro livro)
        {
           using (var context = new AppDbContext())
            {
                await context.Livros.AddAsync(livro);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Livro>> ListarTodosAsync()
        {
            using (var context = new AppDbContext())
            {
                return await context.Livros.ToListAsync();
            }
        }

        public async Task AtualizarAsync(Livro livro)
        {
            using (var context = new AppDbContext())
            {
                // 1. Busca o livro original no banco com todos os dados reais (incluindo os empréstimos)
                var livroOriginal = await context.Livros.FindAsync(livro.Id);

                if (livroOriginal != null)
                {
                    // 2. Trava de Segurança Crítica: 
                    // Não deixa o usuário colocar uma Quantidade Total menor do que o que já está na rua.
                    if (livro.QuantidadeTotal < livroOriginal.QuantidadeEmprestada)
                    {
                        throw new Exception($"Operação inválida! Existem {livroOriginal.QuantidadeEmprestada} cópias deste livro emprestadas no momento. A Quantidade Total não pode ser menor que isso.");
                    }

                    // 3. Atualiza APENAS os campos que vieram da tela
                    livroOriginal.Titulo = livro.Titulo;
                    livroOriginal.Autor = livro.Autor;
                    livroOriginal.Ano = livro.Ano; // Se você usa esse campo
                    livroOriginal.QuantidadeTotal = livro.QuantidadeTotal;

                    // 5. Salva no banco 
                    // O EF Core é inteligente e vai atualizar apenas os campos que foram modificados
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task ExcluirAsync(int id)
        {
            using (var context = new AppDbContext())
            {
                var livro = await context.Livros.FindAsync(id);
                if (livro != null)
                {
                    context.Livros.Remove(livro);
                    await context.SaveChangesAsync();
                }
                await Task.CompletedTask;
            }
        }

        public async Task<List<Livro>> ListarPortituloAsync(string titulo)
        {
            using (var context = new AppDbContext())
            {
                return await context.Livros.Where(l => l.Titulo.Contains(titulo)).ToListAsync();
            }
        }

        public async Task EmprestarAsync(int id)
        {
            using (var context = new AppDbContext())
            {
                var livro = await context.Livros.FindAsync(id);

                if (livro == null) throw new Exception("Livro não encontrado.");

                if (livro.QuantidadeDisponivel <= 0)
                    throw new Exception("Não há cópias disponíveis para empréstimo no momento!");

                livro.QuantidadeEmprestada++; // Aumenta 1 nos emprestados
                await context.SaveChangesAsync();
            }
        }

        public async Task DevolverAsync(int id)
        {
            using (var context = new AppDbContext())
            {
                var livro = await context.Livros.FindAsync(id);

                if (livro == null) throw new Exception("Livro não encontrado.");

                if (livro.QuantidadeEmprestada <= 0)
                    throw new Exception("Este livro não possui cópias emprestadas para serem devolvidas!");

                livro.QuantidadeEmprestada--; // Diminui 1 dos emprestados
                await context.SaveChangesAsync();
            }
        }

        public async Task<Livro> ObterPorIdAsync(int id)
        {
            using (var context = new AppDbContext())
            {
                // O FindAsync procura o livro pelo ID primário de forma super rápida
                return await context.Livros.FindAsync(id);
            }
        }

        public async Task<int> ObterTotalTitulosAsync()
        {
            using (var context = new AppDbContext())
            {
                return await context.Livros.CountAsync();
            }
        }

        // 2. Soma todos os exemplares físicos (a coluna QuantidadeTotal de todos os livros)
        public async Task<int> ObterTotalExemplaresAsync()
        {
            using (var context = new AppDbContext())
            {
                // Usamos (int?) e o ?? 0 para evitar erros caso a biblioteca esteja 100% vazia
                return await context.Livros.SumAsync(l => (int?)l.QuantidadeTotal) ?? 0;
            }
        }
    }
}

       
