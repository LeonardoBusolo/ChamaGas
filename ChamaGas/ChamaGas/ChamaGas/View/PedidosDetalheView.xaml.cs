using ChamaGas.Model;
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
	public partial class PedidosDetalheView : ContentPage
	{
		public PedidosDetalheView (Pedido pedido = null)
		{
			InitializeComponent ();
		}
	}
}