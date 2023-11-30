using Catalog.API.Entities;
using Catalog.API.Entities.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly IProductsRepository _productsRepository;

        public CatalogController(ILogger<CatalogController> logger, IProductsRepository productsRepository)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._productsRepository = productsRepository ?? throw new ArgumentNullException(nameof(productsRepository)); ;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productsRepository.GetProducts();
            return Ok(products);
        }


        [HttpGet("{Id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> GetProductById(string Id)
        {
            var product = await _productsRepository.GetProduct(Id);
            if (product is null)
            {
                _logger.LogError($"product with id :{Id} not found!");
                return NotFound();
            }
            return Ok(product);
        }


        [HttpGet]
        [Route("[action]/categoryName", Name = "GetProductByCategory")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductByCategoryName(string categoryName)
        {
            var products = await _productsRepository.GetProductByCategory(categoryName);
            return Ok(products);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _productsRepository.CreateProduct(product);

            //Redirection to GetProduct action
            return CreatedAtRoute("GetProduct", new { Id = product.Id }, product);
        }


        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateProduct(Product product)
        {
            var result =await _productsRepository.UpdateProduct(product);
            return Ok(result);
        }


        [HttpDelete("{Id:length(24)}",Name ="DeleteProduct")]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteProductById(string Id)
        {
            var result = await _productsRepository.DeleteProduct(Id);
            return Ok(result);
        }

    }
}
