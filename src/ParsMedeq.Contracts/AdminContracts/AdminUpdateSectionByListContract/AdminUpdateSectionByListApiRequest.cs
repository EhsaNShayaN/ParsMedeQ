namespace ParsMedeQ.Contracts.AdminContracts.AdminUpdateSectionByListContract;

public sealed class AdminUpdateSectionByListApiRequest
{
    public int Id { get; set; }
    public AdminUpdateSectionByListItemApiRequest[] Items { get; set; } = [];
}
public sealed class AdminUpdateSectionByListItemApiRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
}