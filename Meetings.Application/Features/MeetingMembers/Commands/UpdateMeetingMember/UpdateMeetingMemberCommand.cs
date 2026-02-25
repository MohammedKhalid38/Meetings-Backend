using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.MeetingMembers.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Meetings.Application.Features.MeetingMembers.Commands.UpdateMeetingMember;

public record UpdateMeetingMemberCommand(Guid Id, string Role, string Status, DateTime? JoinedAt) : IRequest<MeetingMemberDto?>;

public class UpdateMeetingMemberCommandHandler(IApplicationDbContext db) : IRequestHandler<UpdateMeetingMemberCommand, MeetingMemberDto?>
{
    public async Task<MeetingMemberDto?> Handle(UpdateMeetingMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await db.MeetingMembers.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
        if (member is null) return null;
        member.Role = request.Role; member.Status = request.Status; member.JoinedAt = request.JoinedAt;
        await db.SaveChangesAsync(cancellationToken);
        return new MeetingMemberDto(member.Id, member.MeetingId, member.UserId, member.Role, member.Status, member.JoinedAt, member.CreatedAt);
    }
}
