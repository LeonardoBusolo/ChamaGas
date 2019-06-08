using ChamaGas.Helpers;
using ChamaGas.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ChamaGas
{
    public partial class App : Application
    {
        public static string uriAzure = "https://chamagas38.azurewebsites.net";

        public App()
        {
            InitializeComponent();
            //
            //MainPage = new NavigationPage(new MasterView());

            //Habilita pagina principal
            Conexao.Initialize();

            MainPage = new MasterView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
