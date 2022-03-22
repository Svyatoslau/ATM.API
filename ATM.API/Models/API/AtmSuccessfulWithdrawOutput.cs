namespace ATM.API.Models.API;

using static Microsoft.AspNetCore.Http.StatusCodes;

public sealed record AtmSuccessfulWithdrawOutput(
    string Holder,
    int CardBalance,
    string Message = "Successful operation");

