﻿using System;
using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AppointmentRepository : EfRepositoryBase<Appointment, BaseDbContext>, IAppointmentRepository
{
    public AppointmentRepository(BaseDbContext context)
        : base(context) { }
}