using ApiTest.Contracts.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTest.Contracts.Repository;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByNameAsync(string productName);
    Task<Product> GetByIdAsync(int productId);
    Task UpdateAsync(Product product);
}
