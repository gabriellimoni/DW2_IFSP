using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventario_IFSPPRC.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Matricula { get; set; }
        public string Telefone { get; set; }

        public List<int> Funcoes { get; set; } // padronizar numeração das funções
    }
}