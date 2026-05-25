using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaSoares.Models.DTO
{
    public class RelatorioLivrosPorAlunoDTO
    {
        public string NomeAluno { get; set; }
        public string Turma { get; set; }
        public int QuantidadeLivrosEmprestados { get; set; }
    }
}
