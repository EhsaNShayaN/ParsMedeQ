global using ParsMedeq.Domain.Types.EconomicActivistDocument;

namespace ParsMedeq.Infrastructure.Persistance.ValueConverters;
sealed class EconomicActivistDocumentInfoValueConverter : ValueConverter<EconomicActivistDocumentInfo, byte>
{
	public EconomicActivistDocumentInfoValueConverter(): base(
		src => src.GetDbValue(),
		value => EconomicActivistDocumentInfo.FromDb(value)
	){}
}


sealed class EconomicActivistDocumentInfoValueComparer : ValueComparer<EconomicActivistDocumentInfo>
{
	public EconomicActivistDocumentInfoValueComparer(): base(
		(a, b) => a.Equals(b),
		a => a.GetHashCode())
	{}
}

