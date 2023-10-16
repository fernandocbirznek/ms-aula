using MediatR;
using Microsoft.AspNetCore.Mvc;
using ms_aula.Extensions;
using ms_aula.Features.WidgetCursarFeature.Commands;
using ms_aula.Features.WidgetCursarFeature.Queries;

namespace ms_aula.Features.WidgetCursarFeature
{
    [ApiController]
    [Route("api/[controller]")]
    public class WidgetCursarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WidgetCursarController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("inserir")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(InserirWidgetCursarCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpDelete("excluir/{widgetCursarId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(long widgetCursarId)
        {
            return await this.SendAsync(_mediator, new RemoverWidgetCursarCommand() { Id = widgetCursarId });
        }

        [HttpGet("selecionar-aulas-favoritadas/{usuarioId}")]
        public async Task<ActionResult> Get(long usuarioId)
        {
            return await this.SendAsync(_mediator, new SelecionarWidgetCursarManyByUsuarioIdQuery() { Id = usuarioId });
        }
    }
}
