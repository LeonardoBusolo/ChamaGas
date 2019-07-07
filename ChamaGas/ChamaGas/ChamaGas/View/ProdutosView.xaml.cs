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

            icoCarrinho.Text = Font_Index.shopping_cart;
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // TODO : ERRO
            eh_distribuidor = usuarioLogado.Tipo == "Distribuidor";

            if (eh_distribuidor)
            {
                AdicionaBotaoNovoProduto();
            }
            else
            {
                AdicionarBarraCarrinho();
            }

            lblTitulo.Text = eh_distribuidor ? "Meus Produtos" : "Lista de Produtos";

            IEnumerable<Pessoa> pessoas = await pessoa_Service.ListarRegistroAsync();
            IEnumerable<Produto> produtos = await produto_Service.ListarRegistroAsync();
            if (eh_distribuidor) { 
                pessoas = pessoas.Where(p => p.Id == usuarioLogado.Id).ToList();
                produtos = produtos.Where(p => p.FornecedorId == usuarioLogado.Id).ToList();
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


            #region 
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
            #endregion
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

        private void AdicionarBarraCarrinho()
        {
            stackCarrinho.IsVisible = true;
        }

        private void AbrirTelaCadatroProduto(object obj)
        {
            Navigation.PushAsync(new ProdutoView());
        }

        private void GesCarrinho_Tapped(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new CarrinhoView());
        }

        private async void BtnRemover_Clicked(object sender, EventArgs e)
        {
            //Pega o valor do Command do menu da lista CommandParameter
            string id = ((MenuItem)sender).CommandParameter.ToString();

            //Pega o item(objeto) da lista selecionado
            IEnumerable<Produto> produtos = await produto_Service.ListarRegistroAsync();
            Produto produto = produtos.FirstOrDefault(p => p.Id == id);
            if (produto != null)
            {
                bool retorno = await produto_Service.ExcluirRegistroAsync(produto);
                if (retorno)
                {
                    await DisplayAlert("Sucesso", "Registro excluido com sucesso", "Fechar");
                    ListarProdutosAsync();
                    return;
                }
            }

            else
            {
                await DisplayAlert("Atenção", "Não foi possível a exclusão do registro", "Fechar");
            }

        }

        private void LvProdutos_Refreshing(object sender, EventArgs e)
        {
            ListarProdutosAsync(sbBusca.Text);
        }

        private async void ListarProdutosAsync(string busca = null)
        {

            lvProdutos.IsRefreshing = true;
            try
            {
                //Fez a consulta no banco de dados Azure
                IEnumerable<Pessoa> pessoas = await pessoa_Service.ListarRegistroAsync();
                IEnumerable<Produto> produtos = await produto_Service.ListarRegistroAsync();


                if (eh_distribuidor)
                {
                    pessoas = pessoas.Where(p => p.Id == usuarioLogado.Id).ToList();
                    produtos = produtos.Where(p => p.FornecedorId == usuarioLogado.Id).ToList();
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
                //Verifica se existe um termo para a busca
                if (!string.IsNullOrWhiteSpace(busca))
                {
                    lvProdutos.ItemsSource = produtos.Where(p =>
                            p.Descricao.StartsWith(busca))
                            .OrderBy(p => p.Descricao)
                            .ToList();
                }
                else
                {
                    lvProdutos.ItemsSource = produtos
                        .OrderBy(p => p.Descricao)
                                .ToList();
                }
            }
            catch
            {
                await DisplayAlert("Atenção", "Não foi possivel realizar a consulta", "Fechar");
            }

            lvProdutos.IsRefreshing = false;

        }

        private void SbBusca_SearchButtonPressed(object sender, EventArgs e)
        {
            ListarProdutosAsync(sbBusca.Text);
        }

        private void LvProdutos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Pega o item selecionado "e" na lista
            Produto produto = e.SelectedItem as Produto;

            //vai para PessoaView enviando dados do usuario selecionado
            MasterView.NavegacaoMasterDetail.Detail.Navigation.PushAsync(new ProdutoView(produto));
        }
    }
}