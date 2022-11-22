using Microsoft.Extensions.Configuration;
using System.Data.Common;
using TreinamentoMarinho.Repository;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options => options.AddPolicy("MyPolicy", policy =>
{
    policy.WithOrigins("http://localhost:3000", "http://conectaportal.nbwdigital.com.br", "https://conectaportal.nbwdigital.com.br")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials();
}));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Pega string de conexao do banco
BaseRepository.DBConnection = builder.Configuration.GetConnectionString("TreinamentoCSharp");

var app = builder.Build();

app.UseCors("MyPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
