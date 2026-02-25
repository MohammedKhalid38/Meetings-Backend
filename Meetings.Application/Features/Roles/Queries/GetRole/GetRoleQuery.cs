using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Roles.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Roles.Queries.GetRole;

public record GetRoleQuery(Guid Id) : IRequest<RoleDto?>;

public class GetRoleQueryHandler(IApplicationDbContext db) : IRequestHandler<GetRoleQuery, RoleDto?>
{
    public async Task<RoleDto?> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        => await db.Roles
            .Where(r => r.Id == request.Id)
            .Select(r => new RoleDto(r.Id, r.Name!, r.Description, r.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);
}
