using ParsMedeQ.Application.Persistance.Schema;
using ParsMedeQ.Domain.Aggregates.UserAggregate.Validators;

namespace ParsMedeQ.Infrastructure.Services.DomainServices.UserServices;

internal sealed class UserValidatorService : IUserValidatorService
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public UserValidatorService(IReadUnitOfWork readUnitOfWork) => this._readUnitOfWork = readUnitOfWork;

    public ValueTask<PrimitiveResult> IsEmailUnique(int id, EmailType email, CancellationToken cancellationToken) =>
        this._readUnitOfWork.UserReadRepository.EnsureEmailIsUnique(id, email, cancellationToken);

    public ValueTask<PrimitiveResult> IsPhonenumberUnique(int id, MobileType mobile, CancellationToken cancellationToken) =>
        this._readUnitOfWork.UserReadRepository.EnsureMobileIsUnique(id, mobile, cancellationToken);
}
