using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using AttendanceCapture.Services.Implementation;
using AttendanceCapture.Services.Implementations;
using AttendanceCapture.Services.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterPersistence(builder.Configuration);
builder.Services.RegisterIdentity();
builder.Services.RegisterAuthentication(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddCors(options => {
    options.AddPolicy("MyCorsPolicy", builder =>
{

    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
    builder.AllowCredentials();
    builder.WithOrigins(new string[]
    {
        "http://localhost:5173",
        "http://localhost:3000",
        "https://facier.netlify.app",
        "http://facier.netlify.app"
    });

});
});
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value!.Errors.Count > 0)
            .SelectMany(x => x.Value!.Errors)
            .Select(x => x.ErrorMessage)
            .ToList();

        var result = new ValidationResultModel
        {
            Status = false,
            Message = "Some Errors were found ",
            Errors = errors
        };

        return new BadRequestObjectResult(result);
    };
});
builder.Services.AddFluentValidationAutoValidation();
var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseCors("MyCorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
