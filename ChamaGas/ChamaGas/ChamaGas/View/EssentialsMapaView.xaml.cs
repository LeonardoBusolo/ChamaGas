using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChamaGas.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EssentialsMapaView : ContentPage
	{
		public EssentialsMapaView ()
		{
			InitializeComponent ();
		}

        //public async Task NavegateToBuilding25()
        //{
        //    var mPosition = new Location(-20.8141467, -49.3758587);


        //    var location = new Location(47.645160, -122.1306032);
        //    var options = new MapLaunchOptions { NavigationMode = NavigationMode.Driving };

        //    await Map.OpenAsync(location, options);
        //}

        private async void BtnCordenadas_Clicked(object sender, EventArgs e)
        {
            if (!double.TryParse(etLatitude.Text, out double lat))
            {
                return;
            }

            if (!double.TryParse(etLongitude.Text, out double lng))
            {
                return;
            }

            var options = new MapLaunchOptions {
                Name = etName.Text,
                    NavigationMode = NavigationMode.Driving
                };

            await Map.OpenAsync(lat, lng, options);
        }
    }
}