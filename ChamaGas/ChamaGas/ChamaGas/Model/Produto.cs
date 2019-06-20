using ChamaGas.Interface;
using ChamaGas.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChamaGas.Model
{
    public class Produto : ViewModelBase, iAzureTabela
    {

        public string Id { get; set; }

        public string FornecedorId { get; set; }

        public string Descricao { get; set; }

        public double Preco { get; set; }

        public string Foto { get; set; }

        public string Unidade { get; set; }

    }
}
