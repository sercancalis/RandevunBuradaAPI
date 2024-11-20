using System;
using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BusinessImageRepository : EfRepositoryBase<BusinessImage, BaseDbContext>, IBusinessImageRepository
{
    public BusinessImageRepository(BaseDbContext context)
        : base(context) { }
}