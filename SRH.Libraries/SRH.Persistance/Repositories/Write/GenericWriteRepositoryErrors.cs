namespace SRH.Persistance.Repositories.Write;

public static class GenericPrimitiveWriteRepositoryErrors
{
    public static PrimitiveResult<T> Generate_InvalidOperation_On_Null_Entity_Error<T>() where T : class =>
        PrimitiveResult.InternalFailure<T>("GenericWriteRepositoryBase.Error", "Invalid Operation on null entry");
    public static PrimitiveResult Generate_InvalidOperation_On_Null_Entity_Error() => PrimitiveResult.InternalFailure("GenericWriteRepositoryBase.Error", "Invalid Operation on null entry");

    public static PrimitiveResult<T> Generate_Null_Entity_Entry_Error<T>() where T : class =>
        PrimitiveResult.InternalFailure<T>("GenericWriteRepositoryBase.Error", "Entity entry is null");
}