using System;
using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class Business: Entity
    {
        public string UserId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public bool IsConfirmed { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<BusinessImage> BusinessImages { get; set; }
        public virtual ICollection<BusinessService> BusinessServices { get; set; }
        public virtual ICollection<WorkingHour> WorkingHours { get; set; }

        public Business()
        {
            Employees = new HashSet<Employee>();
            BusinessImages = new HashSet<BusinessImage>();
            BusinessServices = new HashSet<BusinessService>();
            WorkingHours = new HashSet<WorkingHour>();
        }

        public Business(string userId, string category, string name, string latitude, string longitude, string phoneNumber, string city, string district, string address, bool isConfirmed)
        {
            UserId = userId;
            Category = category;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            PhoneNumber = phoneNumber;
            City = city;
            District = district;
            Address = address;
            IsConfirmed = isConfirmed;
        }
    }
}

