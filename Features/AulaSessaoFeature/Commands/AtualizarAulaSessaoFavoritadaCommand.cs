using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaSessaoFeature.Commands
{
    public class AtualizarAulaSessaoFavoritadaCommand : IRequest<AtualizarAulaSessaoFavoritadaCommandResponse>
    {
        public long Id { get; set; }
        public long Favoritado { get; set; }
    }

    public class AtualizarAulaSessaoFavoritadaCommandResponse
    {
        public DateTime DataAtualizacao { get; set; }
    }

    public class AtualizarAulaSessaoFavoritadaHandler : IRequestHandler<AtualizarAulaSessaoFavoritadaCommand, AtualizarAulaSessaoFavoritadaCommandResponse>
    {
        private readonly IRepository<AulaSessao> _repository;

        public AtualizarAulaSessaoFavoritadaHandler
        (
            IRepository<AulaSessao> repository
        )
        {
            _repository = repository;
        }

        public async Task<AtualizarAulaSessaoFavoritadaCommandResponse> Handle
        (
            AtualizarAulaSessaoFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaSessaoFavoritadaCommand>());

            await Validator(request, cancellationToken);

            AulaSessao aulaSessao = await GetFirstAsync(request, cancellationToken);
            aulaSessao.Favoritado = request.Favoritado;

            await _repository.UpdateAsync(aulaSessao);
            await _repository.SaveChangesAsync(cancellationToken);

            AtualizarAulaSessaoFavoritadaCommandResponse response = new AtualizarAulaSessaoFavoritadaCommandResponse();
            response.DataAtualizacao = aulaSessao.DataAtualizacao;

            return response;
        }

        private async Task Validator
        (
            AtualizarAulaSessaoFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id <= 0) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaSessaoFavoritadaCommand>(item => item.Id));
            if (request.Favoritado < 0) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaSessaoFavoritadaCommand>(item => item.Favoritado));
        }

        private async Task<AulaSessao> GetFirstAsync
        (
            AtualizarAulaSessaoFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetFirstAsync
                (
                    item => item.Id.Equals(request.Id),
                    cancellationToken
                );
        }
    }
}
