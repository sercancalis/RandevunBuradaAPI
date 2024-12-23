using Application.Features.UserDevices.Command.Save;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class UserDevicesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Save(SaveUserDeviceCommand command)
        {
            bool result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}

