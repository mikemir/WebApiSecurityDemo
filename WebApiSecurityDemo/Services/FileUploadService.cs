using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using WebApiSecurityDemo.Model.Db;
using WebApiSecurityDemo.Utils.Streams;

namespace WebApiSecurityDemo.Services
{
    public class FileUploadService : IFileUploadService
    {
        private const long MaxLength = 10 * 1024 * 1024; // 10MB

        public void UploadFile(FileUpload fileUpload)
        {
            var zFile = new ZipArchive(new MemoryStream(fileUpload.Content), ZipArchiveMode.Read);
            var declaredSize = zFile.Entries.Sum(entry => entry.Length);

            if (declaredSize > MaxLength)
                throw new Exception("Archivo zip demasiado grande.");

            foreach (var entry in zFile.Entries)
            {
                using var entryZipStream = entry.Open();

                using var restrictedStream = new RestrictedStream(entryZipStream, entry.Length);

                using var ms = new MemoryStream();

                //Internamente al hacer la copia no permitirá que se copie más del tamaño supuesto
                restrictedStream.CopyTo(ms);

                //Guardamos los archivos descomprimidos
                //File.WriteAllBytes(entry.Name, ms.ToArray());
            }
        }
    }
}