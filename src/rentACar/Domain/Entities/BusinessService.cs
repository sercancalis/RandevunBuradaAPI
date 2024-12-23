using System;
using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class BusinessService: Entity
    {
        public int BusinessId { get; set; }
        public Business Business { get; set; }
        public string Name { get; set; }

        public BusinessService()
        {

        }

        public BusinessService(int businessId, string name)
        {
            BusinessId = businessId;
            Name = name;
        }
    }
}

