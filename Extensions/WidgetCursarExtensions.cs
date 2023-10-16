using ms_aula.Domains;
using ms_aula.Features.WidgetCursarFeature.Commands;

namespace ms_aula.Extensions
{
    public static class WidgetCursarExtensions
    {
        public static WidgetCursar ToDomain(this InserirWidgetCursarCommand request)
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
