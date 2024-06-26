﻿using Application.Commons.Interfaces;
using Domain.Common.Exceptions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Procedures.Queries
{
    public record GetProcedureByIdQuery(int procedureId) : IRequest<Procedure>;

    public class GetProcedureByIdQueryHandler : IRequestHandler<GetProcedureByIdQuery, Procedure>
    {
        private readonly IApplicationDbContext _context;

        public GetProcedureByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Procedure> Handle(GetProcedureByIdQuery request, CancellationToken cancellationToken)
        {
            Procedure? entity = await _context.Procedures
                .Include(procedure => procedure.Attachments)
                .Include(procedure => procedure.StatusHistory)
                .FirstOrDefaultAsync(procedure => procedure.Id == request.procedureId, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(request.procedureId.ToString(), nameof(Procedure));
            }

            return entity;
        }
    }
}
