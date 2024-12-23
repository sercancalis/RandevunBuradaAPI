using System;
namespace Application.Features.BusinessServices.Queries.GetList
{
    public class GetListServiceResponse
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public string Name { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}

