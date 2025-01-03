using System;
namespace Application.Features.Appointment.Queries.Get;

public class ActiveHoursResponse
{
    public string Hour { get; set; }
    public bool IsAvailable { get; set; }
}

