using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AreaFisicaFeature.Commands
{
    public class RemoverAreaFisicaCommand : IRequest<RemoverAreaFisicaCommandResponse>
    {
        public long Id { get; set; }
    }

    public class RemoverAreaFisicaCommandResponse
    {
        public long Id { get; set; }
    }

    public class RemoverAreaFisicaCommandHandler : IRequestHandler<RemoverAreaFisicaCommand, RemoverAreaFisicaCommandResponse>
    {
        private readonly IRepository<AreaFisica> _repository;

        public RemoverAreaFisicaCommandHandler
        (
            IRepository<AreaFisica> repository
        )
        {
            _repository = repository;
        }

        public async Task<RemoverAreaFisicaCommandResponse> Handle
        (
            RemoverAreaFisicaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverAreaFisicaCommand>());

            await Validator(request, cancellationToken);

            AreaFisica forum = await _repository.GetFirstAsync(item => item.Id.Equals(request.Id), cancellationToken);

            await _repository.RemoveAsync(forum);
            await _repository.SaveChangesAsync(cancellationToken);

            RemoverAreaFisicaCommandResponse response = new RemoverAreaFisicaCommandResponse();
            response.Id = forum.Id;

            return response;
        }

        private async Task Validator
        (
            RemoverAreaFisicaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (!(await ExistsAsync(request, cancellationToken))) throw new ArgumentNullException("Área da Física não encontrada");
        }

        private async Task<bool> ExistsAsync
        (
            RemoverAreaFisicaCommand request,
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
