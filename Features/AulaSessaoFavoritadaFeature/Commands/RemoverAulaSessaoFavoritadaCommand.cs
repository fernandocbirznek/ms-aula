using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaSessaoFavoritadaFeature.Commands
{
    public class RemoverAulaSessaoFavoritadaCommand : IRequest<RemoverAulaSessaoFavoritadaCommandResponse>
    {
        public long Id { get; set; }
    }

    public class RemoverAulaSessaoFavoritadaCommandResponse
    {
        public long Id { get; set; }
    }

    public class RemoverAulaSessaoFavoritadaCommandHandler : IRequestHandler<RemoverAulaSessaoFavoritadaCommand, RemoverAulaSessaoFavoritadaCommandResponse>
    {
        private readonly IRepository<AulaSessaoFavoritada> _repository;

        public RemoverAulaSessaoFavoritadaCommandHandler
        (
            IRepository<AulaSessaoFavoritada> repository
        )
        {
            _repository = repository;
        }

        public async Task<RemoverAulaSessaoFavoritadaCommandResponse> Handle
        (
            RemoverAulaSessaoFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverAulaSessaoFavoritadaCommand>());

            await Validator(request, cancellationToken);

            AulaSessaoFavoritada aulaSessaoFavoritada = await _repository.GetFirstAsync(item => item.Id.Equals(request.Id), cancellationToken);

            await _repository.RemoveAsync(aulaSessaoFavoritada);
            await _repository.SaveChangesAsync(cancellationToken);

            RemoverAulaSessaoFavoritadaCommandResponse response = new RemoverAulaSessaoFavoritadaCommandResponse();
            response.Id = aulaSessaoFavoritada.Id;

            return response;
        }

        private async Task Validator
        (
            RemoverAulaSessaoFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (!(await ExistsAsync(request, cancellationToken))) throw new ArgumentNullException("Aula sessão favoritada não encontrada");
        }

        private async Task<bool> ExistsAsync
        (
            RemoverAulaSessaoFavoritadaCommand request,
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
