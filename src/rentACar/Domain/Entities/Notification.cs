using System;
using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities
{
    public class Notification: Entity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public NotificationType NotificationType { get; set; }

        public Notification()
        {
        }

        public Notification(string title, string body, string senderId, string? receiverId, NotificationType notificationType)
        {
            Title = title;
            Body = body;
            SenderId = senderId;
            ReceiverId = receiverId;
            NotificationType = notificationType;
        }
    }
}

