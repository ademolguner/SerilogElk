using MediatR;

namespace SerilogElasticKibana.Application.Features.Randoms.Query;

public class GetRandomValueForRandomMaxValueQuery : IRequest<string>
{
    public GetRandomValueForRandomMaxValueQuery(int maxValue)
    {
        MaxValue = maxValue;
    }

    public int MaxValue { get; set; }
}