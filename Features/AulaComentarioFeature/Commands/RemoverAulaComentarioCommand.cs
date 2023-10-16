using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaComentarioFeature.Commands
{
    public class RemoverAulaComentarioCommand : IRequest<RemoverAulaComentarioCommandResponse>
    {
        public long Id { get; set; }
    }

    public class RemoverAulaComentarioCommandResponse
    {
        public long Id { get; set; }
    }

    public class RemoverAulaComentarioCommandHandler : IRequestHandler<RemoverAulaComentarioCommand, RemoverAulaComentarioCommandResponse>
    {
        private readonly IRepository<AulaComentario> _repository;

        public RemoverAulaComentarioCommandHandler
        (
            IRepository<AulaComentario> repository
        )
        {
            _repository = repository;
        }

        public async Task<RemoverAulaComentarioCommandResponse> Handle
        (
            RemoverAulaComentarioCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverAulaComentarioCommand>());

            await Validator(request, cancellationToken);

            AulaComentario aula = await _repository.GetFirstAsync(item => item.Id.Equals(request.Id), cancellationToken);

            await _repository.RemoveAsync(aula);
            await _repository.SaveChangesAsync(cancellationToken);

            RemoverAulaComentarioCommandResponse response = new RemoverAulaComentarioCommandResponse();
            response.Id = aula.Id;

            return response;
        }

        private async Task Validator
        (
            RemoverAulaComentarioCommand request,
            CancellationToken cancellationToken
        )
        {
            if (!(await ExistsAsync(request, cancellationToken))) throw new ArgumentNullException("Aula comentário não encontrada");
        }

        private async Task<bool> ExistsAsync
        (
            RemoverAulaComentarioCommand request,
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
