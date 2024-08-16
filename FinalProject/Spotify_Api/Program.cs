using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using Spotify_Api.DB_Connection;
using Spotify_Api.Models.Singeltons;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Initialize EF Core

// befehl: Add-Migration <name>

builder.Services.AddDbContext<BaseContext>(options =>
    options.UseNpgsql("Host=localhost;Database=mydb;Username=user;Password=password;Port=5432"));



// Add services to the container.

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = JwtConfig.Instance.Issuer ,
            ValidAudience = JwtConfig.Instance.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.Instance.Key))
        };
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





        var app = builder.Build();

// Wende die Migrationen an, wenn die Anwendung startet
using (var scope = app.Services.CreateScope())
{
    //var dbContext = scope.ServiceProvider.GetRequiredService<BaseContext>();
    BaseContext dbContext = new BaseContext();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
