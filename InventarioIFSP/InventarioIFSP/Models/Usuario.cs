﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventario_IFSPPRC.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Prontuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Nivel { get; set; } // padronizar numeração das funções
        // 0 - Admin: Gerencia usuários
        // 1 - Supervisor: Cadastra novas categorias, subcategorias, localidades, relatorios
        // 2 - Usuário: altera a localização dos itens

        public Usuario()
        {

        }
    }
}