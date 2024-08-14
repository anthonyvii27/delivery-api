using basic_delivery_api.Persistence.Contexts;

namespace basic_delivery_api.Persistence.Repositories;

public abstract class BaseRepository
{
    protected readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }
}