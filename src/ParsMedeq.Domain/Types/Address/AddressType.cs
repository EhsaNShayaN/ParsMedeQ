namespace EShop.Domain.Types.Address;
public sealed record AddressType(
    string Province,
    string City,
    string Address,
    string PostalCode);