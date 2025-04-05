using FinanceManager.API.Middleware;
using FinanceManager.CompositionRoot;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddLogging(builder.Configuration)
    .AddGlobalExceptionHandler();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<RequestLogContextMiddleWare>();

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();