using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly CatalogContext _context;

    public BrandRepository(CatalogContext catalogContext)
    {
        _context = catalogContext;
    }

    public async Task<IEnumerable<ProductBrand>> GetAllBrands()
    {
       return await _context
            .Brands
            .Find(p => true)
            .ToListAsync();
    }
}
