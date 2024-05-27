using MediatR;
using Microsoft.AspNetCore.Mvc;
using ms_aula.Extensions;
using ms_aula.Features.AreaFisicaDivisaoFeature.Commands;
using ms_aula.Features.AreaFisicaDivisaoFeature.Queries;

namespace ms_aula.Features.AreaFisicaDivisaoFeature
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreaFisicaDivisaoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AreaFisicaDivisaoController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("inserir")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(InserirAreaFisicaDivisaoCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpPut("atualizar")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(AtualizarAreaFisicaDivisaoCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpDelete("excluir/{areaFisicaDivisaoId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(long areaFisicaDivisaoId)
        {
            return await this.SendAsync(_mediator, new RemoverAreaFisicaDivisaoCommand() { Id = areaFisicaDivisaoId });
        }

        [HttpGet("selecionar-area-fisica-divisao/{areaFisicaId}")]
        public async Task<ActionResult> GetForum(long areaFisicaId)
        {
            return await this.SendAsync(_mediator, new SelecionarAreaFisicaDivisaoByAreaFisicaIdQuery() { AreaFisicaId = areaFisicaId });
        }
    }
}
