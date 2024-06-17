using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commons.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Procedure> Procedures { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
