using ChamaGas.Helpers;
using ChamaGas.Model;
using ChamaGas.Services.Azure;
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
            this.BindingContext = usuarioModel;

            

            meuIcone.Text = Font_Index.fire;
        }

        private async void BtnEntrar_Clicked(object sender, EventArgs e)
        {
            BloquearTela(true);
            if (ValidarDados())
            {
                var pessoa = await pessoaAzureServico.AutenticarUsuario(usuarioModel.Email, usuarioModel.Senha);
                //Verificação da autenticao do usuario
                if (pessoa != null)
                {
                    //usuarioModel.Id = pessoa.Id;
                    //usuarioModel.Email = pessoa.Email;
                    //usuarioModel.Senha = pessoa.Senha;
                    //usuarioModel.Permissao = pessoa.Tipo;
                    //usuarioModel.Autenticado = true;


                    //cache de dados para pessoa
                    //salva os dados da pessoa
                    Barrel.Current.Add(key: "pessoa", data: pessoa, expireIn: TimeSpan.FromMinutes(2));

                    //Defini nova MainPage (pagina principal)
                    App.Current.MainPage = new MasterView();
                    BloquearTela(false);
                }
                else
                {
                    BloquearTela(false);
                    await DisplayAlert("Atenção", "Conta não encontrada", "Fechar");
                }
            }
            else
            {
                await DisplayAlert("Atenção", "Não foi possivel autenticar usuario", "Fechar");

            }

            BloquearTela(false);

            return;


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
            else if (entSenha.Text.Length < 1)
            {
                lblErro.IsVisible = true;
                lblErro.Text = "Senha invalida";
                return false;
            }
            lblErro.Text = string.Empty;
                return true;
        }

        private void BloquearTela (bool resultado)
        {
            //default
            aiCarregar.IsVisible = true;
            aiCarregar.IsRunning = true;
            entEmail.IsEnabled = false;
            entSenha.IsEnabled = false;
            btnEntrar.IsEnabled = false;
            btnRegistrar.IsEnabled = false;

            if (!resultado)
            {
                aiCarregar.IsVisible = false;
                aiCarregar.IsRunning = false;
                entEmail.IsEnabled = true;
                entSenha.IsEnabled = true;
                btnEntrar.IsEnabled = true;
                btnRegistrar.IsEnabled = true;
            }
        }

        private void verificarLogin()
        {
            var usuarioLogado = Barrel.Current.Get<Pessoa>("pessoa");
            if (usuarioLogado != null)
            {
                App.Current.MainPage = new MasterView();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            verificarLogin();
        }
    }
}