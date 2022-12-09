using MediatR;

namespace SerilogElasticKibana.Application.Features.Randoms.Query;

public class GetRandomValueQueryHandler : IRequestHandler<GetRandomValueQuery, string>
{
    public async Task<string> Handle(GetRandomValueQuery request, CancellationToken cancellationToken)
    {
        var random = new Random();
        var randomValue = random.Next(0, 100);
        return randomValue.ToString();
    }
}