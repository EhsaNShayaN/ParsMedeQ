using System.Text;

namespace SRH.PrimitiveTypes.Result;

public readonly partial record struct PrimitiveResult
{
    public readonly bool IsSuccess { get; }
    public readonly bool IsFailure => !this.IsSuccess;
    public readonly PrimitiveError[] Errors { get; }
    public PrimitiveError Error => Errors.Any()
        ? this.Errors[0]
        : throw new InvalidOperationException("empty error.");

    private PrimitiveResult(bool isSuccess, PrimitiveError[] errors)
    {
        this.IsSuccess = isSuccess;
        this.Errors = errors;
    }

    public static PrimitiveResult Success() => new(true, PrimitiveError.NoErrorArray);
    public static PrimitiveResult Failure(PrimitiveResult src) =>
        src.IsSuccess
            ? throw new InvalidOperationException("the error of success result can not be accessed.")
            : new(false, src.Errors);
    public static PrimitiveResult Failure(PrimitiveError[] errors) => new(false, errors);
    public static PrimitiveResult Failure(PrimitiveError error) => new(false, new PrimitiveError[1] { error });
    public static PrimitiveResult Failure(string errorCode, string errorMessage) => new(false, new PrimitiveError[1] { PrimitiveError.Create(errorCode, errorMessage) });
    public static PrimitiveResult InternalFailure(string errorCode, string errorMessage) => new(false, new PrimitiveError[1] { PrimitiveError.CreateInternal(errorCode, errorMessage) });

    public static PrimitiveResult<TValue> Success<TValue>(TValue value) => new(value, true, PrimitiveError.NoErrorArray);
    public static PrimitiveResult<TValue> Failure<TValue>(PrimitiveError[] errors) => new(default, false, errors);
    public static PrimitiveResult<TValue> Failure<TValue>(PrimitiveError error) => new(default, false, new PrimitiveError[1] { error });
    public static PrimitiveResult<TValue> Failure<TValue>(string errorCode, string errorMessage) => new(default, false, new PrimitiveError[1] { PrimitiveError.Create(errorCode, errorMessage) });
    public static PrimitiveResult<TValue> InternalFailure<TValue>(string errorCode, string errorMessage) => new(default, false, new PrimitiveError[1] { PrimitiveError.CreateInternal(errorCode, errorMessage) });
    public static PrimitiveResult<TValue> Failure<TValue>(PrimitiveResult result) => new(default, false, result.Errors);
    public static PrimitiveResult<TValue> Failure<TValue>(PrimitiveResult<TValue> result) => new(default, false, result.Errors);

    public static PrimitiveResult From(PrimitiveResult src) =>
        src.IsSuccess
        ? Success()
        : Failure(src.Errors);

    public readonly static PrimitiveResult<bool> Empty = Success(true);

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(" { ");
        if (PrintMembers(stringBuilder))
        {
            stringBuilder.Append(' ');
        }
        stringBuilder.Append('}');
        return stringBuilder.ToString();
    }

    private bool PrintMembers(StringBuilder builder)
    {
        builder.Append("IsSuccess = ");
        builder.Append(IsSuccess.ToString());
        builder.Append(", IsFailure = ");
        builder.Append(IsFailure.ToString());

        if (this.IsFailure)
        {
            builder.Append(", Errors = ");
            builder.Append(Errors);
        }

        return true;
    }

}
