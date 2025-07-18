using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaSessaoFavoritadaFeature.Commands
{
    public class AtualizarManyAulaSessaoFavoritadaCommand
    : IRequest<AtualizarManyAulaSessaoFavoritadaCommandResponse>
    {
        public IEnumerable<AulaSessaoFavoritadaCommand> AulaSessaoFavoritadaMany { get; set; }
    }

    public class AulaSessaoFavoritadaCommand
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public long AulaSessaoId { get; set; }
        public long MuralPosicaoX { get; set; }
        public long MuralPosicaoY { get; set; }
    }

    public class AtualizarManyAulaSessaoFavoritadaCommandResponse
    {
        public DateTime DataAtualizacao { get; set; }
    }

    public class AtualizarManyAulaSessaoFavoritadaHandler
        : IRequestHandler<AtualizarManyAulaSessaoFavoritadaCommand,
            AtualizarManyAulaSessaoFavoritadaCommandResponse>
    {
        private readonly IRepository<AulaSessaoFavoritada> _repository;

        public AtualizarManyAulaSessaoFavoritadaHandler
        (
            IRepository<AulaSessaoFavoritada> repository
        )
        {
            _repository = repository;
        }

        public async Task<AtualizarManyAulaSessaoFavoritadaCommandResponse> Handle
        (
            AtualizarManyAulaSessaoFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<AtualizarManyAulaSessaoFavoritadaCommand>());

            await Validator(request, cancellationToken);

            var entities = await GetAsync(request, cancellationToken);

            var inputById = request.AulaSessaoFavoritadaMany.ToDictionary(item => item.Id);
            foreach (var entity in entities)
            {
                if (inputById.TryGetValue(entity.Id, out var input))
                {
                    entity.MuralPosicaoX = input.MuralPosicaoX;
                    entity.MuralPosicaoY = input.MuralPosicaoY;
                    entity.DataAtualizacao = DateTime.UtcNow;
                }
            }


            await _repository.UpdateCollectionAsync(entities);
            await _repository.SaveChangesAsync(cancellationToken);

            AtualizarManyAulaSessaoFavoritadaCommandResponse response = new AtualizarManyAulaSessaoFavoritadaCommandResponse();
            response.DataAtualizacao = DateTime.Now;

            return response;
        }

        private async Task Validator
        (
            AtualizarManyAulaSessaoFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.AulaSessaoFavoritadaMany.Count() <= 0) 
                throw new ArgumentNullException(
                    MessageHelper.NullFor<AtualizarManyAulaSessaoFavoritadaCommand>(
                        item => item.AulaSessaoFavoritadaMany
                    )
                );
        }

        private async Task<IEnumerable<AulaSessaoFavoritada>> 
            GetAsync
        (
            AtualizarManyAulaSessaoFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            var ids = request.AulaSessaoFavoritadaMany.Select(item => item.Id).ToList();

            return await _repository.GetAsync
                (
                    item => ids.Contains(item.Id),
                    cancellationToken
                );
        }
    }
}
