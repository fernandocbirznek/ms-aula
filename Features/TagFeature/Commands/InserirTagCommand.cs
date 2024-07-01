using MediatR;
using ms_aula.Domains;
using ms_aula.Extensions;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.TagFeature.Commands
{
    public class InserirTagCommand : IRequest<InserirTagCommandResponse>
    {
        public string Nome { get; set; }
    }

    public class InserirTagCommandResponse
    {
        public long Id { get; set; }
        public DateTime DataCadastro { get; set; }

        public string Nome { get; set; }
    }

    public class InserirTagHandler : IRequestHandler<InserirTagCommand, InserirTagCommandResponse>
    {
        private readonly IRepository<Tag> _repository;

        public InserirTagHandler
        (
            IRepository<Tag> repository
        )
        {
            _repository = repository;
        }

        public async Task<InserirTagCommandResponse> Handle
        (
            InserirTagCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<InserirTagCommand>());

            await Validator(request, cancellationToken);

            Tag forum = request.ToDomain();

            await _repository.AddAsync(forum, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            InserirTagCommandResponse response = new InserirTagCommandResponse();
            response.DataCadastro = forum.DataCadastro;
            response.Id = forum.Id;

            response.Nome = forum.Nome;

            return response;
        }

        private async Task Validator
        (
            InserirTagCommand request,
            CancellationToken cancellationToken
        )
        {
            if (String.IsNullOrEmpty(request.Nome)) throw new ArgumentNullException(MessageHelper.NullFor<InserirTagCommand>(item => item.Nome));
            if (await ExistsNomeAsync(request, cancellationToken)) throw new ArgumentNullException("Nome da tag já cadastrado");
        }

        private async Task<bool> ExistsNomeAsync
        (
            InserirTagCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.ExistsAsync
                (
                    item => item.Nome.ToLower().Trim().Equals(request.Nome.ToLower().Trim()),
                    cancellationToken
                );
        }
    }
}
