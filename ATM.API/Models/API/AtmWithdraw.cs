using System.ComponentModel.DataAnnotations;

namespace ATM.API.Models.API;

public sealed record AtmWithdraw
    (int Amount, CardForWithdraw inputCard);