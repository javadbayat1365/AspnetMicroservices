using Shopping.Aggregator.HttpExtensions;
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

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var response =await _httpClient.GetAsync("/api/v1/catalog");
            return await response.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> GetCatalog(string CatalogId)
        {
            var response = await _httpClient.GetAsync($"/api/v1/catalog/{CatalogId}");
            return await response.ReadContentAs<CatalogModel>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByName(string catalogName)
        {
            var response = await _httpClient.GetAsync($"/api/v1/Catalog/GetProductByCategoryName/categoryName?{catalogName}");
            return await response.ReadContentAs<IEnumerable<CatalogModel>>();
        }
    }
}
