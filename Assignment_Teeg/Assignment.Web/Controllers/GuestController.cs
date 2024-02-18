using Microsoft.AspNetCore.Mvc;
using MediatR;
using Assignment.Application.Features.Guest.Queries;
using Assignment.Application.Features.Guest.Commands;
using Microsoft.AspNetCore.Authorization;
using Serilog;

namespace Assignment.Web.Controllers;

//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GuestController : ControllerBase
{
    private readonly IMediator _mediator;

    public GuestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGuests()
    {
        var result = await _mediator.Send(new GetAllGuestsQuery());
        if (result.Count() == 0)
        {
            return NoContent();
        }
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGuestById(Guid id)
    {
        var result = await _mediator.Send(new GetGuestByIdQuery { Id = id });
        if (result == null)
        {
            return NoContent();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddGuest(AddGuestCommand guest)
    {
        var result = await _mediator.Send(guest);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> AddPhone(AddPhoneCommand guest)
    {
        var result = await _mediator.Send(guest);
        return Ok(result);
    }
}

