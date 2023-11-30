using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data;

public class CatalogContext : ICatalogContext
{
    public CatalogContext(IConfiguration configuration)
    {
        var client =new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var Database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
        Products =Database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

        CatalogContextSeed.Seed(Products);
    }
    public IMongoCollection<Product> Products { get; }
}
