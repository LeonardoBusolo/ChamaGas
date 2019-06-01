using System;
using System.Collections.Generic;
using System.Text;

namespace ChamaGas.Model
{
    public class User_ReqRes
    {
        public string id { get; set; }
        public string name { get; set; }
        public string job { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

}
