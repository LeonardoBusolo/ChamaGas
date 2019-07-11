using ChamaGas.Model;
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
        public static Pedido pedido = new Pedido("", "");
        public static ObservableCollection<PedidoItens> itens = new ObservableCollection<PedidoItens>();

		public CarrinhoView ()
		{
			InitializeComponent ();
            this.BindingContext = CarrinhoView.pedido;

            lvItens.ItemsSource = CarrinhoView.itens;
        }
	}
}