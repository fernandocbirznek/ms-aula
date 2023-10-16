using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.WidgetCursarFeature.Commands
{
    public class RemoverWidgetCursarCommand : IRequest<RemoverWidgetCursarCommandResponse>
    {
        public long Id { get; set; }
    }

    public class RemoverWidgetCursarCommandResponse
    {
        public long Id { get; set; }
    }

    public class RemoverWidgetCursarCommandHandler : IRequestHandler<RemoverWidgetCursarCommand, RemoverWidgetCursarCommandResponse>
    {
        private readonly IRepository<WidgetCursar> _repository;

        public RemoverWidgetCursarCommandHandler
        (
            IRepository<WidgetCursar> repository
        )
        {
            _repository = repository;
        }

        public async Task<RemoverWidgetCursarCommandResponse> Handle
        (
            RemoverWidgetCursarCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverWidgetCursarCommand>());

            await Validator(request, cancellationToken);

            WidgetCursar widgetCursar = await _repository.GetFirstAsync(item => item.Id.Equals(request.Id), cancellationToken);

            await _repository.RemoveAsync(widgetCursar);
            await _repository.SaveChangesAsync(cancellationToken);

            RemoverWidgetCursarCommandResponse response = new RemoverWidgetCursarCommandResponse();
            response.Id = widgetCursar.Id;

            return response;
        }

        private async Task Validator
        (
            RemoverWidgetCursarCommand request,
            CancellationToken cancellationToken
        )
        {
            if (!await ExistsAsync(request, cancellationToken)) throw new ArgumentNullException("Aula não encontrada");
        }

        private async Task<bool> ExistsAsync
        (
            RemoverWidgetCursarCommand request,
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
