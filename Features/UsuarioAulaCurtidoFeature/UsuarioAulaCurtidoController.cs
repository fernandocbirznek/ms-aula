using MediatR;
using Microsoft.AspNetCore.Mvc;
using ms_aula.Extensions;
using ms_aula.Features.UsuarioAulaCurtidoFeature.Commands;
using ms_aula.Features.UsuarioAulaCurtidoFeature.Queries;

namespace ms_aula.Features.UsuarioAulaCurtidoFeature
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioAulaCurtidoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioAulaCurtidoController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("inserir")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(InserirUsuarioAulaCurtidoCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpDelete("excluir/{usuarioAulaCurtidoId}/aula/{aulaId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(long usuarioAulaCurtidoId, long aulaId)
        {
            return await this.SendAsync(_mediator, new RemoverUsuarioAulaCurtidoCommand() { Id = usuarioAulaCurtidoId, AulaId = aulaId });
        }

        [HttpGet("selecionar-usuario-aula-curtido/{usuarioId}")]
        public async Task<ActionResult> Get(long usuarioId)
        {
            return await this.SendAsync(_mediator, new SelecionarUsuarioAulaCurtidoByUsuarioIdQuery() { Id = usuarioId });
        }
    }
}
