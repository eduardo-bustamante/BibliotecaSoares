using System;
using System.Collections.Generic;
using System.Text;

namespace BibliotecaSoares.Models.DTO
{
    public class HistorialLeituraDTO
    {
        public string Livro { get; set; }
        public string Autor { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime PrazoDevolucao { get; set; }
        public string Situacao { get; set; }
    }
}
