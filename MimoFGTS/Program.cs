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
//cors

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policy =>
    {
        policy.AllowAnyOrigin()  // Permite qualquer dom�nio
              .AllowAnyHeader()  // Permite qualquer cabe�alho
              .AllowAnyMethod(); // Permite qualquer m�todo (GET, POST, etc.)
    });
});


//MAPPERS
builder.Services.AddAutoMapper(typeof(SaqueProfile).Assembly);

var app = builder.Build();

app.UseCors("AllowAnyOrigin");

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
