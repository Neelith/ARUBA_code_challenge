using Application.Commons.Interfaces;
using Domain.Common.Exceptions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Procedures.Queries
{
    public record GetProcedureByIdQuery (int procedureId) : IRequest<Procedure>;

    public class GetProcedureByIdQueryHandler : IRequestHandler<GetProcedureByIdQuery, Procedure>
    {
        private readonly IApplicationDbContext _context;

        public GetProcedureByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Procedure> Handle(GetProcedureByIdQuery request, CancellationToken cancellationToken)
        {
            Procedure? entity = await _context.Procedures.FindAsync(request.procedureId, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(request.procedureId.ToString(), nameof(Procedure));
            }

            return entity;
        }
    }
}
