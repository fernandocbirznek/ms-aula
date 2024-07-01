using MediatR;
using Microsoft.AspNetCore.Mvc;
using ms_aula.Extensions;
using ms_aula.Features.AdministradorHomeFeature.Queries;

namespace ms_aula.Features.AdministradorHomeFeature
{
    [ApiController]
    [Route("api/administrador-home")]
    public class AdministradorHomeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdministradorHomeController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("selecionar-aula-informacao")]
        public async Task<ActionResult> Get()
        {
            return await this.SendAsync(_mediator, new SelecionarAulaInformacaoSistemaQuery());
        }
    }
}
