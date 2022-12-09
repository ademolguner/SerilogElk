using MediatR;
using SerilogElasticKibana.Application.Models;

namespace Application.Features.CustomModelLogExample.Command.Besiktas;

public class CreateCarCommand:IRequest
{
    public string Marka { get; set; }
    public  string Model { get; set; }
    public int ModelYil { get; set; }
}