using SRH.Persistance.Models;

namespace SRH.Persistance.Extensions;

public static class DefaultProcedureStatusResultExtensions
{
    public static async ValueTask<PrimitiveResult> MapDefaultProcedureStatusResult(this ValueTask<PrimitiveResult<DefaultProcedureStatusResult>> src, int successResult)
    {
        var taskResult = await src.ConfigureAwait(false);
        var result = await taskResult
            .Match(
                data => data.ResultStatus.Equals(successResult) ? PrimitiveResult.Success() : PrimitiveResult.Failure(data.ResultStatus.ToString(), data.Message),
                PrimitiveResult.Failure
            ).ConfigureAwait(false);
        return result;
    }

    public static async ValueTask<PrimitiveResult> MapDefaultProcedureStatusResult(this ValueTask<PrimitiveResult<DefaultProcedureStatusResult>> src) => await src.MapDefaultProcedureStatusResult(1).ConfigureAwait(false);

}