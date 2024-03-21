using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaSessaoFeature.Commands
{
    public class AtualizarAulaSessaoManyOrdemCommand : IRequest<AtualizarAulaSessaoManyOrdemCommandResponse>
    {
        public IEnumerable<AulaSessaoOrdem> AulaSessaoMany { get; set; }
    }

    public class AulaSessaoOrdem : Entity
    {
        public long Ordem { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public long Favoritado { get; set; }
        public AulaSessaoTipo AulaSessaoTipo { get; set; }
        public long AulaId { get; set; }
    }

    public class AtualizarAulaSessaoManyOrdemCommandResponse
    {
        public DateTime DataAtualizacao { get; set; }
    }

    public class AtualizarAulaSessaoManyOrdemHandler :
        IRequestHandler<AtualizarAulaSessaoManyOrdemCommand, AtualizarAulaSessaoManyOrdemCommandResponse>
    {
        private readonly IRepository<AulaSessao> _repository;
        private readonly IRepository<Aula> _repositoryAula;

        public AtualizarAulaSessaoManyOrdemHandler
        (
            IRepository<AulaSessao> repository,
            IRepository<Aula> repositoryAula
        )
        {
            _repository = repository;
            _repositoryAula = repositoryAula;
        }

        public async Task<AtualizarAulaSessaoManyOrdemCommandResponse> Handle
        (
            AtualizarAulaSessaoManyOrdemCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAulaSessaoManyOrdemCommand>());

            await Validator(request, cancellationToken);

            IEnumerable<AulaSessao> aulaSessaoMany = await GetAsync(request, cancellationToken);
            Aula aula = await GetFirstAsync(request, cancellationToken);

            IEnumerable<AulaSessao> aulaSessaoManyAlterado = new List<AulaSessao>();

            foreach (AulaSessao aulaSessao in aulaSessaoMany)
            {
                var aulaSessaoAlterar = request.AulaSessaoMany.First(item => item.Id.Equals(aulaSessao.Id));

                AulaSessao aulaSessaoAtualizada = aulaSessao;
                aulaSessaoAtualizada.Ordem = aulaSessaoAlterar.Ordem;
                aulaSessaoAtualizada.DataAtualizacao = DateTime.Now;

                await _repository.UpdateAsync(aulaSessaoAtualizada);
                await _repository.SaveChangesAsync(cancellationToken);

                aulaSessaoManyAlterado.Append(aulaSessaoAtualizada);
            }

            Aula aulaAlterada = aula;
            aulaAlterada.DataAtualizacao = DateTime.Now;

            await _repositoryAula.UpdateAsync(aulaAlterada);
            await _repositoryAula.SaveChangesAsync(cancellationToken);

            AtualizarAulaSessaoManyOrdemCommandResponse dataAtualizacao = new AtualizarAulaSessaoManyOrdemCommandResponse();
            dataAtualizacao.DataAtualizacao = aulaAlterada.DataAtualizacao;

            return dataAtualizacao;
        }

        private async Task Validator
        (
            AtualizarAulaSessaoManyOrdemCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.AulaSessaoMany.Count() <= 0) throw 
                    new ArgumentNullException
                        (
                            MessageHelper.NullFor<AtualizarAulaSessaoManyOrdemCommand>(item => item.AulaSessaoMany)
                        );
        }

        private async Task<IEnumerable<AulaSessao>> GetAsync
        (
            AtualizarAulaSessaoManyOrdemCommand request,
            CancellationToken cancellationToken
        )
        {

            return await _repository
                .GetAsync
                (
                    item => item.AulaId.Equals(request.AulaSessaoMany.First().AulaId),
                    cancellationToken
                );
        }

        private async Task<Aula> GetFirstAsync
        (
            AtualizarAulaSessaoManyOrdemCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAula
                .GetFirstAsync
                (
                    item => item.Id.Equals(request.AulaSessaoMany.First().AulaId),
                    cancellationToken
                );
        }
    }
}
