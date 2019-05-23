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
	public partial class MenuView : ContentPage
	{
        List<Pagina> paginas;

		public MenuView ()
		{
			InitializeComponent ();
            IniciarLista();
		}

        public void IniciarLista()
        {
            paginas = new List<Pagina>();
            paginas.Add(new Pagina
            {
                Titulo = "Pessoa",
                Icone = "",
                PaginaView = typeof(PessoaView)
            });

            paginas.Add(new Pagina
            {
                Titulo = "Login",
                Icone = "",
                PaginaView = typeof(LoginView)
            });

            paginas.Add(new Pagina
            {
                Titulo = "Lista Pedidos",
                Icone = "",
                PaginaView = typeof(PessoaView)
            });

            lvMenu.ItemsSource = paginas;
        }

        private void LvMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Seleciona a pagina 
            var pagina = e.SelectedItem as Pagina;
            //Verificar se existe a pagina
            if (pagina != null)
            {
                //Iniciar Navegação
                MasterView.NavegacaoMasterDetail.IsPresented = false;
                MasterView.NavegacaoMasterDetail.Detail.Navigation.PopToRootAsync();
                //Cria a pagina view
                Page paginaview = Activator.CreateInstance(pagina.PaginaView) as Page;
                //Navega para a pagina 
                MasterView.NavegacaoMasterDetail.Detail.Navigation.PushAsync(paginaview);
                //Desativa o item selecionado
                lvMenu.SelectedItem = null;
            }
        }
    }
}