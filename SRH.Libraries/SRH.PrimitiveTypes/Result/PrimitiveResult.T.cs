using System.Text;

namespace SRH.PrimitiveTypes.Result;

public readonly partial record struct PrimitiveResult<TValue>
{
    private TValue? _value { get; }

    /// <summary>
    /// NEVER USE THIS PROPERTY, UNLESS YOU ARE SURE 'IsSuccess=true'
    /// </summary>
    public readonly TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("the value of failure result can not be accessed.");

    public readonly bool IsSuccess { get; }
    public readonly bool IsFailure => !IsSuccess;
    public readonly PrimitiveError[] Errors { get; }
    public PrimitiveError Error => Errors.Any()
        ? Errors[0]
        : throw new InvalidOperationException("empty error.");

    internal PrimitiveResult(TValue? value, bool isSuccess, PrimitiveError[] errors)
    {
        _value = value;
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static PrimitiveResult<TValue> Failure(PrimitiveError[] errors) => PrimitiveResult.Failure<TValue>(errors);

    public static PrimitiveResult From(PrimitiveResult<TValue> src) =>
        src.IsSuccess
        ? PrimitiveResult.Success()
        : PrimitiveResult.Failure(src.Errors);

    public static implicit operator PrimitiveResult<TValue>(TValue value) => PrimitiveResult.Success(value);

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

        if (this.IsSuccess)
        {
            builder.Append(", Value = ");
            builder.Append(Value);
        }
        else
        {
            builder.Append(", Errors = ");
            builder.Append(Errors);
        }
        return true;
    }
}
