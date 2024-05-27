using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaFeature.Commands
{
    public class AtualizarAulaFavoritadaCommand : IRequest<AtualizarAulaFavoritadaCommandResponse>
    {
        public long Id { get; set; }
        public bool Adicionar { get; set; }
    }

    public class AtualizarAulaFavoritadaCommandResponse
    {
        public DateTime DataAtualizacao { get; set; }
    }

    public class AtualizarAulaFavoritadaHandler 
        : IRequestHandler<AtualizarAulaFavoritadaCommand, AtualizarAulaFavoritadaCommandResponse>
    {
        private readonly IRepository<Aula> _repositoryAula;

        public AtualizarAulaFavoritadaHandler
        (
            IRepository<Aula> repositoryAula
        )
        {
            _repositoryAula = repositoryAula;
        }

        public async Task<AtualizarAulaFavoritadaCommandResponse> Handle
        (
            AtualizarAulaFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaFavoritadaCommand>());

            await Validator(request, cancellationToken);

            Aula aula = await GetFirstAsync(request, cancellationToken);
            aula.Favoritado = request.Adicionar ? aula.Favoritado + 1 : aula.Favoritado - 1;

            await _repositoryAula.UpdateAsync(aula);
            await _repositoryAula.SaveChangesAsync(cancellationToken);

            AtualizarAulaFavoritadaCommandResponse response = new AtualizarAulaFavoritadaCommandResponse();
            response.DataAtualizacao = aula.DataAtualizacao;

            return response;
        }

        private async Task Validator
        (
            AtualizarAulaFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id <= 0) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaFavoritadaCommand>(item => item.Id));
            if (!(await ExistsAsync(request, cancellationToken))) throw new ArgumentNullException("Aula não encontrada");
        }

        private async Task<Aula> GetFirstAsync
        (
            AtualizarAulaFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAula.GetFirstAsync
                (
                    item => item.Id.Equals(request.Id),
                    cancellationToken
                );
        }

        private async Task<bool> ExistsAsync
        (
            AtualizarAulaFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAula.ExistsAsync
                (
                    item => item.Id.Equals(request.Id),
                    cancellationToken
                );
        }
    }
}
