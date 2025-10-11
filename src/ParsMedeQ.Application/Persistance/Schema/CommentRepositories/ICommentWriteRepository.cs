using ParsMedeQ.Domain.Aggregates.CommentAggregate;
using ParsMedeQ.Domain.Persistance;

namespace ParsMedeQ.Application.Persistance.Schema.CommentRepositories;
public interface ICommentWriteRepository : IDomainRepository
{
    ValueTask<PrimitiveResult<Comment>> AddComment(Comment comment);
}
