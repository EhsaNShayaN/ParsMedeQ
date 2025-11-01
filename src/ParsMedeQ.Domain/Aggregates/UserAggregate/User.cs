using ParsMedeQ.Domain.Abstractions;
using ParsMedeQ.Domain.Aggregates.CartAggregate;
using ParsMedeQ.Domain.Aggregates.CommentAggregate;
using ParsMedeQ.Domain.Aggregates.OrderAggregate;
using ParsMedeQ.Domain.Aggregates.ProductAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.PurchaseAggregate;
using ParsMedeQ.Domain.Aggregates.TicketAggregate;
using ParsMedeQ.Domain.Aggregates.TicketAggregate.Entities;
using ParsMedeQ.Domain.Aggregates.UserAggregate.Validators;
using ParsMedeQ.Domain.Types.Email;
using ParsMedeQ.Domain.Types.FirstName;
using ParsMedeQ.Domain.Types.FullName;
using ParsMedeQ.Domain.Types.LastName;
using ParsMedeQ.Domain.Types.Mobile;
using ParsMedeQ.Domain.Types.Password;

namespace ParsMedeQ.Domain.Aggregates.UserAggregate;
public sealed class User : AggregateRoot<int>
{
    #region " Fields "
    private List<Cart> _carts = [];
    private List<Comment> _comments = [];
    private List<Order> _orders = [];
    private List<Purchase> _purchases = [];
    private List<Ticket> _tickets = [];
    private List<TicketAnswer> _ticketAnswerss = [];
    private List<PeriodicService> _periodicServices = [];
    #endregion

    #region " Properties "

    /// <summary>
    /// نام کامل کاربر
    /// </summary>
    public FullNameType FullName { get; private set; } = FullNameType.Empty;

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
    public PasswordType Password { get; private set; } = PasswordType.Empty;

    /// <summary>
    /// آیا ایمیل تایید شده است؟ 
    /// </summary>
    public bool IsEmailConfirmed { get; private set; } = false;

    /// <summary>
    /// آیا شماره موبایل تایید شده است؟
    /// </summary>
    public bool IsMobileConfirmed { get; private set; } = false;

    #endregion

    #region " Navigation Properties "
    public IReadOnlyCollection<Cart> Carts => this._carts.AsReadOnly();
    public IReadOnlyCollection<Comment> Comments => this._comments.AsReadOnly();
    public IReadOnlyCollection<Order> Orders => this._orders.AsReadOnly();
    public IReadOnlyCollection<Purchase> Purchases => this._purchases.AsReadOnly();
    public IReadOnlyCollection<Ticket> Tickets => this._tickets.AsReadOnly();
    public IReadOnlyCollection<TicketAnswer> TicketAnswerss => this._ticketAnswerss.AsReadOnly();
    public IReadOnlyCollection<PeriodicService> PeriodicServices => this._periodicServices.AsReadOnly();
    #endregion

    private User(int id) : base(id) { }
    public User() : this(0) { }

    private static ValueTask<PrimitiveResult<User>> Create(
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
        }).Map(user => user.Validate(validator, cancellationToken));
    }

    public static ValueTask<PrimitiveResult<User>> CreateUser(
        MobileType mobile,
        FullNameType fullName,
        PasswordType password,
        IUserValidatorService validator,
        CancellationToken cancellationToken) => Create(
            mobile,
            fullName,
            password,
            false,
            true,
            validator,
            cancellationToken);

    public static ValueTask<PrimitiveResult<User>> CreateUnknownUser(
        MobileType mobile,
        IUserValidatorService validator,
        CancellationToken cancellationToken) => Create(
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