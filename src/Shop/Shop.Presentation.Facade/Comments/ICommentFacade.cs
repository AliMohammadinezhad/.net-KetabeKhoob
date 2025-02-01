using Common.Application;
using Shop.Application.Comments.ChangeStatus;
using Shop.Application.Comments.Create;
using Shop.Application.Comments.Edit;
using Shop.Query.Comments.DTOs;

namespace Shop.Presentation.Facade.Comments;

public interface ICommentFacade
{
    Task<OperationResult> ChangeStatus(ChangeCommentStatusCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> CreateComment(CreateCommentCommand command, CancellationToken cancellationToken = default);
    Task<OperationResult> EditComment(EditCommentCommand command, CancellationToken cancellationToken = default);

    Task<CommentDto?> GetCommentById(long id, CancellationToken cancellationToken = default);
    Task<CommentFilterResult> GetCommentByFilter(CommentFilterParams filterParams,
        CancellationToken cancellationToken = default);
}