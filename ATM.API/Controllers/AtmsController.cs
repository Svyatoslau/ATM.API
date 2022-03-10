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
        [HttpGet("{id}", Name = "GetAtm")]
        public ActionResult<AtmDto> GetAtm(int id)
        {
            var atmToReturn = AtmsDataStore.Current.ATMs.FirstOrDefault(atm => atm.Id == id);

            if (atmToReturn == null)
            {
                return NotFound();
            }

            return Ok(atmToReturn);
        }
        [HttpPost("{id}")]
        public ActionResult<AtmDto> WithdrawMoney(int id,
            [FromBody] WithdrawMoneyOperationDto operation)
        {
            var atm = AtmsDataStore.Current.ATMs.FirstOrDefault(a => a.Id == id);
            if (atm == null)
            {
                return NotFound();
            }

            var atmAfterWithdrawMoney = new AtmForWithdrawMoneyOperationDto
            {
                TotalMoney = atm.TotalMoney - operation.amount
            };
            if (!TryValidateModel(atmAfterWithdrawMoney))
            {
                return BadRequest(ModelState);
            }

            atm.TotalMoney = atmAfterWithdrawMoney.TotalMoney;

            return CreatedAtRoute("GetAtm", new
            {
                id = atm.Id
            },
            atm);
        }
    }
}
