using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaSoares.Models.DTO
{
    public class HistorialLeitoresDTO
    {
        public string Aluno { get; set; }
        public string Turma { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime PrazoDevolucao { get; set; }
        public string Situacao { get; set; }
    }
}
