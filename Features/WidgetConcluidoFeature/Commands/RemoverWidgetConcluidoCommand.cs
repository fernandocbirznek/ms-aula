using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.WidgetConcluidoFeature.Commands
{
    public class RemoverWidgetConcluidoCommand : IRequest<RemoverWidgetConcluidoCommandResponse>
    {
        public long Id { get; set; }
    }

    public class RemoverWidgetConcluidoCommandResponse
    {
        public long Id { get; set; }
    }

    public class RemoverWidgetConcluidoCommandHandler : IRequestHandler<RemoverWidgetConcluidoCommand, RemoverWidgetConcluidoCommandResponse>
    {
        private readonly IRepository<WidgetConcluido> _repository;

        public RemoverWidgetConcluidoCommandHandler
        (
            IRepository<WidgetConcluido> repository
        )
        {
            _repository = repository;
        }

        public async Task<RemoverWidgetConcluidoCommandResponse> Handle
        (
            RemoverWidgetConcluidoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverWidgetConcluidoCommand>());

            await Validator(request, cancellationToken);

            WidgetConcluido widgetConcluido = await _repository.GetFirstAsync(item => item.Id.Equals(request.Id), cancellationToken);

            await _repository.RemoveAsync(widgetConcluido);
            await _repository.SaveChangesAsync(cancellationToken);

            RemoverWidgetConcluidoCommandResponse response = new RemoverWidgetConcluidoCommandResponse();
            response.Id = widgetConcluido.Id;

            return response;
        }

        private async Task Validator
        (
            RemoverWidgetConcluidoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (!await ExistsAsync(request, cancellationToken)) throw new ArgumentNullException("Widget aula concluido não encontrada");
        }

        private async Task<bool> ExistsAsync
        (
            RemoverWidgetConcluidoCommand request,
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
