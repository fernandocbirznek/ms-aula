using Microsoft.VisualBasic.FileIO;
using ms_aula.Domains;
using ms_aula.Interface;

namespace ms_aula.services
{
    public class FileService : IFileService
    {
        public async Task PostFileAsync(IFormFile fileData)
        {
            var fileDetails = new ArquivoPdf()
            {
                Id = 0,
                Nome = fileData.FileName,
            };
            using (var stream = new MemoryStream())
            {
                fileData.CopyTo(stream);
                fileDetails.Conteudo = stream.ToArray();
            }

            //var result = dbContextClass.FileDetails.Add(fileDetails);
            //await dbContextClass.SaveChangesAsync();
        }
        public async Task DownloadFileById(ArquivoPdf arquivo)
        {
            var content = new System.IO.MemoryStream(arquivo.Conteudo);
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "FileDownloaded",
                arquivo.Nome);
            await CopyStream(content, path);
        }
        public async Task CopyStream(Stream stream, string downloadPath)
        {
            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
    }
}
