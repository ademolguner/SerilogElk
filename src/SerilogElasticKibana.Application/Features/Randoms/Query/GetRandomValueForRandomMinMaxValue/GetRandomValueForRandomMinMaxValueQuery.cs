using MediatR;
using SerilogElasticKibana.Application.Models;

namespace SerilogElasticKibana.Application.Features.Randoms.Query;

public class GetRandomValueForRandomMinMaxValueQuery : IRequest<ResponseModel>
{
    public GetRandomValueForRandomMinMaxValueQuery(int minvalue, int maxValue)
    {
        MinValue = minvalue;
        MaxValue = maxValue;
    }

    public int MaxValue { get; set; }
    public int MinValue { get; set; }
}