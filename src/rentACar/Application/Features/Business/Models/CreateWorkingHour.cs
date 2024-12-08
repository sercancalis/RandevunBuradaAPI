using System;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Business.Models
{
    public class CreateWorkingHour
    {
        public WorkingDays WorkingDay { get; set; }
        public string Value { get; set; }
    }
}

