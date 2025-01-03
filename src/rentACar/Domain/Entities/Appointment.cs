using System;
using System.Text.Json.Serialization;
using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Appointment:Entity
    {
        public int BusinessId { get; set; }
        [JsonIgnore]
        public Business Business { get; set; }
        public string UserId { get; set; }
        public string PersonelId { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Services { get; set; }
        public bool? IsConfirmed { get; set; }

        public Appointment()
        {
        }

        public Appointment(int businessId, string userId, string personelId, DateTime date, string time, string services,bool? isConfirmed)
        {
            BusinessId = businessId;
            UserId = userId;
            PersonelId = personelId;
            Date = date;
            Time = time;
            Services = services;
            IsConfirmed = isConfirmed;
        }
    }
}

