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
    public partial class MasterView : MasterDetailPage
    {
        //Navegação para estrutura master detail page
        public static MasterDetailPage NavegacaoMasterDetail { get; set; }

        public MasterView()
        {
            InitializeComponent();
            // CONFIGURAÇÃO
            // AREA PRINCIPAL
            this.Detail = new NavigationPage(new HomeView()
            {
                Title = "ChamaGas",
                Icon = "",
            });

            // MENU
            this.Master = new MenuView()
            {
                Title = "Menu",
            };
            this.MasterBehavior = MasterBehavior.Popover;

            //Inicializa a navegacao Master detail page
            NavegacaoMasterDetail = this;

        }
    }
}