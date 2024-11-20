using System;
namespace Application.Features.Business.Commands.Create
{
    public class CreateBusinessResponse
    {
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public bool IsConfirmed { get; set; }

    }
}

