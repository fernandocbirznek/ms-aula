using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaFeature.Commands
{
    public class AtualizarAulaCurtirCommand : IRequest<AtualizarAulaCurtirCommandResponse>
    {
        public long Id { get; set; }
        public bool Adicionar { get; set; }
    }

    public class AtualizarAulaCurtirCommandResponse
    {
        public DateTime DataAtualizacao { get; set; }
    }

    public class AtualizarAulaCurtirHandler : IRequestHandler<AtualizarAulaCurtirCommand, AtualizarAulaCurtirCommandResponse>
    {
        private readonly IRepository<Aula> _repositoryAula;

        public AtualizarAulaCurtirHandler
        (
            IRepository<Aula> repositoryAula
        )
        {
            _repositoryAula = repositoryAula;
        }

        public async Task<AtualizarAulaCurtirCommandResponse> Handle
        (
            AtualizarAulaCurtirCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaCurtirCommand>());

            await Validator(request, cancellationToken);

            Aula aula = await GetFirstAsync(request, cancellationToken);
            aula.Curtido = request.Adicionar ? aula.Curtido + 1 : aula.Curtido - 1;

            await _repositoryAula.UpdateAsync(aula);
            await _repositoryAula.SaveChangesAsync(cancellationToken);

            AtualizarAulaCurtirCommandResponse response = new AtualizarAulaCurtirCommandResponse();
            response.DataAtualizacao = aula.DataAtualizacao;

            return response;
        }

        private async Task Validator
        (
            AtualizarAulaCurtirCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id <= 0) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaCurtirCommand>(item => item.Id));
            if (!(await ExistsAsync(request, cancellationToken))) throw new ArgumentNullException("Aula não encontrada");
        }

        private async Task<Aula> GetFirstAsync
        (
            AtualizarAulaCurtirCommand request,
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
            AtualizarAulaCurtirCommand request,
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
