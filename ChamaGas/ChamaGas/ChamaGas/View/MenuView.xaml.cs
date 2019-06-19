using ChamaGas.Helpers;
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
            meuIcone.Text = Font_Index.fire;
        }

        public void IniciarLista()
        {
            paginas = new List<Pagina>();
            paginas.Add(new Pagina
            {
                Titulo = "Pessoa",
                Icone = Font_Index.user,
                PaginaView = typeof(PessoaView)
            });

            paginas.Add(new Pagina
            {
                Titulo = "Login",
                Icone = Font_Index.key,
                PaginaView = typeof(LoginView)
            });

            paginas.Add(new Pagina
            {
                Titulo = "Lista Pedidos",
                Icone = Font_Index.list,
                PaginaView = typeof(PessoaView)
            });

            paginas.Add(new Pagina
            {
                Titulo = "Mapa",
                Icone = Font_Index.map,
                PaginaView = typeof(EssentialsMapaView)
            });

            paginas.Add(new Pagina
            {
                Titulo = "Camera",
                Icone = Font_Index.camera_retro,
                PaginaView = typeof(CameraView)
            });

            paginas.Add(new Pagina
            {
                Titulo = "Lista Usuarios",
                Icone = Font_Index.users,
                PaginaView = typeof(UsuarioView)
            });

            lvMenu.ItemsSource = paginas;
        }

        private void LvMenu_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            foreach (Pagina item in lvMenu.ItemsSource)
            {
                item.CorLetra = Color.Gray;
            }

            //Seleciona a pagina 
            var pagina = e.Item as Pagina;
            //Verificar se existe a pagina
            if (pagina != null)
            {
                pagina.CorLetra = Color.OrangeRed;

                //Iniciar Navegação
                MasterView.NavegacaoMasterDetail.IsPresented = false;
                MasterView.NavegacaoMasterDetail.Detail.Navigation.PopToRootAsync();

                //Cria a pagina view
                Page paginaView = null;
                if (pagina.PaginaView == typeof(PessoaView))
                {
                    paginaView = new PessoaView(new Pessoa()); 
                }
                else
                {
                    paginaView = Activator.CreateInstance(pagina.PaginaView) as Page;
                }
                    


                //Navega para a pagina 
                MasterView.NavegacaoMasterDetail.Detail.Navigation.PushAsync(paginaView);
                //Desativa o item selecionado
                lvMenu.SelectedItem = null;
            }
        }
    }
}