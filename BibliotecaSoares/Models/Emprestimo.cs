using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaSoares.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }

        // --- Relacionamento com o Usuário ---
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } // Propriedade de navegação

        // --- Relacionamento com o Livro ---
        public int LivroId { get; set; }
        public Livro Livro { get; set; } // Propriedade de navegação

        // --- Dados do Empréstimo ---
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataPrevistaDevolucao { get; set; }
        public bool Devolvido { get; set; } = false;

    }
}
