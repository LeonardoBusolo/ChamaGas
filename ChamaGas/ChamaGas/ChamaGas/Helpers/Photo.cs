﻿using ChamaGas.Extension;
using ChamaGas.Model;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChamaGas.Helpers
{
    public class Photo
    {

        public static async Task<Foto_MD> TiraFoto(string nomeFoto = "test.jpg", string dir = "myDir", bool saveInAlbum = true)
        {
            var md = new Foto_MD();

            var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
            {
                Name = nomeFoto,
                Directory = dir,
                SaveToAlbum = saveInAlbum,
                CompressionQuality = 10,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,

            });

            if (photo == null)
                return null;

            md.pathGaleria = photo.AlbumPath;
            md.pathInterno = photo.Path;
            md.fotoArray = photo.GetStream().ToByteArray();

            return md;
        }
    }
}
