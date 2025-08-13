using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.UserAggregate.Events;
using ParsMedeQ.Domain.Aggregates.UserAggregate.Validators;
using ParsMedeQ.Domain.Types.Email;
using ParsMedeQ.Domain.Types.FirstName;
using ParsMedeQ.Domain.Types.FullName;
using ParsMedeQ.Domain.Types.LastName;
using ParsMedeQ.Domain.Types.Mobile;
using ParsMedeQ.Domain.Types.Password;
using ParsMedeQ.Domain.Types.UserId;

namespace ParsMedeQ.Domain.Aggregates.UserAggregate.UserEntity;
public sealed class User : AggregateRoot<UserIdType>
{
    #region " Fields "
    #endregion

    #region " Properties "
    /// <summary>
    /// شناسه کاریر ثبت نام کننده
    /// </summary>
    public UserIdType RegistrantId { get; private set; }

    /// <summary>
    /// نام کامل کاربر
    /// </summary>
    public FullNameType FullName { get; private set; }

    /// <summary>
    /// ایمیل
    /// </summary>
    public EmailType Email { get; private set; } = EmailType.Empty;

    /// <summary>
    /// موبایل
    /// </summary>
    public MobileType Mobile { get; private set; } = MobileType.Empty;

    /// <summary>
    /// رمز عبور
    /// </summary>
    public PasswordType Password { get; private set; }

    /// <summary>
    /// آیا ایمیل تایید شده است؟ 
    /// </summary>
    public bool IsEmailConfirmed { get; private set; } = false;

    /// <summary>
    /// آیا شماره موبایل تایید شده است؟
    /// </summary>
    public bool IsMobileConfirmed { get; private set; } = false;

    #endregion

    private User(UserIdType id) : base(id) { }
    public User() : this(null) { }

    private static ValueTask<PrimitiveResult<User>> Create(
        UserIdType registrantId,
        MobileType mobile,
        FullNameType fullName,
        PasswordType password,
        bool isEmailConfirmed,
        bool isMobielConfirmed,
        IUserValidatorService validator,
        CancellationToken cancellationToken)
    {
        return PrimitiveResult.Success(new User()
        {
            Mobile = mobile,
            FullName = fullName,
            Password = password,
            IsEmailConfirmed = isEmailConfirmed,
            IsMobileConfirmed = isMobielConfirmed,
            RegistrantId = registrantId,
        }).Map(user => user.Validate(validator, cancellationToken));
    }

    public static ValueTask<PrimitiveResult<User>> CreateUser(
        UserIdType registrantId,
        MobileType mobile,
        FullNameType fullName,
        PasswordType password,
        IUserValidatorService validator,
        CancellationToken cancellationToken) => Create(
            registrantId,
            mobile,
            fullName,
            password,
            false,
            true,
            validator,
            cancellationToken);

    public static ValueTask<PrimitiveResult<User>> CreateUnknownUser(
        UserIdType registrantId,
        MobileType mobile,
        IUserValidatorService validator,
        CancellationToken cancellationToken) => Create(
            registrantId,
            mobile,
            FullNameType.Empty,
            PasswordType.Empty,
            false,
            true,
            validator,
            cancellationToken);


    public ValueTask<PrimitiveResult<User>> UpdateProfile(
        FirstNameType firstName,
        LastNameType lastName,
        EmailType email,
        IUserValidatorService userValidatorService,
        CancellationToken cancellationToken)
    {
        /*
            1 : Fullname misazim, agar dorost sakhte shod boro marhale bad
            2 : Validate mikonim ke Email tekrari nabashe, agar tekrari nabood boro marhale bad
            3 : Event e UserProfileUpdatedEvent ro add mikonim
            3 : akhar sar, kole object ke update shode to barmigardoonim
         */
        return FullNameType.Create(firstName.Value, lastName.Value)
            .OnSuccess(fullname =>
            {
                this.FullName = fullname.Value;
                this.Email = email;
            })
            .Map(_ => userValidatorService.IsEmailUnique(this.Id, this.Email, cancellationToken))
            .OnSuccess(_ => this.AddDomainEvent(new UserReferencedProfileUpdatedEvent(this)))
            .Map(_ => this);
    }

    /// <summary>
    /// اعتبار سنجی کاربر
    /// </summary>
    public ValueTask<PrimitiveResult<User>> Validate(
        IUserValidatorService validator,
        CancellationToken cancellationToken)
    {
        return PrimitiveResult.Success(this)
            .Ensure([() => validator.IsPhonenumberUnique(this.Id, this.Mobile, cancellationToken).Map(() => this)]);
    }

    public ValueTask<PrimitiveResult<User>> UpdatePassword(
      string password,
      string salt,
      IUserValidatorService validator)
    {
        return PasswordType.Create(password, salt)
            .Map(newPass =>
            {
                this.Password = newPass;
                return this;
            });
    }
}