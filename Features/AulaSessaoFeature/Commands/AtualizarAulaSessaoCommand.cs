using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaSessaoFeature.Commands
{
    public class AtualizarAulaSessaoCommand : IRequest<AtualizarAulaSessaoCommandResponse>
    {
        public long Id { get; set; }
        public long Ordem { get; set; }
        public string Conteudo { get; set; }
        public AulaSessaoTipo AulaSessaoTipo { get; set; }
    }

    public class AtualizarAulaSessaoCommandResponse
    {
        public DateTime DataAtualizacao { get; set; }
    }

    public class AtualizarAulaSessaoHandler : IRequestHandler<AtualizarAulaSessaoCommand, AtualizarAulaSessaoCommandResponse>
    {
        private readonly IRepository<AulaSessao> _repository;
        private readonly IRepository<Aula> _repositoryAula;

        public AtualizarAulaSessaoHandler
        (
            IRepository<AulaSessao> repository,
            IRepository<Aula> repositoryAula
        )
        {
            _repository = repository;
            _repositoryAula = repositoryAula;
        }

        public async Task<AtualizarAulaSessaoCommandResponse> Handle
        (
            AtualizarAulaSessaoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaSessaoCommand>());

            await Validator(request, cancellationToken);

            AulaSessao aulaSessao = await GetFirstAsync(request, cancellationToken);
            aulaSessao.Ordem = request.Ordem;
            aulaSessao.Conteudo = request.Conteudo;
            aulaSessao.AulaSessaoTipo = request.AulaSessaoTipo;

            await _repository.UpdateAsync(aulaSessao);
            await _repository.SaveChangesAsync(cancellationToken);

            AtualizarAulaSessaoCommandResponse response = new AtualizarAulaSessaoCommandResponse();
            response.DataAtualizacao = aulaSessao.DataAtualizacao;

            return response;
        }

        private async Task Validator
        (
            AtualizarAulaSessaoCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id <= 0) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaSessaoCommand>(item => item.Id));
            if (String.IsNullOrEmpty(request.Conteudo)) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaSessaoCommand>(item => item.Conteudo));
            if (request.Ordem <= 0) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaSessaoCommand>(item => item.Ordem));
            if (request.AulaSessaoTipo < 0) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaSessaoCommand>(item => item.AulaSessaoTipo));
        }

        private async Task<AulaSessao> GetFirstAsync
        (
            AtualizarAulaSessaoCommand request,
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
