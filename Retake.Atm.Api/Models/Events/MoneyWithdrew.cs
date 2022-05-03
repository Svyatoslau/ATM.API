namespace Retake.Atm.Api.Models.Events;

public sealed record MoneyWithdrew(int Amount) : AtmEventBase;