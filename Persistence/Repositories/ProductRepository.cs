using basic_delivery_api.Domain.Models;
using basic_delivery_api.Persistence.Contexts;
using basic_delivery_api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace basic_delivery_api.Persistence.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the products.", ex);
            }
        }

        public async Task AddAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the product.", ex);
            }
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            try
            {
                return await _context.Products.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while finding the product.", ex);
            }
        }

        public void Update(Product product)
        {
            try
            {
                _context.Products.Update(product);
                _context.SaveChangesAsync().Wait();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the product.", ex);
            }
        }

        public void Remove(Product product)
        {
            try
            {
                _context.Products.Remove(product);
                _context.SaveChangesAsync().Wait();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while removing the product.", ex);
            }
        }
    }
}