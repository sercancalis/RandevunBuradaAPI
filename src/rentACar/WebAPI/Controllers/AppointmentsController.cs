using Application.Features.Appointment.Queries.Get;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateAppointmentCommand command)
    {
        bool result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("GetActiveHours")]
    public async Task<IActionResult> GetActiveHours(GetActiveHoursCommand command)
    {
        List<ActiveHoursResponse> result = await Mediator.Send(command);
        return Ok(result);
    }
}