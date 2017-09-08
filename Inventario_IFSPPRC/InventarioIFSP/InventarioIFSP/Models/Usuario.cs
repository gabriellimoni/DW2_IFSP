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

        public Usuario(int ID, string Nome, string CPF, string Matricula, string Telefone, List<int> Funcoes)
        {
            this.ID = ID;
            this.Nome = Nome;
            this.CPF = CPF;
            this.Matricula = Matricula;
            this.Telefone = Telefone;
            this.Funcoes = Funcoes;
        }

        public Usuario(string Nome, string CPF, string Matricula, string Telefone, List<int> Funcoes)
        {
            this.Nome = Nome;
            this.CPF = CPF;
            this.Matricula = Matricula;
            this.Telefone = Telefone;
            this.Funcoes = Funcoes;
        }

        public Usuario(int ID)
        {
            this.ID = ID;
        }




        public Boolean insert()
        {
            // comandos para inserir 


            return false;
        }

        public Boolean update()
        {
            // comandos para atualizar 


            return false;
        }

        public Boolean delete()
        {
            // comandos para deletar 


            return false;
        }

        public Boolean autenticar()
        {


            return false;
        }
    }
}