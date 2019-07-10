using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace products.backend.Controllers
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        List<Product> products;

        public ProductsController()
        {
            products = new List<Product>
            {
                new Product { ProductId = 1, Name = "Ocelot" },
                new Product { ProductId = 2, Name = "Krakend" },
            };
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(products);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
