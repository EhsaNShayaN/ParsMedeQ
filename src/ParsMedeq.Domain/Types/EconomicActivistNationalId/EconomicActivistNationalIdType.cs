using EShop.Domain.Helpers;
using EShop.Domain.Types.EconomicActivistType;

namespace EShop.Domain.Types.EconomicActivistNationalId;
/// <summary>
/// شناسه ملی /شماره ملی /شناسه مشارکت مدنی /کد فراگیر اتباع غیر ایرانی فعال اقتصادی
/// </summary>
public record EconomicActivistNationalIdType :
    ITaxPayerSystemType<string>,
    IDbType<string>
{
    public const int MAX_LENGTH = 12;
    const string ERROR_MESSAGE = "شناسه ملی/ شماره ملی/ شناسه مشارکت مدنی/ کد فراگیر اشتباه است";
    const string ERROR_MESSAGE_LEGAL = "شناسه ملی اشتباه است";
    const string ERROR_MESSAGE_PERSONAL = "شماره ملی اشتباه است";
    const string ERROR_MESSAGE_CIVILPARTNERSHIP = "شناسه مشارکت مدنی اشتباه است";
    const string ERROR_MESSAGE_FOREIGNER = "کد فراگیر اتباع غیر ایرانی اشتباه است";

    public readonly static EconomicActivistNationalIdType Empty = new(string.Empty);

    public string Value { get; private set; } = string.Empty;

    private EconomicActivistNationalIdType(string value) => this.Value = value.Trim();

    public static PrimitiveResult<EconomicActivistNationalIdType> Create(string? value, EconomicActivistTypeInfo type) => CreateUnsafe(value).Validate(type);

    public static EconomicActivistNationalIdType CreateUnsafe(string? value)
    {
        var v = value ?? string.Empty;
        return string.IsNullOrWhiteSpace(v)
            ? Empty
            : new EconomicActivistNationalIdType(v);
    }


    public bool IsDefault() => this.Equals(default) || this.Value.Equals(default) || string.IsNullOrWhiteSpace(this.Value) || this.Equals(Empty);
    public PrimitiveResult<EconomicActivistNationalIdType> Validate(EconomicActivistTypeInfo type)
    {
        if (string.IsNullOrWhiteSpace(this.Value)) return this;
        var errormessage = type.Id switch
        {
            EconomicActivistTypeInfo.TaxPayerType_Legal_Id => !NationalIdHelper.ValidateLegalId(this.Value) ? ERROR_MESSAGE_LEGAL : string.Empty,
            EconomicActivistTypeInfo.TaxPayerType_Personal_Id => !NationalIdHelper.ValidatePersonalId(this.Value) ? ERROR_MESSAGE_PERSONAL : string.Empty,
            EconomicActivistTypeInfo.TaxPayerType_CivilPartnership_Id => !NationalIdHelper.ValidateCivilPartnershipId(this.Value) ? ERROR_MESSAGE_CIVILPARTNERSHIP : string.Empty,
            EconomicActivistTypeInfo.TaxPayerType_Foreigner_Id => !NationalIdHelper.ValidateForeignerId(this.Value) ? ERROR_MESSAGE_FOREIGNER : string.Empty,
            _ => string.Empty
        };
        if (!string.IsNullOrWhiteSpace(errormessage)) return DomainTypesHelper.CreateTypeErrorResult<EconomicActivistNationalIdType>(errormessage);

        return this;
    }

    public string GetValue() => this.Value;
    public string GetDbValue() => this.Value;

    public static EconomicActivistNationalIdType FromDb(string? value) => CreateUnsafe(value ?? string.Empty);
}
