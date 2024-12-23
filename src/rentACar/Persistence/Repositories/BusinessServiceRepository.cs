using System;
using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BusinessServiceRepository : EfRepositoryBase<BusinessService, BaseDbContext>, IBusinessServiceRepository
{
    public BusinessServiceRepository(BaseDbContext context)
        : base(context) { }
}