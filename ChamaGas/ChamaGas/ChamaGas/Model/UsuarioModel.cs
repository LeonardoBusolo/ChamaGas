using System;
using System.Collections.Generic;
using System.Text;

namespace ChamaGas.Model
{
    public class UsuarioModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Permissao { get; set; }

        public bool Autenticado { get; set; }
    }
}
