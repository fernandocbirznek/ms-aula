using ms_aula.Domains;
using ms_aula.Features.ArquivoPdfFeature.Commands;

namespace ms_aula.Extensions
{
    public static class ArquivoPdfExtensions
    {
        public static InserirArquivoPdfCommand ToInserirArquivoPdf
        (
            this IFormFile formFile,
            AulaSessao aulaSessao
        )
        {
            return new InserirArquivoPdfCommand
            {
                FileData = formFile,
                AulaSessao = aulaSessao
            };
        }
    }
}
