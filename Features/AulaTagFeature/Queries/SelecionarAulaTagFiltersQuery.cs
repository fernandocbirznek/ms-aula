using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaTagFeature.Queries
{
    public class SelecionarAulaTagFiltersQuery : IRequest<IEnumerable<SelecionarAulaTagFiltersQueryResponse>>
    {
    }

    public class SelecionarAulaTagFiltersQueryResponse : Entity
    {
        public long AulaId { get; set; }
        public long TagId { get; set; }
    }

    public class SelecionarAulaTagFiltersQueryResponseHandler :
        IRequestHandler<SelecionarAulaTagFiltersQuery, IEnumerable<SelecionarAulaTagFiltersQueryResponse>>
    {
        private readonly IRepository<AulaTag> _repository;

        public SelecionarAulaTagFiltersQueryResponseHandler
        (
            IRepository<AulaTag> repository
        )
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SelecionarAulaTagFiltersQueryResponse>> Handle
        (
            SelecionarAulaTagFiltersQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarAulaTagFiltersQuery>());

            IEnumerable<AulaTag> conquistasMany = await _repository.GetAsync(cancellationToken);

            List<SelecionarAulaTagFiltersQueryResponse> responseMany = new List<SelecionarAulaTagFiltersQueryResponse>();

            foreach (AulaTag conquistas in conquistasMany)
            {
                SelecionarAulaTagFiltersQueryResponse response = new SelecionarAulaTagFiltersQueryResponse();
                response.AulaId = conquistas.AulaId;
                response.TagId = conquistas.TagId;
                response.DataCadastro = conquistas.DataCadastro;
                response.DataAtualizacao = conquistas.DataAtualizacao;
                response.Id = conquistas.Id;
                responseMany.Add(response);
            }

            return responseMany;
        }
    }
}
