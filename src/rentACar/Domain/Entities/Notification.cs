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
        public bool? Action { get; set; }
        public int ActionId { get; set; }
        public bool IsComplete { get; set; }

        public Notification()
        {
        }

        public Notification(string title, string body, string senderId, string? receiverId, NotificationType notificationType,bool? action,int actionId,bool isComplete)
        {
            Title = title;
            Body = body;
            SenderId = senderId;
            ReceiverId = receiverId;
            NotificationType = notificationType;
            Action = action;
            ActionId = actionId;
            IsComplete = isComplete;
        }
    }
}

