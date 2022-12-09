using System.Diagnostics.CodeAnalysis;
using System.Net;
using Application.Features.CustomModelLogExample.Command.Besiktas;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using SerilogElasticKibana.Application.Features.Randoms.Query;

namespace SerilogElasticKibana.Api.Controllers.v1;

[ExcludeFromCodeCoverage]
[ApiVersion("1.0")]
[Route("api/car")]
[Produces("application/json")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly IMediator _mediator;

    public CarController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }


    [HttpPost]
    [Route("create-default")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetRandomValueAsync([FromHeader(Name = "developer")] string developer, [FromBody] CreateCarCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [Route("create-structured")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetRandomValueForRandomMaxValueAsync([FromHeader(Name = "developer")] string developer, [FromBody] CreateCarStructuredCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}