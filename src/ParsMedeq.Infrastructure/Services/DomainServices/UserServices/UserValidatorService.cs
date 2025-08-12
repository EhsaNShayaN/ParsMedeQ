using ParsMedeq.Application.Persistance.ESopSchema;
using ParsMedeq.Domain.Aggregates.UserAggregate.Validators;

namespace ParsMedeq.Infrastructure.Services.DomainServices.UserServices;

internal sealed class UserValidatorService : IUserValidatorService
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public UserValidatorService(IReadUnitOfWork readUnitOfWork) => this._readUnitOfWork = readUnitOfWork;

    public ValueTask<PrimitiveResult> IsEmailUnique(UserIdType id, EmailType email, CancellationToken cancellationToken) =>
        this._readUnitOfWork.UserReadRepository.EnsureEmailIsUnique(id, email, cancellationToken);

    public ValueTask<PrimitiveResult> IsPhonenumberUnique(UserIdType id, MobileType mobile, CancellationToken cancellationToken) =>
        this._readUnitOfWork.UserReadRepository.EnsureMobileIsUnique(id, mobile, cancellationToken);
}
