using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BibliotecaSoares.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public int Ano { get; set; }
        public string Genero { get; set; }
        public string Idioma { get; set; }
        public int QuantidadeTotal { get; set; }
        public int QuantidadeEmprestada { get; set; }

        // Calculado em tempo real, o banco de dados vai ignorar isso na hora de salvar
        [NotMapped]
        public int QuantidadeDisponivel => QuantidadeTotal - QuantidadeEmprestada;
    }
}
