using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Users.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Users.Queries.GetUsers;

public record GetUsersQuery : IRequest<List<UserDto>>;

public class GetUsersQueryHandler(IApplicationDbContext db) : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        => await db.Users
            .Where(u => u.IsActive)
            .Select(u => new UserDto(u.Id, u.FirstName, u.LastName, u.Email!, u.UserName!, u.ProfilePicture, u.IsActive, u.CreatedAt, u.UpdatedAt))
            .ToListAsync(cancellationToken);
}
