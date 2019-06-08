using ChamaGas.Model;
using PCLExt.FileStorage;
using PCLExt.FileStorage.Folders;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChamaGas.Helpers
{
    public class Conexao
    {
        public static SQLiteConnection GetConn()
        {
            var pasta = new LocalRootFolder();
            var arquivo = pasta.CreateFile("teste.db", CreationCollisionOption.OpenIfExists);

            return new SQLiteConnection(arquivo.Path);
        }

        public static void Initialize()
        {
            var conn = GetConn();
            conn.BeginTransaction();
            conn.CreateTable<Foto_MD>();
            conn.Commit();
            conn.Close();
        }
    }
}
