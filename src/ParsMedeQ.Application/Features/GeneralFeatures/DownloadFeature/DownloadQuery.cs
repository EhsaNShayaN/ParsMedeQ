using ParsMedeQ.Domain.Aggregates.MediaAggregate;
using SRH.MediatRMessaging.Queries;

namespace ParsMedeQ.Application.Features.GeneralFeatures.DownloadFeature;
public readonly record struct DownloadQuery(int id) : IPrimitiveResultQuery<DownloadQueryResponse>;
public readonly record struct DownloadQueryResponse(byte[] Data, string FileName);

sealed class DownloadQueryHandler : IPrimitiveResultQueryHandler<DownloadQuery, DownloadQueryResponse>
{
    private readonly IReadUnitOfWork _readUnitOfWork;

    public DownloadQueryHandler(IReadUnitOfWork readUnitOfWork) => this._readUnitOfWork = readUnitOfWork;

    public async Task<PrimitiveResult<DownloadQueryResponse>> Handle(DownloadQuery request, CancellationToken cancellationToken)
    {
        return await ContextualResult<DownloadContext>.Create(new(request, cancellationToken))
            .Execute(SetMediaId)
            .Execute(FindMedia)
            .Execute(SetFileName)
            .Execute(GeneratePdf)
            .Map(ctx => new DownloadQueryResponse(ctx.Result, ctx.FileName))
            .ConfigureAwait(false);
    }

    ValueTask<PrimitiveResult<DownloadContext>> SetMediaId(DownloadContext ctx)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success
            (ctx.SetMediaId(ctx.Request.id)));
    }

    ValueTask<PrimitiveResult<DownloadContext>> FindMedia(DownloadContext ctx)
    {
        return this._readUnitOfWork.MediaReadRepository.FindById(ctx.Id, ctx.CancellationToken)
            .MapIf(
                data => data is not null,
                data => PrimitiveResult.Success(ctx.SetMedia(data)),
                data => PrimitiveResult.Failure<DownloadContext>("", "اطلاعات شعب مودی مورد نظر پیدا نشد"));
    }

    ValueTask<PrimitiveResult<DownloadContext>> SetFileName(DownloadContext ctx)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                ctx.SetFileName($"{Guid.NewGuid().ToString().Replace("-", "")}{Path.GetExtension(ctx.Media.Path)}")));
    }
    ValueTask<PrimitiveResult<DownloadContext>> GeneratePdf(DownloadContext ctx)
    {
        var x = Directory.GetCurrentDirectory();
        return ValueTask.FromResult(PrimitiveResult.Success(ctx.SetResult(File.ReadAllBytes(Directory.GetCurrentDirectory() + ctx.Media.Path))));
    }
}
sealed record DownloadContext(DownloadQuery Request, CancellationToken CancellationToken)
{
    public int Id { get; private set; }
    public Media Media { get; private set; }
    public string FileName { get; private set; } = string.Empty;
    public byte[] Result { get; private set; } = [];

    public DownloadContext SetMediaId(int value) => this with { Id = value };
    public DownloadContext SetMedia(Media value) => this with { Media = value };
    public DownloadContext SetFileName(string value) => this with { FileName = value };
    public DownloadContext SetResult(byte[] value) => this with { Result = value };
}