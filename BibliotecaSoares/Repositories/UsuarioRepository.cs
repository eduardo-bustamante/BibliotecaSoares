using BibliotecaSoares.Data;
using BibliotecaSoares.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaSoares.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public async Task AdicionarAsync(Usuario usuario)
        {
            using (var context = new AppDbContext())
            {
                await context.Usuarios.AddAsync(usuario);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Usuario>> ObterTodosAsync()
        {
            using (var context = new AppDbContext())
            {
                return await context.Usuarios.ToListAsync();
            }
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            using (var context = new AppDbContext())
            {
                context.Usuarios.Update(usuario);
                await context.SaveChangesAsync();
            }
        }

        public async Task ExcluirAsync(int id)
        {
            using (var context = new AppDbContext())
            {
                var usuario = await context.Usuarios.FindAsync(id);
                if (usuario != null)
                {
                    context.Usuarios.Remove(usuario);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<Usuario>> BuscarPorNomeAsync(string nome)
        {
            using (var context = new AppDbContext())
            {
                // Busca alunos onde o nome contenha o que foi digitado na barra de pesquisa
                return await context.Usuarios
                                    .Where(u => u.Nome.Contains(nome))
                                    .ToListAsync();
            }
        }
    }
}
