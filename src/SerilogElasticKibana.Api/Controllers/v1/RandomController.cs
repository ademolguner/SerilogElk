using System.Diagnostics.CodeAnalysis;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SerilogElasticKibana.Application.Features.Randoms.Query;

namespace SerilogElasticKibana.Api.Controllers.v1;

[ExcludeFromCodeCoverage]
[ApiVersion("1.0")]
[Route("api/random")]
[Produces("application/json")]
[ApiController]
public class RandomController : ControllerBase
{
    private readonly IMediator _mediator;

    public RandomController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }


    [HttpGet]
    [Route("random-value")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetRandomValueAsync([FromHeader(Name = "developer")] string developer)
    {
        var response = await _mediator.Send(new GetRandomValueQuery());
        return Ok(response);
    }

    [HttpGet]
    [Route("random-value-for-max-value/{maxValue:int}")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetRandomValueForRandomMaxValueAsync([FromHeader(Name = "developer")] string developer, [FromRoute] int maxValue)
    {
        var response = await _mediator.Send(new GetRandomValueForRandomMaxValueQuery(maxValue));
        return Ok(response);
    }


    [HttpGet]
    [Route("random-value-for-max-value/{minValue:int}/{maxValue:int}")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetRandomValueForRandomMaxValueAsync([FromHeader(Name = "developer")] string developer, [FromRoute] int maxValue, [FromRoute] int minValue)
    {
        var response = await _mediator.Send(new GetRandomValueForRandomMinMaxValueQuery(minValue, maxValue));
        return Ok(response);
    }
}