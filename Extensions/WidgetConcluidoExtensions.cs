using ms_aula.Domains;
using ms_aula.Features.WidgetConcluidoFeature.Commands;

namespace ms_aula.Extensions
{
    public static class WidgetConcluidoExtensions
    {
        public static WidgetConcluido ToDomain(this InserirWidgetConcluidoCommand request)
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
