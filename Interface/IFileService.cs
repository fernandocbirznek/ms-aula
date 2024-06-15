using Microsoft.VisualBasic.FileIO;
using ms_aula.Domains;


namespace ms_aula.Interface
{
    public interface IFileService
    {
        public Task PostFileAsync(IFormFile fileData);
        public Task DownloadFileById(ArquivoPdf arquivo);
    }
}
