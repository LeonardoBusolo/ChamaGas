using ChamaGas.Model;
using ChamaGas.Services;
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
	public partial class PessoaView : ContentPage
	{
        //Serviços do Azure
        PessoaAzureService pessoaAzureServico;
        Pessoa pessoa;


        Base_Services clientCep = new Base_Services(Base_Services.URL_VIACEP);

        

        
                    
        //Base_Services clientApi = new Base_Services(Base_Services.URL_API);

        //Metodo construtor vai receber uma pessoa por padrao
        public PessoaView (Pessoa usuario=null)
		{
			InitializeComponent ();
            //Instanciando serviço
            pessoaAzureServico = new PessoaAzureService();

            if (usuario == null)
            {
                usuario = new Pessoa();
            }

            pessoa = usuario;
            this.BindingContext = pessoa;

            ListarTipo();
        }

        private async void EtCep_Unfocused(object sender, FocusEventArgs e)
        {
            var cep_ret = await clientCep.Get<CEP>(etCep.Text);

            this.etCep.Text = cep_ret.cep;
            this.etLogradouro.Text = cep_ret.logradouro;
            //this.etComplemento.Text = cep_ret.complemento;
            this.etBairro.Text = cep_ret.bairro;
            this.etLocalidade.Text = cep_ret.localidade;
            this.etUf.Text = cep_ret.uf;
           
            //perdido nao sei o que é
            //md.name = cep_ret.bairro;
            //md.job = cep_ret.localidade;



            //ReqRes_Services clientReqRes_user = new ReqRes_Services("users");
            //var retornoPost = await clientReqRes_user.Post(md);
            //await this.DisplayAlert("meu retorno Post", $"Id:{retornoPost.id} Nome:{retornoPost.name} Job:{retornoPost.job} created{retornoPost.createdAt}", "Entendi");

            //User_ReqRes md = new User_ReqRes();
            //var retornoPut = await clientReqRes_user.Put(md, "2");
            //await this.DisplayAlert("meu retorno Put", $"Id:{retornoPut.id} Nome:{retornoPut.name} Job:{retornoPut.job} Update{retornoPut.updatedAt}", "Entendi");

            //declara variavel api teste clientReqRes_user
            //var obj_reqres = await clientReqRes_user.Get<RetornoTeste>("2");
            //await DisplayAlert("Informações", $"Id:{obj_reqres.data.FirstOrDefault().id.ToString()} Email:{obj_reqres.data.FirstOrDefault().email.ToString()}", "Fechar");



            //var usuarioEntrada = new Usuario
            //{
            //    Email= "eve.holt@reqres.in",
            //    Password= "pistola"
            //};

            //try
            //{

            //    ReqRes_Services clientReqRes_register = new ReqRes_Services("register");
            //    var usuarioSaida = await clientReqRes_register.Post<Usuario, Usuario>(usuarioEntrada);
            //    //await this.DisplayAlert("Retorno login", $"{usuarioSaida.Token}", "Logado");

            //}
            //catch (Exception ex)
            //{

            //    await this.DisplayAlert("Erro", ex.Message, "OK");
            //}


        }

        private async void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            aiCarregando.IsVisible = true;
            aiCarregando.IsRunning = true;
            var resultado = await SalvarAsync();
            if (resultado)
            {
                await DisplayAlert("Confirmar", "Registro Salvo com sucesso", "Fechar");
                await MasterView.NavegacaoMasterDetail.Detail.Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Atenção", "Não foi possivel salvar registro", "Fechar");
            }
            aiCarregando.IsRunning = false;
            aiCarregando.IsVisible = false;
        }


        private async Task<bool> SalvarAsync()
        {
            pessoa = new Pessoa();
            pessoa.Id = lblId.Text;
            pessoa.RazaoSocial = etRazaoSocial.Text;
            pessoa.Tipo = picTipo.SelectedItem.ToString();
            pessoa.Endereco = etLogradouro.Text;
            pessoa.Numero = etNumero.Text;
            pessoa.Bairro = etBairro.Text;
            pessoa.Cep = etCep.Text;
            pessoa.Uf = etUf.Text;
            pessoa.Cidade = etLocalidade.Text;
            pessoa.Telefone = etTelefone.Text;
            pessoa.Email = etEmail.Text;
            pessoa.Senha = etSenha.Text;



            if (string.IsNullOrWhiteSpace(pessoa.Id))
            {
                return await pessoaAzureServico.IncluirRegistroAsync(pessoa);
            }
            else
            {
                return await pessoaAzureServico.AlterarRegistroAsync(pessoa);
            }
        }

        private void ListarTipo()
        {
            List<string> tipos = new List<string>();
            tipos.Add("Consumidor");
            tipos.Add("Distribuidor");
            picTipo.ItemsSource = tipos;
            picTipo.SelectedIndex = 0;
        }



    }
}