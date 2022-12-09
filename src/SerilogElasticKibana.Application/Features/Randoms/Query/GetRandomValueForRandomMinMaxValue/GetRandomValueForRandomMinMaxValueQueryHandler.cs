using MediatR;
using SerilogElasticKibana.Application.Models;

namespace SerilogElasticKibana.Application.Features.Randoms.Query;

public class GetRandomValueForRandomMinMaxValueHandler : IRequestHandler<GetRandomValueForRandomMinMaxValueQuery, ResponseModel>
{
    public async Task<ResponseModel> Handle(GetRandomValueForRandomMinMaxValueQuery request, CancellationToken cancellationToken)
    {
        if (request.MinValue > request.MaxValue)
            throw new Exception("MinValue değeri Max Value değerinden büyük olamaz");

        var random = new Random();
        var randomValue = random.Next(request.MinValue, request.MaxValue);
        return new ResponseModel
        {
            Value = randomValue,
            IsSuccess = true
        };
    }
}