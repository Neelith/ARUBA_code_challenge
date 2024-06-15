using Application.Commons.Constants;
using Application.Commons.Interfaces;
using Domain.Common.Exceptions;
using Domain.Entities;
using Domain.Enum;
using Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Procedures.Commands
{
    public record UpdateProcedureCommand(int procedureId, ProcedureStatus? newStatus, int? attachmentId, byte[]? newAttachment) : IRequest;

    public class UpdateProcedureCommandHandler : IRequestHandler<UpdateProcedureCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProcedureCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateProcedureCommand request, CancellationToken cancellationToken)
        {
            if(request.newStatus is null && request.newAttachment is null && request.attachmentId is null)
            {
                throw new BadRequestException(ApplicationErrors.BadRequest);
            }

            Procedure? entity = await _context.Procedures.FindAsync(request.procedureId, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(request.procedureId.ToString(), nameof(Procedure));
            }

            if (request.newStatus.HasValue)
            {
                entity.CurrentProcedureStatus = request.newStatus.Value;
            }

            if (request.newAttachment is not null && request.attachmentId.HasValue)
            {
                entity.UpdateAttachment(request.attachmentId.Value, request.newAttachment);
            }
            
            if (request.newAttachment is not null && !request.attachmentId.HasValue)
            {
                entity.AddAttachment(request.newAttachment);
            }

            entity.AddDomainEvent(new ProcedureUpdatedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
