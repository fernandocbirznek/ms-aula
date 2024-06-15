using MediatR;
using ms_aula.Domains;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.ArquivoPdfFeature.Commands
{
    public class RemoverArquivoPdfCommand : IRequest<long>
    {
        public long Id { get; set; }
    }

    public class RemoverArquivoPdfCommandHandler
        : IRequestHandler<RemoverArquivoPdfCommand, long>
    {
        private readonly IRepository<ArquivoPdf> _repository;

        public RemoverArquivoPdfCommandHandler
        (
            IRepository<ArquivoPdf> repository
        )
        {
            _repository = repository;
        }

        public async Task<long> Handle
        (
            RemoverArquivoPdfCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverArquivoPdfCommand>());

            await Validator(request, cancellationToken);

            ArquivoPdf arquivoPdf = await _repository.GetFirstAsync
            (
                item => item.Id.Equals(request.Id),
                cancellationToken
            );

            await _repository.RemoveAsync(arquivoPdf);
            await _repository.SaveChangesAsync(cancellationToken);

            return arquivoPdf.Id;
        }

        private async Task Validator
        (
            RemoverArquivoPdfCommand request,
            CancellationToken cancellationToken
        )
        {
            if (!await ExistsAsync(request, cancellationToken)) throw new ArgumentNullException("Arquivo pdf não encontrado");
        }

        private async Task<bool> ExistsAsync
        (
            RemoverArquivoPdfCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.ExistsAsync
                (
                    item => item.Id.Equals(request.Id),
                    cancellationToken
                );
        }
    }
}
