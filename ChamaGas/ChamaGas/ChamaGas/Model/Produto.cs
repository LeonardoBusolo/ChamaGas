using ChamaGas.Extension;
using ChamaGas.Interface;
using ChamaGas.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChamaGas.Model
{
    public class Produto : ViewModelBase, iAzureTabela
    {

        public string Id { get; set; }

        public string FornecedorId { get; set; }

        public string Descricao { get; set; }

        public double Preco { get; set; }

        private string foto;
        public string Foto
        {
            get { return foto; }
            set { SetProperty(ref foto, value);
                if(!string.IsNullOrEmpty(null))
                FotoSource = Convert.FromBase64String(value).ToImagemSource();

            }
        }


        //byte[]
        private byte[] fotoByte;
        [JsonIgnore]
        public byte[] FotoByte
        {
            get
            {
                if (fotoByte == null && Foto != null)
                    FotoByte = Convert.FromBase64String(Foto);
                return fotoByte;
            }

            set
            {
                SetProperty(ref fotoByte, value);

                if (value != null)
                {
                    FotoSource = value.ToImagemSource();
                    Foto = Convert.ToBase64String(value);
                }
            }
        }

        //imageSource
        private ImageSource fotoSource;
        [JsonIgnore]
        public ImageSource FotoSource
        {
            get { return fotoSource; }
            set { SetProperty(ref fotoSource, value); }
        }


        public string Unidade { get; set; }

        [JsonIgnore]
        public string FornecedorNome { get; set; }

        [JsonIgnore]
        public string Distancia { get; set; }

        [JsonIgnore]
        public double Latitude { get; set; }

        [JsonIgnore]
        public double Longitude { get; set; }
    }
}



      
