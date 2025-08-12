global using ParsMedeq.Domain.Types.EconomicActivistDocument;

namespace ParsMedeq.Infrastructure.Persistance.DapperValueConverters;
sealed class EconomicActivistDocumentInfoDapperTypeMapper : SqlMapper.TypeHandler<EconomicActivistDocumentInfo>
{
	public override EconomicActivistDocumentInfo Parse(object value) => EconomicActivistDocumentInfo.FromDb(Convert.ToByte(value));
	public override void SetValue(IDbDataParameter parameter, EconomicActivistDocumentInfo value) => parameter.Value = value.GetDbValue();
}


