using MediatR;
using Microsoft.AspNetCore.Mvc;
using ms_aula.Extensions;
using ms_aula.Features.WidgetConcluidoFeature.Commands;
using ms_aula.Features.WidgetConcluidoFeature.Queries;

namespace ms_aula.Features.WidgetConcluidoFeature
{
    [ApiController]
    [Route("api/[controller]")]
    public class WidgetConcluidoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WidgetConcluidoController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("inserir")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(InserirWidgetConcluidoCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpDelete("excluir/{widgetConcluidaId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(long widgetConcluidaId)
        {
            return await this.SendAsync(_mediator, new RemoverWidgetConcluidoCommand() { Id = widgetConcluidaId });
        }

        [HttpGet("selecionar-aulas-favoritadas/{usuarioId}")]
        public async Task<ActionResult> Get(long usuarioId)
        {
            return await this.SendAsync(_mediator, new SelecionarWidgetConcluidoManyByUsuarioIdQuery() { Id = usuarioId });
        }
    }
}
