using ms_aula.Domains;
using ms_aula.Features.AulaComentarioFeature.Commands;

namespace ms_aula.Extensions
{
    public static class AulaComentarioExtensions
    {
        public static AulaComentario ToDomain(this InserirAulaComentarioCommand request)
        {
            return new()
            {
                Descricao = request.Descricao,
                AulaId = request.AulaId,
                UsuarioId = request.UsuarioId,
                DataCadastro = DateTime.Now
            };
        }
    }
}
