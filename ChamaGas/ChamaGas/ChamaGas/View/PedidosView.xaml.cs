using ChamaGas.Model;
using ChamaGas.Services.Azure;
using MonkeyCache.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChamaGas.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PedidosView : ContentPage
	{
        PedidoAzureService pedido_Service = new PedidoAzureService();
        PedidoItensAzureService pedidoItens_Service = new PedidoItensAzureService();
        PessoaAzureService pessoa_Service = new PessoaAzureService();
        Pessoa usuarioLogado;

		public PedidosView ()
		{
			InitializeComponent ();
            usuarioLogado = Barrel.Current.Get<Pessoa>("pessoa");
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            bool eh_distribuidor = usuarioLogado.Tipo == "Distrubuidor";

            IEnumerable<Pedido> pedidos = await pedido_Service.ListarRegistroAsync();
            IEnumerable<PedidoItens> pedidosItens = await pedidoItens_Service.ListarRegistroAsync();
            IEnumerable<Pessoa> pessoas = await pessoa_Service.ListarRegistroAsync();

            if (eh_distribuidor)
            {
                pedidos = pedidos.Where(p => p.FornecedorId == usuarioLogado.Id);
            }
            else
            {
                pedidos = pedidos.Where(p => p.ClienteId == usuarioLogado.Id);
            }

            foreach (var pedido in pedidos)
            {
                Pessoa pessoa = eh_distribuidor
                                ? pessoas.Where(p => p.Id == pedido.ClienteId).FirstOrDefault()
                                : pessoas.Where(p => p.Id == pedido.FornecedorId).FirstOrDefault();

                pedido.NomeFornecedor = pessoa.RazaoSocial;

                var itensFiltrados = pedidosItens.Where(i => i.PedidoId == pedido.Id).ToList();
                var total = itensFiltrados.Sum(i => i.ValorTotal);
                pedido.TotalPedido = total;// ("C2");
            }

            lvPedidos.ItemsSource = pedidos;

            //lvPedidos.ItemsSource = new List<Pedido>
            //{
            //    new Pedido("10","5")
            //    {
            //        DataAgenda = DateTime.Now,
            //        DataEmissao = DateTime.Now,
            //        DataEntrega = DateTime.Now,
            //        Id = "1",
            //        NomeFornecedor = "Jão do Gás",
            //        ValorTotal = "R$67.45"
            //    },

            //    new Pedido("10","5")
            //    {
            //        DataAgenda = DateTime.Now,
            //        DataEmissao = DateTime.Now,
            //        DataEntrega = DateTime.Now,
            //        Id = "12",
            //        NomeFornecedor = "Zé do Gás",
            //        ValorTotal = "R$99.45"
            //    }
            //};

        }
    }
}