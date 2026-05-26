using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaSoares.Models.DTO
{
    public class UsuarioGridDTO
    {
        public int Id { get; set; } // Sempre bom manter oculto caso precise para editar ou excluir
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Turma { get; set; }
    }
}
