using ParsMedeQ.Application.Persistance.Schema.CommentRepositories;
using ParsMedeQ.Domain.Aggregates.CommentAggregate;
using ParsMedeQ.Domain.Aggregates.SectionAggregate;
using ParsMedeQ.Domain.Aggregates.SectionAggregate.Entities;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.CommentRepositories;
internal sealed class CommentWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, ICommentWriteRepository
{
    public CommentWriteRepository(WriteDbContext dbContext) : base(dbContext) { }

    public ValueTask<PrimitiveResult<Comment>> AddComment(Comment comment) => this.Add(comment);
}
