using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Business.Commands.Create;
using Application.Features.Business.Commands.Update;
using Application.Features.Business.Queries.GetList;
using Application.Features.Employees.Commands.Create;
using Application.Features.Employees.Commands.Update;
using Application.Features.Employees.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BusinessesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateBusinessCommand command)
    {
        try
        {
            bool result = await Mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Ok(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromQuery] UpdateBusinessCommand command)
    {
        UpdateBusinessResponse result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBusiness res = new() { PageRequest = pageRequest };
        GetListResponse<GetListBusinessResponse> result = await Mediator.Send(res);
        return Ok(result);
    }
}

