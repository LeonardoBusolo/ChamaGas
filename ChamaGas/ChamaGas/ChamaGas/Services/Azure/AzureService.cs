using ChamaGas.Interface;
using ChamaGas.Model;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ChamaGas.Services.Azure
{
    public abstract class AzureService<T> where T : iAzureTabela
    {
        MobileServiceClient clienteAzure;
        IMobileServiceTable<T> tabelaAzure;

        public AzureService()
        {
            //Configura o serviço de conexao do Azure, buscando o caminho definido em App.xaml.cs
            clienteAzure = new MobileServiceClient(App.uriAzure);

            //Fazer o acesso a tabela
            tabelaAzure = clienteAzure.GetTable<T>();
        }
        /// <summary>
        /// Metodo de inclusão de um registro no banco de dados Azure
        /// </summary>
        /// <param name="registro">registro</param>
        /// <returns>Retorna verdadeiro ou falso para operação</returns>
        public async Task<bool> IncluirRegistroAsync(T registro)
        {
            try
            {
                await tabelaAzure.InsertAsync(registro);
                return true;
            }
            catch (Exception erro)
            {
                Debug.WriteLine($"Erro Azure: {erro}");
                return false;
            }

        }

        /// <summary>
        /// Metodo de alteração de um registro no banco de dados Azure
        /// </summary>
        /// <param name="registro">registro</param>
        /// <returns>Retorna verdadeiro ou falso para operação</returns>
        public async Task<bool> AlterarRegistroAsync(T registro)
        {
            try
            {
                await tabelaAzure.UpdateAsync(registro);
                return true;
            }
            catch (Exception erro)
            {
                Debug.WriteLine($"Erro Azure: {erro}");
                return false;
            }

        }

        /// <summary>
        /// Metodo de exclusão de um registro no banco de dados Azure
        /// </summary>
        /// <param name="registro">registro</param>
        /// <returns>Retorna verdadeiro ou falso para operação</returns>
        public async Task<bool> ExcluirRegistroAsync(T registro)
        {
            try
            {
                await tabelaAzure.DeleteAsync(registro);
                return true;
            }
            catch (Exception erro)
            {
                Debug.WriteLine($"Erro Azure: {erro}");
                return false;
            }

        }

        /// <summary>
        /// Metodo de lista todos registros da tabela no banco de dados Azure
        /// </summary>
        /// <returns>Retorna todos os registros</returns>
        public async Task<IEnumerable<T>> ListarRegistroAsync()
        {
            return await tabelaAzure.ToListAsync();
        }

        /// <summary>
        /// Metodo de busca de um registro especifico no banco de dados Azure
        /// </summary>
        /// <param name="registroId">Id do registro</param>
        /// <returns>Retorna o registro ou nulo</returns>
        public async Task<T> ObterRegistroAsync(string registroId)
        {
            var tabela = await tabelaAzure.ToListAsync();
            var registro = tabela.FirstOrDefault(r => r.Id == registroId);

            return registro;
        }
    }
}
