using ParsMedeQ.Application.Persistance.Schema.UserRepositories;
using ParsMedeQ.Domain.Aggregates.UserAggregate;
using ParsMedeQ.Infrastructure.Persistance.DbContexts;
using ParsMedeQ.Infrastructure.Schemas;
using SRH.Persistance.Extensions;

namespace ParsMedeQ.Infrastructure.Persistance.Repositories.UserRepositories;


internal sealed class UserReadRepository : GenericDomainEntityReadRepositoryBase<User>, IUserReadRepository
{
    public UserReadRepository(ReadDbContext dbContext) : base(dbContext) { }
    public ValueTask<PrimitiveResult> EnsureEmailIsUnique(int id, EmailType email, CancellationToken cancellationToken)
    {
        return this.DbContext.
            Users
            .Where(user => !user.Id.Equals(id) && user.Email.Equals(email))
            .Select(a => a.Id)
            .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveResult.Success(0))
            .Match(
                userId => userId.Equals(0) ? PrimitiveResult.Success(true) : PrimitiveResult.Failure<bool>("", "پست الکترونیکی تکراری است"),
                errors => PrimitiveResult.Failure<bool>("", "خطا در بررسی پست الکترونیکی تکرای")
            ).ToPrimitiveResult();
    }
    public ValueTask<PrimitiveResult> EnsureMobileIsUnique(int id, MobileType mobile, CancellationToken cancellationToken)
    {
        return this.DbContext.Users
            .Where(user => !user.Id.Equals(id) && user.Mobile.Equals(mobile))
            .Select(a => a.Id)
            .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveResult.Success(0))
            .Match(
                userId => userId.Equals(0) ? PrimitiveResult.Success(true) : PrimitiveResult.Failure<bool>("", "موبایل تکراری است"),
                errors => PrimitiveResult.Failure<bool>("", "خطا در بررسی موبایل تکرای"))
            .ToPrimitiveResult();
    }
    public async ValueTask<PrimitiveResult<User>> FindByEmail(EmailType email, CancellationToken cancellationToken)
    {
        var result = await UserReadRepositoryCompiledQueries.FindTsepUserByEmailCompiledQuery(
            this.DbContext,
            email,
            cancellationToken)
            .ConfigureAwait(false);

        if (result is null)
        {
            return PrimitiveResult.Failure<User>("", "کاربری با شماره موبایل ارسالی پیدا نشد");
        }
        return result!;
    }
    public async ValueTask<PrimitiveResult<User>> FindByMobile(MobileType mobile, CancellationToken cancellationToken)
    {
        var result = await UserReadRepositoryCompiledQueries.FindTsepUserByMobileCompiledQuery(
            this.DbContext,
            mobile,
            cancellationToken)
            .ConfigureAwait(false);

        if (result is null)
        {
            return PrimitiveResult.Failure<User>("", "کاربری با شماره موبایل ارسالی پیدا نشد");
        }
        return result!;
    }
    public ValueTask<PrimitiveResult<User>> FindUser(int id, CancellationToken cancellationToken)
    {
        return this.DbContext.Users
            .Where(a => a.Id.Equals(id))
            .AsSplitQuery()
            .Run(q => q.FirstOrDefaultAsync(cancellationToken), PrimitiveError.Create("", "کاربری با شناسه مورد نظر پیدا نشد"))
            .Map(a => a!);
    }
}
internal static class UserReadRepositoryCompiledQueries
{
    public static Func<ReadDbContext, MobileType, CancellationToken, Task<User?>> FindTsepUserByMobileCompiledQuery =>
        EF.CompileAsyncQuery((ReadDbContext dbcontext, MobileType mobile, CancellationToken cancellationToken) =>
            dbcontext.Users
                .Where(user => user.Mobile.Equals(mobile) && user.IsMobileConfirmed)
                .FirstOrDefault());
    public static Func<ReadDbContext, EmailType, CancellationToken, Task<User?>> FindTsepUserByEmailCompiledQuery =>
        EF.CompileAsyncQuery((ReadDbContext dbcontext, EmailType email, CancellationToken cancellationToken) =>
            dbcontext.Users
                .Where(user => user.Email.Equals(email) && user.IsEmailConfirmed)
                .FirstOrDefault());
}
