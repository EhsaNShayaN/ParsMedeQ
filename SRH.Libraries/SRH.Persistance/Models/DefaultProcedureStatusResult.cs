namespace SRH.Persistance.Models;
public sealed record DefaultProcedureStatusResult(int ResultStatus, string Message)
{
    public readonly static DefaultProcedureStatusResult NotRan = new DefaultProcedureStatusResult(int.MinValue, "Some error occured");
}
