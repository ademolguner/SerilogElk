using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace SerilogElasticKibana.Api.Controllers.v1;

[ExcludeFromCodeCoverage]
[ApiVersion("1.0")]
[Route("api/serilog")]
[Produces("application/json")]
[ApiController]
public class SerilogController : ControllerBase
{
    // GET: api/values  
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
}