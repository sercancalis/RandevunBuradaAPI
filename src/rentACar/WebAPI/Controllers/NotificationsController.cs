using System;
using Application.Features.Business.Queries.GetList;
using Application.Features.Notifications.Command.Send;
using Application.Features.Notifications.GetList;
using Application.Features.UserDevices.Command.Save;
using Core.Application.Requests;
using Core.Application.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : BaseController
    {
        [HttpPost("SendNotification")]
        public async Task<IActionResult> SendNotification(SendNotificationCommand command)
        {
            bool result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetNotificationList")]
        public async Task<IActionResult> GetNotificationList([FromQuery]PageRequest pageRequest, string receiverId)
        {
            var command = new GetNotificationListCommand { PageRequest = pageRequest, ReceiverId = receiverId };
            GetListResponse<Notification> result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}

