using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AreaFisicaDivisaoFeature.Commands
{
    public class AtualizarAreaFisicaDivisaoCommand : IRequest<AtualizarAreaFisicaDivisaoCommandResponse>
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public byte[]? Foto { get; set; }
    }

    public class AtualizarAreaFisicaDivisaoCommandResponse
    {
        public DateTime DataAtualizacao { get; set; }
    }

    public class AtualizarAreaFisicaDivisaoHandler
        : IRequestHandler<AtualizarAreaFisicaDivisaoCommand, AtualizarAreaFisicaDivisaoCommandResponse>
    {
        private readonly IRepository<AreaFisicaDivisao> _repository;

        public AtualizarAreaFisicaDivisaoHandler
        (
            IRepository<AreaFisicaDivisao> repository
        )
        {
            _repository = repository;
        }

        public async Task<AtualizarAreaFisicaDivisaoCommandResponse> Handle
        (
            AtualizarAreaFisicaDivisaoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAreaFisicaDivisaoCommand>());

            await Validator(request, cancellationToken);

            AreaFisicaDivisao areaFisicaDivisao = await GetFirstAsync(request, cancellationToken);
            areaFisicaDivisao.Descricao = request.Descricao;
            areaFisicaDivisao.Titulo = request.Titulo;
            areaFisicaDivisao.Foto = request.Foto;

            await _repository.UpdateAsync(areaFisicaDivisao);
            await _repository.SaveChangesAsync(cancellationToken);

            AtualizarAreaFisicaDivisaoCommandResponse response = new AtualizarAreaFisicaDivisaoCommandResponse();
            response.DataAtualizacao = areaFisicaDivisao.DataAtualizacao;

            return response;
        }

        private async Task Validator
        (
            AtualizarAreaFisicaDivisaoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id <= 0) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAreaFisicaDivisaoCommand>(item => item.Id));
        }

        private async Task<AreaFisicaDivisao> GetFirstAsync
        (
            AtualizarAreaFisicaDivisaoCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetFirstAsync
                (
                    item => item.Id.Equals(request.Id),
                    cancellationToken
                );
        }
    }
}
