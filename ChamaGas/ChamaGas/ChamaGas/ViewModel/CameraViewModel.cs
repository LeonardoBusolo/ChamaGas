using ChamaGas.Extension;
using ChamaGas.Helpers;
using ChamaGas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ChamaGas.ViewModel
{
    public class CameraViewModel : ViewModelBase
    {
        Command tirarFotoCommand;
        public Command TirarFotoCommand
        {
            get { return tirarFotoCommand; }
            set { SetProperty(ref tirarFotoCommand, value); }
        }



        ImageSource imgSBanco;
        public ImageSource ImgSBanco
        {
            get { return imgSBanco; }
            set { SetProperty(ref imgSBanco, value); }
        }



        ImageSource imgSGaleria;
        public ImageSource ImgSGaleria
        {
            get { return imgSGaleria; }
            set { SetProperty(ref imgSGaleria, value); }
        }


        ImageSource imgSInterna;
        public ImageSource ImgSInterna
        {
            get { return imgSInterna; }
            set { SetProperty(ref imgSInterna, value); }
        }

        public CameraViewModel()
        {
            //correto seria na cada dados
            var conn = Conexao.GetConn();
            var md = conn.Table<Foto_MD>().LastOrDefault();

            if (md != null)
                PreencheFotos(md);

            this.TirarFotoCommand = new Command(TirarFoto);

        }

        private  void PreencheFotos(Foto_MD md)
        {
            this.ImgSBanco = md.fotoArray.ToImagemSource();
            this.ImgSGaleria = md.pathGaleria;
            this.ImgSInterna = md.pathInterno;
        }

        private async void TirarFoto()
        {
            var md = await Photo.TiraFoto();
            
            //correto seria na cada dados
            var conn = Conexao.GetConn();
            conn.Insert(md);
            conn.Commit();
            conn.Close();

            PreencheFotos(md);
        }
    }
}
