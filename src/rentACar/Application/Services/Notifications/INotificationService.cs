using Core.Application.Requests;
using Core.Application.Responses; 
using Domain.Entities;

namespace Application.Services.Notifications
{
    public interface INotificationService
    {
        public Task<bool> SendNotification(Notification notification);
        public Task<GetListResponse<Notification>> GetNotificationList(PageRequest pageRequest, string receiverId);
        public Task<bool> NotificationAction(int id, bool action, string message);
    }
}

