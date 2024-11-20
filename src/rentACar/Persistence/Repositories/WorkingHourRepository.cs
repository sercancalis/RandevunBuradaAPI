using System;
using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class WorkingHourRepository : EfRepositoryBase<WorkingHour, BaseDbContext>, IWorkingHourRepository
{
    public WorkingHourRepository(BaseDbContext context)
        : base(context) { }
}