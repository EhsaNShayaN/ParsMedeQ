using MediatR;

public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, CreateServiceResponse>
{
    private readonly IServiceRepository _repository;

    public CreateServiceCommandHandler(IServiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateServiceResponse> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var entity = new Service
        {

        };

        await _repository.AddAsync(entity, cancellationToken);

        return new CreateServiceResponse
        {
            Id = entity.Id,
            Message = "Service created successfully"
        };
    }
}
