using AuthenticationAPI.JWTProvider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Repositories.AccountRepository;
using Repositories.HashAlgorithmRepository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(typeof(IAccountRepository), typeof(AccountRepository));
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddSingleton(typeof(IHashAlgorithmRepository), typeof(HashAlgorithmRepository));
builder.Services.AddScoped<IHashAlgorithmRepository, HashAlgorithmRepository>();
builder.Services.AddScoped<JwtTokenGenerator>();
var JWTSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(JWTSettings["SecretKey"]);
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
        ValidIssuer = JWTSettings["Issuer"],
        ValidAudience = JWTSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

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
