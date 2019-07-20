using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChamaGas.Helpers
{
    public class FileManager
    {
        public static string Save(string content)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string settingsPath = Path.Combine(path, "relat.html");
            StreamWriter stream = File.CreateText(settingsPath);
            stream.Write(content);
            stream.Close();

            return settingsPath;
        }
    }
}
