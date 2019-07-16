using ChamaGas.Interface;
using ChamaGas.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChamaGas.Model
{
    public class Pedido : ViewModelBase, iAzureTabela
    {
        public string Id { get; set; }

        public string ClienteId { get; set; }

        public string FornecedorId { get; set; }

        public DateTime DataEmissao { get; set; }

        public DateTime DataAgenda { get; set; }

        public DateTime DataEntrega { get; set; }

        [JsonIgnore]
        public string Entrega
        {
            get
            {
                if (DataEntrega == DateTime.MinValue)
                    return string.Empty;

                return DataEntrega.ToString();
            }
            set
            {
                DataEntrega = DateTime.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
            }
        }


        //[JsonIgnore]
        //public string ValorTotal { get; set; }

        [JsonIgnore]
        public string NomeFornecedor { get; set; }

        
        //declarando para barra de carrinho ficar atualizada janela produtos & carrinho
        public double totalPedido;
        [JsonIgnore]
        public double TotalPedido
        {
            get { return totalPedido;  }
            set { SetProperty(ref totalPedido, value);  }
        }

        //declarando para barra de carrinho ficar atualizada janela produtos & carrinho
        public int totalItens;
        [JsonIgnore]
        public int TotalItens
        {
            get { return totalItens; }
            set { SetProperty(ref totalItens, value); }
        }


        //Metodo construtor
        //ctor +tab (cria metodo construtor)
        public Pedido(string clienteId, string fornecedorId)
        {
            ClienteId = clienteId;
            FornecedorId = fornecedorId;
            DataEmissao = DateTime.Now;
            DataEntrega = DateTime.MinValue;
            //DataEntrega = DateTime.Now.AddHours(3);
        }

        public event EventHandler DelegateAtualizadorLista;

        public void AtualizaLista()
        {
            DelegateAtualizadorLista(this, new EventArgs());
        }
    }
}
