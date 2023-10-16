using ms_aula.Domains;
using ms_aula.Features.AulaComentarioFeature.Commands;
using ms_aula.Features.AulaSessaoFeature.Commands;

namespace ms_aula.Extensions
{
    public static class AulaSessaoExtensions
    {
        public static AulaSessao ToDomain(this InserirAulaSessaoCommand request)
        {
            return new()
            {
                Ordem = request.Ordem,
                Conteudo = request.Conteudo,
                AulaSessaoTipo = request.AulaSessaoTipo,
                AulaId = request.AulaId,
                DataCadastro = DateTime.Now
            };
        }
    }
}
