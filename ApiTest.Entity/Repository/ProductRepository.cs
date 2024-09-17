using ApiTest.Contracts.Database;
using ApiTest.Contracts.Repository;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Entity.Repository;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        this._context = context;
    }


    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int productId)
    {
        return await _context.Products.FindAsync(productId);
    }

    public  async Task<Product> GetByNameAsync(string productName)
    {
        return await _context.Products.Where(x=>x.Name == productName).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}
