using MediatR;

namespace SerilogElasticKibana.Application.Features.Randoms.Query;

public class GetRandomValueForRandomMaxValueHandler : IRequestHandler<GetRandomValueForRandomMaxValueQuery, string>
{
    public async Task<string> Handle(GetRandomValueForRandomMaxValueQuery request, CancellationToken cancellationToken)
    {
        if (request.MaxValue <= 0)
            throw new Exception($"id cannot be less than or equal to o. value passed is {request.MaxValue}");

        var random = new Random();
        var randomValue = random.Next(0, request.MaxValue);
        return randomValue.ToString();
    }
}