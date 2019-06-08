using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChamaGas.Model
{
    public class Foto_MD
    {
        [PrimaryKey, AutoIncrement, NotNull ]
        public int id { get; set; }

        [NotNull]
        public byte[] fotoArray { get; set; }

        [NotNull]
        public string pathGaleria { get; set; }

        [NotNull]
        public string pathInterno { get; set; }

    }
}
