using ChamaGas.Interface;
using ChamaGas.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChamaGas.Model 
{
    public class PedidoItens : ViewModelBase, iAzureTabela
    {
        public string Id { get; set; }

        public string PedidoId { get; set; }

        public string ProdutoId { get; set; }

        public int Quantidade { get; set; }

        public double Preco { get; set; }

        public double ValorTotal { get; private set; }


        public PedidoItens(string pedidoId, string produtoId, int quantidade = 1, double preco = 0)
        {
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
            Preco = preco;
            if (quantidade > 0 && preco > 0)
            {
                ValorTotal = Preco * Quantidade;
            }
        }
    }
}
