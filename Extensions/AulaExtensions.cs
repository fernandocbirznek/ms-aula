using ms_aula.Domains;
using ms_aula.Features.AulaFeature.Commands;

namespace ms_aula.Extensions
{
    public static class AulaExtensions
    {
        public static Aula ToDomain(this InserirAulaCommand request)
        {
            return new()
            {
                Titulo = request.Titulo,
                Resumo = request.Resumo,
                Favoritado = 0,
                Curtido = 0,
                ProfessorId = request.ProfessorId,
                AreaFisicaId = request.AreaFisicaId,
                Publicado = false,
                DataCadastro = DateTime.Now
            };
        }
    }
}
