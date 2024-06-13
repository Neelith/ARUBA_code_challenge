using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commons.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Procedure> Procedures { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
