using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.ArquivoPdfFeature.Queries
{
    public class SelecionarManyArquivoPdfByAulaIdQuery
        : IRequest<IEnumerable<SelecionarManyArquivoPdfByAulaIdQueryResponse>>
    {
        public long AulaId { get; set; }
    }

    public class SelecionarManyArquivoPdfByAulaIdQueryResponse
    {
        public long Id { get; set; }
        public byte[] Conteudo { get; set; }
        public long AulaId { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Nome { get; set; }
    }

    public class SelecionarManyArquivoPdfByAulaIdQueryHandler
        : IRequestHandler<SelecionarManyArquivoPdfByAulaIdQuery,
            IEnumerable<SelecionarManyArquivoPdfByAulaIdQueryResponse>>
    {
        private readonly IRepository<ArquivoPdf> _repository;

        public SelecionarManyArquivoPdfByAulaIdQueryHandler
        (
            IRepository<ArquivoPdf> repository
        )
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SelecionarManyArquivoPdfByAulaIdQueryResponse>> Handle
        (
            SelecionarManyArquivoPdfByAulaIdQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarManyArquivoPdfByAulaIdQuery>());

            IEnumerable<ArquivoPdf> arquivoPdfMany = await GetAsync(request, cancellationToken);

            List<SelecionarManyArquivoPdfByAulaIdQueryResponse> responseMany =
                new List<SelecionarManyArquivoPdfByAulaIdQueryResponse>();

            foreach (ArquivoPdf arquivoPdf in arquivoPdfMany)
            {
                SelecionarManyArquivoPdfByAulaIdQueryResponse response =
                    new SelecionarManyArquivoPdfByAulaIdQueryResponse();

                response.AulaId = arquivoPdf.AulaId;
                response.Nome = arquivoPdf.Nome;
                response.Conteudo = arquivoPdf.Conteudo;

                response.DataCadastro = arquivoPdf.DataCadastro;
                response.Id = arquivoPdf.Id;
                responseMany.Add(response);
            }

            return responseMany;
        }

        private async Task<IEnumerable<ArquivoPdf>> GetAsync
        (
            SelecionarManyArquivoPdfByAulaIdQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAsync
                (
                    item => item.AulaId.Equals(request.AulaId),
                    cancellationToken
                );
        }
    }
}
