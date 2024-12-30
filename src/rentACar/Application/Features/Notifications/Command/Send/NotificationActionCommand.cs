using System;
using Application.Services.Notifications;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Notifications.Command.Send;

public class NotificationActionCommand : IRequest<bool>, ICacheRemoverRequest
{
    public int Id { get; set; }
    public bool Action { get; set; }
    public string Message { get; set; }  

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetNotificationList";

    public class NotificationActionCommandHandler : IRequestHandler<NotificationActionCommand, bool>
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        public NotificationActionCommandHandler(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(NotificationActionCommand request, CancellationToken cancellationToken)
        {
            var notificationData = _mapper.Map<Notification>(request);
            return await _notificationService.NotificationAction(request.Id,request.Action,request.Message);
        }
    }
}