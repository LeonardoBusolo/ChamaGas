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
	public partial class ProdutoView : ContentPage
	{
        ProdutoAzureService produto_Service = new ProdutoAzureService();

        private Produto produtoBC { get { return (Produto)this.BindingContext; } }

		public ProdutoView (Produto produto=null)
		{
			InitializeComponent ();


            //this.BindingContext = new Produto();

            if (produto == null)
                produto = new Produto();

            Produto produtoo = produto;
            this.BindingContext = produtoo;

            
            var usuarioLogado = Barrel.Current.Get<Pessoa>("pessoa");
            produtoBC.FornecedorId = usuarioLogado.Id;

            btnFoto.Text = Font_Index.camera;
        }

        private async void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            bool sucesso = string.IsNullOrEmpty(produtoBC.Id)
                ? await produto_Service.IncluirRegistroAsync(produtoBC)
                : await produto_Service.AlterarRegistroAsync(produtoBC);

            if (sucesso)
            {
                await this.DisplayAlert("Sucesso", "Registro salvo com sucesso!", "OK");
                await Navigation.PopAsync();
            }
                    
        }

        private async void BtnFoto_Clicked(object sender, EventArgs e)
        {
            var md = await Photo.TiraFoto("produto.jpg");

            if (md == null)
                return;

            this.produtoBC.FotoByte = md.fotoArray;
        }
    }
}