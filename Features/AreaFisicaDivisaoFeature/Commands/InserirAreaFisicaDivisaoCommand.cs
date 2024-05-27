using MediatR;
using ms_aula.Domains;
using ms_aula.Extensions;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AreaFisicaDivisaoFeature.Commands
{
    public class InserirAreaFisicaDivisaoCommand : IRequest<InserirAreaFisicaDivisaoCommandResponse>
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public byte[]? Foto { get; set; }
        public long AreaFisicaId { get; set; }
    }

    public class InserirAreaFisicaDivisaoCommandResponse
    {
        public long Id { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public class InserirAreaFisicaDivisaoHandler : IRequestHandler<InserirAreaFisicaDivisaoCommand, InserirAreaFisicaDivisaoCommandResponse>
    {
        private readonly IRepository<AreaFisicaDivisao> _repository;

        public InserirAreaFisicaDivisaoHandler
        (
            IRepository<AreaFisicaDivisao> repository
        )
        {
            _repository = repository;
        }

        public async Task<InserirAreaFisicaDivisaoCommandResponse> Handle
        (
            InserirAreaFisicaDivisaoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<InserirAreaFisicaDivisaoCommand>());

            await Validator(request, cancellationToken);

            AreaFisicaDivisao areaFisicaDivisao = request.ToDomain();

            await _repository.AddAsync(areaFisicaDivisao, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            InserirAreaFisicaDivisaoCommandResponse response = new InserirAreaFisicaDivisaoCommandResponse();
            response.DataCadastro = areaFisicaDivisao.DataCadastro;
            response.Id = areaFisicaDivisao.Id;

            return response;
        }

        private async Task Validator
        (
            InserirAreaFisicaDivisaoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.AreaFisicaId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirAreaFisicaDivisaoCommand>(item => item.AreaFisicaId));
        }
    }
}
