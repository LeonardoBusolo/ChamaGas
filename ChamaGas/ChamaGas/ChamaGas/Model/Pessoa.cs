using ChamaGas.Helpers;
using ChamaGas.Interface;
using ChamaGas.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ChamaGas.Extension;

namespace ChamaGas.Model
{
    //[Newtonsoft.Json.JsonObject("Pessoa")]
    public class Pessoa : ViewModelBase, iAzureTabela
    {
        public string Id { get; set; }

        //public string Tipo { get; set; }

        private string tipo;
        public string Tipo
        {
            get { return tipo; }
            set { SetProperty(ref tipo, value); }
        }

        public string RazaoSocial { get; set; }

        public string Endereco { get; set; }

        public string Numero { get; set; }

        public string Bairro { get; set; }

        public string Cep { get; set; }

        public string Cidade { get; set; }

        public string Uf { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Telefone { get; set; }

        //byteArray convertido para string
        private string foto;
        public string Foto {
            get { return foto; }
            set { SetProperty(ref foto, value); }
        }

        //byte[]
        private byte[] fotoByte;
        [JsonIgnore]
        public byte[] FotoByte
        {
            get {
                if (fotoByte == null && Foto != null)
                    FotoByte = Convert.FromBase64String(Foto);
                return fotoByte;
            }

            set {
                SetProperty(ref fotoByte, value);                

                if (value != null)
                {
                    FotoSource = value.ToImagemSource();
                    Foto = Convert.ToBase64String(value);
                }
            }
        }

        //imageSource
        private ImageSource  fotoSource;
        [JsonIgnore]
        public ImageSource FotoSource
        {
            get { return fotoSource; }
            set { SetProperty(ref fotoSource, value); }
        }


        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public bool Deleted { get; set; }

        [JsonIgnore]  // ignora campo quando converte para para texto no formato json
        public double Distancia { get; set; }

        
        private bool botaoVisivel;

        [JsonIgnore]
        public bool BotaoVisivel
        {
            get { return botaoVisivel; }
            set { SetProperty(ref botaoVisivel, value); }
        }

        
        private bool imageVisivel;

        [JsonIgnore]
        public bool ImageVisivel
        {
            get { return imageVisivel; }
            set { SetProperty(ref imageVisivel, value); }
        }



        [JsonIgnore]
        public string TextoBotaoFoto { get; set; }


        public Pessoa()
        {
            
            BotaoVisivel = true;
            ImageVisivel = false;

            TextoBotaoFoto = Font_Index.camera;
        }



        
    }
}
