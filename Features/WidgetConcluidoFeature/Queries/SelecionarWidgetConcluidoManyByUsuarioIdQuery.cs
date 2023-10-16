using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.WidgetConcluidoFeature.Queries
{
    public class SelecionarWidgetConcluidoManyByUsuarioIdQuery : IRequest<IEnumerable<SelecionarWidgetConcluidoManyByUsuarioIdQueryResponse>>
    {
        public long Id { get; set; }
    }

    public class SelecionarWidgetConcluidoManyByUsuarioIdQueryResponse : Entity
    {
        public Aula? Aula { get; set; }
    }

    public class SelecionarWidgetConcluidoManyByUsuarioIdQueryHandler : IRequestHandler<SelecionarWidgetConcluidoManyByUsuarioIdQuery, IEnumerable<SelecionarWidgetConcluidoManyByUsuarioIdQueryResponse>>
    {
        private readonly IRepository<WidgetConcluido> _repository;

        public SelecionarWidgetConcluidoManyByUsuarioIdQueryHandler
        (
            IRepository<WidgetConcluido> repository
        )
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SelecionarWidgetConcluidoManyByUsuarioIdQueryResponse>> Handle
        (
            SelecionarWidgetConcluidoManyByUsuarioIdQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarWidgetConcluidoManyByUsuarioIdQuery>());

            IEnumerable<WidgetConcluido> widgetConcluidoMany = await GetAsync(request, cancellationToken);

            List<SelecionarWidgetConcluidoManyByUsuarioIdQueryResponse> responseMany = new List<SelecionarWidgetConcluidoManyByUsuarioIdQueryResponse>();

            foreach (WidgetConcluido widgetConcluido in widgetConcluidoMany)
            {
                SelecionarWidgetConcluidoManyByUsuarioIdQueryResponse response = new SelecionarWidgetConcluidoManyByUsuarioIdQueryResponse();
                response.Aula = widgetConcluido.Aula;
                response.DataCadastro = widgetConcluido.DataCadastro;
                response.DataAtualizacao = widgetConcluido.DataAtualizacao;
                response.Id = widgetConcluido.Id;
                responseMany.Add(response);
            }

            return responseMany;
        }

        private async Task<IEnumerable<WidgetConcluido>> GetAsync
        (
            SelecionarWidgetConcluidoManyByUsuarioIdQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAsync
                (
                    item => item.UsuarioId.Equals(request.Id),
                    cancellationToken,
                    item => item.Aula
                );
        }
    }
}
