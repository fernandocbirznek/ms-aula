using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaComentarioFeature.Commands
{
    public class AtualizarAulaComentarioCommand : IRequest<AtualizarAulaComentarioCommandResponse>
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public long UsuarioId { get; set; }
    }

    public class AtualizarAulaComentarioCommandResponse
    {
        public DateTime DataAtualizacao { get; set; }
    }

    public class AtualizarAulaComentarioHandler : IRequestHandler<AtualizarAulaComentarioCommand, AtualizarAulaComentarioCommandResponse>
    {
        private readonly IRepository<AulaComentario> _repositoryAulaComentario;
        private readonly IRepository<Aula> _repositoryAula;

        public AtualizarAulaComentarioHandler
        (
            IRepository<AulaComentario> repositoryAulaComentario,
            IRepository<Aula> repositoryAula
        )
        {
            _repositoryAulaComentario = repositoryAulaComentario;
            _repositoryAula = repositoryAula;
        }

        public async Task<AtualizarAulaComentarioCommandResponse> Handle
        (
            AtualizarAulaComentarioCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaComentarioCommand>());

            await Validator(request, cancellationToken);

            AulaComentario aulaComentario = await GetFirstAsync(request, cancellationToken);
            aulaComentario.Descricao = request.Descricao;

            await _repositoryAulaComentario.UpdateAsync(aulaComentario);
            await _repositoryAulaComentario.SaveChangesAsync(cancellationToken);

            AtualizarAulaComentarioCommandResponse response = new AtualizarAulaComentarioCommandResponse();
            response.DataAtualizacao = aulaComentario.DataAtualizacao;

            return response;
        }

        private async Task Validator
        (
            AtualizarAulaComentarioCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id <= 0) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaComentarioCommand>(item => item.Id));
            if (String.IsNullOrEmpty(request.Descricao)) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaComentarioCommand>(item => item.Descricao));
            if (request.UsuarioId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaComentarioCommand>(item => item.UsuarioId));
        }

        private async Task<AulaComentario> GetFirstAsync
        (
            AtualizarAulaComentarioCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAulaComentario.GetFirstAsync
                (
                    item => item.Id.Equals(request.Id),
                    cancellationToken
                );
        }
    }
}
