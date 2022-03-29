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
    private readonly ICardService _cardService;

    public AtmController(Atm atm, Bank bank, ICardService cardService)
        => (_atm, _bank, _cardService) = (atm, bank, cardService);

    [HttpGet(Name = "GetAtmAmount")]
    public ActionResult<AtmForGet> GetAtmAmount()
    {
        return Ok(new AtmForGet(_atm.GetTotalAmount()));
    }

    [HttpPost("withdraw")]
    public ActionResult Withdraw([FromBody] AtmWithdraw model)
    {
        //Usually controller stands for receiving request data
        //and mediates data to service.
        //So it's better to move this logic to ATM class.
        //
        //You should only define here Withdraw logic
        //to use ATM class like so:
        //
        //_atm.Withdraw(model.Amount, model.CardNumber);
        //
        //return Ok(new { Message = "You successfully withdrawn money from your card" });
        
        var isValidCard = _cardService.IsValidCardNumber(model.CardNumber);

        if (!isValidCard)
        {
            return BadRequest();
        }

        var card = _bank.GetCard(model.CardNumber);

        _atm.Withdraw(model.Amount, card.Number);

        return Ok(new AtmSuccessfulWithdrawOutput(card.Holder, card.Balance));
    }


}
