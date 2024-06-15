using Application.Commons.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Application.Procedures.Commands
{
    public record CreateProcedureCommand(byte[]? attachment) : IRequest<Procedure>;

    public class CreateProcedureCommandHandler : IRequestHandler<CreateProcedureCommand, Procedure>
    {
        private readonly IApplicationDbContext _context;

        public CreateProcedureCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Procedure> Handle(CreateProcedureCommand request, CancellationToken cancellationToken)
        {
            var entity = Procedure.Create();

            if (request.attachment is not null)
            {
                entity.AddAttachment(request.attachment);
            }

            entity.AddDomainEvent(new ProcedureCreatedEvent(entity));

            _context.Procedures.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
