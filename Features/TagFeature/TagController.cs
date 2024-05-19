using MediatR;
using Microsoft.AspNetCore.Mvc;
using ms_aula.Extensions;
using ms_aula.Features.TagFeature.Commands;
using ms_aula.Features.TagFeature.Queries;

namespace ms_aula.Features.TagFeature
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("inserir")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(InserirTagCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpDelete("excluir/{tagId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(long tagId)
        {
            return await this.SendAsync(_mediator, new RemoverTagCommand() { Id = tagId });
        }

        [HttpGet("selecionar-tag-sistema")]
        public async Task<ActionResult> Get()
        {
            return await this.SendAsync(_mediator, new SelecionarTagFiltersQuery());
        }
    }
}
