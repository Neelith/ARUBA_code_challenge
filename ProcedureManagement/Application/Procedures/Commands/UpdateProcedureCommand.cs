using Application.Commons.Exceptions;
using Application.Commons.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Procedures.Commands
{
    public record UpdateProcedureCommand(int procedureId) : IRequest;

    public class UpdateProcedureCommandHandler : IRequestHandler<UpdateProcedureCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProcedureCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateProcedureCommand request, CancellationToken cancellationToken)
        {
            Procedure? entity = await _context.Procedures.FindAsync(request.procedureId, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(request.procedureId.ToString(), nameof(Procedure));
            }

            //TODO: Add update logic here

            entity.AddDomainEvent(new ProcedureUpdatedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
