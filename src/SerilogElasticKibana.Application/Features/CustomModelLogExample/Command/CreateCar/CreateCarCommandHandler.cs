using MediatR;

namespace Application.Features.CustomModelLogExample.Command.Besiktas;

public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand>
{
    public async Task<Unit> Handle(CreateCarCommand command, CancellationToken cancellationToken)
    {
        return Unit.Value;
    }
}