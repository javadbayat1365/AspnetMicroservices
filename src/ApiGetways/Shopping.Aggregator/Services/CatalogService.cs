using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            throw new NotImplementedException();
        }

        public Task<CatalogModel> GetCatalog(string CatalogId)
        {
            throw new NotImplementedException();
        }

        public Task<CatalogModel> GetCatalogByName(string catalogName)
        {
            throw new NotImplementedException();
        }
    }
}
