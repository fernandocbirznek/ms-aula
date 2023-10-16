using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AreaFisicaFeature.Commands
{
    public class AtualizarAreaFisicaCommand : IRequest<AtualizarAreaFisicaCommandResponse>
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
    }

    public class AtualizarAreaFisicaCommandResponse
    {
        public DateTime DataAtualizacao { get; set; }
    }

    public class AtualizarAreaFisicaHandler : IRequestHandler<AtualizarAreaFisicaCommand, AtualizarAreaFisicaCommandResponse>
    {
        private readonly IRepository<AreaFisica> _repositoryAreaFisica;

        public AtualizarAreaFisicaHandler
        (
            IRepository<AreaFisica> repositoryAreaFisica
        )
        {
            _repositoryAreaFisica = repositoryAreaFisica;
        }

        public async Task<AtualizarAreaFisicaCommandResponse> Handle
        (
            AtualizarAreaFisicaCommand request, 
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAreaFisicaCommand>());

            await Validator(request, cancellationToken);

            AreaFisica areaFisica = await GetFirstAsync(request, cancellationToken);
            areaFisica.Descricao = request.Descricao;

            await _repositoryAreaFisica.UpdateAsync(areaFisica);
            await _repositoryAreaFisica.SaveChangesAsync(cancellationToken);

            AtualizarAreaFisicaCommandResponse response = new AtualizarAreaFisicaCommandResponse();
            response.DataAtualizacao = areaFisica.DataAtualizacao;

            return response;
        }

        private async Task Validator
        (
            AtualizarAreaFisicaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.Id <= 0) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAreaFisicaCommand>(item => item.Id));
            if (String.IsNullOrEmpty(request.Descricao)) throw new ArgumentNullException(MessageHelper.NullFor<AtualizarAreaFisicaCommand>(item => item.Descricao));
            if (!(await ExistsAsync(request, cancellationToken))) throw new ArgumentNullException("Área da Física não encontrada");
            if (await ExistsTituloAsync(request, cancellationToken)) throw new ArgumentNullException("Título já cadastrado");
        }

        private async Task<AreaFisica> GetFirstAsync
        (
            AtualizarAreaFisicaCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAreaFisica.GetFirstAsync
                (
                    item => item.Id.Equals(request.Id),
                    cancellationToken
                );
        }

        private async Task<bool> ExistsAsync
        (
            AtualizarAreaFisicaCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAreaFisica.ExistsAsync
                (
                    item => item.Id.Equals(request.Id),
                    cancellationToken
                );
        }

        private async Task<bool> ExistsTituloAsync
        (
            AtualizarAreaFisicaCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAreaFisica.ExistsAsync
                (
                    item => item.Descricao.ToLower().Trim().Equals(request.Descricao.ToLower().Trim()) &&
                    !item.Id.Equals(request.Id),
                    cancellationToken
                );
        }
    }
}
