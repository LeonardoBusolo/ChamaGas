using ChamaGas.Helpers;
using ChamaGas.Interface;
using ChamaGas.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChamaGas.Model 
{
    public class PedidoItens : ViewModelBase, iAzureTabela
    {
        public string Id { get; set; }

        public string PedidoId { get; set; }

        public string ProdutoId { get; set; }

        private double quantidade;
        public double Quantidade
        {
            get { return quantidade; }
            set { SetProperty(ref quantidade, value); }
        }

        public double Preco { get; set; }


        private double valorTotal;
        public double ValorTotal
        {
            get { return valorTotal; }
            private set { SetProperty(ref valorTotal, value); }
        }

        [JsonIgnore]
        public Command MaisCommand { get; set; }

        [JsonIgnore]
        public Command MenosCommand { get; set; }

        [JsonIgnore]
        public string IcoMenos { get; set; }

        [JsonIgnore]
        public string IcoMais { get; set; }


        [JsonIgnore]
        public string DescricaoProduto { get; set; }

        public PedidoItens(string pedidoId, 
            string produtoId, 
            string IdDoItem, 
            int quantidade = 1, 
            double preco = 0)
        {

            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Id = IdDoItem;
            Quantidade = quantidade;
            Preco = preco;
            if (quantidade > 0 && preco > 0)
            {
                ValorTotal = Preco * Quantidade;
            }

            MaisCommand = new Command(Mais);
            MenosCommand = new Command(Menos);

            IcoMenos = Font_Index.minus_circle;
            IcoMais = Font_Index.plus_circle;
        }

        private void Menos()
        {
            if (Quantidade > 0)
            {
                Quantidade -= 1;
                ValorTotal = Quantidade * Preco;
            }
                

        }

        private void Mais()
        {
            Quantidade += 1;
            ValorTotal = Quantidade * Preco;
        }
    }
}
