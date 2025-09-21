using G_Task.Application;
using G_Task.Common.Helpers;
using G_Task.Infrastructure;
using G_Task.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();


builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigurationApplicationServices();
builder.Services.ConfigurationInfrastructureServices(builder.Configuration);

builder.Services.AddCors(config => config.AddPolicy("default",
    policy => policy
        .SetIsOriginAllowed(ho => true)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
));

builder.UseLogger();
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseCors("default");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
