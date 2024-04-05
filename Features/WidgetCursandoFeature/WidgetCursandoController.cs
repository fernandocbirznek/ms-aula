using MediatR;
using Microsoft.AspNetCore.Mvc;
using ms_aula.Extensions;
using ms_aula.Features.WidgetCursandoFeature.Commands;
using ms_aula.Features.WidgetCursandoFeature.Queries;

namespace ms_aula.Features.WidgetCursandoFeature
{
    [ApiController]
    [Route("api/[controller]")]
    public class WidgetCursandoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WidgetCursandoController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("inserir")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(InserirWidgetCursandoCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpDelete("excluir/{widgetCursandoId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(long widgetCursandoId)
        {
            return await this.SendAsync(_mediator, new RemoverWidgetCursandoCommand() { Id = widgetCursandoId });
        }

        [HttpGet("selecionar-aulas-cursando/{usuarioId}")]
        public async Task<ActionResult> Get(long usuarioId)
        {
            return await this.SendAsync(_mediator, new SelecionarWidgetCursandoManyByUsuarioIdQuery() { Id = usuarioId });
        }
    }
}
