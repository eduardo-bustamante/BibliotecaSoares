using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BibliotecaSoares.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Turma { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        // O NotMapped impede que o EF Core tente salvar essa string no banco de dados
        [NotMapped]
        public string NomeComTurma => $"{Nome} - Turma: {Turma}";
    }
}
