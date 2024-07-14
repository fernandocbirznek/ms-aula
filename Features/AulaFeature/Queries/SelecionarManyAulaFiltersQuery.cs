using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaFeature.Queries
{
    public class SelecionarManyAulaFiltersQuery : IRequest<IEnumerable<SelecionarManyAulaFiltersQueryResponse>>
    {
    }

    public class SelecionarManyAulaFiltersQueryResponse : Entity
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
        public ICollection<AulaComentario>? AulaComentarioMany { get; set; }
        public ICollection<AulaSessao>? AulaSessaoMany { get; set; }

        public string UsuarioNome { get; set; }
        public byte[]? UsuarioFoto { get; set; }
    }

    public class SelecionarManyAulaFiltersQueryResponseHandler :
        IRequestHandler<SelecionarManyAulaFiltersQuery, IEnumerable<SelecionarManyAulaFiltersQueryResponse>>
    {
        private readonly IRepository<Aula> _repository;
        private readonly IUsuarioService _usuarioService;

        public SelecionarManyAulaFiltersQueryResponseHandler
        (
            IRepository<Aula> repository,
            IUsuarioService usuarioService
        )
        {
            _repository = repository;
            _usuarioService = usuarioService;
        }

        public async Task<IEnumerable<SelecionarManyAulaFiltersQueryResponse>> Handle
        (
            SelecionarManyAulaFiltersQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarManyAulaFiltersQuery>());

            IEnumerable<Aula> aulaMany = await GetAsync(cancellationToken);

            List<SelecionarManyAulaFiltersQueryResponse> responseMany = new List<SelecionarManyAulaFiltersQueryResponse>();

            foreach (Aula aula in aulaMany)
            {
                var usuario = await _usuarioService.GetUsuarioByIdAsync(aula.ProfessorId);
                if (usuario is null)
                {
                    usuario = new services.UsuarioService.UsuarioResponse();
                }

                SelecionarManyAulaFiltersQueryResponse response = new SelecionarManyAulaFiltersQueryResponse();
                response.Titulo = aula.Titulo;
                response.Resumo = aula.Resumo;
                response.Favoritado = aula.Favoritado;
                response.Curtido = aula.Curtido;
                response.ProfessorId = aula.ProfessorId;
                response.AreaFisicaId = aula.AreaFisicaId;
                response.Publicado = aula.Publicado;

                response.AulaSessaoMany = getAulaSessaMany(aula.AulaSessaoMany);
                response.AulaComentarioMany = getAulaComentarioMany(aula.AulaComentarioMany);

                response.DataCadastro = aula.DataCadastro;
                response.DataAtualizacao = aula.DataAtualizacao;
                response.Id = aula.Id;

                response.UsuarioNome = usuario.Nome;
                response.UsuarioFoto = usuario.Foto;

                responseMany.Add(response);
            }

            return responseMany;
        }

        private async Task<IEnumerable<Aula>> GetAsync
        (
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAsync
                (
                    cancellationToken,
                    item => item.AulaComentarioMany,
                    item => item.AulaSessaoMany
                );
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
                sessao.Ordem = aulaSessao.Ordem;
                sessao.Titulo = aulaSessao.Titulo;
                sessao.DataCadastro = aulaSessao.DataCadastro;
                sessao.AulaSessaoTipo = aulaSessao.AulaSessaoTipo;
                sessao.DataAtualizacao = aulaSessao.DataAtualizacao;
                sessao.Conteudo = aulaSessao.Conteudo;
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
