using MediatR;
using Microsoft.AspNetCore.Mvc;
using ms_aula.Extensions;
using ms_aula.Features.AulaFeature.Commands;
using ms_aula.Features.AulaFeature.Queries;

namespace ms_aula.Features.AulaFeature
{
    [ApiController]
    [Route("api/[controller]")]
    public class AulaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AulaController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("inserir")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(InserirAulaCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpPut("atualizar")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(AtualizarAulaCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpPut("atualizar-curtir")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(AtualizarAulaCurtirCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpPut("atualizar-favoritada")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(AtualizarAulaFavoritadaCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpPut("atualizar-publicado")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(AtualizarAulaPublicadoCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpDelete("excluir/{aulaId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(long aulaId)
        {
            return await this.SendAsync(_mediator, new RemoverAulaCommand() { Id = aulaId });
        }

        [HttpGet("selecionar-aula/{aulaId}")]
        public async Task<ActionResult> GetAula(long aulaId)
        {
            return await this.SendAsync(_mediator, new SelecionarAulaByIdQuery() { Id = aulaId });
        }

        [HttpGet("selecionar-aulas-professor/{professorId}")]
        public async Task<ActionResult> GetAulaProfessor(long professorId)
        {
            return await this.SendAsync(_mediator, new SelecionarAulaManyByProfessorIdQuery() { Id = professorId });
        }

        [HttpGet("selecionar-aulas-area-fisica/{areaFisicaId}")]
        public async Task<ActionResult> Get(long areaFisicaId)
        {
            return await this.SendAsync(_mediator, new SelecionarAulaManyByAreaFisicaIdQuery() { Id = areaFisicaId });
        }

        [HttpGet("selecionar-aulas-sistema")]
        public async Task<ActionResult> GetAulaSistema()
        {
            return await this.SendAsync(_mediator, new SelecionarManyAulaFiltersQuery());
        }
    }
}
