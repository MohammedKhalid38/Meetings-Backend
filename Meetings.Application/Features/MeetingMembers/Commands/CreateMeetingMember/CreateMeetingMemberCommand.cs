using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.MeetingMembers.DTOs;
using Meetings.Domain.Entities;

namespace Meetings.Application.Features.MeetingMembers.Commands.CreateMeetingMember;

public record CreateMeetingMemberCommand(Guid MeetingId, Guid UserId, string Role) : IRequest<MeetingMemberDto>;

public class CreateMeetingMemberCommandHandler(IApplicationDbContext db) : IRequestHandler<CreateMeetingMemberCommand, MeetingMemberDto>
{
    public async Task<MeetingMemberDto> Handle(CreateMeetingMemberCommand request, CancellationToken cancellationToken)
    {
        var member = new MeetingMember { Id = Guid.NewGuid(), MeetingId = request.MeetingId, UserId = request.UserId, Role = request.Role, Status = "Invited", CreatedAt = DateTime.UtcNow };
        db.MeetingMembers.Add(member);
        await db.SaveChangesAsync(cancellationToken);
        return new MeetingMemberDto(member.Id, member.MeetingId, member.UserId, member.Role, member.Status, member.JoinedAt, member.CreatedAt);
    }
}
