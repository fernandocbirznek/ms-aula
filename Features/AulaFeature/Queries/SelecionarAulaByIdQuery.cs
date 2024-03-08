using MediatR;
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
        public ICollection<AulaComentario>? AulaComentarioMany { get; set; }
        public ICollection<AulaSessao>? AulaSessaoMany { get; set; }
    }

    public class SelecionarAulaByIdQueryHandler : IRequestHandler<SelecionarAulaByIdQuery, SelecionarAulaByIdQueryResponse>
    {
        private readonly IRepository<Aula> _repository;

        public SelecionarAulaByIdQueryHandler
        (
            IRepository<Aula> repository
        )
        {
            _repository = repository;
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

            SelecionarAulaByIdQueryResponse response = new SelecionarAulaByIdQueryResponse();
            response.Titulo = aula.Titulo;
            response.Resumo = aula.Resumo;
            response.Favoritado = aula.Favoritado;
            response.Curtido = aula.Curtido;
            response.ProfessorId = aula.ProfessorId;
            response.AreaFisicaId = aula.AreaFisicaId;
            response.AulaSessaoMany = aula.AulaSessaoMany;
            response.AulaComentarioMany = aula.AulaComentarioMany;
            response.DataCadastro = aula.DataCadastro;
            response.DataAtualizacao = aula.DataAtualizacao;
            response.Id = aula.Id;

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
                    item => item.AulaSessaoMany
                );
        }
    }
}
