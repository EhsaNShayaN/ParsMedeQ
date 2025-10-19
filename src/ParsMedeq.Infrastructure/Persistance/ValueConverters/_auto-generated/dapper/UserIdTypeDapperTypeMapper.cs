namespace ParsMedeQ.Infrastructure.Persistance.DapperValueConverters;
sealed class intDapperTypeMapper : SqlMapper.TypeHandler<int>
{
    public override int Parse(object value) => Convert.ToInt32(value);
    public override void SetValue(IDbDataParameter parameter, int value) => parameter.Value = value;
}