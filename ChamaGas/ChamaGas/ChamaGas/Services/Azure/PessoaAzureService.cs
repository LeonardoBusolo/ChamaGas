using ChamaGas.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ChamaGas.Services.Azure
{
    public class PessoaAzureService
    {
        MobileServiceClient clienteAzure;
        IMobileServiceTable<Pessoa_MD> tabelaPessoaAzure;

        public PessoaAzureService()
        {
            //Configura o serviço de conexao do Azure, buscando o caminho definido em App.xaml.cs
            clienteAzure = new MobileServiceClient(App.uriAzure);

            //Fazer o acesso a tabela
            tabelaPessoaAzure = clienteAzure.GetTable<Pessoa_MD>();
        }

        public async Task<bool> IncluirPessoa(Pessoa_MD pessoa)
        {
            try
            {
                await tabelaPessoaAzure.InsertAsync(pessoa);
                return true;
            }
            catch (Exception erro)
            {
                Debug.WriteLine($"Erro Azure: {erro}");
                return false;
            }
            
        }

        public async Task<bool> AlterarPessoa(Pessoa_MD pessoa)
        {
            try
            {
                await tabelaPessoaAzure.UpdateAsync(pessoa);
                return true;
            }
            catch (Exception erro)
            {
                Debug.WriteLine($"Erro Azure: {erro}");
                return false;
            }

        }

        public async Task<bool> ExcluirPessoa(Pessoa_MD pessoa)
        {
            try
            {
                await tabelaPessoaAzure.DeleteAsync(pessoa);
                return true;
            }
            catch (Exception erro)
            {
                Debug.WriteLine($"Erro Azure: {erro}");
                return false;
            }

        }
    }
}
