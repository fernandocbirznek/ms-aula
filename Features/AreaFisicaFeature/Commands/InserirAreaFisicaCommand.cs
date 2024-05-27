using MediatR;
using ms_aula.Domains;
using ms_aula.Extensions;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AreaFisicaFeature.Commands
{
    public class InserirAreaFisicaCommand : IRequest<InserirAreaFisicaCommandResponse>
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Aplicacao { get; set; }
    }

    public class InserirAreaFisicaCommandResponse
    {
        public long Id { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public class InserirAreaFisicaHandler : IRequestHandler<InserirAreaFisicaCommand, InserirAreaFisicaCommandResponse>
    {
        private readonly IRepository<AreaFisica> _repositoryAreaFisica;

        public InserirAreaFisicaHandler
        (
            IRepository<AreaFisica> repositoryAreaFisica
        )
        {
            _repositoryAreaFisica = repositoryAreaFisica;
        }

        public async Task<InserirAreaFisicaCommandResponse> Handle
        (
            InserirAreaFisicaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<InserirAreaFisicaCommand>());

            await Validator(request, cancellationToken);

            AreaFisica areaFisica = request.ToDomain();

            await _repositoryAreaFisica.AddAsync(areaFisica, cancellationToken);
            await _repositoryAreaFisica.SaveChangesAsync(cancellationToken);

            InserirAreaFisicaCommandResponse response = new InserirAreaFisicaCommandResponse();
            response.DataCadastro = areaFisica.DataCadastro;
            response.Id = areaFisica.Id;

            return response;
        }

        private async Task Validator
        (
            InserirAreaFisicaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (String.IsNullOrEmpty(request.Descricao)) throw new ArgumentNullException(MessageHelper.NullFor<InserirAreaFisicaCommand>(item => item.Descricao));
            if (await ExistsTituloAsync(request, cancellationToken)) throw new ArgumentNullException("Título já cadastrado");
        }

        private async Task<bool> ExistsTituloAsync
        (
            InserirAreaFisicaCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repositoryAreaFisica.ExistsAsync
                (
                    item => item.Descricao.ToLower().Trim().Equals(request.Descricao.ToLower().Trim()),
                    cancellationToken
                );
        }
    }
}
