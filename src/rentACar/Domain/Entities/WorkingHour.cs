using System;
using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities
{
    public class WorkingHour: Entity
    {
        public int BusinessId { get; set; }
        public Business Business { get; set; }
        public WorkingDays WorkingDay { get; set; }
        public string Value { get; set; }

        public WorkingHour()
        {
        }

        public WorkingHour(int businessId, WorkingDays workingDay, string value)
        {
            BusinessId = businessId;
            WorkingDay = workingDay;
            Value = value;
        }
    }
}

