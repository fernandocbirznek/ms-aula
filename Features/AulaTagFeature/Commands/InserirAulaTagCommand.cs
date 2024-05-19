using MediatR;
using ms_aula.Domains;
using ms_aula.Extensions;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaTagFeature.Commands
{
    public class InserirAulaTagCommand : IRequest<IEnumerable<InserirAulaTagCommandResponse>>
    {
        public List<InserirAulaTagMany> AulaTagMany { get; set; }
    }

    public class InserirAulaTagMany
    {
        public long AulaId { get; set; }
        public long TagId { get; set; }
    }

    public class InserirAulaTagCommandResponse
    {
        public long Id { get; set; }
        public long AulaId { get; set; }
        public long TagId { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public class InserirAulaTagHandler
        : IRequestHandler<InserirAulaTagCommand, IEnumerable<InserirAulaTagCommandResponse>>
    {
        private readonly IRepository<AulaTag> _repository;

        public InserirAulaTagHandler
        (
            IRepository<AulaTag> repository
        )
        {
            _repository = repository;
        }

        public async Task<IEnumerable<InserirAulaTagCommandResponse>> Handle
        (
            InserirAulaTagCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaTagCommand>());

            Validator(request);

            var aulaTagMany = await GetAulaTagManyAsync(request, cancellationToken);

            IEnumerable<InserirAulaTagCommandResponse> responseMany = new List<InserirAulaTagCommandResponse>();

            foreach (InserirAulaTagMany item in request.AulaTagMany)
            {
                if (!aulaTagMany.Any(aulaTag => aulaTag.TagId.Equals(item.TagId)))
                {
                    AulaTag aulaTag = item.ToDomain();
                    await _repository.AddAsync(aulaTag, cancellationToken);
                    await _repository.SaveChangesAsync(cancellationToken);

                    InserirAulaTagCommandResponse response = new InserirAulaTagCommandResponse();
                    response.DataCadastro = aulaTag.DataCadastro;
                    response.Id = aulaTag.Id;
                    response.AulaId = aulaTag.Id;
                    response.TagId = aulaTag.TagId;

                    responseMany.Append(response);
                }
                if (aulaTagMany.Any(aulaTag => aulaTag.TagId.Equals(item.TagId)))
                {
                    var teste = aulaTagMany.Single(aulaTag => aulaTag.TagId.Equals(item.TagId));
                    InserirAulaTagCommandResponse response = new InserirAulaTagCommandResponse();
                    response.DataCadastro = teste.DataCadastro;
                    response.Id = teste.Id;
                    response.AulaId = teste.Id;
                    response.TagId = teste.TagId;

                    responseMany.Append(response);
                }
            }

            foreach (AulaTag item in aulaTagMany)
            {
                if (!request.AulaTagMany.Any(aulaTag => aulaTag.TagId.Equals(item.TagId)))
                {
                    await _repository.RemoveAsync(item);
                    await _repository.SaveChangesAsync(cancellationToken);
                }
            }

            return responseMany;
        }

        private async Task<IEnumerable<AulaTag>> GetAulaTagManyAsync
        (
            InserirAulaTagCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAsync
                (
                    item => item.AulaId.Equals(request.AulaTagMany[0].AulaId),
                    cancellationToken
                );
        }

        private static void Validator
        (
            InserirAulaTagCommand request
        )
        {
            if (request.AulaTagMany.Count() > 3) throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaTagCommand>(item => item.AulaTagMany));
            if (request.AulaTagMany.Count() < 1) throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaTagCommand>(item => item.AulaTagMany));
        }
    }
}
