using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaSessaoFavoritadaFeature.Commands
{
    public class RemoverAulaSessaoFavoritadaCommand : IRequest<long>
    {
        public long UsuarioId { get; set; }
        public long AulaSessaoId { get; set; }
    }

    public class RemoverAulaSessaoFavoritadaCommandHandler : IRequestHandler<RemoverAulaSessaoFavoritadaCommand, long>
    {
        private readonly IRepository<AulaSessaoFavoritada> _repository;

        public RemoverAulaSessaoFavoritadaCommandHandler
        (
            IRepository<AulaSessaoFavoritada> repository
        )
        {
            _repository = repository;
        }

        public async Task<long> Handle
        (
            RemoverAulaSessaoFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverAulaSessaoFavoritadaCommand>());

            await Validator(request, cancellationToken);

            AulaSessaoFavoritada aulaSessaoFavoritada = await _repository.GetFirstAsync
                (
                    item => item.UsuarioId.Equals(request.UsuarioId) && item.AulaSessaoId.Equals(request.AulaSessaoId), 
                    cancellationToken
                );

            await _repository.RemoveAsync(aulaSessaoFavoritada);
            await _repository.SaveChangesAsync(cancellationToken);

            return aulaSessaoFavoritada.Id;
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
                    item => item.UsuarioId.Equals(request.UsuarioId) && item.AulaSessaoId.Equals(request.AulaSessaoId),
                    cancellationToken
                );
        }
    }
}
