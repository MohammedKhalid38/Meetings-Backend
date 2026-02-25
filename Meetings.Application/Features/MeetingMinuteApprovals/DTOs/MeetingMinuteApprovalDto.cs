namespace Meetings.Application.Features.MeetingMinuteApprovals.DTOs;

public record MeetingMinuteApprovalDto(
    Guid Id,
    Guid MeetingMinuteId,
    Guid ApproverId,
    string Status,
    string? Comments,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
