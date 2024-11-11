using System;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.Identity.Client;
namespace Application.Services.Repositories;

public interface IHairdresserRepository : IAsyncRepository<Hairdresser>, IRepository<Hairdresser> { }