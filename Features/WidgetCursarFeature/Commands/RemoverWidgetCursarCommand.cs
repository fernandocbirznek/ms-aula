using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.WidgetCursarFeature.Commands
{
    public class RemoverWidgetCursarCommand : IRequest<long>
    {
        public long Id { get; set; }
    }

    public class RemoverWidgetCursarCommandHandler : IRequestHandler<RemoverWidgetCursarCommand, long>
    {
        private readonly IRepository<WidgetCursar> _repository;

        public RemoverWidgetCursarCommandHandler
        (
            IRepository<WidgetCursar> repository
        )
        {
            _repository = repository;
        }

        public async Task<long> Handle
        (
            RemoverWidgetCursarCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverWidgetCursarCommand>());

            if (!await ExistsAsync(request, cancellationToken))
            {
                return 0;
            }

            WidgetCursar widgetCursar = await _repository.GetFirstAsync(item => item.AulaId.Equals(request.Id), cancellationToken);

            await _repository.RemoveAsync(widgetCursar);
            await _repository.SaveChangesAsync(cancellationToken);

            return widgetCursar.AulaId;
        }

        private async Task<bool> ExistsAsync
        (
            RemoverWidgetCursarCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.ExistsAsync
                (
                    item => item.AulaId.Equals(request.Id),
                    cancellationToken
                );
        }
    }
}
