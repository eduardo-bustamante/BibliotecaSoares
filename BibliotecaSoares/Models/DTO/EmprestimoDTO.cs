using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaSoares.Models.DTO
{
    public class EmprestimoAtivoDTO
    {
        public int Id { get; set; }
        public string Livro { get; set; }  // Título do Livro
        public string Aluno { get; set; }  // Nome do Aluno
        public string Autor { get; set; }  // Autor do Livro
        public string Turma { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucaoPrevista { get; set; }
    }
}
