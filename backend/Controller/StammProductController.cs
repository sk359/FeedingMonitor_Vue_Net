using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Data;

namespace backend.Controllers
{
    [Route("api/stammdaten/product")]
    [ApiController]    
    public class StammProductController(IFeedingRepository repo) : ControllerBase
    {

        private IFeedingRepository _repo = repo;
        public List<Product> products = new List<Product>();
        

        [HttpGet]        
        public ActionResult<List<Product>> Get()
        {
            products = _repo.GetAllProducts();     
            if (products == null) {
                products = new List<Product>();
            }          
            return products;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Product))]
        public ActionResult<Feeding> Post([FromBody] Product product )
        {
           Product f = _repo.AddNewProduct(product);
            _repo.Commit();
            Console.WriteLine(f);
            return Ok(f);
        }

    }

}