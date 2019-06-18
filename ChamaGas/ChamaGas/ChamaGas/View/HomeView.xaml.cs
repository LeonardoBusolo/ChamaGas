using ChamaGas.Model;
using ChamaGas.Services.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using ChamaGas.Helpers;

namespace ChamaGas.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomeView : ContentPage
	{
        PessoaAzureService pessoa_service = new PessoaAzureService();
		public HomeView ()
		{
			InitializeComponent ();
            meuIcone.Text = Font_Index.home;
		}

        protected async override void OnAppearing()
        {
            //NutGet - Solution
            //xamarin.essentials
            //1.0.1
            //var mPosition = new Location(-20.8141467, -49.3758587);

            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var mPosition = await Geolocation.GetLocationAsync(request);

            List<Pessoa> fornecedores = (List<Pessoa>)
                await pessoa_service.List(etBusca.Text);

            foreach (var forn in fornecedores)
            {
                Location locForn = new Location(forn.Latitude, forn.Longitude);
                forn.Distancia = mPosition.CalculateDistance(locForn, DistanceUnits.Kilometers);
            }

            var fornOrdenado = fornecedores.OrderBy(f => f.Distancia).ToList();
            lvForns.ItemsSource = fornOrdenado;

            base.OnAppearing();
        }

        private async void LvForns_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Pessoa pessoaSelecionada = (Pessoa)e.Item;

            await Map.OpenAsync(pessoaSelecionada.Latitude, pessoaSelecionada.Longitude,
                new MapLaunchOptions
                {
                    Name = "Localização destino",
                    NavigationMode = NavigationMode.Driving,
                });
           
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void Icon_Tapped(object sender, EventArgs e)
        {
            this.DisplayAlert("Aviso", "Era uma label, mas pode ser clicado também.", "OK");
        }
    }
}