using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.WidgetCursandoFeature.Commands
{
    public class RemoverWidgetCursandoCommand : IRequest<long>
    {
        public long Id { get; set; }
    }

    public class RemoverWidgetCursandoCommandHandler : IRequestHandler<RemoverWidgetCursandoCommand, long>
    {
        private readonly IRepository<WidgetCursando> _repository;

        public RemoverWidgetCursandoCommandHandler
        (
            IRepository<WidgetCursando> repository
        )
        {
            _repository = repository;
        }

        public async Task<long> Handle
        (
            RemoverWidgetCursandoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverWidgetCursandoCommand>());

            if (!await ExistsAsync(request, cancellationToken))
            {
                return 0;
            }

            WidgetCursando widgetCursando = await _repository.GetFirstAsync(item => item.AulaId.Equals(request.Id), cancellationToken);

            await _repository.RemoveAsync(widgetCursando);
            await _repository.SaveChangesAsync(cancellationToken);

            return widgetCursando.AulaId;
        }

        private async Task<bool> ExistsAsync
        (
            RemoverWidgetCursandoCommand request,
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
