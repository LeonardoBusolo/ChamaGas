using ChamaGas.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChamaGas.Helpers
{
    public class GeradorRelatorioPedido
    {
        public static string GerarHtml(Pedido pedido, IEnumerable<PedidoItens> itens)
        {
            var str = new StringBuilder();
            str.Append(@"
            <!DOCTYPE html>
            <html>
                <head>
                    <style>
                        h5{
                            display: inline-block;
                        }
                        h6{
                            display: inline-block;
                            float: right;
                            color: red;
                        }
                    </style>
                </head>
                <body>       ");
            str.Append($"<h1>{pedido.NomeCliente}</h1>");
            str.Append($"<h1>{pedido.NomeFornecedor}</h1>");
            str.Append($"<h1>Data Agenda {pedido.DataAgenda.ToString("dd/MM/yyyy")}</h1>");
            str.Append($"<h1>Data Entrega {pedido.Entrega}</h1><hr>");

            foreach (var item in itens)
                str.Append($"<div><h5>{item.DescricaoProduto}</h5><h6>{item.Preco.ToString("C2")}</h6> </div>");

            str.Append($"<hr><div><h5>Total</h5><h6>{pedido.TotalPedido.ToString("C2")}</h6></div></body></html>");
            return str.ToString();
        }
    }
}


