using Microsoft.EntityFrameworkCore;
using NorthWind.IoC;
using NorthWind.Repositories.EFCore.DataContext;
using NorthWind.WebExceptionsPresenter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NorthWindContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers(Filters.Register);

builder.Services.AddNorthWindServices(builder.Configuration);

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