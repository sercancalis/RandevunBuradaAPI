using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities; 
using Persistence.Contexts;
namespace Persistence.Repositories;

public class HairdresserRepository : EfRepositoryBase<Hairdresser, BaseDbContext>, IHairdresserRepository
{
    public HairdresserRepository(BaseDbContext context)
        : base(context) { }
}
