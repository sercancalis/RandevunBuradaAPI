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
        private readonly IBusinessRepository _businessRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        public NotificationManager(INotificationRepository notificationRepository, HttpClient httpClient, IUserDeviceRepository userDeviceRepository, IMapper mapper, IBusinessRepository businessRepository, IEmployeeRepository employeeRepository, IAppointmentRepository appointmentRepository)
        {
            _notificationRepository = notificationRepository;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://exp.host/--/api/v2/push/");
            _userDeviceRepository = userDeviceRepository;
            _mapper = mapper;
            _businessRepository = businessRepository;
            _employeeRepository = employeeRepository;
            _appointmentRepository = appointmentRepository;
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

        public async Task<bool> NotificationAction(int id, bool action, string message)
        {
            var notification = await _notificationRepository.GetAsync(x => x.Id == id);
            if(notification != null)
            {
                if (notification.NotificationType == Domain.Enums.NotificationType.SaveBusiness)
                {
                    var title = "İşletme Kaydet";
                    var body = "İşletmeniz kaydedilmiştir.";
                    if(action == false)
                    {
                        body = "İşletmeniz reddedilmiştir. " + message;
                    }

                    await SendNotification(new Notification
                    {
                        Title = title,
                        Body = body,
                        SenderId = notification.ReceiverId!,
                        ReceiverId = notification.SenderId,
                        NotificationType = Domain.Enums.NotificationType.SaveBusiness,
                        Action = action,
                        ActionId = notification.ActionId,
                        IsComplete = true
                    });

                    var business = await _businessRepository.GetAsync(x => x.Id == notification.ActionId);
                    business!.IsConfirmed = true;
                    await _businessRepository.UpdateAsync(business); 
                }
                else if (notification.NotificationType == Domain.Enums.NotificationType.SaveEmployee)
                {
                    var title = "Personel Kayıt";
                    var body = "Personel kaydedilmiştir.";
                    if (action == false)
                    {
                        body = "Personel reddedilmiştir. " + message;
                    }

                    await SendNotification(new Notification
                    {
                        Title = title,
                        Body = body,
                        SenderId = notification.ReceiverId!,
                        ReceiverId = notification.SenderId,
                        NotificationType = Domain.Enums.NotificationType.SaveEmployee,
                        Action = action,
                        ActionId = notification.ActionId,
                        IsComplete = true
                    });

                    var employee = await _employeeRepository.GetAsync(x => x.Id == notification.ActionId);
                    employee!.IsConfirmed = true;
                    await _employeeRepository.UpdateAsync(employee); 
                }
                else if (notification.NotificationType == Domain.Enums.NotificationType.RequestAppointment)
                {
                    var title = action ? "Randevu Onay" : "Randevu Ret";
                    var body = action ? "Randevunuz Onaylanmıştır" : "Randevunuz Reddedilmiştir." + message;
                    await SendNotification(new Notification
                    {
                        Title = title,
                        Body = body,
                        SenderId = notification.ReceiverId!,
                        ReceiverId = notification.SenderId,
                        NotificationType = action ? Domain.Enums.NotificationType.ConfirmAppointment:  Domain.Enums.NotificationType.RejectAppointment,
                        Action = action,
                        ActionId = notification.ActionId,
                        IsComplete = true
                    });

                    var appointment = await _appointmentRepository.GetAsync(x => x.Id == notification.ActionId);
                    appointment!.IsConfirmed = true;
                    await _appointmentRepository.UpdateAsync(appointment); 
                }

                notification.IsComplete = true;
                await _notificationRepository.UpdateAsync(notification);
                return true;
            }

            return false;
        }
    }
}

