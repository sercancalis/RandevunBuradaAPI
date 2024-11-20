using System;
using System.Xml.Linq;
using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Employee: Entity
    {
        public string UserId { get; set; }
        public int BusinessId { get; set; }
        public Business Business { get; set; }
        public bool IsConfirmed { get; set; }

        public Employee()
        {

        }

        public Employee(string userId, int businessId, bool isConfirmed) : this()
        {
            UserId = userId;
            BusinessId = businessId;
            IsConfirmed = isConfirmed;
        }
    }
}

