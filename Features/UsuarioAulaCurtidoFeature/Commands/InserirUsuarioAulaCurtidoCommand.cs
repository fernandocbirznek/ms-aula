using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;
using ms_aula;
using ms_aula.Extensions;
using ms_aula.Features.AulaFeature.Commands;

namespace ms_aula.Features.UsuarioAulaCurtidoFeature.Commands
{
    public class InserirUsuarioAulaCurtidoCommand : IRequest<InserirUsuarioAulaCurtidoCommandResponse>
    {
        public long UsuarioId { get; set; }
        public long AulaId { get; set; }
    }

    public class InserirUsuarioAulaCurtidoCommandResponse
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public long AulaId { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public class InserirUsuarioAulaCurtidoHandler :
        IRequestHandler<InserirUsuarioAulaCurtidoCommand, InserirUsuarioAulaCurtidoCommandResponse>
    {
        private IMediator _mediator;

        private readonly IRepository<UsuarioAulaCurtido> _repository;
        private readonly IRepository<Aula> _repositoryAula;

        public InserirUsuarioAulaCurtidoHandler
        (
            IMediator mediator,

            IRepository<UsuarioAulaCurtido> repository,
            IRepository<Aula> repositoryAula
        )
        {
            _mediator = mediator;

            _repository = repository;
            _repositoryAula = repositoryAula;
        }

        public async Task<InserirUsuarioAulaCurtidoCommandResponse> Handle
        (
            InserirUsuarioAulaCurtidoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<InserirUsuarioAulaCurtidoCommand>());

            await Validator(request, cancellationToken);

            UsuarioAulaCurtido usuarioAulaCurtido = request.ToDomain();

            await _mediator.Send(new AtualizarAulaCurtirCommand { Id = request.AulaId, Adicionar = true });

            await _repository.AddAsync(usuarioAulaCurtido, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            InserirUsuarioAulaCurtidoCommandResponse response = new InserirUsuarioAulaCurtidoCommandResponse();
            response.DataCadastro = usuarioAulaCurtido.DataCadastro;
            response.Id = usuarioAulaCurtido.Id;
            response.UsuarioId = request.UsuarioId;
            response.AulaId = request.AulaId;

            return response;
        }

        private async Task Validator
        (
            InserirUsuarioAulaCurtidoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.AulaId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirUsuarioAulaCurtidoCommand>(item => item.AulaId));
            if (request.UsuarioId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirUsuarioAulaCurtidoCommand>(item => item.UsuarioId));
            if (!await ExistsAulaAsync(request, cancellationToken)) throw new ArgumentNullException("Aula não encontrada");
        }

        private async Task<bool> ExistsAulaAsync
        (
            InserirUsuarioAulaCurtidoCommand request,
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
