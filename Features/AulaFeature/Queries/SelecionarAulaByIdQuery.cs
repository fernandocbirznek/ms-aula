﻿using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaFeature.Queries
{
    public class SelecionarAulaByIdQuery : IRequest<SelecionarAulaByIdQueryResponse>
    {
        public long Id { get; set; }
    }

    public class SelecionarAulaByIdQueryResponse : Entity
    {
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public long Favoritado { get; set; }
        public long Curtido { get; set; }
        public long ProfessorId { get; set; }
        public long AreaFisicaId { get; set; }
        public bool Publicado { get; set; }
        public long? AulaAnteriorId { get; set; }
        public long? AulaPosteriorId { get; set; }
        public ICollection<AulaTag>? AulaTagMany { get; set; }
        public ICollection<AulaComentario>? AulaComentarioMany { get; set; }
        public ICollection<AulaSessao>? AulaSessaoMany { get; set; }

        public string UsuarioNome { get; set; }
        public byte[]? UsuarioFoto { get; set; }
    }

    public class SelecionarAulaByIdQueryHandler : IRequestHandler<SelecionarAulaByIdQuery, SelecionarAulaByIdQueryResponse>
    {
        private readonly IRepository<Aula> _repository;
        private readonly IUsuarioService _usuarioService;

        public SelecionarAulaByIdQueryHandler
        (
            IRepository<Aula> repository,
            IUsuarioService usuarioService
        )
        {
            _repository = repository;
            _usuarioService = usuarioService;
        }

        public async Task<SelecionarAulaByIdQueryResponse> Handle
        (
            SelecionarAulaByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarAulaByIdQuery>());

            Aula aula = await GetFirstAsync(request, cancellationToken);

            Validator(aula, cancellationToken);

            var usuario = await _usuarioService.GetUsuarioByIdAsync(aula.ProfessorId);
            if (usuario is null)
            {
                usuario = new services.UsuarioService.UsuarioResponse();
            }

            SelecionarAulaByIdQueryResponse response = new SelecionarAulaByIdQueryResponse();
            response.Titulo = aula.Titulo;
            response.Resumo = aula.Resumo;
            response.Favoritado = aula.Favoritado;
            response.Curtido = aula.Curtido;
            response.ProfessorId = aula.ProfessorId;
            response.AreaFisicaId = aula.AreaFisicaId;
            response.AulaAnteriorId = aula.AulaAnteriorId;
            response.AulaPosteriorId = aula.AulaPosteriorId;
            response.Publicado = aula.Publicado;

            response.AulaTagMany = getAulaTagMany(aula.AulaTagMany);
            response.AulaSessaoMany = getAulaSessaMany(aula.AulaSessaoMany);
            response.AulaComentarioMany = getAulaComentarioMany(aula.AulaComentarioMany);

            response.DataCadastro = aula.DataCadastro;
            response.DataAtualizacao = aula.DataAtualizacao;
            response.Id = aula.Id;

            response.UsuarioNome = usuario.Nome;
            response.UsuarioFoto = usuario.Foto;

            return response;
        }

        private async void Validator
        (
            Aula aula,
            CancellationToken cancellationToken
        )
        {
            if (aula is null) throw new ArgumentNullException("Aula não encontrado");
        }

        private async Task<Aula> GetFirstAsync
        (
            SelecionarAulaByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetFirstAsync
                (
                    item => item.Id.Equals(request.Id),
                    cancellationToken,
                    item => item.AulaComentarioMany,
                    item => item.AulaSessaoMany,
                    item => item.AulaTagMany
                );
        }

        private List<AulaTag> getAulaTagMany
        (
            ICollection<AulaTag> entity
        )
        {
            List<AulaTag> aulaTagMany = new List<AulaTag>();
            foreach (AulaTag aulaTag in entity)
            {
                AulaTag sessao = new AulaTag();
                sessao.Id = aulaTag.Id;
                sessao.DataCadastro = aulaTag.DataCadastro;
                sessao.DataAtualizacao = aulaTag.DataAtualizacao;

                sessao.AulaId = aulaTag.AulaId;
                sessao.TagId = aulaTag.TagId;
                aulaTagMany.Add(sessao);
            }
            return aulaTagMany;
        }

        private List<AulaSessao> getAulaSessaMany
        (
           ICollection<AulaSessao> entity
        )
        {
            List<AulaSessao> aulaSessaoMany = new List<AulaSessao>();
            foreach (AulaSessao aulaSessao in entity)
            {
                AulaSessao sessao = new AulaSessao();
                sessao.Id = aulaSessao.Id;
                sessao.DataCadastro = aulaSessao.DataCadastro;
                sessao.DataAtualizacao = aulaSessao.DataAtualizacao;

                sessao.Ordem = aulaSessao.Ordem;
                sessao.Titulo = aulaSessao.Titulo;
                sessao.AulaSessaoTipo = aulaSessao.AulaSessaoTipo;
                sessao.Conteudo = aulaSessao.Conteudo;
                sessao.Favoritado = aulaSessao.Favoritado;
                aulaSessaoMany.Add(sessao);
            }
            return aulaSessaoMany;
        }

        private List<AulaComentario> getAulaComentarioMany
        (
            ICollection<AulaComentario> entity
        )
        {
            List<AulaComentario> aulaComentarioMany = new List<AulaComentario>();
            foreach (AulaComentario aulaComentario in entity)
            {
                AulaComentario comentario = new AulaComentario();
                comentario.Id = aulaComentario.Id;
                comentario.Descricao = aulaComentario.Descricao;
                comentario.DataCadastro = aulaComentario.DataCadastro;
                comentario.UsuarioId = aulaComentario.UsuarioId;
                aulaComentarioMany.Add(comentario);
            }
            return aulaComentarioMany;
        }
    }
}
