using MediatR;
using Microsoft.AspNetCore.Mvc;
using ms_aula.Extensions;
using ms_aula.Features.AulaComentarioFeature.Commands;
using ms_aula.Features.AulaComentarioFeature.Queries;

namespace ms_aula.Features.AulaComentarioFeature
{
    [ApiController]
    [Route("api/[controller]")]
    public class AulaComentarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AulaComentarioController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("inserir")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(InserirAulaComentarioCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpPut("atualizar")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(AtualizarAulaComentarioCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpDelete("excluir/{aulaComentarioId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(long aulaComentarioId)
        {
            return await this.SendAsync(_mediator, new RemoverAulaComentarioCommand() { Id = aulaComentarioId });
        }

        //[HttpGet("selecionar-aula-comentario/{aulaComentarioId}")]
        //public async Task<ActionResult> GetForum(long aulaComentarioId)
        //{
        //    return await this.SendAsync(_mediator, new SelecionarAulaComentarioByIdQuery() { Id = aulaComentarioId });
        //}

        [HttpGet("selecionar-aula-comentario/{aulaId}")]
        public async Task<ActionResult> Get(long aulaId)
        {
            return await this.SendAsync(_mediator, new SelecionarAulaComentarioManyByAulaIdQuery() { Id = aulaId });
        }
    }
}
