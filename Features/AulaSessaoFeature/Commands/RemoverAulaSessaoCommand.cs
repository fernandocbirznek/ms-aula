using MediatR;
using ms_aula.Domains;
using ms_aula.Features.ArquivoPdfFeature.Commands;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaSessaoFeature.Commands
{
    public class RemoverAulaSessaoCommand : IRequest<RemoverAulaSessaoCommandResponse>
    {
        public long Id { get; set; }
    }

    public class RemoverAulaSessaoCommandResponse
    {
        public long Id { get; set; }
    }

    public class RemoverAulaSessaoCommandHandler : IRequestHandler<RemoverAulaSessaoCommand, RemoverAulaSessaoCommandResponse>
    {
        private readonly IRepository<AulaSessao> _repository;
        private IMediator _mediator;

        public RemoverAulaSessaoCommandHandler
        (
            IRepository<AulaSessao> repository,
            IMediator mediator
        )
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<RemoverAulaSessaoCommandResponse> Handle
        (
            RemoverAulaSessaoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverAulaSessaoCommand>());

            await Validator(request, cancellationToken);

            AulaSessao aulaSessao = await _repository.GetFirstAsync(item => item.Id.Equals(request.Id), cancellationToken);

            if (aulaSessao.AulaSessaoTipo.Equals(AulaSessaoTipo.Pdf))
                await _mediator.Send(new RemoverArquivoPdfCommand { Id = long.Parse(aulaSessao.Conteudo) });

            await _repository.RemoveAsync(aulaSessao);
            await _repository.SaveChangesAsync(cancellationToken);

            RemoverAulaSessaoCommandResponse response = new RemoverAulaSessaoCommandResponse();
            response.Id = aulaSessao.Id;

            return response;
        }

        private async Task Validator
        (
            RemoverAulaSessaoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (!(await ExistsAsync(request, cancellationToken))) throw new ArgumentNullException("Aula sessão não encontrada");
        }

        private async Task<bool> ExistsAsync
        (
            RemoverAulaSessaoCommand request,
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
