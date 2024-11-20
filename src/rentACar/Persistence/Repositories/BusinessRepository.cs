using System;
using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BusinessRepository : EfRepositoryBase<Business, BaseDbContext>, IBusinessRepository
{
    public BusinessRepository(BaseDbContext context)
        : base(context) { }
}