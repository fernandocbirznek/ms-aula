using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AreaFisicaFeature.Queries
{
    public class SelecionarAreaFisicaByIdQuery : IRequest<SelecionarAreaFisicaByIdQueryResponse>
    {
        public long Id { get; set; }
    }

    public class SelecionarAreaFisicaByIdQueryResponse : Entity
    {
        public string Descricao { get; set; }
    }

    public class SelecionarAreaFisicaByIdQueryHandler : IRequestHandler<SelecionarAreaFisicaByIdQuery, SelecionarAreaFisicaByIdQueryResponse>
    {
        private readonly IRepository<AreaFisica> _repository;

        public SelecionarAreaFisicaByIdQueryHandler
        (
            IRepository<AreaFisica> repository
        )
        {
            _repository = repository;
        }

        public async Task<SelecionarAreaFisicaByIdQueryResponse> Handle
        (
            SelecionarAreaFisicaByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarAreaFisicaByIdQuery>());

            AreaFisica areaFisica = await GetFirstAsync(request, cancellationToken);

            Validator(areaFisica, cancellationToken);

            SelecionarAreaFisicaByIdQueryResponse response = new SelecionarAreaFisicaByIdQueryResponse();

            response.Descricao = areaFisica.Descricao;
            response.DataCadastro = areaFisica.DataCadastro;
            response.DataAtualizacao = areaFisica.DataAtualizacao;
            response.Id = areaFisica.Id;

            return response;
        }

        private async void Validator
        (
            AreaFisica areaFisica,
            CancellationToken cancellationToken
        )
        {
            if (areaFisica is null) throw new ArgumentNullException("Fórum não encontrado");
        }

        private async Task<AreaFisica> GetFirstAsync
        (
            SelecionarAreaFisicaByIdQuery request,
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
