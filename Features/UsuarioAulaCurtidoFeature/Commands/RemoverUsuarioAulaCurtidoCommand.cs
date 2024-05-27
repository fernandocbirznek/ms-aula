using MediatR;
using ms_aula.Domains;
using ms_aula.Features.AulaFeature.Commands;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.UsuarioAulaCurtidoFeature.Commands
{
    public class RemoverUsuarioAulaCurtidoCommand : IRequest<long>
    {
        public long Id { get; set; }
        public long AulaId { get; set; }
    }

    public class RemoverUsuarioAulaCurtidoCommandHandler
        : IRequestHandler<RemoverUsuarioAulaCurtidoCommand, long>
    {
        private IMediator _mediator;
        private readonly IRepository<UsuarioAulaCurtido> _repository;

        public RemoverUsuarioAulaCurtidoCommandHandler
        (
            IMediator mediator,
            IRepository<UsuarioAulaCurtido> repository
        )
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<long> Handle
        (
            RemoverUsuarioAulaCurtidoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverUsuarioAulaCurtidoCommand>());

            await Validator(request, cancellationToken);

            UsuarioAulaCurtido usuarioAulaCurtido = await _repository.GetFirstAsync
                (
                    item => item.Id.Equals(request.Id),
            cancellationToken
            );

            await _mediator.Send(new AtualizarAulaCurtirCommand { Id = request.AulaId, Adicionar = false });

            await _repository.RemoveAsync(usuarioAulaCurtido);
            await _repository.SaveChangesAsync(cancellationToken);

            return usuarioAulaCurtido.Id;
        }

        private async Task Validator
        (
            RemoverUsuarioAulaCurtidoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (!await ExistsAsync(request, cancellationToken)) throw new ArgumentNullException("Aula favoritada não encontrada");
        }

        private async Task<bool> ExistsAsync
        (
            RemoverUsuarioAulaCurtidoCommand request,
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
