using ATM.API.Models;
using ATM.API.Models.API;
using ATM.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATM.API.Controllers;

[ApiController, Route("api/atm")]
public class AtmController : ControllerBase
{
    //
    // [GET]:[Init]:[To find card in bank] - api/v1/atm/cards/{cardNumber}
    // - store card in ATM
    // 
    // [POST]:[Authorize]:[To verify card password] - api/v1/atm/cards/{cardNumber}
    // - find stored card in ATM
    // - verify card password
    // - create and store token in ATM for further actions
    // - return token
    // 
    // [POST]:[Withdraw]:[To withdraw money from card] - api/v1/atm/cards/{cardNumber}/withdraw
    // - find verified card in ATM by token
    // - store pending withdraw in ATM
    // 
    
    // How to create simple token
    //
    // public class SecurityManager 
    // {
    //    public Guid CreateToken() => Guid.NewGuid();
    // }
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService) => _atmService = atmService;

    //[HttpGet(Name = "GetAtmAmount")]
    //public ActionResult<AtmForGet> GetAtmAmount()
    //{
    //    return Ok(new AtmForGet(_atmService.GetTotalAmount()));
    //}

    [HttpPost("withdraw")]
    public ActionResult Withdraw([FromBody] AtmWithdraw model)
    {
        _atmService.Withdraw(model.CardNumber, model.Amount);

        return Ok(new
        { 
            Message = $"You successfully withdraw {model.Amount}"
        });
    }


}
