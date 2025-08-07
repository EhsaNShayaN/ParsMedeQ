global using EShop.Domain.Types.EconomicActivistDocument;

namespace EShop.Infrastructure.Persistance.DapperValueConverters;
sealed class EconomicActivistDocumentInfoDapperTypeMapper : SqlMapper.TypeHandler<EconomicActivistDocumentInfo>
{
	public override EconomicActivistDocumentInfo Parse(object value) => EconomicActivistDocumentInfo.FromDb(Convert.ToByte(value));
	public override void SetValue(IDbDataParameter parameter, EconomicActivistDocumentInfo value) => parameter.Value = value.GetDbValue();
}


