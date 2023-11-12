using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaFeature.Commands
{
    public class AtualizarAulaCommand : IRequest<AtualizarAulaCommandResponse>
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public long AreaFisicaId { get; set; }
    }

    public class AtualizarAulaCommandResponse
    {
        public DateTime DataAtualizacao { get; set; }
    }

    public class AtualizarAulaHandler : IRequestHandler<AtualizarAulaCommand, AtualizarAulaCommandResponse>
    {
        private readonly IRepository<Aula> _repositoryAula;

        public AtualizarAulaHandler
        (
            IRepository<Aula> repositoryAula
        )
        {
            _repositoryAula = repositoryAula;
        }

        public async Task<AtualizarAulaCommandResponse> Handle
        (
            AtualizarAulaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaCommand>());

            await Validator(request, cancellationToken);

            Aula aula = await GetFirstAsync(request, cancellationToken);
            aula.Titulo = request.Titulo;
            aula.Resumo = request.Resumo;
            aula.AreaFisicaId = request.AreaFisicaId;

            await _repositoryAula.UpdateAsync(aula);
            await _repositoryAula.SaveChangesAsync(cancellationToken);

            AtualizarAulaCommandResponse response = new AtualizarAulaCommandResponse();
            response.DataAtualizacao = aula.DataAtualizacao;

            return response;
        }

        private async Task Validator
        (
            AtualizarAulaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id <= 0) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaCommand>(item => item.Id));
            if (String.IsNullOrEmpty(request.Titulo)) throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaCommand>(item => item.Titulo));
            if (!(await ExistsAsync(request, cancellationToken))) throw new ArgumentNullException("Área da Física não encontrada");
            if (request.AreaFisicaId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaCommand>(item => item.AreaFisicaId));
            if (await ExistsTituloAsync(request, cancellationToken)) throw new ArgumentNullException("Título já cadastrado");
        }

        private async Task<Aula> GetFirstAsync
        (
            AtualizarAulaCommand request,
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
            AtualizarAulaCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAula.ExistsAsync
                (
                    item => item.Id.Equals(request.Id),
                    cancellationToken
                );
        }

        private async Task<bool> ExistsTituloAsync
        (
            AtualizarAulaCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAula.ExistsAsync
                (
                    item => item.Titulo.ToLower().Trim().Equals(request.Titulo.ToLower().Trim()) &&
                    !item.Id.Equals(request.Id),
                    cancellationToken
                );
        }
    }
}
