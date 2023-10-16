using ms_aula.Domains;
using ms_aula.Features.AulaSessaoFavoritadaFeature.Commands;

namespace ms_aula.Extensions
{
    public static class AulaSessaoFavoritadaExtensions
    {
        public static AulaSessaoFavoritada ToDomain(this InserirAulaSessaoFavoritadaCommand request)
        {
            return new()
            {
                UsuarioId = request.UsuarioId,
                AulaSessaoId = request.AulaSessaoId,
                DataCadastro = DateTime.Now
            };
        }
    }
}
