using System;
using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IWorkingHourRepository : IAsyncRepository<WorkingHour>, IRepository<WorkingHour> { }