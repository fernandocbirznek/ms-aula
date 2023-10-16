using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaFeature.Commands
{
    public class RemoverAulaCommand : IRequest<RemoverAulaCommandResponse>
    {
        public long Id { get; set; }
    }

    public class RemoverAulaCommandResponse
    {
        public long Id { get; set; }
    }

    public class RemoverAulaCommandHandler : IRequestHandler<RemoverAulaCommand, RemoverAulaCommandResponse>
    {
        private readonly IRepository<Aula> _repository;

        public RemoverAulaCommandHandler
        (
            IRepository<Aula> repository
        )
        {
            _repository = repository;
        }

        public async Task<RemoverAulaCommandResponse> Handle
        (
            RemoverAulaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverAulaCommand>());

            await Validator(request, cancellationToken);

            Aula aula = await _repository.GetFirstAsync(item => item.Id.Equals(request.Id), cancellationToken);

            await _repository.RemoveAsync(aula);
            await _repository.SaveChangesAsync(cancellationToken);

            RemoverAulaCommandResponse response = new RemoverAulaCommandResponse();
            response.Id = aula.Id;

            return response;
        }

        private async Task Validator
        (
            RemoverAulaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (!(await ExistsAsync(request, cancellationToken))) throw new ArgumentNullException("Aula não encontrada");
        }

        private async Task<bool> ExistsAsync
        (
            RemoverAulaCommand request,
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
