﻿using ms_aula.Domains;
using ms_aula.Features.AreaFisicaFeature.Commands;

namespace ms_aula.Extensions
{
    public static class AreaFisicaExtensions
    {
        public static AreaFisica ToDomain(this InserirAreaFisicaCommand request)
        {
            return new()
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                Aplicacao =  request.Aplicacao,
                DataCadastro = DateTime.Now
            };
        }
    }
}
