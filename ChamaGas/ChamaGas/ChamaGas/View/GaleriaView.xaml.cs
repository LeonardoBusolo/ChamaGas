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
	public partial class GaleriaView : ContentPage
	{
		public GaleriaView ()
		{
			InitializeComponent ();
            ListarImagens();
		}

        private void ListarImagens()
        {
            // url da imagem
            var uri = new Uri(@"https://picsum.photos/200/300");

            // contador de 10 imagens
            for (int i = 0; i < 10; i++)
            {
               
                
                // instancia de cada imagem
                var imagem = new Image
                {
                    Source = ImageSource.FromUri(uri),
                    //Aspect = Aspect.
                };

                //adicionar no layout
                flxGaleria.Children.Add(imagem);
            }
        }
	}
}