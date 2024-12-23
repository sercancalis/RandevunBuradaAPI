using System;
using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface INotificationRepository : IAsyncRepository<Notification>, IRepository<Notification> { }