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
        IEnumerable<Pessoa> usuarios;
        public UsuarioView()
        {
            InitializeComponent();
            pessoaAzureServico = new PessoaAzureService();
            ListarUsuariosAsync(sbBusca.Text);
        }

        //void aguarda resultado
        private async void ListarUsuariosAsync(string busca=null)
        {
            
            lvUsuarios.IsRefreshing = true;
            try
            {


                //Fez a consulta no banco de dados Azure
                usuarios = await pessoaAzureServico.ListarRegistroAsync();

                //Verifica se existe um termo para a busca
                if (!string.IsNullOrWhiteSpace(busca))
                {
                    lvUsuarios.ItemsSource = usuarios.Where(p =>
                            p.RazaoSocial.StartsWith(busca) )
                            .OrderBy(p => p.RazaoSocial)
                            .ToList();

                }
                else {


                lvUsuarios.ItemsSource = usuarios
                    .OrderBy(p => p.RazaoSocial)
                            .ToList();
            }

            }catch
            {
                await DisplayAlert("Atenção", "Não foi possivel realizar a consulta", "Fechar");
            }

            lvUsuarios.IsRefreshing = false;
           
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

        private async void BtnRemover_Clicked(object sender, EventArgs e)
        {
            //Pega o valor do Command do menu da lista CommandParameter
            string id = ((MenuItem)sender).CommandParameter.ToString();

            //Pega o item(objeto) da lista selecionado
            Pessoa usuario = usuarios.FirstOrDefault(p => p.Id == id);
            if (usuario != null)
            {
                bool retorno = await pessoaAzureServico.ExcluirRegistroAsync(usuario);
                if (retorno)
                {
                    await DisplayAlert("Sucesso", "Registro excluido com sucesso", "Fechar");
                    ListarUsuariosAsync();
                    return;
                }
            }
            
            else
            {
                await DisplayAlert("Atenção", "Não foi possível a exclusão do registro", "Fechar"); 
            }
            
        }

        private void BtnAdicionar_Clicked(object sender, EventArgs e)
        {
            MasterView.NavegacaoMasterDetail.Detail.Navigation.PushAsync(new PessoaView());
        }

        private void LvUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Pega o item selecionado "e" na lista
            Pessoa usuario = e.SelectedItem as Pessoa;

            //vai para PessoaView enviando dados do usuario selecionado
            MasterView.NavegacaoMasterDetail.Detail.Navigation.PushAsync(new PessoaView(usuario));
        }
    }
}