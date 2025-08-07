using EShop.Application.Persistance.ESopSchema;
using EShop.Domain.Aggregates.UserAggregate.Validators;

namespace EShop.Infrastructure.Services.DomainServices.UserServices;

internal sealed class UserValidatorService : IUserValidatorService
{
    private readonly IEShopReadUnitOfWork _eshopReadUnitOfWork;

    public UserValidatorService(IEShopReadUnitOfWork eshopReadUnitOfWork) => this._eshopReadUnitOfWork = eshopReadUnitOfWork;

    public ValueTask<PrimitiveResult> IsEmailUnique(UserIdType id, EmailType email, CancellationToken cancellationToken) =>
        this._eshopReadUnitOfWork.UserReadRepository.EnsureEmailIsUnique(id, email, cancellationToken);

    public ValueTask<PrimitiveResult> IsPhonenumberUnique(UserIdType id, MobileType mobile, CancellationToken cancellationToken) =>
        this._eshopReadUnitOfWork.UserReadRepository.EnsureMobileIsUnique(id, mobile, cancellationToken);
}
