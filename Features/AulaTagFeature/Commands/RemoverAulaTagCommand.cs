using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaTagFeature.Commands
{
    public class RemoverAulaTagCommand : IRequest<long>
    {
        public long Id { get; set; }
    }

    public class RemoverAulaTagCommandHandler : IRequestHandler<RemoverAulaTagCommand, long>
    {
        private readonly IRepository<AulaTag> _repository;

        public RemoverAulaTagCommandHandler
        (
            IRepository<AulaTag> repository
        )
        {
            _repository = repository;
        }

        public async Task<long> Handle
        (
            RemoverAulaTagCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverAulaTagCommand>());

            AulaTag aulaTag = await _repository.GetFirstAsync(item => item.Id.Equals(request.Id), cancellationToken);

            Validator(aulaTag);

            await _repository.RemoveAsync(aulaTag);
            await _repository.SaveChangesAsync(cancellationToken);

            return aulaTag.Id;
        }

        private void Validator
        (
            AulaTag aulaTag
        )
        {
            if (aulaTag is null) throw new ArgumentNullException("Aula tag não encontrado");
        }
    }
}
