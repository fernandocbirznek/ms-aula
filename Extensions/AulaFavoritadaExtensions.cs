using ms_aula.Domains;
using ms_aula.Features.AulaFavoritadaFeature.Commands;

namespace ms_aula.Extensions
{
    public static class AulaFavoritadaExtensions
    {
        public static AulaFavoritada ToDomain(this InserirAulaFavoritadaCommand request)
        {
            return new()
            {
                AulaId = request.AulaId,
                UsuarioId = request.UsuarioId,
                DataCadastro = DateTime.Now
            };
        }
    }
}
