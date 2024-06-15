using MediatR;
using Microsoft.AspNetCore.Mvc;
using ms_aula.Domains;
using ms_aula.Extensions;
using ms_aula.Features.ArquivoPdfFeature.Commands;
using ms_aula.Features.ArquivoPdfFeature.Queries;
using Newtonsoft.Json;

namespace ms_aula.Features.ArquivoPdfFeature
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArquivoPdfController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArquivoPdfController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("arquivo-pdf-many/{aulaId}")]
        public async Task<IEnumerable<SelecionarManyArquivoPdfByAulaIdQueryResponse>> GetArquivoPdfMany
        (
            long aulaId
        )
        {
            var request = new SelecionarManyArquivoPdfByAulaIdQuery { AulaId = aulaId };
            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("inserir")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PostSingleFile
        (
            [FromForm] IFormFile file,
            [FromForm] string aulaSessao
        )
        {
            var aulaSessaoObj = JsonConvert.DeserializeObject<AulaSessao>(aulaSessao);

            var request = file.ToInserirArquivoPdf(aulaSessaoObj);
            return await this.SendAsync(_mediator, request);
        }

        [HttpDelete("excluir/{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(long id)
        {
            return await this.SendAsync(_mediator, new RemoverArquivoPdfCommand() { Id = id });
        }
    }
}
