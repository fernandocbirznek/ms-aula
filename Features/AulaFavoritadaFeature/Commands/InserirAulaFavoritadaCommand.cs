using MediatR;
using ms_aula.Domains;
using ms_aula.Extensions;
using ms_aula.Features.AulaFeature.Commands;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaFavoritadaFeature.Commands
{
    public class InserirAulaFavoritadaCommand : IRequest<InserirAulaFavoritadaCommandResponse>
    {
        public long UsuarioId { get; set; }
        public long AulaId { get; set; }
    }

    public class InserirAulaFavoritadaCommandResponse
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public long AulaId { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public class InserirAulaFavoritadaHandler : IRequestHandler<InserirAulaFavoritadaCommand, InserirAulaFavoritadaCommandResponse>
    {
        private IMediator _mediator;

        private readonly IRepository<AulaFavoritada> _repository;
        private readonly IRepository<Aula> _repositoryAula;

        public InserirAulaFavoritadaHandler
        (
            IMediator mediator,

            IRepository<AulaFavoritada> repository,
            IRepository<Aula> repositoryAula
        )
        {
            _mediator = mediator;

            _repository = repository;
            _repositoryAula = repositoryAula;
        }

        public async Task<InserirAulaFavoritadaCommandResponse> Handle
        (
            InserirAulaFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaFavoritadaCommand>());

            await Validator(request, cancellationToken);

            AulaFavoritada aula = request.ToDomain();

            await _mediator.Send(new AtualizarAulaFavoritadaCommand { Id = request.AulaId, Adicionar = true });

            await _repository.AddAsync(aula, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            InserirAulaFavoritadaCommandResponse response = new InserirAulaFavoritadaCommandResponse();
            response.DataCadastro = aula.DataCadastro;
            response.AulaId = aula.AulaId;
            response.UsuarioId = aula.UsuarioId;
            response.Id = aula.Id;

            return response;
        }

        private async Task Validator
        (
            InserirAulaFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.AulaId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaFavoritadaCommand>(item => item.AulaId));
            if (request.UsuarioId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaFavoritadaCommand>(item => item.UsuarioId));
            if (!await ExistsAulaAsync(request, cancellationToken)) throw new ArgumentNullException("Aula não encontrada cadastrado");
        }

        private async Task<bool> ExistsAulaAsync
        (
            InserirAulaFavoritadaCommand request,
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
