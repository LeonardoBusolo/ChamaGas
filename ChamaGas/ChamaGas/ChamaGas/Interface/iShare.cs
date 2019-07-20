using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChamaGas.Interface
{
    public interface iShare
    {
         Task  Share(string path, string title);
   
    }
}
