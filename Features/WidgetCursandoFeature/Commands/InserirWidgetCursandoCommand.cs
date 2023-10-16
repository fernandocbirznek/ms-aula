using MediatR;
using ms_aula.Domains;
using ms_aula.Extensions;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.WidgetCursandoFeature.Commands
{
    public class InserirWidgetCursandoCommand : IRequest<InserirWidgetCursandoCommandResponse>
    {
        public long UsuarioId { get; set; }
        public long AulaId { get; set; }
    }

    public class InserirWidgetCursandoCommandResponse
    {
        public long Id { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public class InserirWidgetCursandoHandler : IRequestHandler<InserirWidgetCursandoCommand, InserirWidgetCursandoCommandResponse>
    {
        private readonly IRepository<WidgetCursando> _repository;
        private readonly IRepository<Aula> _repositoryAula;

        public InserirWidgetCursandoHandler
        (
            IRepository<WidgetCursando> repository,
            IRepository<Aula> repositoryAula
        )
        {
            _repository = repository;
            _repositoryAula = repositoryAula;
        }

        public async Task<InserirWidgetCursandoCommandResponse> Handle
        (
            InserirWidgetCursandoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<InserirWidgetCursandoCommand>());

            await Validator(request, cancellationToken);

            WidgetCursando widgetCursando = request.ToDomain();

            await _repository.AddAsync(widgetCursando, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            InserirWidgetCursandoCommandResponse response = new InserirWidgetCursandoCommandResponse();
            response.DataCadastro = widgetCursando.DataCadastro;
            response.Id = widgetCursando.Id;

            return response;
        }

        private async Task Validator
        (
            InserirWidgetCursandoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.AulaId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirWidgetCursandoCommand>(item => item.AulaId));
            if (request.UsuarioId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirWidgetCursandoCommand>(item => item.UsuarioId));
            if (!await ExistsAulaAsync(request, cancellationToken)) throw new ArgumentNullException("Aula não encontrada");
        }

        private async Task<bool> ExistsAulaAsync
        (
            InserirWidgetCursandoCommand request,
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
