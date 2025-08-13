using ParsMedeQ.Application.Persistance.Schema.UserRepositories;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using SRH.Persistance.Repositories.Write;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.UserRepositories;

internal sealed class UserWriteRepository : GenericPrimitiveWriteRepositoryBase<WriteDbContext>, IUserWriteRepository
{
    public UserWriteRepository(WriteDbContext dbContext) : base(dbContext) { }

    public ValueTask<PrimitiveResult<User>> AddUser(User src, CancellationToken cancellationToken) => this.Add(src);
    public ValueTask<PrimitiveResult<User>> FindById(UserIdType id, CancellationToken cancellationToken) =>
        this.FindByIdAsync<User, UserIdType>(id, cancellationToken);
    public async ValueTask<PrimitiveResult<User>> FindByMobile(MobileType mobile, CancellationToken cancellationToken) =>
        await this.FirstOrDefaultAsync<User>(u => u.Mobile == mobile, cancellationToken);
    public ValueTask<PrimitiveResult<User>> UpdatePassword(User src, CancellationToken cancellationToken) => this.Update(src);
    public ValueTask<PrimitiveResult<User>> UpdateUser(User src) => this.Update(src);
}