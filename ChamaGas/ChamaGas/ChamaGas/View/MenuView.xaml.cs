﻿using ChamaGas.Extension;
using ChamaGas.Helpers;
using ChamaGas.Model;
using MonkeyCache.SQLite;
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

        Pessoa usuarioLogado;
        bool eh_distribuidor;


        public MenuView ()
		{
			InitializeComponent ();

            usuarioLogado = Barrel.Current.Get<Pessoa>("pessoa");
            eh_distribuidor = usuarioLogado.Tipo == "Distribuidor";

            ExibirPessoa();

            if (eh_distribuidor)
                IniciarListaDistribuidor();
            else
                IniciarListaConsumidor();


            meuIcone.Text = Font_Index.fire;
        }

        public void IniciarListaDistribuidor()
        {
            paginas = new List<Pagina>();
            paginas.Add(new Pagina
            {
                Titulo = "Perfil",
                Icone = Font_Index.user,
                PaginaView = typeof(PessoaView)
            });

            paginas.Add(new Pagina
            {
                Titulo = "Meus Produtos",
                Icone = Font_Index.barcode,
                PaginaView = typeof(ProdutosView)
            });

            paginas.Add(new Pagina
            {
                Titulo = "Lista Pedidos",
                Icone = Font_Index.list,
                PaginaView = typeof(PedidosView)
            });

            paginas.Add(new Pagina
            {
                Titulo = "Lista Usuarios",
                Icone = Font_Index.users,
                PaginaView = typeof(UsuarioView)
            });

            //paginas.Add(new Pagina
            //{
            //    Titulo = "Galeria",
            //    Icone = Font_Index.images,
            //    PaginaView = typeof(GaleriaView)
            //});

            lvMenu.ItemsSource = paginas;
        }

        public void IniciarListaConsumidor()
        {
            paginas = new List<Pagina>();
            paginas.Add(new Pagina
            {
                Titulo = "Perfil",
                Icone = Font_Index.user,
                PaginaView = typeof(PessoaView)
            });

            paginas.Add(new Pagina
            {
                Titulo = "Produtos",
                Icone = Font_Index.barcode,
                PaginaView = typeof(ProdutosView)
            });

            paginas.Add(new Pagina
            {
                Titulo = "Meus Pedidos",
                Icone = Font_Index.list,
                PaginaView = typeof(PedidosView)
            });

            paginas.Add(new Pagina
            {
                Titulo = "FlexLayout",
                Icone = Font_Index.flipboard,
                PaginaView = typeof(FlexPage)
            });

            //paginas.Add(new Pagina
            //{
            //    Titulo = "Galeria",
            //    Icone = Font_Index.images,
            //    PaginaView = typeof(GaleriaView)
            //});

            lvMenu.ItemsSource = paginas;
        }

        //Lista Criada Inicialmente para aprender Lista
        //public void IniciarListaInicial()
        //{
        //    paginas = new List<Pagina>();
        //    paginas.Add(new Pagina
        //    {
        //        Titulo = "Perfil",
        //        Icone = Font_Index.user,
        //        PaginaView = typeof(PessoaView)
        //    });

        //    paginas.Add(new Pagina
        //    {
        //        Titulo = "Produtos",
        //        Icone = Font_Index.barcode,
        //        PaginaView = typeof(HomeView)
        //    });

        //    paginas.Add(new Pagina
        //    {
        //        Titulo = "Lista Pedidos",
        //        Icone = Font_Index.list,
        //        PaginaView = typeof(PedidosView)
        //    });

        //    paginas.Add(new Pagina
        //    {
        //        Titulo = "Mapa",
        //        Icone = Font_Index.map,
        //        PaginaView = typeof(EssentialsMapaView)
        //    });

        //    paginas.Add(new Pagina
        //    {
        //        Titulo = "Camera",
        //        Icone = Font_Index.camera_retro,
        //        PaginaView = typeof(CameraView)
        //    });

        //    paginas.Add(new Pagina
        //    {
        //        Titulo = "Lista Usuarios",
        //        Icone = Font_Index.users,
        //        PaginaView = typeof(UsuarioView)
        //    });

        //    lvMenu.ItemsSource = paginas;
        //}

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

                //Navega para a pagina 
                //comando altera toda master detail
                //MasterView.NavegacaoMasterDetail.Detail.Navigation.PopToRootAsync();
                
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
                //comando altera toda master detail
                //MasterView.NavegacaoMasterDetail.Detail.Navigation.PushAsync(paginaView);
                MasterView.NavegacaoMasterDetail.Detail = new NavigationPage(paginaView);

                //Desativa o item selecionado
                lvMenu.SelectedItem = null;
            }
        }

        private void ExibirPessoa()
        {
            var pessoa = Barrel.Current.Get<Pessoa>("pessoa");
            if (pessoa != null)
            {
                vNome.Text = pessoa.RazaoSocial;
                vEmail.Text = pessoa.Email;
                vTipo.Text = pessoa.Tipo;
                vTelefone.Text = pessoa.Telefone;

                pessoa.FotoSource = pessoa.FotoByte.ToImagemSource();
                vFoto.Source = pessoa.FotoSource;
            }
        }
  


        private void GesSair_Tapped(object sender, EventArgs e)
        {
            //Finalizar sessao 
            Barrel.Current.Empty("pessoa");

            //fechar o aplicativo
            App.Current.MainPage = new LoginView();
         
        }

        private void GesIntro_Tapped(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new IntroView());
        }
    }

    
}