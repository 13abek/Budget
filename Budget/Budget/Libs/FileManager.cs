using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Budget.Libs
{
    public interface IFileManager
    {
        string Upload(IFormFile file, string savePath = "uploads", string newName = null);
        void Delete(string filename, string deletePath = "uploads");

        bool FileExists(string filename, string searchPath="uploads");
    }
    public class FileManager : IFileManager
    {
        public void Delete(string filename, string deletePath = "uploads")
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(),"wwroot",deletePath,filename);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public bool FileExists(string filename, string searchPath = "uploads")
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", searchPath, filename); //file uzantisini qaytarir

            return File.Exists(path);

        }

        public string Upload(IFormFile file, string savePath = "uploads", string newName = null)
        {

            var list = file.FileName.Split(',');

            string filename;
            if (newName==null)
            {
                filename = Guid.NewGuid() + "." + list[^1];

            }
            else
            {
                filename = newName + "." + list[^1]; 
            }
            var writePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", savePath);

            if (!Directory.Exists(writePath))
                Directory.CreateDirectory(writePath);


            var path = Path.Combine(writePath, filename);

            using (var  stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return filename;

        }
    }
}
