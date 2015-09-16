using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Drawing;

namespace MojKatalog.Models.ViewModels
{
    public class UploadImage
    {
        public string ImageTitle { get; set; }
        public string ImagePath { get; set; }
        public string ImageStream { get; set; }

        public void CreateAndSaveImage()
        {
            byte[] bytes = Convert.FromBase64String(ImageStream);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            string path = Path.Combine(HttpContext.Current.Server.MapPath("/Areas/Portfolio/UploadedFiles"), "image.jpeg");
            image.Save(path, System.Drawing.Imaging.ImageFormat.Png);

            Bitmap bitmap = new Bitmap(image);
            bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
       
    }
}

