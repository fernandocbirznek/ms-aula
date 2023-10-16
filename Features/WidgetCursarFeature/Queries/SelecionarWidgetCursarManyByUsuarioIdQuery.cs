using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.WidgetCursarFeature.Queries
{
    public class SelecionarWidgetCursarManyByUsuarioIdQuery : IRequest<IEnumerable<SelecionarWidgetCursarManyByUsuarioIdQueryResponse>>
    {
        public long Id { get; set; }
    }

    public class SelecionarWidgetCursarManyByUsuarioIdQueryResponse : Entity
    {
        public Aula? Aula { get; set; }
    }

    public class SelecionarWidgetCursarManyByUsuarioIdQueryHandler : IRequestHandler<SelecionarWidgetCursarManyByUsuarioIdQuery, IEnumerable<SelecionarWidgetCursarManyByUsuarioIdQueryResponse>>
    {
        private readonly IRepository<WidgetCursar> _repository;

        public SelecionarWidgetCursarManyByUsuarioIdQueryHandler
        (
            IRepository<WidgetCursar> repository
        )
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SelecionarWidgetCursarManyByUsuarioIdQueryResponse>> Handle
        (
            SelecionarWidgetCursarManyByUsuarioIdQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarWidgetCursarManyByUsuarioIdQuery>());

            IEnumerable<WidgetCursar> widgetCursarMany = await GetAsync(request, cancellationToken);

            List<SelecionarWidgetCursarManyByUsuarioIdQueryResponse> responseMany = new List<SelecionarWidgetCursarManyByUsuarioIdQueryResponse>();

            foreach (WidgetCursar widgetCursar in widgetCursarMany)
            {
                SelecionarWidgetCursarManyByUsuarioIdQueryResponse response = new SelecionarWidgetCursarManyByUsuarioIdQueryResponse();
                response.Aula = widgetCursar.Aula;
                response.DataCadastro = widgetCursar.DataCadastro;
                response.DataAtualizacao = widgetCursar.DataAtualizacao;
                response.Id = widgetCursar.Id;
                responseMany.Add(response);
            }

            return responseMany;
        }

        private async Task<IEnumerable<WidgetCursar>> GetAsync
        (
            SelecionarWidgetCursarManyByUsuarioIdQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAsync
                (
                    item => item.Id.Equals(request.Id),
                    cancellationToken,
                    item => item.Aula
                );
        }
    }
}
