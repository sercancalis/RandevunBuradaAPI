using System;
namespace Application.Features.Employees.Commands.Create
{
    public class CreateEmployeeResponse
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

