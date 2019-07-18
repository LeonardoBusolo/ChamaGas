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
    public partial class ConsultaPedidoView : ContentPage
    {
        Pedido pedido;
        PedidoAzureService pedido_Service = new PedidoAzureService();
        public ConsultaPedidoView(Pedido pedido)
        {
            this.pedido = pedido;
            InitializeComponent();
            
            PopulaCampos(pedido);
        }

        private void PopulaCampos(Pedido pedido)
        {
            

            lblId.Text = pedido.Id;
            lblEmissao.Text = pedido.DataEmissao.ToString("dd/MM/yyyy");
            lblForne.Text = pedido.NomeFornecedor;
            lblCliente.Text = pedido.NomeCliente;
            lvItens.ItemsSource = pedido.listaItens;
            lblValorTotal.Text = pedido.TotalPedido.ToString("C2");
            lblDataAgenda.Text = pedido.DataAgenda.ToString("dd/MM/yyyy HH:mm:ss");          
            if (lblDataEntrega == null || pedido.DataEntrega == DateTime.MinValue)
            {
                EntregaVisible(string.Empty);
            }
            else
            {
                EntregaVisible(pedido.DataEntrega.ToString("dd/MM/yyyy HH: mm:ss"));
            }
        }

        private void EntregaVisible(string dataEntrega)
        {
            bool visibilidaeEntregaVazia = string.IsNullOrEmpty(dataEntrega);
            lblDataEntrega.Text = dataEntrega;
            lblEntrega.IsVisible = !visibilidaeEntregaVazia;
            lblDataEntrega.IsVisible = !visibilidaeEntregaVazia;
            btnEntregar.IsVisible = visibilidaeEntregaVazia;
        }

        private async void BtnEntregar_Clicked(object sender, EventArgs e)
        {
            this.pedido.DataEntrega = DateTime.Now;

            bool sucesso = await pedido_Service.AlterarRegistroAsync(this.pedido);

            if (sucesso)
            {
                await this.DisplayAlert("Sucesso", "Pedido entregue", "OK");
                await Navigation.PopAsync();
            }
            else
                await this.DisplayAlert("Falha", "Falha ao registrar entrega", "Fechar");
        }
    }
}