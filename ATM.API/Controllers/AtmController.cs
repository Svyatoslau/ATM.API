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
