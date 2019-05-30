using ChamaGas.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChamaGas.Model
{
    public class Pessoa_MD : iAzureTabela
    {
        public string Id { get; set; }

        public string Tipo { get; set; }

        public string RazaoSocial { get; set; }

        public string Endereco { get; set; }

        public string Numero { get; set; }

        public string Bairro { get; set; }

        public string Cep { get; set; }

        public string Cidade { get; set; }

        public string Uf { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Telefone { get; set; }

        public string Foto { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public bool Deleted { get; set; }
    }
}
