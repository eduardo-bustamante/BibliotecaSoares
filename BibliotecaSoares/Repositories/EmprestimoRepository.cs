using BibliotecaSoares.Data;
using BibliotecaSoares.Models;
using BibliotecaSoares.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaSoares.Repositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        public async Task RegistrarEmprestimoAsync(int usuarioId, int livroId, int diasParaDevolver)
        {
            using (var context = new AppDbContext())
            {
                // 1. Busca o livro para conferir se há cópias disponíveis
                var livro = await context.Livros.FindAsync(livroId);

                if (livro == null)
                    throw new Exception("O livro selecionado não foi encontrado no banco de dados.");

                if (livro.QuantidadeDisponivel <= 0)
                    throw new Exception("Não há cópias disponíveis deste livro no momento!");

                // 2. Cria o registro do empréstimo
                var novoEmprestimo = new Emprestimo
                {
                    UsuarioId = usuarioId,
                    LivroId = livroId,
                    DataEmprestimo = DateTime.Now,
                    DataPrevistaDevolucao = DateTime.Now.AddDays(diasParaDevolver),
                    Devolvido = false
                };

                // 3. Atualiza o contador de estoque do livro
                livro.QuantidadeEmprestada++;

                // 4. Salva o histórico e o livro de uma só vez
                await context.Emprestimos.AddAsync(novoEmprestimo);
                await context.SaveChangesAsync();
            }
        }

        public async Task RegistrarDevolucaoAsync(int emprestimoId)
        {
            using (var context = new AppDbContext())
            {
                var emprestimo = await context.Emprestimos.FindAsync(emprestimoId);

                if (emprestimo == null)
                    throw new Exception("Empréstimo não encontrado.");

                if (emprestimo.Devolvido)
                    throw new Exception("Este empréstimo já consta como devolvido no sistema.");

                var livro = await context.Livros.FindAsync(emprestimo.LivroId);

                if (livro != null)
                {
                    // Devolve o livro para a prateleira (diminui os emprestados)
                    livro.QuantidadeEmprestada--;
                }

                // Marca no histórico que o aluno entregou
                emprestimo.Devolvido = true;

                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Emprestimo>> ObterTodosAtivosAsync()
        {
            using (var context = new AppDbContext())
            {
                // Busca apenas os não devolvidos, incluindo os dados do Aluno e do Livro para facilitar a visualização na tela
                return await context.Emprestimos
                                    .Include(e => e.Usuario)
                                    .Include(e => e.Livro)
                                    .Where(e => e.Devolvido == false)
                                    .ToListAsync();
            }
        }

        public async Task<List<RelatorioLivrosPorAlunoDTO>> ObterRelatorioQuantidadePorAlunoAsync()
        {
            using (var context = new AppDbContext())
            {
                var relatorio = await context.Emprestimos
                    // 2. Agrupa os registros pelo Nome do aluno
                    .GroupBy(e => e.Usuario.Nome)

                    // 3. Seleciona o resultado montando a nossa nova classe
                    .Select(grupo => new RelatorioLivrosPorAlunoDTO
                    {
                        NomeAluno = grupo.Key, // O 'Key' do GroupBy é o Nome do aluno
                        Turma = grupo.FirstOrDefault().Usuario.Turma, // Pega a turma do primeiro registro do grupo (todos os registros do grupo tem a mesma turma)
                        QuantidadeLivrosEmprestados = grupo.Count() // Conta quantos livros tem no grupo
                    })

                    // 4. Ordena do maior para o menor (quem tem mais livros aparece no topo)
                    .OrderByDescending(r => r.QuantidadeLivrosEmprestados)
                    .ToListAsync();

                return relatorio;
            }
        }
    }

}

