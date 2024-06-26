﻿using MediatR;
using ms_aula.Domains;
using ms_aula.Features.AulaFeature.Commands;
using ms_aula.Helpers;
using ms_aula.Interface;

namespace ms_aula.Features.AulaFavoritadaFeature.Commands
{
    public class RemoverAulaFavoritadaCommand : IRequest<long>
    {
        public long Id { get; set; }
    }

    public class RemoverAulaFavoritadaCommandHandler
        : IRequestHandler<RemoverAulaFavoritadaCommand, long>
    {
        private IMediator _mediator;

        private readonly IRepository<AulaFavoritada> _repository;

        public RemoverAulaFavoritadaCommandHandler
        (
            IMediator mediator,

            IRepository<AulaFavoritada> repository
        )
        {
            _mediator = mediator;

            _repository = repository;
        }

        public async Task<long> Handle
        (
            RemoverAulaFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request is null)
                throw new ArgumentNullException(MessageHelper.NullFor<RemoverAulaFavoritadaCommand>());

            await Validator(request, cancellationToken);

            AulaFavoritada aulaFavoritada = await _repository.GetFirstAsync
                (
                    item => item.Id.Equals(request.Id),
                    cancellationToken
                );

            await _mediator.Send(new AtualizarAulaFavoritadaCommand { Id = aulaFavoritada.AulaId, Adicionar = false });

            await _repository.RemoveAsync(aulaFavoritada);
            await _repository.SaveChangesAsync(cancellationToken);

            return aulaFavoritada.Id;
        }

        private async Task Validator
        (
            RemoverAulaFavoritadaCommand request,
            CancellationToken cancellationToken
        )
        {
            if (!await ExistsAsync(request, cancellationToken)) throw new ArgumentNullException("Aula favoritada não encontrada");
        }

        private async Task<bool> ExistsAsync
        (
            RemoverAulaFavoritadaCommand request,
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
