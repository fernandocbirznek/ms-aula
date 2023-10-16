using MediatR;
using ms_aula.Domains;
using ms_aula.Extensions;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.WidgetCursarFeature.Commands
{
    public class InserirWidgetCursarCommand : IRequest<InserirWidgetCursarCommandResponse>
    {
        public long UsuarioId { get; set; }
        public long AulaId { get; set; }
    }

    public class InserirWidgetCursarCommandResponse
    {
        public long Id { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public class InserirWidgetCursarHandler : IRequestHandler<InserirWidgetCursarCommand, InserirWidgetCursarCommandResponse>
    {
        private readonly IRepository<WidgetCursar> _repository;
        private readonly IRepository<Aula> _repositoryAula;

        public InserirWidgetCursarHandler
        (
            IRepository<WidgetCursar> repository,
            IRepository<Aula> repositoryAula
        )
        {
            _repository = repository;
            _repositoryAula = repositoryAula;
        }

        public async Task<InserirWidgetCursarCommandResponse> Handle
        (
            InserirWidgetCursarCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<InserirWidgetCursarCommand>());

            await Validator(request, cancellationToken);

            WidgetCursar widgetCursar = request.ToDomain();

            await _repository.AddAsync(widgetCursar, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            InserirWidgetCursarCommandResponse response = new InserirWidgetCursarCommandResponse();
            response.DataCadastro = widgetCursar.DataCadastro;
            response.Id = widgetCursar.Id;

            return response;
        }

        private async Task Validator
        (
            InserirWidgetCursarCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.AulaId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirWidgetCursarCommand>(item => item.AulaId));
            if (request.UsuarioId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirWidgetCursarCommand>(item => item.UsuarioId));
            if (!await ExistsAulaAsync(request, cancellationToken)) throw new ArgumentNullException("Aula não encontrada");
        }

        private async Task<bool> ExistsAulaAsync
        (
            InserirWidgetCursarCommand request,
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
