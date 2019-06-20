using ChamaGas.Helpers;
using ChamaGas.Model;
using ChamaGas.Services.Azure;
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
	public partial class LoginView : ContentPage
	{
        PessoaAzureService pessoaAzureServico;
      

        //API Edu
        Usuario usuario { get; set; }

        //Autenticacao Fabio
        UsuarioModel usuarioModel { get; set; }

        public LoginView ()
		{
			InitializeComponent ();
            pessoaAzureServico = new PessoaAzureService();
            usuarioModel = new UsuarioModel();

            //APi Edu
            usuario = new Usuario();
            this.BindingContext = usuario;


            meuIcone.Text = Font_Index.fire;
        }

        private async void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            if (ValidarDados())
            {
                var pessoa = await pessoaAzureServico.AutenticarUsuario(usuarioModel.Email, usuarioModel.Senha);
                //Verificação da autenticao do usuario
                if (pessoa != null)
                {
                    usuarioModel.Id = pessoa.Id;
                    usuarioModel.Email = pessoa.Email;
                    usuarioModel.Senha = pessoa.Senha;
                    usuarioModel.Permissao = pessoa.Tipo;
                    usuarioModel.Autenticado = true;
                    //salvar em banco de dados

                    //Defini nova MainPage (pagina principal)
                    App.Current.MainPage = new MasterView();
                }
            }
            else
            {
                await DisplayAlert("Atenção", "Não foi possivel autenticar usuario", "Fechar");
            }


            //Imprimir dados APII EDU
            //DisplayAlert("Informações", $"E-mail:{usuario.Email} Senha {usuario.Senha}", "Fechar");
        }

        private void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new PessoaView());
        }

        private bool ValidarDados()
        {
            
            if (string.IsNullOrWhiteSpace(entEmail.Text)) 
            {
                lblErro.IsVisible = true;
                lblErro.Text = "Por favor informe os dados do e-mail para autenticação";
                return false;
            }
            else if (string.IsNullOrWhiteSpace(entSenha.Text))
            {
                lblErro.IsVisible = true;
                lblErro.Text = "Por favor informe os dados da senha para autenticação";
                return false;
            }
            else if (entSenha.Text.Length > 8)
            {
                lblErro.IsVisible = true;
                lblErro.Text = "Senha invalidade";
                return false;
            }

                return true;

        }
    }
}