using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem_API
{
    [ApiController]
    [Route("/inventory")]

    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository repository;

        private readonly ILogger<ProductsController> logger;

        public ProductsController(IProductsRepository repository, ILogger<ProductsController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts(string name)
        {
            var products = repository.GetAllProducts(name).ToList();
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {products.Count} products");
            return products;
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = repository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost("{Name}, {category}, {cost_price}, {selling_price}, {quantity}")]
        public async Task<ActionResult<Product>> CreateProduct(string Name, string category, double cost_price, double selling_price, int quantity)
        {
            string[] categories = { "Hardware", "Clothing", "Food", "Books", "Furniture", "Stationery", "Toys", "Tools" };
            int i = 0;
            while (true)
            {
                if (i > categories.Length - 1)
                {
                    return BadRequest("Category not found");
                }
                if (category == categories[i])
                {
                    break;
                }

                i++;
            }

            Product product = new()
            {
                id = new Guid(),
                name = Name,
                category = category,
                cost_price = cost_price,
                selling_price = selling_price,
                quantity = quantity,
                profit_per_unit = selling_price - cost_price,
                total_profit = 0,
                sold = 0
            };
            repository.AddProduct(product);
            logger.LogInformation($"{DateTime.UtcNow:hh:mm:ss}: Created new product with name {product.name}");
            return CreatedAtAction(nameof(GetProduct), new { id = product.id }, product);
        }
        [HttpPut("{id}, {sold}/sell")]
        public async Task<ActionResult<Product>> SellProduct(int id, int sold)
        {
            var existingProduct = repository.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.sold += sold;
            existingProduct.quantity -= sold;
            existingProduct.total_profit += existingProduct.profit_per_unit * sold;

            repository.UpdateProduct(existingProduct);

            logger.LogInformation($"{DateTime.UtcNow:hh:mm:ss}: Sold {sold} units of {existingProduct.name}, {existingProduct.quantity} units left");
            return NoContent();
        }

        [HttpPut("{id}, {bought}/buy")]
        public async Task<ActionResult<Product>> BuyProduct(int id, int bought)
        {
            var existingProduct = repository.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.quantity += bought;

            repository.UpdateProduct(existingProduct);

            logger.LogInformation($"{DateTime.UtcNow:hh:mm:ss}: Bought {bought} units of {existingProduct.name}, {existingProduct.quantity} units left");
            return NoContent();
        }

        [HttpPut("{id}/name")]
        public async Task<ActionResult<Product>> UpdateProductName(int id, string name)
        {
            var existingProduct = repository.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }
            repository.UpdateProduct(existingProduct);

            logger.LogInformation($"{DateTime.UtcNow:hh:mm:ss}: Updated name of product to {name}");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var existingProduct = repository.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }
            repository.DeleteProduct(id);
            logger.LogInformation($"{DateTime.UtcNow:hh:mm:ss}: Deleted product with id {id}");

            return NoContent();
        }

        [HttpPut("{id}/category")]
        public async Task<ActionResult<Product>> UpdateProductCategory(int id, string category)
        {
            var existingProduct = repository.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.category = category;
            repository.UpdateProduct(existingProduct);

            logger.LogInformation($"{DateTime.UtcNow:hh:mm:ss}: Updated category of product to {category}");

            return NoContent();
        }

        [HttpPut("{id}/price")]
        public async Task<ActionResult<Product>> UpdateProductPrice(int id, double price)
        {
            var existingProduct = repository.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.selling_price = price;
            existingProduct.profit_per_unit = price - existingProduct.cost_price;

            repository.UpdateProduct(existingProduct);

            logger.LogInformation($"{DateTime.UtcNow:hh:mm:ss}: Updated price of product to {price}");

            return NoContent();
        }

        [HttpPut("{id}/cost")]
        public async Task<ActionResult<Product>> UpdateProductCost(int id, double cost)
        {
            var existingProduct = repository.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.cost_price = cost;
            existingProduct.profit_per_unit = existingProduct.selling_price - cost;

            repository.UpdateProduct(existingProduct);

            logger.LogInformation($"{DateTime.UtcNow:hh:mm:ss}: Updated cost of product to {cost}");

            return NoContent();
        }
    }
}