using MediatR;
using Microsoft.AspNetCore.Mvc;
using ms_aula.Extensions;
using ms_aula.Features.AulaFavoritadaFeature.Commands;
using ms_aula.Features.AulaFavoritadaFeature.Queries;

namespace ms_aula.Features.AulaFavoritadaFeature
{
    [ApiController]
    [Route("api/[controller]")]
    public class AulaFavoritadaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AulaFavoritadaController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("inserir")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(InserirAulaFavoritadaCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpDelete("excluir/{aulaFavoritadaId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(long aulaFavoritadaId)
        {
            return await this.SendAsync(_mediator, new RemoverAulaFavoritadaCommand() { Id = aulaFavoritadaId });
        }

        [HttpGet("selecionar-aulas-favoritadas/{usuarioId}")]
        public async Task<ActionResult> Get(long usuarioId)
        {
            return await this.SendAsync(_mediator, new SelecionarAulaFavoritadaByUsuarioIdQuery() { Id = usuarioId });
        }
    }
}
