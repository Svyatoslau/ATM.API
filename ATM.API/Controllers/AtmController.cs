using ATM.API.Models.API;
using ATM.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        // - store card in ATM
        //_atmService.SaveCard(cardNumber);

        var token = _atmService.StartSession(cardNumber);

        return Ok(new
        {
            Token = token,
            Message = "ATM accepted the card"
        });
    }

    [HttpPost("cards/{cardNumber}")]
    public ActionResult Authorize(string cardNumber, [FromBody] AtmForAuthorize model, [FromHeader(Name = "X-Token")] Guid token)
    {
        // - find stored card in ATM
        // - verify card password
        //_atmService.ValidCard(cardNumber, model.password);

        // - create and store token in ATM for further actions
        //var token = _atmService.CreateToken();

        if (!_atmService.Authorize(token, model.password))
        {
            return Unauthorized(new { cardNumber });
        }

        // - return token
        return Ok(new { token });
    }

    [HttpPost("cards/{cardNumber}/withdraw")]
    public ActionResult Withdraw([FromHeader(Name = "X-Token")] Guid token, string cardNumber, [FromBody] AtmWithdraw model)
    {
        // - find verified card in ATM by token
        // - store pending withdraw in ATM
        //_atmService.ValidToken(token);

        _atmService.Withdraw(token, model.Amount);

        return Ok(new
        {
            Message = $"You successfully withdraw {model.Amount}"
        });
    }


}
