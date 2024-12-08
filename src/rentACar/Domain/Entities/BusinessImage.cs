using System;
using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class BusinessImage: Entity
    {
        public int BusinessId { get; set; }
        public Business Business { get; set; }
        public string ImageUrl { get; set; }

        public BusinessImage()
        {

        }

        public BusinessImage(int businessId, string imageUrl)
        {
            BusinessId = businessId;
            ImageUrl = imageUrl;
        }
    }
}

