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
	public partial class IntroView : ContentPage
	{
		public IntroView ()
		{
			InitializeComponent ();
		}

     

        private void BtnVoltar_Clicked(object sender, EventArgs e)
        {
            //voltar ao inicio
            //App.Current.MainPage = new MasterView();

            this.Navigation.PopModalAsync();
        }
    }
}