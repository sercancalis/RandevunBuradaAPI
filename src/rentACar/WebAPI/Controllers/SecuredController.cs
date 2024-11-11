using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SecuredController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = User.Identity.Name;
        return Ok(new { message = "Bu veriye yalnızca yetkili kullanıcılar erişebilir!", userId });
    }
}

