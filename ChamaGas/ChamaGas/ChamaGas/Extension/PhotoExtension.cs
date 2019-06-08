using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace ChamaGas.Extension
{
    public static class PhotoExtension
    {
        public static ImageSource ToImagemSource(this Stream stream)
        {
            return ImageSource.FromStream(() => stream);
        }

        public static ImageSource ToImagemSource(this byte[] bytes)
        {
            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }

        public static byte[] ToByteArray(this Stream stream)
        {
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];

            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length; )
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);

                return buffer;

        }

    }
}
