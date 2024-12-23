using System;
using System.Net.Http.Json;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Services.Notifications
{
    public class NotificationManager : INotificationService
    {
        public class ExpoPushNotification
        {
            public string[] To { get; set; } // Expo Push Token
            public string Title { get; set; } // Notification title
            public string Body { get; set; } // Notification body
            public object Data { get; set; } // Optional: Additional data
        }

        private readonly IMapper _mapper;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserDeviceRepository _userDeviceRepository;
        private readonly HttpClient _httpClient;
        public NotificationManager(INotificationRepository notificationRepository, HttpClient httpClient, IUserDeviceRepository userDeviceRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://exp.host/--/api/v2/push/");
            _userDeviceRepository = userDeviceRepository;
            _mapper = mapper;
        }

        public async Task<bool> SendNotification(Notification notification)
        {
            var res = await _notificationRepository.AddAsync(notification);
            if (res != null)
            {
                var receiverList = new List<string>();
                if (notification.NotificationType == Domain.Enums.NotificationType.All)
                {
                    var userRes = await _userDeviceRepository.GetListAsync(index: 0, size:999);
                    receiverList.AddRange(userRes.Items.Select(x => x.DeviceToken));
                }
                else
                {
                    var userRes = await _userDeviceRepository.GetAsync(x=>x.UserId == notification.ReceiverId);
                    if(userRes != null)
                    {
                        receiverList.Add(userRes.DeviceToken);
                    }
                }

                var notificationModel = new ExpoPushNotification
                {
                    Title = notification.Title,
                    Body = notification.Body,
                    To = receiverList.ToArray(),
                    Data = notification
                };
                var response = await _httpClient.PostAsJsonAsync("send", notificationModel);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        public async Task<GetListResponse<Notification>> GetNotificationList(PageRequest pageRequest, string receiverId)
        {
            IPaginate<Notification> res = await _notificationRepository.GetListAsync(
                index: pageRequest.Page,
                size: pageRequest.PageSize,
                predicate: x => x.NotificationType == Domain.Enums.NotificationType.All || x.ReceiverId == receiverId);
            return _mapper.Map<GetListResponse<Notification>>(res);
        }
    }
}

