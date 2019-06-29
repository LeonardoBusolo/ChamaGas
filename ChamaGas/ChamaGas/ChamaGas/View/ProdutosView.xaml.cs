using ChamaGas.Model;
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
    public partial class ProdutosView : ContentPage
    {
        Pessoa usuarioLogado;
        bool eh_distribuidor;

        public ProdutosView()
        {
            InitializeComponent();
            Barrel.Current.Get<Pessoa>("pessoa");
            
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // TODO : ERRO
            eh_distribuidor = usuarioLogado.Tipo == "Distribuidor";

            lblTitulo.Text = eh_distribuidor ? "Meus Produtos" : "Lista de Produtos";

            lvProdutos.ItemsSource = new List<Produto>
            {
                new Produto
                {
                    Descricao ="Gás do Bom",
                    Distancia = "5km",
                    FornecedorNome = "Zé do Gás",
                    Preco = 10.00
                },

                new Produto
                {
                    Descricao ="Gás do Mais ou Menos",
                    Distancia = "5km",
                    FornecedorNome = "Zé do Gás",
                    Preco = 6.50
                }
            };
        }
    }
}