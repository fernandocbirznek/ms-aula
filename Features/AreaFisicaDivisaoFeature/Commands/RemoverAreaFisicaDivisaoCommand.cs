using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AreaFisicaDivisaoFeature.Commands
{
    public class RemoverAreaFisicaDivisaoCommand : IRequest<long>
    {
        public long Id { get; set; }
    }

    public class RemoverAreaFisicaDivisaoCommandHandler :
        IRequestHandler<RemoverAreaFisicaDivisaoCommand, long>
    {
        private readonly IRepository<AreaFisicaDivisao> _repository;

        public RemoverAreaFisicaDivisaoCommandHandler
        (
            IRepository<AreaFisicaDivisao> repository
        )
        {
            _repository = repository;
        }

        public async Task<long> Handle
        (
            RemoverAreaFisicaDivisaoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverAreaFisicaDivisaoCommand>());

            await Validator(request, cancellationToken);

            AreaFisicaDivisao areaFisicaDivisao = await _repository.GetFirstAsync(item => item.Id.Equals(request.Id), cancellationToken);

            await _repository.RemoveAsync(areaFisicaDivisao);
            await _repository.SaveChangesAsync(cancellationToken);

            return areaFisicaDivisao.Id;
        }

        private async Task Validator
        (
            RemoverAreaFisicaDivisaoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (!(await ExistsAsync(request, cancellationToken))) throw new ArgumentNullException("Área da Física Divisão não encontrada");
        }

        private async Task<bool> ExistsAsync
        (
            RemoverAreaFisicaDivisaoCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.ExistsAsync
                (
                    item => item.Id.Equals(request.Id),
                    cancellationToken
                );
        }
    }
}
