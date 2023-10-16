using MediatR;
using ms_aula.Domains;
using ms_aula.Features.WidgetCursarFeature.Commands;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.WidgetCursandoFeature.Commands
{
    public class RemoverWidgetCursandoCommand : IRequest<RemoverWidgetCursandoCommandResponse>
    {
        public long Id { get; set; }
    }

    public class RemoverWidgetCursandoCommandResponse
    {
        public long Id { get; set; }
    }

    public class RemoverWidgetCursandoCommandHandler : IRequestHandler<RemoverWidgetCursandoCommand, RemoverWidgetCursandoCommandResponse>
    {
        private readonly IRepository<WidgetCursando> _repository;

        public RemoverWidgetCursandoCommandHandler
        (
            IRepository<WidgetCursando> repository
        )
        {
            _repository = repository;
        }

        public async Task<RemoverWidgetCursandoCommandResponse> Handle
        (
            RemoverWidgetCursandoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverWidgetCursandoCommand>());

            await Validator(request, cancellationToken);

            WidgetCursando widgetCursando = await _repository.GetFirstAsync(item => item.Id.Equals(request.Id), cancellationToken);

            await _repository.RemoveAsync(widgetCursando);
            await _repository.SaveChangesAsync(cancellationToken);

            RemoverWidgetCursandoCommandResponse response = new RemoverWidgetCursandoCommandResponse();
            response.Id = widgetCursando.Id;

            return response;
        }

        private async Task Validator
        (
            RemoverWidgetCursandoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (!await ExistsAsync(request, cancellationToken)) throw new ArgumentNullException("Widget aula cursando não encontrada");
        }

        private async Task<bool> ExistsAsync
        (
            RemoverWidgetCursandoCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.ExistsAsync
                (
                    item => item.Id.Equals(request.Id),
                    cancellationToken
                );
        }
    }
}
