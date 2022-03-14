using ATM.API.Models;

namespace ATM.API.Models;

public sealed record Card(
    string Number,
    string Initials,
    CardBrand Brand,
    int Code);

