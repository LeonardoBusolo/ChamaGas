using ChamaGas.Interface;
using ChamaGas.ViewModel;
using Newtonsoft.Json;
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

        [JsonIgnore]
        public string FornecedorNome { get; set; }

        [JsonIgnore]
        public string Distancia { get; set; }

        [JsonIgnore]
        public double Latitude { get; set; }

        [JsonIgnore]
        public double Longitude { get; set; }
    }
}



      
