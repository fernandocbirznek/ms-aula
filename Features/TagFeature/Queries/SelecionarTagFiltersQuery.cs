using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.TagFeature.Queries
{
    public class SelecionarTagFiltersQuery : IRequest<IEnumerable<SelecionarTagFiltersQueryResponse>>
    {
    }

    public class SelecionarTagFiltersQueryResponse : Entity
    {
        public string Nome { get; set; }
    }

    public class SelecionarTagFiltersQueryResponseHandler : IRequestHandler<SelecionarTagFiltersQuery, IEnumerable<SelecionarTagFiltersQueryResponse>>
    {
        private readonly IRepository<Tag> _repository;

        public SelecionarTagFiltersQueryResponseHandler
        (
            IRepository<Tag> repository
        )
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SelecionarTagFiltersQueryResponse>> Handle
        (
            SelecionarTagFiltersQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarTagFiltersQuery>());

            IEnumerable<Tag> tagMany = await _repository.GetAsync(cancellationToken);

            List<SelecionarTagFiltersQueryResponse> responseMany = new List<SelecionarTagFiltersQueryResponse>();

            foreach (Tag tag in tagMany)
            {
                SelecionarTagFiltersQueryResponse response = new SelecionarTagFiltersQueryResponse();
                response.Nome = tag.Nome;
                response.DataCadastro = tag.DataCadastro;
                response.DataAtualizacao = tag.DataAtualizacao;
                response.Id = tag.Id;
                responseMany.Add(response);
            }

            return responseMany;
        }
    }
}
