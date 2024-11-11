using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Haidresser.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class HairdressersController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListHairdressers res = new() { PageRequest = pageRequest};
        GetListResponse<GetListHaidressersResponse> result = await Mediator.Send(res);
        return Ok(result);
    }
}

