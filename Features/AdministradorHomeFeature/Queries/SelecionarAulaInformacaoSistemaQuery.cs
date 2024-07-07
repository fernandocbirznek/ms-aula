using MediatR;
using ms_aula.Domains;
using ms_aula.Features.AreaFisicaDivisaoFeature.Queries;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AdministradorHomeFeature.Queries
{
    public class SelecionarAulaInformacaoSistemaQuery
        : IRequest<SelecionarAulaInformacaoSistemaQueryResponse>
    {
    }

    public class SelecionarAulaInformacaoSistemaQueryResponse
    {
        public long AreaFisicaCount { get; set; }
        public long ArquivoPdfCount { get; set; }
        public long AulaComentarioCount { get; set; }
        public long AulaFavoritadaCount { get; set; }
        public long AulaCount { get; set; }
        public long AulaSessaoCount { get; set; }
        public long AulaSessaoFavoritadaCount { get; set; }
        public long AulaCurtidoCount { get; set; }
        public long WidgetConcluidoCount { get; set; }
        public long WidgetCursandoCount { get; set; }
        public long WidgetCursarCount { get; set; }
    }

    public class SelecionarAulaInformacaoSistemaQueryHandler
        : IRequestHandler<SelecionarAulaInformacaoSistemaQuery,
            SelecionarAulaInformacaoSistemaQueryResponse>
    {
        private readonly IRepository<AreaFisicaDivisao> _repository;
        private readonly IRepository<ArquivoPdf> _repositoryArquivoPdf;
        private readonly IRepository<AulaComentario> _repositoryAulaComentario;
        private readonly IRepository<AulaFavoritada> _repositoryAulaFavoritada;
        private readonly IRepository<Aula> _repositoryAula;
        private readonly IRepository<AulaSessao> _repositoryAulaSessao;
        private readonly IRepository<AulaSessaoFavoritada> _repositoryAulaSessaoFavoritada;
        private readonly IRepository<UsuarioAulaCurtido> _repositoryUsuarioAulaCurtido;
        private readonly IRepository<WidgetConcluido> _repositoryWidgetConcluido;
        private readonly IRepository<WidgetCursando> _repositoryWidgetCursando;
        private readonly IRepository<WidgetCursar> _repositoryWidgetCursar;

        public SelecionarAulaInformacaoSistemaQueryHandler
        (
            IRepository<AreaFisicaDivisao> repository,
            IRepository<ArquivoPdf> repositoryArquivoPdf,
            IRepository<AulaComentario> repositoryAulaComentario,
            IRepository<AulaFavoritada> repositoryAulaFavoritada,
            IRepository<Aula> repositoryAula,
            IRepository<AulaSessao> repositoryAulaSessao,
            IRepository<AulaSessaoFavoritada> repositoryAulaSessaoFavoritada,
            IRepository<UsuarioAulaCurtido> repositoryUsuarioAulaCurtido,
            IRepository<WidgetConcluido> repositoryWidgetConcluido,
            IRepository<WidgetCursando> repositoryWidgetCursando,
            IRepository<WidgetCursar> repositoryWidgetCursar
        )
        {
            _repository = repository;
            _repositoryArquivoPdf = repositoryArquivoPdf;
            _repositoryAulaComentario = repositoryAulaComentario;
            _repositoryAulaFavoritada = repositoryAulaFavoritada;
            _repositoryAula = repositoryAula;
            _repositoryAulaSessao = repositoryAulaSessao;
            _repositoryAulaSessaoFavoritada = repositoryAulaSessaoFavoritada;
            _repositoryUsuarioAulaCurtido = repositoryUsuarioAulaCurtido;
            _repositoryWidgetConcluido = repositoryWidgetConcluido;
            _repositoryWidgetCursando = repositoryWidgetCursando;
            _repositoryWidgetCursar = repositoryWidgetCursar;
        }

        public async Task<SelecionarAulaInformacaoSistemaQueryResponse> Handle
        (
            SelecionarAulaInformacaoSistemaQuery request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<SelecionarAulaInformacaoSistemaQuery>());

            var response = new SelecionarAulaInformacaoSistemaQueryResponse();
            response.AreaFisicaCount = await _repository.CountAsync(cancellationToken);
            response.ArquivoPdfCount = await _repositoryArquivoPdf.CountAsync(cancellationToken);
            response.AulaComentarioCount = await _repositoryAulaComentario.CountAsync(cancellationToken);
            response.AulaFavoritadaCount = await _repositoryAulaFavoritada.CountAsync(cancellationToken);
            response.AulaCount = await _repositoryAula.CountAsync(cancellationToken);
            response.AulaSessaoCount = await _repositoryAulaSessao.CountAsync(cancellationToken);
            response.AulaSessaoFavoritadaCount = await _repositoryAulaSessaoFavoritada.CountAsync(cancellationToken);
            response.AulaCurtidoCount = await _repositoryUsuarioAulaCurtido.CountAsync(cancellationToken);
            response.WidgetConcluidoCount = await _repositoryWidgetConcluido.CountAsync(cancellationToken);
            response.WidgetCursandoCount = await _repositoryWidgetCursando.CountAsync(cancellationToken);
            response.WidgetCursarCount = await _repositoryWidgetCursar.CountAsync(cancellationToken);

            return response;
        }
    }
}
