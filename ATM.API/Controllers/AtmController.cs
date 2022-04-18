using ATM.API.Models.API;
using ATM.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ATM.API.Controllers;

[ApiController, ApiVersion("1.0"), Route("api/v1/atm")]

public class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService) => _atmService = atmService;

    [HttpGet("cards/{cardNumber}")]
    public ActionResult Init(string cardNumber)
    {
        var token = _atmService.StartSession(cardNumber);

        return Accepted(new
        {
            Token = token,
            Message = "ATM accepted the card"
        });
    }

    [HttpPost("cards/{cardNumber}")]
    public ActionResult Authorize([FromBody] AtmForAuthorize model, [FromHeader(Name = "X-Token")] Guid token)
    {
        _atmService.AuthorizeSession(token, model.Password);

        return Ok();
    }

    [HttpPut("cards/{cardNumber}")]
    public ActionResult Withdraw([FromHeader(Name = "X-Token")] Guid token, [FromBody] AtmWithdraw model)
    {
        _atmService.WithdrawMoney(token, model.Amount);

        return Ok(new
        {
            Message = $"You successfully withdraw {model.Amount}"
        });
    }
}
