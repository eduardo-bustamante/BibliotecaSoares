using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BibliotecaSoares.Models;
using BibliotecaSoares.Models.DTO;

namespace BibliotecaSoares.Repositories
{
    public interface IEmprestimoRepository
    {
        // Registra um novo empréstimo
        Task RegistrarEmprestimoAsync(int usuarioId, int livroId, int diasParaDevolver);

        // Dá baixa em um livro que o aluno devolveu
        Task RegistrarDevolucaoAsync(int emprestimoId);

        // Traz a lista de empréstimos que ainda NÃO foram devolvidos (para a sua grade)
        Task<List<Emprestimo>> ObterTodosAtivosAsync();
        Task<List<RelatorioLivrosPorAlunoDTO>> ObterRelatorioQuantidadePorAlunoAsync();
    }
}
