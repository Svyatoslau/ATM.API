using ATM.API.Models;
using ATM.API.Models.Interfaces;
using ATM.API.Repositories;
using ATM.API.Services;
using ATM.API.Services.Card;
using ATM.API.Services.Interfaces;
using ATM.API.Services.Sessions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services
    .AddSingleton<IAtm, Atm>()
    .AddSingleton<IAtmService, AtmService>();

builder
    .Services
    .AddSingleton<IBank, Bank>();

builder
    .Services
    .AddSingleton<ICardService, CardService>()
    .AddSingleton<ICardSecurity, CardSecurityManager>()
    .AddSingleton<CardStorage>();

builder
    .Services
    .AddSingleton<SessionStorage>()
    .AddSingleton<SessionManager>()
    .AddSingleton<SessionProvider>()
    .AddSingleton<ISecurityManager, SecurityManager>();

builder
    .Services
    .AddSingleton<ReceiptService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(setupAction =>
{
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1,0);
    setupAction.ReportApiVersions = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandling();

app.UseTimeSessionHandling();

app.UseVerifyAuthorizeHandling();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
