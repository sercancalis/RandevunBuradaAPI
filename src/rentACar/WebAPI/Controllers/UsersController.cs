using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Haidresser.GetList;
using Application.Features.Users;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] List<string> userIds)
    {
        GetListUsers res = new() { UserIds = userIds };
        List<GetListUsersResponse> result = await Mediator.Send(res);
        return Ok(result);
    }
}

