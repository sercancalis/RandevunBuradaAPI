using Application.Services.Notifications;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Notifications.Command.Send;

public class SendNotificationCommand : IRequest<bool>
{
    public string Title { get; set; }
    public string Body { get; set; }
    public string SenderId { get; set; }
    public string? ReceiverId { get; set; }
    public NotificationType NotificationType { get; set; }

    public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand, bool>
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        public SendNotificationCommandHandler(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        { 
            var notificationData = _mapper.Map<Notification>(request);
            return await _notificationService.SendNotification(notificationData);
        }
    }
}