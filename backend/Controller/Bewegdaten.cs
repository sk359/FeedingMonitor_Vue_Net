using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Data;

namespace backend.Controllers
{
    [Route("api/bewegdaten")]
    [ApiController]    
    public class BewegdatenController(IFeedingRepository repo) : ControllerBase
    {

        private IFeedingRepository _repo = repo;
        public IEnumerable<Feeding> feedings = new List<Feeding>();
        

        [HttpGet]        
        public ActionResult<List<Feeding>> Get()
        {
            feedings = _repo.GetAllFeedingsForCat("Tobi");
            foreach (Feeding row in feedings) 
            {
                  Console.WriteLine($"{row.catname} {row.brandname} - {row.eatenpercentage} - {row.feedingtime}");
            } 
            return feedings.ToList();
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Feeding))]
        public ActionResult<Feeding> Post([FromBody] Feeding feeding)
        {
            Feeding f = _repo.AddNewFeeding(feeding);
            _repo.Commit();
            return Ok(f);
        }

        [HttpDelete]
        [Route("{feedingId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<int> DeleteById(int feedingId)
        {
            Feeding? f = _repo.GetFeedingById(feedingId);
            if (f == null) {
                return NotFound();
            }
            _repo.DeleteFeedingById(f);
            _repo.Commit();
            return Ok(feedingId);
        }
    }
}