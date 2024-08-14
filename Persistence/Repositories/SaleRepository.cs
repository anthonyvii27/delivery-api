using basic_delivery_api.Domain.Models;
using basic_delivery_api.Persistence.Contexts;
using basic_delivery_api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace basic_delivery_api.Persistence.Repositories
{
    public class SaleRepository : BaseRepository, ISaleRepository
    {
        public SaleRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Sale>> ListAsync()
        {
            return await _context.Sales.Include(s => s.SaleItems).ToListAsync();
        }

        public async Task<Sale?> FindByIdAsync(int id)
        {
            return await _context.Sales
                .Include(s => s.SaleItems)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
        }

        public void Remove(Sale sale)
        {
            _context.Sales.Remove(sale);
            _context.SaveChangesAsync().Wait();
        }
    }
}