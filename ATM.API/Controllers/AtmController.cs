using ATM.API.Models;
using ATM.API.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace ATM.API.Controllers;

[ApiController, Route("api/atm")]
public sealed class AtmController : ControllerBase
{
    readonly Atm _atm;

    public AtmController(Atm atm) => _atm = atm;
    
    [HttpPost("withdraw")]
    public IActionResult Withdraw([FromBody] AtmWithdrawApiModel model)
    {
        try
        {
            _atm.Withdraw(model.Amount);

            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
        }
    }
}