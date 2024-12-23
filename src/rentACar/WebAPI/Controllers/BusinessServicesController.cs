using System;
using Application.Features.Business.Commands.Create;
using Application.Features.Business.Commands.Update;
using Application.Features.Business.Queries.Get;
using Application.Features.Business.Queries.GetList;
using Application.Features.BusinessServices.Command.Create;
using Application.Features.BusinessServices.Command.Update;
using Application.Features.BusinessServices.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class BusinessServicesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateServiceCommand command)
    {
        CreateServiceResponse result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateServiceCommand command)
    {
        UpdateServiceResponse result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] int businessId)
    {
        GetListBusinessService res = new() { BusinessId = businessId };
        GetListResponse<GetListServiceResponse> result = await Mediator.Send(res);
        return Ok(result);
    } 
} 
