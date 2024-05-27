using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.UsuarioAulaCurtidoFeature.Queries
{
    public class SelecionarUsuarioAulaCurtidoByUsuarioIdQuery
        : IRequest<IEnumerable<SelecionarUsuarioAulaCurtidoByUsuarioIdQueryResponse>>
    {
        public long Id { get; set; }
    }

    public class SelecionarUsuarioAulaCurtidoByUsuarioIdQueryResponse : Entity
    {
        public long UsuarioId { get; set; }
        public long AulaId { get; set; }
    }

    public class SelecionarUsuarioAulaCurtidoByUsuarioIdQueryHandler
        : IRequestHandler<SelecionarUsuarioAulaCurtidoByUsuarioIdQuery,
            IEnumerable<SelecionarUsuarioAulaCurtidoByUsuarioIdQueryResponse>>
    {
        private readonly IRepository<UsuarioAulaCurtido> _repository;

        public SelecionarUsuarioAulaCurtidoByUsuarioIdQueryHandler
        (
            IRepository<UsuarioAulaCurtido> repository
        )
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SelecionarUsuarioAulaCurtidoByUsuarioIdQueryResponse>> Handle
        (
            SelecionarUsuarioAulaCurtidoByUsuarioIdQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarUsuarioAulaCurtidoByUsuarioIdQuery>());

            IEnumerable<UsuarioAulaCurtido> usuarioAulaCurtidoMany = await GetAsync(request, cancellationToken);

            List<SelecionarUsuarioAulaCurtidoByUsuarioIdQueryResponse> responseMany =
                new List<SelecionarUsuarioAulaCurtidoByUsuarioIdQueryResponse>();

            foreach (UsuarioAulaCurtido usuarioAulaCurtido in usuarioAulaCurtidoMany)
            {
                SelecionarUsuarioAulaCurtidoByUsuarioIdQueryResponse response =
                    new SelecionarUsuarioAulaCurtidoByUsuarioIdQueryResponse();
                response.AulaId = usuarioAulaCurtido.AulaId;
                response.UsuarioId = usuarioAulaCurtido.UsuarioId;

                response.DataCadastro = usuarioAulaCurtido.DataCadastro;
                response.DataAtualizacao = usuarioAulaCurtido.DataAtualizacao;
                response.Id = usuarioAulaCurtido.Id;
                responseMany.Add(response);
            }

            return responseMany;
        }

        private async Task<IEnumerable<UsuarioAulaCurtido>> GetAsync
        (
            SelecionarUsuarioAulaCurtidoByUsuarioIdQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAsync
                (
                    item => item.UsuarioId.Equals(request.Id),
                    cancellationToken
                );
        }
    }
}
