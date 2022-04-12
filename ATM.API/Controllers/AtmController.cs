using ATM.API.Models.API;
using ATM.API.Models.Interfaces;
using ATM.API.Models.Managers;
using ATM.API.Models.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ATM.API.Controllers;

[ApiController, ApiVersion("1.0"), Route("api/v1/atm")]

public class AtmController : ControllerBase
{
    // Controller should have only one service injected
    // Controller usually serve handling requests and transmitting them to appropriate service
    // 
    // You should fix it
    private readonly IAtm _atmService;
    private readonly SessionManager _sessionManager;
    private readonly ICardSecurity _cardSecurity;

    public AtmController(IAtm atmService, SessionManager sessionManager, ICardSecurity cardSecurity)
    {
        _atmService = atmService;
        _sessionManager = sessionManager;
        _cardSecurity = cardSecurity;
    }

    [HttpGet("cards/{cardNumber}")]
    public ActionResult Init(string cardNumber)
    {
        var token = _sessionManager.Start(cardNumber);

        return Accepted(new
        {
            Token = token,
            Message = "ATM accepted the card"
        });
    }

    [HttpPost("cards/{cardNumber}")]
    public ActionResult Authorize(string cardNumber, [FromBody] AtmForAuthorize model, [FromHeader(Name = "X-Token")] Guid token)
    {
        // We created token to use it
        // In your case I can change card number but use token from Init() method
        _cardSecurity.VerifyCardPassword(cardNumber, model.password);

        _sessionManager.Authorize(token);

        return Ok();
    }

    [HttpPut("cards/{cardNumber}")]
    public ActionResult Withdraw([FromHeader(Name = "X-Token")] Guid token, string cardNumber, [FromBody] AtmWithdraw model)
    {
        _atmService.Withdraw(token, model.Amount);

        return Ok(new
        {
            Message = $"You successfully withdraw {model.Amount}"
        });
    }


}
