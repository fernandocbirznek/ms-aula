using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaSessaoFeature.Queries
{
    public class SelecionarAulaSessaoManyByAulaIdQuery : IRequest<IEnumerable<SelecionarAulaSessaoManyByAulaIdQueryResponse>>
    {
        public long Id { get; set; }
    }

    public class SelecionarAulaSessaoManyByAulaIdQueryResponse : Entity
    {
        public long Ordem { get; set; }
        public string Conteudo { get; set; }
        public long Favoritado { get; set; }
        public AulaSessaoTipo AulaSessaoTipo { get; set; }
    }

    public class SelecionarAulaSessaoManyByAulaIdQueryHandler : IRequestHandler<SelecionarAulaSessaoManyByAulaIdQuery, IEnumerable<SelecionarAulaSessaoManyByAulaIdQueryResponse>>
    {
        private readonly IRepository<AulaSessao> _repository;

        public SelecionarAulaSessaoManyByAulaIdQueryHandler
        (
            IRepository<AulaSessao> repository
        )
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SelecionarAulaSessaoManyByAulaIdQueryResponse>> Handle
        (
            SelecionarAulaSessaoManyByAulaIdQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarAulaSessaoManyByAulaIdQuery>());

            IEnumerable<AulaSessao> aulaSessaoMany = await GetAsync(request, cancellationToken);

            List<SelecionarAulaSessaoManyByAulaIdQueryResponse> responseMany = new List<SelecionarAulaSessaoManyByAulaIdQueryResponse>();

            foreach (AulaSessao aulaSessao in aulaSessaoMany)
            {
                SelecionarAulaSessaoManyByAulaIdQueryResponse response = new SelecionarAulaSessaoManyByAulaIdQueryResponse();
                response.Ordem = aulaSessao.Ordem;
                response.Conteudo = aulaSessao.Conteudo;
                response.Favoritado = aulaSessao.Favoritado;
                response.AulaSessaoTipo = aulaSessao.AulaSessaoTipo;
                response.DataCadastro = aulaSessao.DataCadastro;
                response.DataAtualizacao = aulaSessao.DataAtualizacao;
                response.Id = aulaSessao.Id;
                responseMany.Add(response);
            }

            return responseMany;
        }

        private async Task<IEnumerable<AulaSessao>> GetAsync
        (
            SelecionarAulaSessaoManyByAulaIdQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAsync
                (
                    item => item.AulaId.Equals(request.Id),
                    cancellationToken
                );
        }
    }
}
