using MediatR;
using Meetings.Application.Common.Interfaces;
using Meetings.Application.Features.Boards.DTOs;
using Meetings.Domain.Entities;

namespace Meetings.Application.Features.Boards.Commands.CreateBoard;

public record CreateBoardCommand(string Name, string? Description) : IRequest<BoardDto>;
public class CreateBoardCommandHandler(IApplicationDbContext db) : IRequestHandler<CreateBoardCommand, BoardDto>
{
    public async Task<BoardDto> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        var board = new Board
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        db.Boards.Add(board);
        await db.SaveChangesAsync(cancellationToken);

        return new BoardDto(board.Id, board.Name, board.Description, board.IsActive, board.CreatedAt, board.UpdatedAt);
    }
}