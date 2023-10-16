using MediatR;
using ms_aula.Domains;
using ms_aula.Extensions;
using ms_aula.Features.AulaFeature.Commands;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaComentarioFeature.Commands
{
    public class InserirAulaComentarioCommand : IRequest<InserirAulaComentarioCommandResponse>
    {
        public string Descricao { get; set; }
        public long UsuarioId { get; set; }
        public long AulaId { get; set; }
    }

    public class InserirAulaComentarioCommandResponse
    {
        public long Id { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public class InserirAulaComentarioHandler : IRequestHandler<InserirAulaComentarioCommand, InserirAulaComentarioCommandResponse>
    {
        private readonly IRepository<AulaComentario> _repositoryAulaComentario;
        private readonly IRepository<Aula> _repositoryAula;

        public InserirAulaComentarioHandler
        (
            IRepository<AulaComentario> repositoryAulaComentario,
            IRepository<Aula> repositoryAula
        )
        {
            _repositoryAulaComentario = repositoryAulaComentario;
            _repositoryAula = repositoryAula;
        }

        public async Task<InserirAulaComentarioCommandResponse> Handle
        (
            InserirAulaComentarioCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaComentarioCommand>());

            await Validator(request, cancellationToken);

            AulaComentario aula = request.ToDomain();

            await _repositoryAulaComentario.AddAsync(aula, cancellationToken);
            await _repositoryAulaComentario.SaveChangesAsync(cancellationToken);

            InserirAulaComentarioCommandResponse response = new InserirAulaComentarioCommandResponse();
            response.DataCadastro = aula.DataCadastro;
            response.Id = aula.Id;

            return response;
        }

        private async Task Validator
        (
            InserirAulaComentarioCommand request,
            CancellationToken cancellationToken
        )
        {
            if (String.IsNullOrEmpty(request.Descricao)) throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaComentarioCommand>(item => item.Descricao));
            if (request.AulaId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaCommand>(item => item.ProfessorId));
            if (request.UsuarioId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaCommand>(item => item.AreaFisicaId));
            if (!await ExistsAulaAsync(request, cancellationToken)) throw new ArgumentNullException("A aula não existe");
        }

        private async Task<bool> ExistsAulaAsync
        (
            InserirAulaComentarioCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAula.ExistsAsync
                (
                    item => item.Id.Equals(request.AulaId),
                    cancellationToken
                );
        }
    }
}
