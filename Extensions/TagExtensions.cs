using ms_aula.Domains;
using ms_aula.Features.TagFeature.Commands;

namespace ms_aula.Extensions
{
    public static class TagExtensions
    {
        public static Tag ToDomain(this InserirTagCommand request)
        {
            return new()
            {
                Nome = request.Nome,
                DataCadastro = DateTime.Now
            };
        }
    }
}
