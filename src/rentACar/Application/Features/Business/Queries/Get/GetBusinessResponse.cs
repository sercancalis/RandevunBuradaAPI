using System;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Business.Queries.Get;

public class GetBusinessResponse
{
    public int Id { get; set; }
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

    public List<string> ImageUrls { get; set; }
    public List<WorkingHoursDto> WorkingHours { get; set; }
    public List<BusinessServiceDto> BusinessServices { get; set; }

    public class WorkingHoursDto
    {
        public int Id { get; set; }
        public WorkingDays WorkingDay { get; set; }
        public string Value { get; set; }
    }

    public class BusinessServiceDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
    }
}


