using ChamaGas.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ChamaGas.Services.Azure
{
    public class PessoaAzureService : AzureService<Pessoa_MD>
    {
        public async Task<IEnumerable<Pessoa_MD>> List(string busca)
        {
            //IEnumerable<Pessoa_MD> listaRetorno = new List<Pessoa_MD>();
            List<Pessoa_MD> listaRetorno = new List<Pessoa_MD>();

            listaRetorno.Add(new Pessoa_MD
            { 
                RazaoSocial = "Senac",
                Longitude = -49.3758587 ,
                Latitude = -20.8141467,
            });

            listaRetorno.Add(new Pessoa_MD
            {
                RazaoSocial = "Terminal",
                Longitude = -49.3766612,
                Latitude = -20.8096339,
            });

            listaRetorno.Add(new Pessoa_MD
            {
                RazaoSocial = "Mc Donald",
                Longitude = -49.3865877,
                Latitude = -20.822495,
            });

            listaRetorno.Add(new Pessoa_MD
            {
                RazaoSocial = "Habib's",
                Longitude = - -49.381770,
                Latitude = -20.817285,
                
            });

            return listaRetorno;
        }
    }
}
