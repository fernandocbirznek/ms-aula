using MediatR;
using ms_aula.Domains;
using ms_aula.Features.AulaSessaoFeature.Commands;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.ArquivoPdfFeature.Commands
{
    public class InserirArquivoPdfCommand : IRequest<InserirArquivoPdfCommandResponse>
    {
        public IFormFile FileData { get; set; }
        public AulaSessao AulaSessao { get; set; }
    }

    public class InserirArquivoPdfCommandResponse
    {
        public long Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public byte[] Conteudo { get; set; }

        public long AulaSessaoId { get; set; }
        public DateTime AulaSessaoDataCadastro { get; set; }
        public string ContentType { get; set; }
    }

    public class InserirArquivoPdfCommandHandler
        : IRequestHandler<InserirArquivoPdfCommand, InserirArquivoPdfCommandResponse>
    {
        private IMediator _mediator;
        private readonly IRepository<ArquivoPdf> _repository;

        public InserirArquivoPdfCommandHandler
        (
            IMediator mediator,
            IRepository<ArquivoPdf> repository
        )
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<InserirArquivoPdfCommandResponse> Handle
        (
            InserirArquivoPdfCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<InserirArquivoPdfCommandResponse>());

            ArquivoPdf arquivoPdf = new ArquivoPdf()
            {
                Nome = request.FileData.Name,
            };
            using (var stream = new MemoryStream())
            {
                request.FileData.CopyTo(stream);
                arquivoPdf.Conteudo = stream.ToArray();
            }

            arquivoPdf.DataCadastro = DateTime.Now;
            arquivoPdf.ContentType = "application/pdf";
            arquivoPdf.AulaId = request.AulaSessao.AulaId;

            await _repository.AddAsync(arquivoPdf, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            var aulaSessao = await _mediator.Send
                (
                    new InserirAulaSessaoCommand
                    {
                        Ordem = request.AulaSessao.Ordem,
                        Titulo = request.AulaSessao.Titulo,
                        Conteudo = arquivoPdf.Id.ToString(),
                        AulaSessaoTipo = request.AulaSessao.AulaSessaoTipo,
                        AulaId = request.AulaSessao.AulaId
                    }
                );

            InserirArquivoPdfCommandResponse response = new InserirArquivoPdfCommandResponse();
            response.Id = arquivoPdf.Id;
            response.DataCadastro = arquivoPdf.DataCadastro;
            response.AulaSessaoId = aulaSessao.Id;
            response.AulaSessaoDataCadastro = aulaSessao.DataCadastro;
            response.ContentType = arquivoPdf.ContentType;
            response.Conteudo = arquivoPdf.Conteudo;

            return response;
        }

    }
}
