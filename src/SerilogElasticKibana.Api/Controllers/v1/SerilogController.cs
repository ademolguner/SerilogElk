using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using SerilogElasticKibana.Api.Exceptions;

namespace SerilogElasticKibana.Api.Controllers.v1;

[ExcludeFromCodeCoverage]
[ApiVersion("1.0")]
[Route("api/serilog")]
[Produces("application/json")]
[ApiController]
public class SerilogController : ControllerBase
{
    [HttpGet]
    public Task<int> GetRandomValue()
    {
        var random = new Random();
        var randomValue = random.Next(0, 100);
        return Task.FromResult<int>(randomValue);
    }

    [HttpGet("{id:int}")]
    public Task<string> ThrowErrorMessage(int id)
    {
        if (id <= 0)
            throw new Exception($"id cannot be less than or equal to o. value passed is {id}");
        return Task.FromResult(id.ToString());
    }


    [HttpPost("custom-post")]
    public Task<int> PostRandomValue([FromBody] int value)
    {
        var random = new Random();
        var randomValue = random.Next(0, value);
        return Task.FromResult(randomValue);
    }
    
    [HttpPut("custom-put/{minValue}/{maxValue}")]
    public Task<ResponseModel> PutRandomValue([FromRoute] int minValue, [FromRoute] int maxValue)
    {
        if (minValue > maxValue)
         throw new ArgumentValidationException(new List<string>() {"MinValue değeri Max Value değerinden büyük olamaz"});
        
        var random = new Random();
        var randomValue = random.Next(minValue, maxValue);
        return Task.FromResult(new ResponseModel
        {
            Value = randomValue,
            IsSuccess = true
        });
    }
}


public class ResponseModel
{
    public int Value { get; set; }
    public bool IsSuccess { get; set; }
}