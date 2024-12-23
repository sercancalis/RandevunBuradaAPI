using System;
using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class UserDeviceRepository : EfRepositoryBase<UserDevice, BaseDbContext>, IUserDeviceRepository
{
    public UserDeviceRepository(BaseDbContext context)
        : base(context) { }
}