using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.Api.Repositories;

public class ProductRepository : IProductRepository
{
    #region Constructor

    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext context)
    {
        _context = context;
    }

    #endregion

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context.Products.Find(p => true).ToListAsync();
    }

    public async Task<Product> GetProduct(string id)
    {
        return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p=>p.Name,name);
        return await _context.Products.Find(filter).ToListAsync();

    }

    public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, category);
        return await _context.Products.Find(filter).ToListAsync();
    }

    public async Task CreateProduct(Product product)
    {
        await _context.Products.InsertOneAsync(product);
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateResult = await _context.Products.ReplaceOneAsync(filter: p => p.Id == product.Id,
            replacement: product);

        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
        var result = await _context.Products.DeleteOneAsync(filter);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}