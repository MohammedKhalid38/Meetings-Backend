using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Roles.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Roles.Queries.GetRoles;

public record GetRolesQuery : IRequest<List<RoleDto>>;

public class GetRolesQueryHandler(IApplicationDbContext db) : IRequestHandler<GetRolesQuery, List<RoleDto>>
{
    public async Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        => await db.Roles
            .Select(r => new RoleDto(r.Id, r.Name!, r.Description, r.CreatedAt))
            .ToListAsync(cancellationToken);
}
