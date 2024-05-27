using ms_aula.Domains;
using ms_aula.Features.UsuarioAulaCurtidoFeature.Commands;

namespace ms_aula.Extensions
{
    public static class UsuarioAulaCurtidoExtensions
    {
        public static UsuarioAulaCurtido ToDomain(this InserirUsuarioAulaCurtidoCommand request)
        {
            return new()
            {
                UsuarioId = request.UsuarioId,
                AulaId = request.AulaId,
                DataCadastro = DateTime.Now
            };
        }
    }
}
