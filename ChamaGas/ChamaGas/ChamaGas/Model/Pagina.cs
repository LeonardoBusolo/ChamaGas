using ChamaGas.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChamaGas.Model
{
    public class Pagina : ViewModelBase
    {
        //Propriedades  
        public string Titulo { get; set; }

        public string Icone { get; set; }

        public Type PaginaView { get; set; }


        private Color corLetra;
        public Color CorLetra {
            get { return corLetra; }
            set { SetProperty(ref corLetra, value);  }
            }

    }
}
