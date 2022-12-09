using MediatR;
using Serilog;
using SerilogElasticKibana.Application.Models;

namespace Application.Features.CustomModelLogExample.Command.Besiktas;

public class CreateCarStructuredCommandHandler : IRequestHandler<CreateCarStructuredCommand, CarDto>
{
    public Task<CarDto> Handle(CreateCarStructuredCommand command, CancellationToken cancellationToken)
    {
        var car = new Car
        {
            Marka = command.Marka,
            Model = command.Model,
            ModelYil = command.ModelYil,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false,
            Id = Guid.NewGuid().ToString()
        };

        // throw new Exception($"Structured log example {command}");
        //
        //Log.Information("Ben bir arabayım {@car}", car);
        
        var carDto = new CarDto
        {
            Marka = car.Marka,
            Model = car.Model,
            ModelYil = car.ModelYil,
            Id = car.Id
        };
        
        return Task.FromResult(carDto);
    }
}