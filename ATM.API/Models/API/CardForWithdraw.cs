namespace ATM.API.Models.API;

public sealed record CardForWithdraw(
    string Number,
    string Initials,
    string Brand,
    int Code);
