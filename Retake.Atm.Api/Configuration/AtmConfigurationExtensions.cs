using Retake.Atm.Api.Services;
using Retake.Atm.Api.Interfaces.Services;

public static class AtmConfigurationExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
        => services
            .AddSingleton<IAtmService, AtmService>()
            .AddSingleton<IBankService, BankService>()
            .AddSingleton<IAtmEventBroker, AtmEventBroker>()
            .AddSingleton<IAtmOperationService, AtmOperationService>();
}