using Application.Commons.Interfaces;
using Domain.Common.Exceptions;
using Domain.Entities;
using Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Procedures.Commands
{
    public record DeleteProcedureCommand(int procedureId) : IRequest;

    public class DeleteProcedureCommandHandler : IRequestHandler<DeleteProcedureCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProcedureCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteProcedureCommand request, CancellationToken cancellationToken)
        {
            Procedure? entity = await _context.Procedures.FindAsync(request.procedureId, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(request.procedureId.ToString(), nameof(Procedure));
            }

            _context.Procedures.Remove(entity);

            entity.AddDomainEvent(new ProcedureDeletedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}