using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.WidgetConcluidoFeature.Commands
{
    public class RemoverWidgetConcluidoCommand : IRequest<long>
    {
        public long Id { get; set; }
    }

    public class RemoverWidgetConcluidoCommandHandler : IRequestHandler<RemoverWidgetConcluidoCommand, long>
    {
        private readonly IRepository<WidgetConcluido> _repository;

        public RemoverWidgetConcluidoCommandHandler
        (
            IRepository<WidgetConcluido> repository
        )
        {
            _repository = repository;
        }

        public async Task<long> Handle
        (
            RemoverWidgetConcluidoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverWidgetConcluidoCommand>());


            if (!await ExistsAsync(request, cancellationToken))
            {
                return 0;
            }

            WidgetConcluido widgetConcluido = await _repository.GetFirstAsync(item => item.AulaId.Equals(request.Id), cancellationToken);

            await _repository.RemoveAsync(widgetConcluido);
            await _repository.SaveChangesAsync(cancellationToken);

            return widgetConcluido.AulaId;
        }

        private async Task<bool> ExistsAsync
        (
            RemoverWidgetConcluidoCommand request,
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
