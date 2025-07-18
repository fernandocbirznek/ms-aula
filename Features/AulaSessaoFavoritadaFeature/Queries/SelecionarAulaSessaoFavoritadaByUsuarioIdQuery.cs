﻿using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaSessaoFavoritadaFeature.Queries
{
    public class SelecionarAulaSessaoFavoritadaByUsuarioIdQuery : IRequest<IEnumerable<SelecionarAulaSessaoFavoritadaByUsuarioIdQueryResponse>>
    {
        public long Id { get; set; }
    }

    public class SelecionarAulaSessaoFavoritadaByUsuarioIdQueryResponse : Entity
    {
        public long AulaId { get; set; }
        public long AulaSessaoId { get; set; }
        public long MuralPosicaoX { get; set; }
        public long MuralPosicaoY { get; set; }
        public string Titulo { get; set; }
        public long Ordem { get; set; }
        public string Conteudo { get; set; }
        public byte[] ArquivoConteudo { get; set; }
        public long Favoritado { get; set; }
        public AulaSessaoTipo AulaSessaoTipo { get; set; }
    }

    public class SelecionarAulaSessaoFavoritadaByUsuarioIdQueryHandler : IRequestHandler<SelecionarAulaSessaoFavoritadaByUsuarioIdQuery, IEnumerable<SelecionarAulaSessaoFavoritadaByUsuarioIdQueryResponse>>
    {
        private readonly IRepository<AulaSessaoFavoritada> _repository;
        private readonly IRepository<ArquivoPdf> _repositoryArquivoPdf;

        public SelecionarAulaSessaoFavoritadaByUsuarioIdQueryHandler
        (
            IRepository<AulaSessaoFavoritada> repository,
            IRepository<ArquivoPdf> repositoryArquivoPdf
        )
        {
            _repository = repository;
            _repositoryArquivoPdf = repositoryArquivoPdf;
        }

        public async Task<IEnumerable<SelecionarAulaSessaoFavoritadaByUsuarioIdQueryResponse>> Handle
        (
            SelecionarAulaSessaoFavoritadaByUsuarioIdQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarAulaSessaoFavoritadaByUsuarioIdQuery>());

            IEnumerable<AulaSessaoFavoritada> aulaFavoritadaMany = await GetAsync(request, cancellationToken);

            List<SelecionarAulaSessaoFavoritadaByUsuarioIdQueryResponse> responseMany = new List<SelecionarAulaSessaoFavoritadaByUsuarioIdQueryResponse>();

            foreach (AulaSessaoFavoritada aulaFavoritada in aulaFavoritadaMany)
            {
                SelecionarAulaSessaoFavoritadaByUsuarioIdQueryResponse response = new SelecionarAulaSessaoFavoritadaByUsuarioIdQueryResponse();
                response.DataCadastro = aulaFavoritada.DataCadastro;
                response.DataAtualizacao = aulaFavoritada.DataAtualizacao;
                response.Id = aulaFavoritada.Id;

                if (aulaFavoritada.AulaSessao.AulaSessaoTipo.Equals(AulaSessaoTipo.Pdf)) {
                    ArquivoPdf arquivoPdfMany = await GetArquivoPdfFirstAsync(aulaFavoritada.AulaSessao.Conteudo, cancellationToken);
                    response.ArquivoConteudo = arquivoPdfMany.Conteudo;
                }
                
                response.AulaId = aulaFavoritada.AulaSessao.AulaId;
                response.AulaSessaoId = aulaFavoritada.AulaSessaoId;
                response.Titulo = aulaFavoritada.AulaSessao.Titulo;
                response.Ordem = aulaFavoritada.AulaSessao.Ordem;
                response.Conteudo = aulaFavoritada.AulaSessao.Conteudo;
                response.Favoritado = aulaFavoritada.AulaSessao.Favoritado;
                response.AulaSessaoTipo = aulaFavoritada.AulaSessao.AulaSessaoTipo;
                response.MuralPosicaoX = aulaFavoritada.MuralPosicaoX;
                response.MuralPosicaoY = aulaFavoritada.MuralPosicaoY;
                responseMany.Add(response);
            }

            return responseMany;
        }

        private async Task<IEnumerable<AulaSessaoFavoritada>> GetAsync
        (
            SelecionarAulaSessaoFavoritadaByUsuarioIdQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAsync
                (
                    item => item.UsuarioId.Equals(request.Id),
                    cancellationToken,
                    item => item.AulaSessao
                );
        }

        private async Task<ArquivoPdf> GetArquivoPdfFirstAsync
        (
            string id,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryArquivoPdf.GetFirstAsync
                (
                    item => item.Id.Equals(long.Parse(id)),
                    cancellationToken
                );
        }
    }
}
