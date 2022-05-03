namespace Retake.Atm.Api.Models;

public sealed record CardBrandLimit(
    string CardBrand,
    int CardLimit);