using ChamaGas.Extension;
using ChamaGas.Helpers;
using ChamaGas.ViewModel;
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
    public partial class CameraView : ContentPage
    {
        public CameraView()
        {
            InitializeComponent();
            this.BindingContext = new CameraViewModel();
        }

        //private async void BtnTiraFoto_Clicked(object sender, EventArgs e)
        //{
        //    //DateTime.Now.ToString(); 
        //   var foto_md = await Photo.TiraFoto(DateTime.Now.ToString()+".jpg", saveInAlbum:false);

        //    imgBanco.Source = foto_md.fotoArray.ToImagemSource();
        //    imgGaleria.Source = foto_md.pathGaleria;
        //    imgInterna.Source = foto_md.pathInterno;
        //}
    }
}