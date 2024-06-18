using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaFeature.Commands
{
    public class AtualizarAulaAnteriorCommand : IRequest<AtualizarAulaAnteriorCommandResponse>
    {
        public long Id { get; set; }
        public long AulaAnteriorId { get; set; }
    }

    public class AtualizarAulaAnteriorCommandResponse
    {
        public long Id { get; set; }
        public long AulaAnteriorId { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }

    public class AtualizarAulaAnteriorHandler :
        IRequestHandler<AtualizarAulaAnteriorCommand, AtualizarAulaAnteriorCommandResponse>
    {
        private readonly IRepository<Aula> _repositoryAula;

        public AtualizarAulaAnteriorHandler
        (
            IRepository<Aula> repositoryAula
        )
        {
            _repositoryAula = repositoryAula;
        }

        public async Task<AtualizarAulaAnteriorCommandResponse> Handle
        (
            AtualizarAulaAnteriorCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaAnteriorCommand>());

            await Validator(request, cancellationToken);

            Aula aula = await GetFirstAsync(request, cancellationToken);
            aula.AulaAnteriorId = request.AulaAnteriorId;

            await _repositoryAula.UpdateAsync(aula);
            await _repositoryAula.SaveChangesAsync(cancellationToken);

            AtualizarAulaAnteriorCommandResponse response = new AtualizarAulaAnteriorCommandResponse();
            response.DataAtualizacao = aula.DataAtualizacao;
            response.Id = request.Id;
            response.AulaAnteriorId = request.AulaAnteriorId;

            return response;
        }

        private async Task Validator
        (
            AtualizarAulaAnteriorCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id <= 0) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaAnteriorCommand>(item => item.Id));
            if (request.AulaAnteriorId < 0) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaAnteriorCommand>(item => item.AulaAnteriorId));
            if (!(await ExistsAsync(request, cancellationToken))) throw new ArgumentNullException("Aula não encontrada");
            if (request.AulaAnteriorId > 0)
                if (!(await ExistsAulaAnteriorAsync(request, cancellationToken))) throw new ArgumentNullException("Aula anterior não encontrada");
        }

        private async Task<Aula> GetFirstAsync
        (
            AtualizarAulaAnteriorCommand request,
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
            AtualizarAulaAnteriorCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAula.ExistsAsync
                (
                    item => item.Id.Equals(request.Id),
                    cancellationToken
                );
        }

        private async Task<bool> ExistsAulaAnteriorAsync
        (
            AtualizarAulaAnteriorCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAula.ExistsAsync
                (
                    item => item.Id.Equals(request.AulaAnteriorId),
                    cancellationToken
                );
        }
    }
}
