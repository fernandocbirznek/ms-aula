using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaFavoritadaFeature.Commands
{
    public class RemoverAulaFavoritadaCommand : IRequest<RemoverAulaFavoritadaCommandResponse>
    {
        public long Id { get; set; }
    }

    public class RemoverAulaFavoritadaCommandResponse
    {
        public long Id { get; set; }
    }

    public class RemoverAulaFavoritadaCommandHandler : IRequestHandler<RemoverAulaFavoritadaCommand, RemoverAulaFavoritadaCommandResponse>
    {
        private readonly IRepository<AulaFavoritada> _repository;

        public RemoverAulaFavoritadaCommandHandler
        (
            IRepository<AulaFavoritada> repository
        )
        {
            _repository = repository;
        }

        public async Task<RemoverAulaFavoritadaCommandResponse> Handle
        (
            RemoverAulaFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverAulaFavoritadaCommand>());

            await Validator(request, cancellationToken);

            AulaFavoritada aula = await _repository.GetFirstAsync(item => item.Id.Equals(request.Id), cancellationToken);

            await _repository.RemoveAsync(aula);
            await _repository.SaveChangesAsync(cancellationToken);

            RemoverAulaFavoritadaCommandResponse response = new RemoverAulaFavoritadaCommandResponse();
            response.Id = aula.Id;

            return response;
        }

        private async Task Validator
        (
            RemoverAulaFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (!await ExistsAsync(request, cancellationToken)) throw new ArgumentNullException("Aula favoritada não encontrada");
        }

        private async Task<bool> ExistsAsync
        (
            RemoverAulaFavoritadaCommand request,
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
