using ms_aula.Domains;
using ms_aula.Features.AreaFisicaDivisaoFeature.Commands;
using ms_aula.Features.AreaFisicaFeature.Commands;

namespace ms_aula.Extensions
{
    public static class AreaFisicaDivisaoExtensions
    {
        public static AreaFisicaDivisao ToDomain(this InserirAreaFisicaDivisaoCommand request)
        {
            return new()
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                Foto = request.Foto,
                AreaFisicaId = request.AreaFisicaId,
                DataCadastro = DateTime.Now
            };
        }
    }
}
