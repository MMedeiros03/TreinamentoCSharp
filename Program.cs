using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;
using System.Text;
using TreinamentoMarinho.Repository;
using TreinamentoMarinho.Services.Authorization;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options => options.AddPolicy("MyPolicy", policy =>
{
    policy.WithOrigins("http://localhost:3000", "http://conectaportal.nbwdigital.com.br", "https://conectaportal.nbwdigital.com.br")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials();
}));


// pega secretyKey e passa para classe Token Service
TokenService.secretKey = builder.Configuration.GetSection("JWT_SECRET").Value;
byte[] key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JWT_SECRET").Value);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

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
