using System;
namespace Application.Features.Employees.Queries.GetList
{
    public class GetListBusinessEmployeesResponse
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

