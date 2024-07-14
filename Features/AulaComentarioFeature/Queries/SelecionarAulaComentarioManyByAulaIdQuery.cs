using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaComentarioFeature.Queries
{
    public class SelecionarAulaComentarioManyByAulaIdQuery : IRequest<IEnumerable<SelecionarAulaComentarioManyByAulaIdQueryResponse>>
    {
        public long Id { get; set; }
    }

    public class SelecionarAulaComentarioManyByAulaIdQueryResponse : Entity
    {
        public string Descricao { get; set; }
        public long AulaId { get; set; }
        public long UsuarioId { get; set; }

        public string UsuarioNome { get; set; }
        public byte[]? UsuarioFoto { get; set; }
    }

    public class SelecionarAulaComentarioManyByAulaIdQueryHandler : IRequestHandler<SelecionarAulaComentarioManyByAulaIdQuery, IEnumerable<SelecionarAulaComentarioManyByAulaIdQueryResponse>>
    {
        private readonly IRepository<AulaComentario> _repository;
        private readonly IUsuarioService _usuarioService;

        public SelecionarAulaComentarioManyByAulaIdQueryHandler
        (
            IRepository<AulaComentario> repository,
            IUsuarioService usuarioService
        )
        {
            _repository = repository;
            _usuarioService = usuarioService;
        }

        public async Task<IEnumerable<SelecionarAulaComentarioManyByAulaIdQueryResponse>> Handle
        (
            SelecionarAulaComentarioManyByAulaIdQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarAulaComentarioManyByAulaIdQuery>());

            IEnumerable<AulaComentario> aulaComentarioMany = await GetAsync(request, cancellationToken);

            List<SelecionarAulaComentarioManyByAulaIdQueryResponse> responseMany = new List<SelecionarAulaComentarioManyByAulaIdQueryResponse>();

            foreach (AulaComentario aulaComentario in aulaComentarioMany)
            {
                var usuario = await _usuarioService.GetUsuarioByIdAsync(aulaComentario.UsuarioId);
                if (usuario is null)
                {
                    usuario = new services.UsuarioService.UsuarioResponse();
                }

                SelecionarAulaComentarioManyByAulaIdQueryResponse response = new SelecionarAulaComentarioManyByAulaIdQueryResponse();
                response.Descricao = aulaComentario.Descricao;
                response.AulaId = aulaComentario.AulaId;
                response.UsuarioId = aulaComentario.UsuarioId;
                response.DataCadastro = aulaComentario.DataCadastro;
                response.DataAtualizacao = aulaComentario.DataAtualizacao;
                response.Id = aulaComentario.Id;

                response.UsuarioNome = usuario.Nome;
                response.UsuarioFoto = usuario.Foto;

                responseMany.Add(response);
            }

            return responseMany;
        }

        private async Task<IEnumerable<AulaComentario>> GetAsync
        (
            SelecionarAulaComentarioManyByAulaIdQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAsync
                (
                    item => item.AulaId.Equals(request.Id),
                    cancellationToken
                );
        }
    }
}
