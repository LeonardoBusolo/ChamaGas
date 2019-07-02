using ChamaGas.Extension;
using ChamaGas.Helpers;
using ChamaGas.Model;
using ChamaGas.Services.Azure;
using MonkeyCache.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChamaGas.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProdutosView : ContentPage
    {
        ProdutoAzureService produto_Service = new ProdutoAzureService();
        PessoaAzureService  pessoa_Service = new PessoaAzureService();

        Pessoa usuarioLogado;
        bool eh_distribuidor;

        public ProdutosView()
        {
            InitializeComponent();
            usuarioLogado = Barrel.Current.Get<Pessoa>("pessoa");
            
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // TODO : ERRO
            eh_distribuidor = usuarioLogado.Tipo == "Distribuidor";

            if (eh_distribuidor)
                AdicionaBotaoNovoProduto();


            lblTitulo.Text = eh_distribuidor ? "Meus Produtos" : "Lista de Produtos";

            IEnumerable<Pessoa> pessoas = await pessoa_Service.ListarRegistroAsync();
            IEnumerable<Produto> produtos = await produto_Service.ListarRegistroAsync();
            if (eh_distribuidor) { 
                pessoas.Where(p => p.Id == usuarioLogado.Id).ToList();
                produtos.Where(p => p.FornecedorId == usuarioLogado.Id).ToList();
            }
            else
                pessoas.Where(p => p.Tipo == "Distribuidor").ToList();

            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var mPosition = await Geolocation.GetLocationAsync(request);

            foreach (Produto produto in produtos)
            {
                var forn = pessoas.Where(p => p.Id == produto.FornecedorId).FirstOrDefault();

                if (forn == null)
                    continue;

                produto.FornecedorNome = forn.RazaoSocial;
                produto.Latitude = forn.Latitude;
                produto.Longitude = forn.Longitude;

                Location locForn = new Location(forn.Latitude, forn.Longitude);
                forn.Distancia = mPosition.CalculateDistance(locForn, DistanceUnits.Kilometers);


                produto.Distancia = $"{forn.Distancia.ToString("N4")} KMS";

                produto.FotoSource = produto.FotoByte.ToImagemSource();
            }

            lvProdutos.ItemsSource = produtos;



            //populando lvProdutos na mão
            //lvProdutos.ItemsSource = new List<Produto>
            //{
            //    new Produto
            //    {
            //        Descricao ="Gás do Bom",
            //        Distancia = "5km",
            //        FornecedorNome = "Zé do Gás",

            //        Preco = 10.00
            //    },

            //    new Produto
            //    {
            //        Descricao ="Gás do Mais ou Menos",
            //        Distancia = "5km",
            //        FornecedorNome = "Zé do Gás",
            //        Preco = 6.50
            //    }
            //};
        }

        private void AdicionaBotaoNovoProduto()
        {
            if (this.ToolbarItems.Count == 0)
            this.ToolbarItems.Add(new ToolbarItem
            {
                Text = "Add",
                Command = new Command(AbrirTelaCadatroProduto),
            });
        }

        private void AbrirTelaCadatroProduto(object obj)
        {
            Navigation.PushAsync(new ProdutoView());
        }
    }
}