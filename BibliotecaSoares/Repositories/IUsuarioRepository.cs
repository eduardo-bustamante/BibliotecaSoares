using BibliotecaSoares.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaSoares.Repositories
{
    public interface IUsuarioRepository
    {
        Task AdicionarAsync(Usuario usuario);
        Task<List<Usuario>> ObterTodosAsync();
        Task AtualizarAsync(Usuario usuario);
        Task ExcluirAsync(int id);
        Task<List<Usuario>> BuscarPorNomeAsync(string nome); // Para a nossa barra de pesquisa
    }
}
