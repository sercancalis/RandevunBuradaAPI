using System;
using Application.Services.Notifications;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Notifications.GetList;

public class GetNotificationListCommand : IRequest<GetListResponse<Notification>>
{
    public PageRequest PageRequest { get; set; }
    public string ReceiverId { get; set; }
      
    public class GetNotificationListCommandHandler : IRequestHandler<GetNotificationListCommand, GetListResponse<Notification>>
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        public GetNotificationListCommandHandler(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<GetListResponse<Notification>> Handle(GetNotificationListCommand request, CancellationToken cancellationToken)
        { 
            return await _notificationService.GetNotificationList(request.PageRequest,request.ReceiverId);
        }
    }
}