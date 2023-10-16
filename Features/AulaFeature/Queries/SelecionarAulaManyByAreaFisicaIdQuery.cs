using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaFeature.Queries
{
    public class SelecionarAulaManyByAreaFisicaIdQuery : IRequest<IEnumerable<SelecionarAulaManyByAreaFisicaIdQueryResponse>>
    {
        public long Id { get; set; }
    }

    public class SelecionarAulaManyByAreaFisicaIdQueryResponse : Entity
    {
        public string Titulo { get; set; }
        public long Favoritado { get; set; }
        public long Curtido { get; set; }
        public long ProfessorId { get; set; }
        public ICollection<AulaComentario>? AulaComentarioMany { get; set; }
        public ICollection<AulaSessao>? AulaSessaoMany { get; set; }
    }

    public class SelecionarAulaManyByAreaFisicaIdQueryHandler : IRequestHandler<SelecionarAulaManyByAreaFisicaIdQuery, IEnumerable<SelecionarAulaManyByAreaFisicaIdQueryResponse>>
    {
        private readonly IRepository<Aula> _repository;

        public SelecionarAulaManyByAreaFisicaIdQueryHandler
        (
            IRepository<Aula> repository
        )
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SelecionarAulaManyByAreaFisicaIdQueryResponse>> Handle
        (
            SelecionarAulaManyByAreaFisicaIdQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarAulaManyByAreaFisicaIdQuery>());

            IEnumerable<Aula> aulaMany = await GetAsync(request, cancellationToken);

            List<SelecionarAulaManyByAreaFisicaIdQueryResponse> responseMany = new List<SelecionarAulaManyByAreaFisicaIdQueryResponse>();

            foreach (Aula aula in aulaMany)
            {
                SelecionarAulaManyByAreaFisicaIdQueryResponse response = new SelecionarAulaManyByAreaFisicaIdQueryResponse();
                response.Titulo = aula.Titulo;
                response.Favoritado = aula.Favoritado;
                response.Curtido = aula.Curtido;
                response.ProfessorId = aula.ProfessorId;
   
                response.AulaSessaoMany = getAulaSessaMany(aula.AulaSessaoMany);
                response.AulaComentarioMany = getAulaComentarioMany(aula.AulaComentarioMany);

                response.DataCadastro = aula.DataCadastro;
                response.DataAtualizacao = aula.DataAtualizacao;
                response.Id = aula.Id;
                responseMany.Add(response);
            }

            return responseMany;
        }

        private async Task<IEnumerable<Aula>> GetAsync
        (
            SelecionarAulaManyByAreaFisicaIdQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAsync
                (
                    item => item.AreaFisicaId.Equals(request.Id),
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
