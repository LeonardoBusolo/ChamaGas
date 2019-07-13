using ChamaGas.Helpers;
using ChamaGas.Model;
using ChamaGas.Services.Azure;
using MonkeyCache.SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChamaGas.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarrinhoView : ContentPage
    {
        PedidoAzureService Pedido_Service = new PedidoAzureService();
        PedidoItensAzureService PedidoItens_Service = new PedidoItensAzureService();

        public static Pedido pedido = new Pedido("", "");
        public static ObservableCollection<PedidoItens> itens = new ObservableCollection<PedidoItens>();

        Pessoa usuario;

        public CarrinhoView()
        {
            InitializeComponent();

             usuario = Barrel.Current.Get<Pessoa>("pessoa");
            CarrinhoView.pedido.ClienteId = usuario.Id;

            this.BindingContext = CarrinhoView.pedido;

            icoCarrinho.Text = Font_Index.shopping_cart;
            icoListaItens.Text = Font_Index.box;
            icoDadosEntrega.Text = Font_Index.calendar_alt;
            lvItens.ItemsSource = CarrinhoView.itens;

        }

        private async void BtnSalvarPedido_Clicked(object sender, EventArgs e)
        {
            bool confirmar = await DisplayAlert(
                "Confirmação",
                "Deseja realmente enviar esse pedido ?",
                "Sim",
                "Não");

            if (!confirmar)
                return;

            if (!await Pedido_Service.IncluirRegistroAsync(CarrinhoView.pedido))
                return;

            var inserido = (await Pedido_Service.ListarRegistroAsync()).LastOrDefault();

            foreach (var item in CarrinhoView.itens)
            {
                item.PedidoId = inserido.Id;
                item.Id = string.Empty;
                if(!await PedidoItens_Service.IncluirRegistroAsync(item))
                {
                    await this.DisplayAlert("Falha", "Falha ao transmitir pedido", "Fechar");
                    //Criar metodo para excluir registros incluidos caso falha
                    //Roolback();
                    return;
                }
            }

            ZeraPedido();


        }

        private async void ZeraPedido()
        {
            await this.DisplayAlert("Sucesso", "Sucesso ao enviar pedido", "OK");
            await Navigation.PopAsync();

            CarrinhoView.pedido.Id = string.Empty;
            CarrinhoView.pedido.FornecedorId = string.Empty;
            CarrinhoView.pedido.NomeFornecedor = string.Empty;
            CarrinhoView.pedido.TotalItens = 0;
            CarrinhoView.pedido.TotalPedido = 0;
            CarrinhoView.itens = new ObservableCollection<PedidoItens>();

            
        }
    }
}