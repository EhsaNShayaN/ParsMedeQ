namespace SRH.Persistance.Repositories.Read;

public static class GenericPrimitiveReadRepositoryErrors
{
    public readonly static PrimitiveError Given_Id_Is_Null_Error = PrimitiveError.Create("Repository.Error", "The given id is null. can not find entity with null primarykey.");
    public readonly static PrimitiveError Entity_With_Id_Not_Found_Error = PrimitiveError.Create("Repository.Error", "Entity with given id is not found.");
    public readonly static PrimitiveError Null_Reader_Error = PrimitiveError.CreateInternal("Repository.Error", "Reader is null.");
    public readonly static PrimitiveError Null_DbResult_Error = PrimitiveError.CreateInternal("Repository.Error", "The query has null result.");


    public static PrimitiveError Generate_Entity_PrimaryKey_Not_Found_Error<TEntity>() => PrimitiveError.Create("Repository.Error", $"Can not find PrimaryKey of '{typeof(TEntity)}'.");
    public static PrimitiveError Generate_Entity_HasComplexPrimaryKey_Error<TEntity>() => PrimitiveError.Create("Repository.Error", $"The provided EntityType '{typeof(TEntity)}' has complex primary key.");
}