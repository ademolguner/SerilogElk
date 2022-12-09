using MediatR;

namespace SerilogElasticKibana.Application.Features.Randoms.Query;

public class GetRandomValueQuery : IRequest<string>
{
}