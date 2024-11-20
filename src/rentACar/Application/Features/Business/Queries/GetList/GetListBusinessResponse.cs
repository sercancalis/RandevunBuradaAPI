using System;
using Domain.Entities;

namespace Application.Features.Business.Queries.GetList
{
    public class GetListBusinessResponse
    {
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public bool IsConfirmed { get; set; }

        public List<string> ImageUrls { get; set; }
        public List<WorkingHour> WorkingHours { get; set; }
    }
}

