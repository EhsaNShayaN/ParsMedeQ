using EShop.Domain.Types;

namespace EShop.Domain.Helpers;
public static class DomainTypesHelper
{
    public static PrimitiveError CreateTypeError(string message) => PrimitiveError.Create(TypeConstants.TypeErrorCode, message.Trim());
    public static PrimitiveResult CreateTypeErrorResult(string message) => PrimitiveResult.Failure(CreateTypeError(message));
    public static PrimitiveResult<T> CreateTypeErrorResult<T>(string message) => PrimitiveResult.Failure<T>(CreateTypeError(message));

}
