var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddServices()
    .AddControllers();

var app = builder.Build();

app
    .UseRouting()
    .UseMiddleware<ExceptionHandlingMiddleware>()
    .UseEndpoints(x => x.MapControllers());

app.Run();