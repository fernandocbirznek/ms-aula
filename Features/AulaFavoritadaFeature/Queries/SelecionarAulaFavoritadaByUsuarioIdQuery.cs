using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaFavoritadaFeature.Queries
{
    public class SelecionarAulaFavoritadaByUsuarioIdQuery : IRequest<IEnumerable<SelecionarAulaFavoritadaByUsuarioIdQueryResponse>>
    {
        public long Id { get; set; }
    }

    public class SelecionarAulaFavoritadaByUsuarioIdQueryResponse : Entity
    {
        public long UsuarioId { get; set; }
        public long AulaId { get; set; }
    }

    public class SelecionarAulaFavoritadaByUsuarioIdQueryHandler : IRequestHandler<SelecionarAulaFavoritadaByUsuarioIdQuery, IEnumerable<SelecionarAulaFavoritadaByUsuarioIdQueryResponse>>
    {
        private readonly IRepository<AulaFavoritada> _repository;

        public SelecionarAulaFavoritadaByUsuarioIdQueryHandler
        (
            IRepository<AulaFavoritada> repository
        )
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SelecionarAulaFavoritadaByUsuarioIdQueryResponse>> Handle
        (
            SelecionarAulaFavoritadaByUsuarioIdQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarAulaFavoritadaByUsuarioIdQuery>());

            IEnumerable<AulaFavoritada> aulaFavoritadaMany = await GetAsync(request, cancellationToken);

            List<SelecionarAulaFavoritadaByUsuarioIdQueryResponse> responseMany = new List<SelecionarAulaFavoritadaByUsuarioIdQueryResponse>();

            foreach (AulaFavoritada aulaFavoritada in aulaFavoritadaMany)
            {
                SelecionarAulaFavoritadaByUsuarioIdQueryResponse response = new SelecionarAulaFavoritadaByUsuarioIdQueryResponse();
                response.UsuarioId = aulaFavoritada.UsuarioId;
                response.AulaId = aulaFavoritada.AulaId;

                response.DataCadastro = aulaFavoritada.DataCadastro;
                response.DataAtualizacao = aulaFavoritada.DataAtualizacao;
                response.Id = aulaFavoritada.Id;
                responseMany.Add(response);
            }

            return responseMany;
        }

        private async Task<IEnumerable<AulaFavoritada>> GetAsync
        (
            SelecionarAulaFavoritadaByUsuarioIdQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAsync
                (
                    item => item.UsuarioId.Equals(request.Id),
                    cancellationToken
                );
        }
    }
}
