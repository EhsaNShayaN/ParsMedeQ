using ParsMedeQ.Application.Services.UserContextAccessorServices;
using ParsMedeQ.Domain.Aggregates.CommentAggregate;
using ParsMedeQ.Domain.Aggregates.SectionAggregate;
using ParsMedeQ.Domain.Aggregates.SectionAggregate.Entities;

namespace ParsMedeQ.Application.Features.CommentFeatures.AddCommentFeature;
public sealed class AddCommentCommandHandler : IPrimitiveResultCommandHandler<AddCommentCommand, AddCommentCommandResponse>
{
    private readonly IWriteUnitOfWork _writeUnitOfWork;
    private readonly IUserContextAccessor _userContextAccessor;

    public AddCommentCommandHandler(
        IWriteUnitOfWork writeUnitOfWork,
        IUserContextAccessor userContextAccessor)
    {
        this._writeUnitOfWork = writeUnitOfWork;
        this._userContextAccessor = userContextAccessor;
    }

    public async Task<PrimitiveResult<AddCommentCommandResponse>> Handle(AddCommentCommand request, CancellationToken cancellationToken) =>
        await Comment.Create(
            this._userContextAccessor.GetCurrent().UserId,
            request.TableId,
            request.TableName,
            request.RelatedId,
            request.Icon,
            request.Description)
        .Map(comment => this._writeUnitOfWork.CommentWriteRepository.AddComment(comment)
        .Map(comment => this._writeUnitOfWork.SaveChangesAsync(CancellationToken.None).Map(_ => comment))
        .Map(comment => new AddCommentCommandResponse(comment is not null)))
        .ConfigureAwait(false);
}
