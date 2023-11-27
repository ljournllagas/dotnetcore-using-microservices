using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class TypeRepository : ITypesRepository
{
    private readonly CatalogContext _context;

    public TypeRepository(CatalogContext catalogContext)
    {
        _context = catalogContext;
    }

    public async Task<IEnumerable<ProductType>> GetAllTypes()
    {
        return await _context
            .Types
            .Find(t => true)
            .ToListAsync();
    }
}
