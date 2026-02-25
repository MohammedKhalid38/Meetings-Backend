using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Users.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.Users.Queries.GetUser;

public record GetUserQuery(Guid Id) : IRequest<UserDto?>;

public class GetUserQueryHandler(IApplicationDbContext db) : IRequestHandler<GetUserQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserQuery request, CancellationToken cancellationToken)
        => await db.Users
            .Where(u => u.Id == request.Id)
            .Select(u => new UserDto(u.Id, u.FirstName, u.LastName, u.Email!, u.UserName!, u.ProfilePicture, u.IsActive, u.CreatedAt, u.UpdatedAt))
            .FirstOrDefaultAsync(cancellationToken);
}
