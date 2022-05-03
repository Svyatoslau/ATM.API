using Microsoft.AspNetCore.Mvc;
using Retake.Atm.Api.Interfaces.Services;
using Retake.Atm.Api.Controllers.Requests;

namespace Retake.Atm.Api.Controllers;

[ApiController, Route("api/[controller]/cards")]
public class AtmController : ControllerBase
{
    private readonly IAtmService _atm;

    public AtmController(IAtmService atm) => _atm = atm;

    [HttpGet("{cardNumber}/init")]
    public IActionResult Init([FromRoute] string cardNumber)
    {
        _atm.Init(cardNumber);
        
        return Accepted(new {message = "Card initialized successfully"});
    }

    [HttpPost("authorize")]
    public IActionResult Authorize([FromBody] AtmCardAuthorize request)
    {
        _atm.Authorize(request.CardNumber, request.CardPassword);
        
        return Ok(new {message = "Card authorized successfully"});
    }

    [HttpGet("{cardNumber}/balance")]
    public IActionResult GetBalance([FromRoute] string cardNumber)
    {
        var balance = _atm.GetBalance(cardNumber);
        
        return Ok(new {message = $"Your card balance is {balance}"});
    }
    
    [HttpPost("withdraw")]
    public IActionResult Withdraw([FromBody] AtmCardWithdraw request)
    {
        _atm.Withdraw(request.CardNumber, request.Amount);

        return Ok(new {message = "Operation completed successfully"});
    }
}