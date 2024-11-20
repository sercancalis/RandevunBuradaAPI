using System;
namespace Application.Features.Employees.Commands.Update
{
    public class UpdateEmployeeResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BusinessId { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

