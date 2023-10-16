using MediatR;
using Microsoft.AspNetCore.Mvc;
using ms_aula.Extensions;
using ms_aula.Features.AulaSessaoFeature.Commands;
using ms_aula.Features.AulaSessaoFeature.Queries;

namespace ms_aula.Features.AulaSessaoFeature
{
    [ApiController]
    [Route("api/[controller]")]
    public class AulaSessaoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AulaSessaoController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("inserir")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(InserirAulaSessaoCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpPut("atualizar")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(AtualizarAulaSessaoCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpDelete("excluir/{aulaSessaoId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(long aulaSessaoId)
        {
            return await this.SendAsync(_mediator, new RemoverAulaSessaoCommand() { Id = aulaSessaoId });
        }

        [HttpGet("selecionar-aula-sessao/{aulaSessaoId}")]
        public async Task<ActionResult> GetForum(long aulaSessaoId)
        {
            return await this.SendAsync(_mediator, new SelecionarAulaSessaoByIdQuery() { Id = aulaSessaoId });
        }

        [HttpGet("selecionar-sessoes-aula/{aulaId}")]
        public async Task<ActionResult> Get(long aulaId)
        {
            return await this.SendAsync(_mediator, new SelecionarAulaSessaoManyByAulaIdQuery() { Id = aulaId });
        }
    }
}
