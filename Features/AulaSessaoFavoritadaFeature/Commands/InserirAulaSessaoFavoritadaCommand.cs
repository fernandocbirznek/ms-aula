using MediatR;
using ms_aula.Domains;
using ms_aula.Extensions;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaSessaoFavoritadaFeature.Commands
{
    public class InserirAulaSessaoFavoritadaCommand : IRequest<InserirAulaSessaoFavoritadaCommandResponse>
    {
        public long UsuarioId { get; set; }
        public long AulaSessaoId { get; set; }
    }

    public class InserirAulaSessaoFavoritadaCommandResponse
    {
        public long Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public long AulaId { get; set; }
        public long UsuarioId { get; set; }
        public long AulaSessaoId { get; set; }
        public string Titulo { get; set; }
        public long Ordem { get; set; }
        public string Conteudo { get; set; }
        public long Favoritado { get; set; }
        public AulaSessaoTipo AulaSessaoTipo { get; set; }
    }

    public class InserirAulaSessaoFavoritadaHandler : IRequestHandler<InserirAulaSessaoFavoritadaCommand, InserirAulaSessaoFavoritadaCommandResponse>
    {
        private readonly IRepository<AulaSessaoFavoritada> _repository;
        private readonly IRepository<AulaSessao> _repositoryAulaSessao;

        public InserirAulaSessaoFavoritadaHandler
        (
            IRepository<AulaSessaoFavoritada> repository,
            IRepository<AulaSessao> repositoryAulaSessao
        )
        {
            _repository = repository;
            _repositoryAulaSessao = repositoryAulaSessao;
        }

        public async Task<InserirAulaSessaoFavoritadaCommandResponse> Handle
        (
            InserirAulaSessaoFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaSessaoFavoritadaCommand>());

            await Validator(request, cancellationToken);

            AulaSessao aulaSessao = await GetAulaSessaoFirstAsync(request, cancellationToken);

            AulaSessaoFavoritada aula = request.ToDomain();

            await _repository.AddAsync(aula, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            InserirAulaSessaoFavoritadaCommandResponse response = new InserirAulaSessaoFavoritadaCommandResponse();
            response.DataCadastro = aula.DataCadastro;
            response.Id = aula.Id;

            response.AulaId = aulaSessao.AulaId;
            response.AulaSessaoId = request.AulaSessaoId;
            response.Titulo = aulaSessao.Titulo;
            response.Ordem = aulaSessao.Ordem;
            response.Conteudo = aulaSessao.Conteudo;
            response.Favoritado = aulaSessao.Favoritado;
            response.AulaSessaoTipo = aulaSessao.AulaSessaoTipo;
            response.UsuarioId = request.UsuarioId;

            return response;
        }

        private async Task Validator
        (
            InserirAulaSessaoFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.UsuarioId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaSessaoFavoritadaCommand>(item => item.UsuarioId));
            if (request.AulaSessaoId <= 0) throw new ArgumentNullException(MessageHelper.NullFor<InserirAulaSessaoFavoritadaCommand>(item => item.AulaSessaoId));
            if (!await ExistsAulaSessaoAsync(request, cancellationToken)) throw new ArgumentNullException("Aula não encontrada cadastrado");
        }

        private async Task<bool> ExistsAulaSessaoAsync
        (
            InserirAulaSessaoFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAulaSessao.ExistsAsync
                (
                    item => item.Id.Equals(request.AulaSessaoId),
                    cancellationToken
                );
        }

        private async Task<AulaSessao> GetAulaSessaoFirstAsync
        (
            InserirAulaSessaoFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAulaSessao.GetFirstAsync
                (
                    item => item.Id.Equals(request.AulaSessaoId),
                    cancellationToken
                );
        }
    }
}
