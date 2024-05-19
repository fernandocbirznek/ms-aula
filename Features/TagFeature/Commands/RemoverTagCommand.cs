using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.TagFeature.Commands
{
    public class RemoverTagCommand : IRequest<long>
    {
        public long Id { get; set; }
    }

    public class RemoverTagCommandHandler : IRequestHandler<RemoverTagCommand, long>
    {
        private readonly IRepository<Tag> _repository;

        public RemoverTagCommandHandler
        (
            IRepository<Tag> repository
        )
        {
            _repository = repository;
        }

        public async Task<long> Handle
        (
            RemoverTagCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverTagCommand>());

            await Validator(request, cancellationToken);

            Tag tag = await _repository.GetFirstAsync(item => item.Id.Equals(request.Id), cancellationToken);

            await _repository.RemoveAsync(tag);
            await _repository.SaveChangesAsync(cancellationToken);

            return tag.Id;
        }

        private async Task Validator
        (
            RemoverTagCommand request,
            CancellationToken cancellationToken
        )
        {
            if (!(await ExistsAsync(request, cancellationToken))) throw new ArgumentNullException("Tag não encontrada");
        }

        private async Task<bool> ExistsAsync
        (
            RemoverTagCommand request,
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
