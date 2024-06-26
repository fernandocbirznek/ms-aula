﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using ms_aula.Domains;
using ms_aula.Extensions;
using ms_aula.Features.AreaFisicaFeature.Commands;
using ms_aula.Features.AreaFisicaFeature.Queries;

namespace ms_aula.Features.AreaFisicaFeature
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreaFisicaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AreaFisicaController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("inserir")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(InserirAreaFisicaCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpPut("atualizar")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(AtualizarAreaFisicaCommand request)
        {
            return await this.SendAsync(_mediator, request);
        }

        [HttpDelete("excluir/{areaFisicaId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(long areaFisicaId)
        {
            return await this.SendAsync(_mediator, new RemoverAreaFisicaCommand() { Id = areaFisicaId });
        }

        [HttpGet("selecionar-area-fisica/{areaFisicaId}")]
        public async Task<ActionResult> GetForum(long areaFisicaId)
        {
            return await this.SendAsync(_mediator, new SelecionarAreaFisicaByIdQuery() { Id = areaFisicaId });
        }

        [HttpGet("selecionar-areas-fisica")]
        public async Task<ActionResult> Get()
        {
            return await this.SendAsync(_mediator, new SelecionarAreaFisicaFiltersQuery());
        }
    }
}
