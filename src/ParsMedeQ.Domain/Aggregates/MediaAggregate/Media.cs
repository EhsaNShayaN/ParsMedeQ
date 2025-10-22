using ParsMedeQ.Domain.Abstractions;

namespace ParsMedeQ.Domain.Aggregates.MediaAggregate;

public sealed class Media : EntityBase<int>
{
    #region " Fields "
    #endregion

    #region " Properties "
    public int TableId { get; private set; }
    public string Path { get; private set; } = null!;
    public string MimeType { get; private set; } = string.Empty;
    public string? FileName { get; private set; } = string.Empty;
    #endregion

    #region " Navigation Properties "
    //public Resource Resource => this._resource;
    #endregion

    #region " Constructors "
    private Media() : base(0) { }
    public Media(int id) : base(id) { }
    #endregion

    #region " Factory "
    public static PrimitiveResult<Media> Create(
        int tableId,
        string path,
        string mimeType,
        string fileName)
    {
        return PrimitiveResult.Success(
            new Media()
            {
                TableId = tableId,
                Path = path,
                MimeType = mimeType,
                FileName = fileName
            });
    }
    #endregion
}