namespace Meetings.Application.Features.MeetingMinuteSignatures.DTOs;

public record MeetingMinuteSignatureDto(
    Guid Id,
    Guid MeetingMinuteId,
    Guid SignedBy,
    string? SignatureData,
    DateTime SignedAt
);
