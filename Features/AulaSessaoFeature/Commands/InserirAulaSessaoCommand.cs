using MediatR;
using ms_aula.Domains;
using ms_aula.Extensions;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaSessaoFeature.Commands
{
    public class InserirAulaSessaoCommand : IRequest<InserirAulaSessaoCommandResponse>
    {
        public long Ordem { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public AulaSessaoTipo AulaSessaoTipo { get; set; }
        public long AulaId { get; set; }
    }

    public class InserirAulaSessaoCommandResponse
    {
        public long Id { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public class InserirAulaSessaoHandler : IRequestHandler<InserirAulaSessaoCommand, InserirAulaSessaoCommandResponse>
    {
        private readonly IRepository<AulaSessao> _repository;
        private readonly IRepository<Aula> _repositoryAula;

        public InserirAulaSessaoHandler
        (
            IRepository<AulaSessao> repository,
            IRepository<Aula> repositoryAula
        )
        {
            _repository = repository;
            _repositoryAula = repositoryAula;
        }

        public async Task<InserirAulaSessaoCommandResponse> Handle
        (
            InserirAulaSessaoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaSessaoCommand>());

            await Validator(request, cancellationToken);

            AulaSessao aula = request.ToDomain();

            await _repository.AddAsync(aula, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            InserirAulaSessaoCommandResponse response = new InserirAulaSessaoCommandResponse();
            response.DataCadastro = aula.DataCadastro;
            response.Id = aula.Id;

            return response;
        }

        private async Task Validator
        (
            InserirAulaSessaoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.AulaId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaSessaoCommand>(item => item.AulaId));
            if (request.Ordem <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaSessaoCommand>(item => item.Ordem));
            if (request.Conteudo is null) throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaSessaoCommand>(item => item.Conteudo));
            if (request.AulaSessaoTipo < 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaSessaoCommand>(item => item.AulaSessaoTipo));
            if (!await ExistsAulaAsync(request, cancellationToken)) throw new ArgumentNullException("Aula não encontrada cadastrado");
        }

        private async Task<bool> ExistsAulaAsync
        (
            InserirAulaSessaoCommand request,
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
