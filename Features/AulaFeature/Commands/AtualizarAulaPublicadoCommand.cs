using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaFeature.Commands
{
    public class AtualizarAulaPublicadoCommand : IRequest<AtualizarAulaPublicadoCommandResponse>
    {
        public long Id { get; set; }
        public bool Publicado { get; set; }
    }

    public class AtualizarAulaPublicadoCommandResponse
    {
        public DateTime DataAtualizacao { get; set; }
    }

    public class AtualizarAulaPublicadoHandler
        : IRequestHandler<AtualizarAulaPublicadoCommand, AtualizarAulaPublicadoCommandResponse>
    {
        private readonly IRepository<Aula> _repositoryAula;

        public AtualizarAulaPublicadoHandler
        (
            IRepository<Aula> repositoryAula
        )
        {
            _repositoryAula = repositoryAula;
        }

        public async Task<AtualizarAulaPublicadoCommandResponse> Handle
        (
            AtualizarAulaPublicadoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaPublicadoCommand>());

            await Validator(request, cancellationToken);

            Aula aula = await GetFirstAsync(request, cancellationToken);
            aula.Publicado = request.Publicado;

            await _repositoryAula.UpdateAsync(aula);
            await _repositoryAula.SaveChangesAsync(cancellationToken);

            AtualizarAulaPublicadoCommandResponse response = new AtualizarAulaPublicadoCommandResponse();
            response.DataAtualizacao = aula.DataAtualizacao;

            return response;
        }

        private async Task Validator
        (
            AtualizarAulaPublicadoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id <= 0) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaPublicadoCommand>(item => item.Id));
            if (!(await ExistsAsync(request, cancellationToken))) throw new ArgumentNullException("Aula não encontrada");
        }

        private async Task<Aula> GetFirstAsync
        (
            AtualizarAulaPublicadoCommand request,
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
            AtualizarAulaPublicadoCommand request,
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
