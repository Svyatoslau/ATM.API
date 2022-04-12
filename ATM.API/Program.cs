using ATM.API.Middlewares.Extensions;
using ATM.API.Models;
using ATM.API.Models.Interfaces;
using ATM.API.Models.Managers;
using ATM.API.Models.Managers.Interfaces;
using ATM.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services
    .AddSingleton<IBank, Bank>()
    .AddSingleton<ICardSecurity, CardSecurityManager>()
    .AddSingleton<ISecurityManager, SecurityManager>()
    .AddSingleton<IAtm, Atm>()
    .AddSingleton<ICardService, CardService>();

builder
    .Services
    .AddSingleton<SessionStorage>()
    .AddSingleton<SessionManager>()
    .AddSingleton<SessionProvider>();

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

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
