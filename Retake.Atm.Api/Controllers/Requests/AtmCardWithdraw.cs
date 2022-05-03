namespace Retake.Atm.Api.Controllers.Requests;

public sealed record AtmCardWithdraw(
    string CardNumber,
    int Amount);