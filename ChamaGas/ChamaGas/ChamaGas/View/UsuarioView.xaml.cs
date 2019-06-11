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
    public partial class UsuarioView : ContentPage
    {
        PessoaAzureService pessoaAzureServico;
        public UsuarioView()
        {
            InitializeComponent();
            pessoaAzureServico = new PessoaAzureService();
        }

        //void aguarda resultado
        private async void ListarUsuariosAsync(string busca)
        {
            aiCarregando.IsVisible = true;
            lvUsuarios.IsRefreshing = true;
            try
            {


                //Fez a consulta no banco de dados Azure
                var usuarios = await pessoaAzureServico.ListarRegistroAsync();

                //Verifica se existe um termo para a busca
                if (!string.IsNullOrWhiteSpace(busca))
                {
                    usuarios.Where(p =>
                            p.RazaoSocial.StartsWith(busca) ||
                            p.Endereco.Contains(busca) ||
                            p.Cep == busca).OrderBy(p => p.RazaoSocial);
                }


                lvUsuarios.ItemsSource = usuarios;
            }catch
            {
                await DisplayAlert("Atenção", "Não foi possivel realizar a consulta", "Fechar");
            }

            lvUsuarios.IsRefreshing = false;
            aiCarregando.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ListarUsuariosAsync(sbBusca.Text);
        }

        private void SbBusca_SearchButtonPressed(object sender, EventArgs e)
        {
            ListarUsuariosAsync(sbBusca.Text);
        }

        private void LvUsuarios_Refreshing(object sender, EventArgs e)
        {
            ListarUsuariosAsync(sbBusca.Text);
        }
    }
}