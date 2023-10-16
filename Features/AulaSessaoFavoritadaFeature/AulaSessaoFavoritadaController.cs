using MediatR;
using Microsoft.AspNetCore.Mvc;
using ms_aula.Extensions;
using ms_aula.Features.AulaSessaoFavoritadaFeature.Commands;
using ms_aula.Features.AulaSessaoFavoritadaFeature.Queries;

namespace ms_aula.Features.AulaSessaoFavoritadaFeature
{
    [ApiController]
    [Route("api/[controller]")]
    public class AulaSessaoFavoritadaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AulaSessaoFavoritadaController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("inserir")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(InserirAulaSessaoFavoritadaCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpDelete("excluir/{aulaSessaoFavoritadaId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(long aulaSessaoFavoritadaId)
        {
            return await this.SendAsync(_mediator, new RemoverAulaSessaoFavoritadaCommand() { Id = aulaSessaoFavoritadaId });
        }

        [HttpGet("selecionar-aulas-favoritadas/{usuarioId}")]
        public async Task<ActionResult> Get(long usuarioId)
        {
            return await this.SendAsync(_mediator, new SelecionarAulaSessaoFavoritadaByUsuarioIdQuery() { Id = usuarioId });
        }
    }
}
