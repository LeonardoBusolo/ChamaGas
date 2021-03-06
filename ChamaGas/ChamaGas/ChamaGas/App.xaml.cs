﻿using ChamaGas.Helpers;
using ChamaGas.View;
using MonkeyCache.SQLite;
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
            //MainPage = new NavigationPage(new MasterView())
            

            //Habilita pagina principal
            Conexao.Initialize();


            Barrel.ApplicationId = "ChamaGasCacheBd";

            //removes all expired data
            Barrel.Current.EmptyExpired();

            //MainPage = new MasterView();
            MainPage = new LoginView();

            // tentativa colocar cor barra menu SEM SUCESSO
            //MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.OrangeRed);
            
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
