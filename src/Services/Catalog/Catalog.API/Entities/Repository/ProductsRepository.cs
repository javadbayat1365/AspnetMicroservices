using Catalog.API.Data;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.API.Entities.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ICatalogContext _context;

        public ProductsRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
         
        public async Task CreateProduct(Product product)
        =>  await  _context.Products.InsertOneAsync(product);

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(e => e.Id,id);
            var result = await _context.Products.DeleteOneAsync(filter);
            return result.IsAcknowledged && result.DeletedCount>0;
        }

        public async Task<Product> GetProduct(string id) 
            => await _context.Products.Find(a => a.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<Product>> GetProductByCategory(string CategoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(e => e.Category, CategoryName);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(e => e.Name,name);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts() 
            => await _context.Products.Find(a => true).ToListAsync();

        public async Task<bool> UpdateProduct(Product product)
        {
            var UpdateResult =await _context
                .Products.ReplaceOneAsync(filter:g => g.Id == product.Id,replacement: product);
            return UpdateResult.IsAcknowledged && UpdateResult.ModifiedCount > 0;
        }
    }
}
