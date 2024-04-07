using MediatR;
using Microsoft.AspNetCore.Mvc;
using ms_aula.Domains;
using ms_aula.Extensions;
using ms_aula.Features.AulaSessaoFavoritadaFeature.Commands;
using ms_aula.Features.AulaSessaoFavoritadaFeature.Queries;
using ms_aula.models;

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

        [HttpDelete("excluir/{usuarioId}/{aulaSessaoId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(long usuarioId, long aulaSessaoId)
        {
            return await this.SendAsync(_mediator, new RemoverAulaSessaoFavoritadaCommand() { UsuarioId = usuarioId, AulaSessaoId = aulaSessaoId });
        }

        [HttpGet("selecionar-many-aula-sessao-favoritado/{usuarioId}")]
        public async Task<ActionResult> Get(long usuarioId)
        {
            return await this.SendAsync(_mediator, new SelecionarAulaSessaoFavoritadaByUsuarioIdQuery() { Id = usuarioId });
        }
    }
}
