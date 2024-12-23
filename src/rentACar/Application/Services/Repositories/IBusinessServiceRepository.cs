using System;
using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IBusinessServiceRepository : IAsyncRepository<BusinessService>, IRepository<BusinessService> { }