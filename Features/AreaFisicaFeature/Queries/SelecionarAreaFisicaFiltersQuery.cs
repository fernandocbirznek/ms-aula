using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AreaFisicaFeature.Queries
{
    public class SelecionarAreaFisicaFiltersQuery : IRequest<IEnumerable<SelecionarAreaFisicaFiltersQueryResponse>>
    {
    }

    public class SelecionarAreaFisicaFiltersQueryResponse : Entity
    {
        public string Descricao { get; set; }
        public string Titulo { get; set; }
        public string Aplicacao { get; set; }
    }

    public class SelecionarAreaFisicaFiltersQueryResponseHandler : IRequestHandler<SelecionarAreaFisicaFiltersQuery, IEnumerable<SelecionarAreaFisicaFiltersQueryResponse>>
    {
        private readonly IRepository<AreaFisica> _repository;

        public SelecionarAreaFisicaFiltersQueryResponseHandler
        (
            IRepository<AreaFisica> repository
        )
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SelecionarAreaFisicaFiltersQueryResponse>> Handle
        (
            SelecionarAreaFisicaFiltersQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarAreaFisicaFiltersQuery>());

            IEnumerable<AreaFisica> areaFisicaMany = await _repository.GetAsync(cancellationToken);

            List<SelecionarAreaFisicaFiltersQueryResponse> responseMany = new List<SelecionarAreaFisicaFiltersQueryResponse>();

            foreach (AreaFisica areaFisica in areaFisicaMany)
            {
                SelecionarAreaFisicaFiltersQueryResponse response = new SelecionarAreaFisicaFiltersQueryResponse();
                response.Descricao = areaFisica.Descricao;
                response.Titulo = areaFisica.Titulo;
                response.Aplicacao = areaFisica.Aplicacao;

                response.DataCadastro = areaFisica.DataCadastro;
                response.DataAtualizacao = areaFisica.DataAtualizacao;
                response.Id = areaFisica.Id;
                responseMany.Add(response);
            }

            return responseMany;
        }
    }
}
