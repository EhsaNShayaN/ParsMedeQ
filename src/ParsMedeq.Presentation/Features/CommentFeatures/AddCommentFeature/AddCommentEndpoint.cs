using ParsMedeQ.Application.Features.CommentFeatures.AddCommentFeature;
using ParsMedeQ.Contracts;
using ParsMedeQ.Contracts.CommentContracts.AddCommentContract;

namespace ParsMedeQ.Presentation.Features.CommentFeatures.AddCommentFeature;
sealed class AddCommentEndpoint : EndpointHandlerBase<
    AddCommentApiRequest,
    AddCommentCommand,
    AddCommentCommandResponse,
    AddCommentApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public AddCommentEndpoint(
        IPresentationMapper<AddCommentApiRequest, AddCommentCommand> apiRequestMapper
        ) : base(
            Endpoints.Comment.Add,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class AddCommentApiRequestMapper : IPresentationMapper<AddCommentApiRequest, AddCommentCommand>
{
    public async ValueTask<PrimitiveResult<AddCommentCommand>> Map(AddCommentApiRequest src, CancellationToken cancellationToken)
    {
        return await ValueTask.FromResult(
            PrimitiveResult.Success(
                new AddCommentCommand(
                    src.TableId,
                    src.TableName,
                    src.RelatedId,
                    src.Icon,
                    src.Description)));
    }
}