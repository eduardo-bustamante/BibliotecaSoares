using BibliotecaSoares.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaSoares.Repositories
{
    public interface ILivroRepository

    {
        Task AdicionarAsync(Livro livro);
        Task<List<Livro>> ListarTodosAsync();
        Task<Livro> ObterPorIdAsync(int id);
        Task AtualizarAsync(Livro livro);
        Task ExcluirAsync(int id);
        Task<List<Livro>> ListarPortituloAsync(string titulo);
        Task EmprestarAsync(int id);
        Task DevolverAsync(int id);
        Task<int> ObterTotalTitulosAsync();
        Task<int> ObterTotalExemplaresAsync();


    }
}
