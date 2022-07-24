using Microsoft.EntityFrameworkCore;
using MeterReadingsTask.Data;
using MeterReadingsTask.Services.Interfaces;
using MeterReadingsTask.Services;
using MeterReadingsTask.Repositories.Interfaces;
using MeterReadingsTask.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MeterReadingsTaskContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MeterReadingsTaskContext") ?? throw new InvalidOperationException("Connection string 'MeterReadingsTaskContext' not found.")));

// Add services to the container.
builder.Services.AddTransient<IMeterReadingService, MeterReadingService>();
builder.Services.AddTransient<IFileConverter, FileConverter>();
builder.Services.AddTransient<IMeterReadingValidationService, MeterReadingValidationService>();
builder.Services.AddTransient<IUploadedFileValidator, UploadedFileValidator>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IMeterReadingRepository, MeterReadingRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
