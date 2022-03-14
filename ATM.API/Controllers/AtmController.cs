using ATM.API.Models;
using ATM.API.Models.API;
using ATM.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATM.API.Controllers;

[ApiController, Route("api/atm")]
public class AtmController : ControllerBase
{
    private readonly Atm _atm;
    private readonly Bank _bank;
    private readonly CardServise _cardService;

    public AtmController(Atm atm, Bank bank, CardServise cardService)
        => (_atm, _bank, _cardService) = (atm, bank, cardService);

    [HttpGet(Name = "GetAtmAmount")]
    public ActionResult<AtmForGet> GetAtmAmount()
    {
        return Ok(new AtmForGet(_atm.GetTotalAmount()));
    }

    [HttpPost("withdraw")]
    public ActionResult Withdraw([FromBody] AtmWithdraw model)
    {
        try
        {
            var card = _cardService.MakeCard(model.inputCard);

            _bank.CardCheck(card);

            _atm.Withdraw(model.Amount, card.Brand);

            return Ok();
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }


}
