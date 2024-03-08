using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaSessaoFeature.Queries
{
    public class SelecionarAulaSessaoByIdQuery : IRequest<SelecionarAulaSessaoByIdQueryResponse>
    {
        public long Id { get; set; }
    }

    public class SelecionarAulaSessaoByIdQueryResponse : Entity
    {
        public long Ordem { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public long Favoritado { get; set; }
        public AulaSessaoTipo AulaSessaoTipo { get; set; }
    }

    public class SelecionarAulaSessaoByIdQueryHandler : IRequestHandler<SelecionarAulaSessaoByIdQuery, SelecionarAulaSessaoByIdQueryResponse>
    {
        private readonly IRepository<AulaSessao> _repository;

        public SelecionarAulaSessaoByIdQueryHandler
        (
            IRepository<AulaSessao> repository
        )
        {
            _repository = repository;
        }

        public async Task<SelecionarAulaSessaoByIdQueryResponse> Handle
        (
            SelecionarAulaSessaoByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarAulaSessaoByIdQuery>());

            AulaSessao aulaSessao = await GetFirstAsync(request, cancellationToken);

            Validator(aulaSessao, cancellationToken);

            SelecionarAulaSessaoByIdQueryResponse response = new SelecionarAulaSessaoByIdQueryResponse();
            response.Ordem = aulaSessao.Ordem;
            response.Titulo = aulaSessao.Titulo;
            response.Conteudo = aulaSessao.Conteudo;
            response.Favoritado = aulaSessao.Favoritado;
            response.AulaSessaoTipo = aulaSessao.AulaSessaoTipo;
            response.DataCadastro = aulaSessao.DataCadastro;
            response.DataAtualizacao = aulaSessao.DataAtualizacao;
            response.Id = aulaSessao.Id;

            return response;
        }

        private async void Validator
        (
            AulaSessao aulaSessao,
            CancellationToken cancellationToken
        )
        {
            if (aulaSessao is null) throw new ArgumentNullException("Aula sessão não encontrado");
        }

        private async Task<AulaSessao> GetFirstAsync
        (
            SelecionarAulaSessaoByIdQuery request,
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
