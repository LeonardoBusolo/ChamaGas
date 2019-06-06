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
        Pessoa_MD obj_pessoaMD;


        Base_Services clientCep = new Base_Services(Base_Services.URL_VIACEP);

        ReqRes_Services clientReqRes_user = new ReqRes_Services("users");
        ReqRes_Services clientReqRes_register = new ReqRes_Services("register");

        User_ReqRes md = new User_ReqRes();



        //Base_Services clientApi = new Base_Services(Base_Services.URL_API);
        public PessoaView ()
		{
			InitializeComponent ();
            //Instanciando serviço
            pessoaAzureServico = new PessoaAzureService();

            obj_pessoaMD = new Pessoa_MD();

            ListarTipo();
        }

        private async void EtCep_Unfocused(object sender, FocusEventArgs e)
        {
            var cep_ret = await clientCep.Get<CEP>(etCep.Text);

            this.etCep.Text = cep_ret.cep;
            this.etLogradouro.Text = cep_ret.logradouro;
            this.etComplemento.Text = cep_ret.complemento;
            this.etBairro.Text = cep_ret.bairro;
            this.etLocalidade.Text = cep_ret.localidade;
            this.etUf.Text = cep_ret.uf;

            var obj_reqres = await clientReqRes_user.Get<RetornoTeste>("2");

            md.name = cep_ret.bairro;
            md.job = cep_ret.localidade;

            var retornoPost = await clientReqRes_user.Post(md);
            //await this.DisplayAlert("meu retorno Post", $"Id:{retornoPost.id} Nome:{retornoPost.name} Job:{retornoPost.job} created{retornoPost.createdAt}", "Entendi");

            var retornoPut = await clientReqRes_user.Put(md, "2");
            //await this.DisplayAlert("meu retorno Put", $"Id:{retornoPut.id} Nome:{retornoPut.name} Job:{retornoPut.job} Update{retornoPut.updatedAt}", "Entendi");

            //await DisplayAlert("Informações", $"Id:{obj_reqres.data.FirstOrDefault().id.ToString()} Email:{obj_reqres.data.FirstOrDefault().email.ToString()}", "Fechar");

            var usuarioEntrada = new Usuario
            {
                Email= "eve.holt@reqres.in",
                Password= "pistola"
            };

            try
            {


                var usuarioSaida = await clientReqRes_register.Post<Usuario, Usuario>(usuarioEntrada);
                //await this.DisplayAlert("Retorno login", $"{usuarioSaida.Token}", "Logado");

            }
            catch (Exception ex)
            {

                await this.DisplayAlert("Erro", ex.Message, "OK");
            }

            
        }

        private async void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            var resultado = await SalvarAsync();
            if (resultado)
            {
                await DisplayAlert("Confirmar", "Registro Salvo com sucesso", "Fechar");
            }
            else
            {
                await DisplayAlert("Atenção", "Não foi possivel salvar registro", "Fechar");
            }
        }


        private async Task<bool> SalvarAsync()
        {
            obj_pessoaMD = new Pessoa_MD();
            obj_pessoaMD.Id = "";
            obj_pessoaMD.RazaoSocial = etRazaoSocial.Text;
            obj_pessoaMD.Tipo = picTipo.SelectedItem.ToString();
            obj_pessoaMD.Endereco = etLogradouro.Text;
            obj_pessoaMD.Numero = etNumero.Text;
            obj_pessoaMD.Bairro = etBairro.Text;
            obj_pessoaMD.Cep = etCep.Text;
            obj_pessoaMD.Uf = etUf.Text;
            obj_pessoaMD.Cidade = etLocalidade.Text;
            obj_pessoaMD.Telefone = etTelefone.Text;
            obj_pessoaMD.Email = etEmail.Text;
            obj_pessoaMD.Senha = etSenha.Text;



            if (string.IsNullOrWhiteSpace(obj_pessoaMD.Id))
            {
                return await pessoaAzureServico.IncluirRegistroAsync(obj_pessoaMD);
            }
            else
            {
                return await pessoaAzureServico.AlterarRegistroAsync(obj_pessoaMD);
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