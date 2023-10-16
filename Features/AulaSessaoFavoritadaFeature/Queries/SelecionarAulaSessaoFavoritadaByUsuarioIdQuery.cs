using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaSessaoFavoritadaFeature.Queries
{
    public class SelecionarAulaSessaoFavoritadaByUsuarioIdQuery : IRequest<IEnumerable<SelecionarAulaSessaoFavoritadaByUsuarioIdQueryResponse>>
    {
        public long Id { get; set; }
    }

    public class SelecionarAulaSessaoFavoritadaByUsuarioIdQueryResponse : Entity
    {
        public AulaSessao? AulaSessao { get; set; }
    }

    public class SelecionarAulaSessaoFavoritadaByUsuarioIdQueryHandler : IRequestHandler<SelecionarAulaSessaoFavoritadaByUsuarioIdQuery, IEnumerable<SelecionarAulaSessaoFavoritadaByUsuarioIdQueryResponse>>
    {
        private readonly IRepository<AulaSessaoFavoritada> _repository;

        public SelecionarAulaSessaoFavoritadaByUsuarioIdQueryHandler
        (
            IRepository<AulaSessaoFavoritada> repository
        )
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SelecionarAulaSessaoFavoritadaByUsuarioIdQueryResponse>> Handle
        (
            SelecionarAulaSessaoFavoritadaByUsuarioIdQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarAulaSessaoFavoritadaByUsuarioIdQuery>());

            IEnumerable<AulaSessaoFavoritada> aulaFavoritadaMany = await GetAsync(request, cancellationToken);

            List<SelecionarAulaSessaoFavoritadaByUsuarioIdQueryResponse> responseMany = new List<SelecionarAulaSessaoFavoritadaByUsuarioIdQueryResponse>();

            foreach (AulaSessaoFavoritada aulaFavoritada in aulaFavoritadaMany)
            {
                SelecionarAulaSessaoFavoritadaByUsuarioIdQueryResponse response = new SelecionarAulaSessaoFavoritadaByUsuarioIdQueryResponse();
                response.AulaSessao = aulaFavoritada.AulaSessao;
                response.DataCadastro = aulaFavoritada.DataCadastro;
                response.DataAtualizacao = aulaFavoritada.DataAtualizacao;
                response.Id = aulaFavoritada.Id;
                responseMany.Add(response);
            }

            return responseMany;
        }

        private async Task<IEnumerable<AulaSessaoFavoritada>> GetAsync
        (
            SelecionarAulaSessaoFavoritadaByUsuarioIdQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAsync
                (
                    item => item.Id.Equals(request.Id),
                    cancellationToken,
                    item => item.AulaSessao
                );
        }
    }
}
