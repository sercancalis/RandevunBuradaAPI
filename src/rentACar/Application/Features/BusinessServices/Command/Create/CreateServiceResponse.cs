using System;
namespace Application.Features.BusinessServices.Command.Create
{
    public class CreateServiceResponse
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public string Name { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}

