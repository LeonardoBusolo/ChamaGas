using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChamaGas.Components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Icon : Label
	{
		public Icon ()
		{
			InitializeComponent ();

            if (Device.RuntimePlatform == Device.Android)
                FontFamily = "fa-solid-900.ttf#Font Awesone 5 Free Solid";

            if (Device.RuntimePlatform == Device.iOS)
                FontFamily = "Font Awesone 5 Free";

            if (Device.RuntimePlatform == Device.UWP)
                FontFamily = "Assets/fa-solid-900.ttf#Font Awesone 5 Free Solid";

        }
	}
}