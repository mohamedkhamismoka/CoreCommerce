using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WebApplication28.BL.services
{
    public class imageUploader
    {

        public static string upload(IFormFile img)
        {
            var path = Directory.GetCurrentDirectory() + "/wwwroot/files/images";
            var filename=Guid.NewGuid()+Path.GetFileName(img.FileName);
            var finalPath=Path.Combine(path, filename); 
            using(var stream=new FileStream(finalPath, FileMode.Create))
            {
              img.CopyTo(stream);
            }

            return filename;   
        }


        public static void delete(string imngame)
        {
            try
            {
                var path = Directory.GetCurrentDirectory() + "/wwwroot/files/images/"+imngame ;
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

            }
            catch (Exception e)
            {

            }
        }
    }
}
