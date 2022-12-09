using MediatR;
using SerilogElasticKibana.Application.Models;

namespace Application.Features.CustomModelLogExample.Command.Besiktas;

public class CreateCarStructuredCommand:IRequest<CarDto>
{
    public string Marka { get; set; }
    public  string Model { get; set; }
    public int ModelYil { get; set; }
}