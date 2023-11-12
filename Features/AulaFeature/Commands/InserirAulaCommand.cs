using MediatR;
using ms_aula.Domains;
using ms_aula.Extensions;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaFeature.Commands
{
    public class InserirAulaCommand : IRequest<InserirAulaCommandResponse>
    {
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public long ProfessorId { get; set; }
        public long AreaFisicaId { get; set; }
    }

    public class InserirAulaCommandResponse
    {
        public long Id { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public class InserirAulaHandler : IRequestHandler<InserirAulaCommand, InserirAulaCommandResponse>
    {
        private readonly IRepository<Aula> _repositoryAula;

        public InserirAulaHandler
        (
            IRepository<Aula> repositoryAula
        )
        {
            _repositoryAula = repositoryAula;
        }

        public async Task<InserirAulaCommandResponse> Handle
        (
            InserirAulaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaCommand>());

            await Validator(request, cancellationToken);

            Aula aula = request.ToDomain();

            await _repositoryAula.AddAsync(aula, cancellationToken);
            await _repositoryAula.SaveChangesAsync(cancellationToken);

            InserirAulaCommandResponse response = new InserirAulaCommandResponse();
            response.DataCadastro = aula.DataCadastro;
            response.Id = aula.Id;

            return response;
        }

        private async Task Validator
        (
            InserirAulaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (String.IsNullOrEmpty(request.Titulo)) throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaCommand>(item => item.Titulo));
            if (request.ProfessorId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaCommand>(item => item.ProfessorId));
            if (request.AreaFisicaId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaCommand>(item => item.AreaFisicaId));
            if (await ExistsTituloAsync(request, cancellationToken)) throw new ArgumentNullException("Título já cadastrado");
        }

        private async Task<bool> ExistsTituloAsync
        (
            InserirAulaCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAula.ExistsAsync
                (
                    item => item.Titulo.ToLower().Trim().Equals(request.Titulo.ToLower().Trim()),
                    cancellationToken
                );
        }
    }
}
