using System;
using Domain.Entities; 

namespace Application.Services.WorkingHourService
{
    public interface IWorkingHourService
    {
        public Task SaveWorkingHours(List<WorkingHour> workingHours);
    }
}

