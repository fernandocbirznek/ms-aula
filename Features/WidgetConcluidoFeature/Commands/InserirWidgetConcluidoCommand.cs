using MediatR;
using ms_aula.Domains;
using ms_aula.Extensions;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.WidgetConcluidoFeature.Commands
{
    public class InserirWidgetConcluidoCommand : IRequest<InserirWidgetConcluidoCommandResponse>
    {
        public long UsuarioId { get; set; }
        public long AulaId { get; set; }
    }

    public class InserirWidgetConcluidoCommandResponse
    {
        public long Id { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public class InserirWidgetConcluidoHandler : IRequestHandler<InserirWidgetConcluidoCommand, InserirWidgetConcluidoCommandResponse>
    {
        private readonly IRepository<WidgetConcluido> _repository;
        private readonly IRepository<Aula> _repositoryAula;

        public InserirWidgetConcluidoHandler
        (
            IRepository<WidgetConcluido> repository,
            IRepository<Aula> repositoryAula
        )
        {
            _repository = repository;
            _repositoryAula = repositoryAula;
        }

        public async Task<InserirWidgetConcluidoCommandResponse> Handle
        (
            InserirWidgetConcluidoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<InserirWidgetConcluidoCommand>());

            await Validator(request, cancellationToken);

            WidgetConcluido widgetConcluido = request.ToDomain();

            await _repository.AddAsync(widgetConcluido, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            InserirWidgetConcluidoCommandResponse response = new InserirWidgetConcluidoCommandResponse();
            response.DataCadastro = widgetConcluido.DataCadastro;
            response.Id = widgetConcluido.Id;

            return response;
        }

        private async Task Validator
        (
            InserirWidgetConcluidoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.AulaId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirWidgetConcluidoCommand>(item => item.AulaId));
            if (request.UsuarioId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirWidgetConcluidoCommand>(item => item.UsuarioId));
            if (!await ExistsAulaAsync(request, cancellationToken)) throw new ArgumentNullException("Aula não encontrada");
        }

        private async Task<bool> ExistsAulaAsync
        (
            InserirWidgetConcluidoCommand request,
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
