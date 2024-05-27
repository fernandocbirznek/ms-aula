using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AreaFisicaDivisaoFeature.Queries
{
    public class SelecionarAreaFisicaDivisaoByAreaFisicaIdQuery
        : IRequest<IEnumerable<SelecionarAreaFisicaDivisaoByAreaFisicaIdQueryResponse>>
    {
        public long AreaFisicaId { get; set; }
    }

    public class SelecionarAreaFisicaDivisaoByAreaFisicaIdQueryResponse : Entity
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public long AreaFisicaId { get; set; }
        public byte[]? Foto { get; set; }
    }

    public class SelecionarAreaFisicaDivisaoByAreaFisicaIdQueryHandler
        : IRequestHandler<SelecionarAreaFisicaDivisaoByAreaFisicaIdQuery,
            IEnumerable<SelecionarAreaFisicaDivisaoByAreaFisicaIdQueryResponse>>
    {
        private readonly IRepository<AreaFisicaDivisao> _repository;

        public SelecionarAreaFisicaDivisaoByAreaFisicaIdQueryHandler
        (
            IRepository<AreaFisicaDivisao> repository
        )
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SelecionarAreaFisicaDivisaoByAreaFisicaIdQueryResponse>> Handle
        (
            SelecionarAreaFisicaDivisaoByAreaFisicaIdQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarAreaFisicaDivisaoByAreaFisicaIdQuery>());

            IEnumerable<AreaFisicaDivisao> areaFisicaDivisaoMany = await GetAsync(request, cancellationToken);

            List<SelecionarAreaFisicaDivisaoByAreaFisicaIdQueryResponse> responseMany = new List<SelecionarAreaFisicaDivisaoByAreaFisicaIdQueryResponse>();

            foreach (AreaFisicaDivisao areaFisicaDivisao in areaFisicaDivisaoMany)
            {
                SelecionarAreaFisicaDivisaoByAreaFisicaIdQueryResponse response = new SelecionarAreaFisicaDivisaoByAreaFisicaIdQueryResponse();
                response.Titulo = areaFisicaDivisao.Titulo;
                response.Descricao = areaFisicaDivisao.Descricao;
                response.Foto = areaFisicaDivisao.Foto;
                response.AreaFisicaId = areaFisicaDivisao.AreaFisicaId;

                response.DataCadastro = areaFisicaDivisao.DataCadastro;
                response.DataAtualizacao = areaFisicaDivisao.DataAtualizacao;
                response.Id = areaFisicaDivisao.Id;
                responseMany.Add(response);
            }

            return responseMany;
        }

        private async Task<IEnumerable<AreaFisicaDivisao>> GetAsync
        (
            SelecionarAreaFisicaDivisaoByAreaFisicaIdQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAsync
                (
                    item => item.AreaFisicaId.Equals(request.AreaFisicaId),
                    cancellationToken
                );
        }
    }
}
