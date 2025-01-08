using MimoFGTS.Content.Saque.Interface;
using MimoFGTS.Content.Saque.Service;
using MimoFGTS.Mappers.Saque;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//SERVICES
builder.Services.AddScoped<ISaqueService, SaqueService>();

//MAPPERS
builder.Services.AddAutoMapper(typeof(SaqueProfile).Assembly);

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
