using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.WidgetCursandoFeature.Queries
{
    public class SelecionarWidgetCursandoManyByUsuarioIdQuery : IRequest<IEnumerable<SelecionarWidgetCursandoManyByUsuarioIdQueryResponse>>
    {
        public long Id { get; set; }
    }

    public class SelecionarWidgetCursandoManyByUsuarioIdQueryResponse : Entity
    {
        public Aula? Aula { get; set; }
    }

    public class SelecionarWidgetCursandoManyByUsuarioIdQueryHandler : IRequestHandler<SelecionarWidgetCursandoManyByUsuarioIdQuery, IEnumerable<SelecionarWidgetCursandoManyByUsuarioIdQueryResponse>>
    {
        private readonly IRepository<WidgetCursando> _repository;

        public SelecionarWidgetCursandoManyByUsuarioIdQueryHandler
        (
            IRepository<WidgetCursando> repository
        )
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SelecionarWidgetCursandoManyByUsuarioIdQueryResponse>> Handle
        (
            SelecionarWidgetCursandoManyByUsuarioIdQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarWidgetCursandoManyByUsuarioIdQuery>());

            IEnumerable<WidgetCursando> widgetCursandoMany = await GetAsync(request, cancellationToken);

            List<SelecionarWidgetCursandoManyByUsuarioIdQueryResponse> responseMany = new List<SelecionarWidgetCursandoManyByUsuarioIdQueryResponse>();

            foreach (WidgetCursando widgetCursando in widgetCursandoMany)
            {
                SelecionarWidgetCursandoManyByUsuarioIdQueryResponse response = new SelecionarWidgetCursandoManyByUsuarioIdQueryResponse();
                response.Aula = widgetCursando.Aula;
                response.DataCadastro = widgetCursando.DataCadastro;
                response.DataAtualizacao = widgetCursando.DataAtualizacao;
                response.Id = widgetCursando.Id;
                responseMany.Add(response);
            }

            return responseMany;
        }

        private async Task<IEnumerable<WidgetCursando>> GetAsync
        (
            SelecionarWidgetCursandoManyByUsuarioIdQuery request,
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
