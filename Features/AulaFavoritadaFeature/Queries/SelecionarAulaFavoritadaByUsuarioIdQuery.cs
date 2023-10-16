using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;
using System.Collections.Generic;

namespace ms_aula.Features.AulaFavoritadaFeature.Queries
{
    public class SelecionarAulaFavoritadaByUsuarioIdQuery : IRequest<IEnumerable<SelecionarAulaFavoritadaByUsuarioIdQueryResponse>>
    {
        public long Id { get; set; }
    }

    public class SelecionarAulaFavoritadaByUsuarioIdQueryResponse : Entity
    {
        public Aula? Aula { get; set; }
    }

    public class SelecionarAulaFavoritadaByUsuarioIdQueryHandler : IRequestHandler<SelecionarAulaFavoritadaByUsuarioIdQuery, IEnumerable<SelecionarAulaFavoritadaByUsuarioIdQueryResponse>>
    {
        private readonly IRepository<AulaFavoritada> _repository;
        private readonly IRepository<AreaFisica> _repositoryAreaFisica;

        public SelecionarAulaFavoritadaByUsuarioIdQueryHandler
        (
            IRepository<AulaFavoritada> repository,
            IRepository<AreaFisica> repositoryAreaFisica
        )
        {
            _repository = repository;
            _repositoryAreaFisica = repositoryAreaFisica;
        }

        public async Task<IEnumerable<SelecionarAulaFavoritadaByUsuarioIdQueryResponse>> Handle
        (
            SelecionarAulaFavoritadaByUsuarioIdQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarAulaFavoritadaByUsuarioIdQuery>());

            IEnumerable<AulaFavoritada> aulaFavoritadaMany = await GetAsync(request, cancellationToken);
            IEnumerable<AreaFisica> areaFisicaMany = await GetAreaFisicaAsync(cancellationToken);

            List<SelecionarAulaFavoritadaByUsuarioIdQueryResponse> responseMany = new List<SelecionarAulaFavoritadaByUsuarioIdQueryResponse>();

            foreach (AulaFavoritada aulaFavoritada in aulaFavoritadaMany)
            {
                SelecionarAulaFavoritadaByUsuarioIdQueryResponse response = new SelecionarAulaFavoritadaByUsuarioIdQueryResponse();
                response.Aula = aulaFavoritada.Aula;
                response.Aula.AreaFisica = GetAreaFisica(aulaFavoritada, areaFisicaMany);
                response.DataCadastro = aulaFavoritada.DataCadastro;
                response.DataAtualizacao = aulaFavoritada.DataAtualizacao;
                response.Id = aulaFavoritada.Id;
                responseMany.Add(response);
            }

            return responseMany;
        }

        private async Task<IEnumerable<AulaFavoritada>> GetAsync
        (
            SelecionarAulaFavoritadaByUsuarioIdQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAsync
                (
                    item => item.UsuarioId.Equals(request.Id),
                    cancellationToken,
                    item => item.Aula
                );
        }

        private async Task<IEnumerable<AreaFisica>> GetAreaFisicaAsync
        (
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAreaFisica.GetAsync
                (
                    cancellationToken
                );
        }

        private AreaFisica GetAreaFisica
        (
            AulaFavoritada aulaFavoritada,
            IEnumerable<AreaFisica> areaFisicaMany
        )
        {
            return areaFisicaMany.FirstOrDefault(item => item.Id.Equals(aulaFavoritada.Aula.AreaFisicaId));
        }
    }
}
