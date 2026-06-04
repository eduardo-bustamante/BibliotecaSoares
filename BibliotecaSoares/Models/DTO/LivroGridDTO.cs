using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BibliotecaSoares.Models.DTO
{
    public class LivroGridDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int QuantidadeTotal { get; set; }
        public int QuantidadeEmprestada { get; set; }
        public int QuantidadeDisponivel { get; set; }

        public string Editora { get; set; }
        public int Ano { get; set; }
        public string Genero { get; set; }
        public string Idioma { get; set; }
        public string? CaminhoCapa { get; set; } = null;
    }
}
