using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext catalogContext)
    {
        _context = catalogContext;
    }

    public async Task<Product> CreateProduct(Product product)
    {
        await _context.Products.InsertOneAsync(product);

        return product;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

        DeleteResult deleteResult = await _context
            .Products
            .DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

    public async Task<Product> GetProduct(string id)
    {
        return await _context
            .Products
            .Find(a => a.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByBrand(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(a => a.Brands.Name, name);

        return await _context
            .Products
            .Find(filter)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(a => a.Name, name);

        return await _context
            .Products
            .Find(filter)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context
            .Products
            .Find(a => true)
            .ToListAsync();
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateResult = await _context
             .Products
             .ReplaceOneAsync(p => p.Id == product.Id, product);

        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }
}
