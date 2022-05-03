namespace Retake.Atm.Api.Controllers.Requests;

public sealed record AtmCardAuthorize(
    string CardNumber,
    string CardPassword);