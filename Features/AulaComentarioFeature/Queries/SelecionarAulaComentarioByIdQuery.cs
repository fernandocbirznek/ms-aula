using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaComentarioFeature.Queries
{
    public class SelecionarAulaComentarioByIdQuery : IRequest<SelecionarAulaComentarioByIdQueryResponse>
    {
        public long Id { get; set; }
    }

    public class SelecionarAulaComentarioByIdQueryResponse : Entity
    {
        public string Descricao { get; set; }
        public long AulaId { get; set; }
        public long UsuarioId { get; set; }
    }

    public class SelecionarAulaComentarioByIdQueryHandler : IRequestHandler<SelecionarAulaComentarioByIdQuery, SelecionarAulaComentarioByIdQueryResponse>
    {
        private readonly IRepository<AulaComentario> _repository;

        public SelecionarAulaComentarioByIdQueryHandler
        (
            IRepository<AulaComentario> repository
        )
        {
            _repository = repository;
        }

        public async Task<SelecionarAulaComentarioByIdQueryResponse> Handle
        (
            SelecionarAulaComentarioByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarAulaComentarioByIdQuery>());

            AulaComentario aula = await GetFirstAsync(request, cancellationToken);

            Validator(aula, cancellationToken);

            SelecionarAulaComentarioByIdQueryResponse response = new SelecionarAulaComentarioByIdQueryResponse();
            response.Descricao = aula.Descricao;
            response.AulaId = aula.AulaId;
            response.UsuarioId = aula.UsuarioId;
            response.DataCadastro = aula.DataCadastro;
            response.DataAtualizacao = aula.DataAtualizacao;
            response.Id = aula.Id;

            return response;
        }

        private async void Validator
        (
            AulaComentario aula,
            CancellationToken cancellationToken
        )
        {
            if (aula is null) throw new ArgumentNullException("Aula comentário não encontrado");
        }

        private async Task<AulaComentario> GetFirstAsync
        (
            SelecionarAulaComentarioByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetFirstAsync
                (
                    item => item.Id.Equals(request.Id),
                    cancellationToken
                );
        }
    }
}
