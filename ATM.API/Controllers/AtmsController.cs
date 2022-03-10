using ATM.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATM.API.Controllers
{
    [ApiController]
    [Route("api/atms")]
    public class AtmsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<AtmDto>> GetAtms()
        {
            return Ok(AtmsDataStore.Current);
        }
        [HttpGet("{id}")]
        public ActionResult<AtmDto> GetAtm(int id)
        {
            var atmToReturn = AtmsDataStore.Current.ATMs.FirstOrDefault(atm => atm.Id == id);

            if (atmToReturn == null)
            {
                return NotFound();
            }

            return Ok(atmToReturn);
        }
        [HttpPost]
        public ActionResult
    }
}
