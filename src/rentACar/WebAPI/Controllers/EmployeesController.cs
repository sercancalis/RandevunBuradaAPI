using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Employees.Commands.Create;
using Application.Features.Employees.Commands.Update;
using Application.Features.Employees.Queries.GetList;
using Application.Features.Haidresser.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromQuery] CreateEmployeeCommand command)
    {
        CreateEmployeeResponse result = await Mediator.Send(command); 
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromQuery] UpdateEmployeeCommand command)
    {
        UpdateEmployeeResponse result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("GetBussinessEmployees")]
    public async Task<IActionResult> GetBussinessEmployees([FromQuery] int businessId)
    { 
        GetBussinessEmployees res = new() { BusinessId = businessId };
        GetListResponse<GetListBusinessEmployeesResponse> result = await Mediator.Send(res);
        return Ok(result);
    }
}

