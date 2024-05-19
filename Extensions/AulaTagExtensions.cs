using ms_aula.Domains;
using ms_aula.Features.AulaTagFeature.Commands;

namespace ms_aula.Extensions
{
    public static class AulaTagExtensions
    {
        public static AulaTag ToDomain(this InserirAulaTagMany request)
        {
            return new()
            {
                AulaId = request.AulaId,
                TagId = request.TagId,
                DataCadastro = DateTime.Now
            };
        }
    }
}
