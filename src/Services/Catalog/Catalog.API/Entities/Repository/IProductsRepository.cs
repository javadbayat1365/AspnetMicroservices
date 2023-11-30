namespace Catalog.API.Entities.Repository
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProductByName(string name);
        Task<IEnumerable<Product>> GetProductByCategory(string CategoryName);
        Task CreateProduct(Product product);
        Task<bool> DeleteProduct(string id);
        Task<bool> UpdateProduct(Product product);

    }
}
