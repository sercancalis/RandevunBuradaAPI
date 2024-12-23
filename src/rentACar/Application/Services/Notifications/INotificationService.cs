using Core.Application.Requests;
using Core.Application.Responses; 
using Domain.Entities;

namespace Application.Services.Notifications
{
    public interface INotificationService
    {
        public Task<bool> SendNotification(Notification notification);
        public Task<GetListResponse<Notification>> GetNotificationList(PageRequest pageRequest, string receiverId);
    }
}

