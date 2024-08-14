using basic_delivery_api.Persistence.Contexts;
using basic_delivery_api.Repositories;

namespace basic_delivery_api.Persistence.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;     
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}