using ChamaGas.Components;
using ChamaGas.Helpers;
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
    public partial class FlexPage : ContentPage
    {
        public FlexPage()
        {
            InitializeComponent();

            foreach (Mesa mesa in GerarMesas())
            {
                var stc = new StackLayout() { Margin = new Thickness(10) };
                
                var ic = new Icon() { Text = Font_Index.table, FontSize = 40, TextColor = mesa.Cor };
                var lbl = new Label() { Text = mesa.Nome};

                stc.Children.Add(ic);
                stc.Children.Add(lbl);
                flPrincipal.Children.Add(stc);


            }
        }

        public IEnumerable<Mesa> GerarMesas()
        {
            return new List<Mesa>
            {
            new Mesa { Nome = "Mesa 1", Status = "Disponivel" },
            new Mesa { Nome = "Mesa 2", Status = "Ocupado" },
            new Mesa { Nome = "Mesa 3", Status = "Ocupado" },
            new Mesa { Nome = "Mesa 4", Status = "Disponivel" },
            new Mesa { Nome = "Mesa 5", Status = "Ocupado" },
            new Mesa { Nome = "Mesa 6", Status = "Disponivel" },
            new Mesa { Nome = "Mesa 7", Status = "Ocupado" },
            new Mesa { Nome = "Mesa 8", Status= "Fechando" },
            new Mesa { Nome = "Mesa 9", Status= "Fechando" },
            new Mesa { Nome = "Mesa 9", Status = "Disponivel" },
            };
        }
    }

    public class Mesa
    {
        public string Nome { get; set; }

        public Color Cor { get
            {
                var corSelecao = this.Status == "Disponivel"
                       ? Color.Gray
                       : this.Status == "Ocupado"
                           ? Color.DarkRed
                           : Color.Yellow;

                return corSelecao;
            }  }

        public string Status { get; set; }
    }
}