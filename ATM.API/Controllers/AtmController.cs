using ATM.API.Models;
using ATM.API.Models.API;
using ATM.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATM.API.Controllers;

[ApiController, Route("api/atm")]
public class AtmController : ControllerBase
{
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

        return Ok(new { 
            Message = $"You successfully withdraw {model.Amount}",
            Amount = model.Amount
        });
    }


}
