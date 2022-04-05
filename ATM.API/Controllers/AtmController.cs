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
        _atmService.SaveCard(cardNumber);

        return Ok(new
        {
            Message = "ATM accepted the card"
        });
    }

    [HttpPost("cards/{cardNumber}")]
    public ActionResult Authorize(string cardNumber, [FromBody] AtmForAuthorize model)
    {
        // - find stored card in ATM
        // - verify card password
        _atmService.ValidCard(cardNumber, model.password);

        // - create and store token in ATM for further actions
        var token = _atmService.CreateToken();

        // - return token
        return Ok(token);
    }

    [HttpPost("cards/{cardNumber}/withdraw")]
    public ActionResult Withdraw([FromHeader] string token, string cardNumber, [FromBody] AtmWithdraw model)
    {
        // - find verified card in ATM by token
        // - store pending withdraw in ATM
        _atmService.ValidToken(token);

        _atmService.Withdraw(model.Amount);

        return Ok(new
        {
            Message = $"You successfully withdraw {model.Amount}"
        });
    }


}
