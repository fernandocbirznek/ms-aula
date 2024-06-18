using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaFeature.Commands
{
    public class AtualizarAulaPosteriorCommand : IRequest<AtualizarAulaPosteriorCommandResponse>
    {
        public long Id { get; set; }
        public long AulaPosteriorId { get; set; }
    }

    public class AtualizarAulaPosteriorCommandResponse
    {
        public long Id { get; set; }
        public long AulaPosteriorId { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }

    public class AtualizarAulaPosteriorHandler :
        IRequestHandler<AtualizarAulaPosteriorCommand, AtualizarAulaPosteriorCommandResponse>
    {
        private readonly IRepository<Aula> _repositoryAula;

        public AtualizarAulaPosteriorHandler
        (
            IRepository<Aula> repositoryAula
        )
        {
            _repositoryAula = repositoryAula;
        }

        public async Task<AtualizarAulaPosteriorCommandResponse> Handle
        (
            AtualizarAulaPosteriorCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaPosteriorCommand>());

            await Validator(request, cancellationToken);

            Aula aula = await GetFirstAsync(request, cancellationToken);
            aula.AulaPosteriorId = request.AulaPosteriorId;

            await _repositoryAula.UpdateAsync(aula);
            await _repositoryAula.SaveChangesAsync(cancellationToken);

            AtualizarAulaPosteriorCommandResponse response = new AtualizarAulaPosteriorCommandResponse();
            response.DataAtualizacao = aula.DataAtualizacao;
            response.Id = request.Id;
            response.AulaPosteriorId = request.AulaPosteriorId;

            return response;
        }

        private async Task Validator
        (
            AtualizarAulaPosteriorCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id <= 0) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaPosteriorCommand>(item => item.Id));
            if (request.AulaPosteriorId < 0) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaPosteriorCommand>(item => item.AulaPosteriorId));
            if (!(await ExistsAsync(request, cancellationToken))) throw new ArgumentNullException("Aula não encontrada");
            if (request.AulaPosteriorId > 0)
                if (!(await ExistsAulaPosteriorAsync(request, cancellationToken))) throw new ArgumentNullException("Aula posterior não encontrada");
        }

        private async Task<Aula> GetFirstAsync
        (
            AtualizarAulaPosteriorCommand request,
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
            AtualizarAulaPosteriorCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAula.ExistsAsync
                (
                    item => item.Id.Equals(request.Id),
                    cancellationToken
                );
        }

        private async Task<bool> ExistsAulaPosteriorAsync
        (
            AtualizarAulaPosteriorCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAula.ExistsAsync
                (
                    item => item.Id.Equals(request.AulaPosteriorId),
                    cancellationToken
                );
        }
    }
}
