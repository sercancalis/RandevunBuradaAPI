using System;
using Application.Services.Notifications;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Notifications.Command.Send;

public class NotificationActionCommand : IRequest<bool>
{
    public int Id { get; set; }
    public bool Action { get; set; }
    public string Message { get; set; }   

    public class NotificationActionCommandHandler : IRequestHandler<NotificationActionCommand, bool>
    {
        private readonly INotificationService _notificationService; 
        public NotificationActionCommandHandler(INotificationService notificationService)
        {
            _notificationService = notificationService; 
        }

        public async Task<bool> Handle(NotificationActionCommand request, CancellationToken cancellationToken)
        {  
            return await _notificationService.NotificationAction(request.Id,request.Action,request.Message);
        }
    }
}