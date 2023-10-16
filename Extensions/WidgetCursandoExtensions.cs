using ms_aula.Domains;
using ms_aula.Features.WidgetCursandoFeature.Commands;

namespace ms_aula.Extensions
{
    public static class WidgetCursandoExtensions
    {
        public static WidgetCursando ToDomain(this InserirWidgetCursandoCommand request)
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
