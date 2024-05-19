using MediatR;
using Microsoft.AspNetCore.Mvc;
using ms_aula.Extensions;
using ms_aula.Features.AulaTagFeature.Commands;
using ms_aula.Features.AulaTagFeature.Queries;

namespace ms_aula.Features.AulaTagFeature
{
    [ApiController]
    [Route("api/[controller]")]
    public class AulaTagController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AulaTagController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("inserir")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(InserirAulaTagCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpDelete("excluir/{aulaTagId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(long aulaTagId)
        {
            return await this.SendAsync(_mediator, new RemoverAulaTagCommand() { Id = aulaTagId });
        }

        [HttpGet("selecionar-aula-tag-interesse")]
        public async Task<ActionResult> Get()
        {
            return await this.SendAsync(_mediator, new SelecionarAulaTagFiltersQuery());
        }
    }
}
