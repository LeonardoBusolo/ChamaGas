using ChamaGas.Model;
using ChamaGas.Services;
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
        Base_Services clientCep = new Base_Services(Base_Services.URL_VIACEP);

        ReqRes_Services clientReqRes = new ReqRes_Services("api/users");


        //Base_Services clientApi = new Base_Services(Base_Services.URL_API);
        public PessoaView ()
		{
			InitializeComponent ();
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

            var obj_reqres = await clientReqRes.Get<RetornoTeste>("2");
            await DisplayAlert("Informações", $"Id:{obj_reqres.data.FirstOrDefault().id.ToString()}, " +
                $" Email:{obj_reqres.data.FirstOrDefault().email.ToString()}", "Fechar");
        }
    }
}