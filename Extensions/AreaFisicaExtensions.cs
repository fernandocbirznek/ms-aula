using ms_aula.Domains;
using ms_aula.Features.AreaFisicaFeature.Commands;

namespace ms_aula.Extensions
{
    public static class AreaFisicaExtensions
    {
        public static AreaFisica ToDomain(this InserirAreaFisicaCommand request)
        {
            return new()
            {
                Descricao = request.Descricao,
                DataCadastro = DateTime.Now
            };
        }
    }
}
