using Loggin_Example.Models;
using Microsoft.AspNetCore.Mvc;

namespace Loggin_Example.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        //**[Dependency Injections yapılışı]**//
        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;   
        }
        [HttpGet]
        public IActionResult GetAllProduct()
        {
            List<Product> product = new List<Product>()
            {
                new Product() { Id=1, ProductName="Computer"},
                new Product() { Id=2, ProductName="Phone"},
            };
            _logger.LogInformation("GetAllProduct metodu çağırıldı.");  //Loglama işlemi console ekranına yazdırıldı.
            return Ok(product);
        }
    }
}
