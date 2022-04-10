using ATM.API.Models.API;
using ATM.API.Models.Interfaces;
using ATM.API.Models.Managers.Interfaces;
using ATM.API.Models.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATM.API.Controllers;

[ApiController, ApiVersion("1.0"), Route("api/v1/atm")]

public class AtmController : ControllerBase
{
    private readonly IAtm _atmService;
    private readonly CardSessionManager _cardSessionManager;

    public AtmController(IAtm atmService, CardSessionManager cardSessionManager) 
        => (_atmService, _cardSessionManager) = (atmService,cardSessionManager);

    [HttpGet("cards/{cardNumber}")]
    public ActionResult Init(string cardNumber)
    {
        var token = _cardSessionManager.StartSession(cardNumber);

        return Ok(new
        {
            Token = token,
            Message = "ATM accepted the card"
        });
    }

    [HttpPost("cards/{cardNumber}")]
    public ActionResult Authorize(string cardNumber, [FromBody] AtmForAuthorize model, [FromHeader(Name = "X-Token")] Guid token)
    {
        _cardSessionManager.AuthorizeSession(token, model.password);

        return Ok(new { token });
    }

    [HttpPost("cards/{cardNumber}/withdraw")]
    public ActionResult Withdraw([FromHeader(Name = "X-Token")] Guid token, string cardNumber, [FromBody] AtmWithdraw model)
    {
        _atmService.Withdraw(token, model.Amount);

        return Ok(new
        {
            Message = $"You successfully withdraw {model.Amount}"
        });
    }


}
