using Meetings.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Board> Boards { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
