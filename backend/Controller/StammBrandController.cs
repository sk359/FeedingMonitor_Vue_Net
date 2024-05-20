using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Data;

namespace backend.Controllers
{
    [Route("api/stammdaten/brand")]
    [ApiController]    
    public class StammBrandController(IFeedingRepository repo) : ControllerBase
    {

        private IFeedingRepository _repo = repo;
        public IEnumerable<Brand> brands = new List<Brand>();
        

        [HttpGet]        
        public ActionResult<List<Brand>> Get()
        {
            brands = _repo.GetAllBrandsWithProducts();
            foreach(var brand in brands)
            {
              foreach(var prod in brand.Products)
              {
                prod.Brand = null; // zirkulaere Abhaengigkeit verhindet vor Serialisierung
              }
            }        
            return brands.ToList();
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Brand))]
        public ActionResult<Feeding> Post([FromBody] Brand brand)
        {
           Brand f = _repo.AddNewBrand(brand);
            _repo.Commit();
            return Ok(f);
        }


    }

}