using AuthenticationAPI.JWTProvider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Repositories.AccountRepository;
using Repositories.HashAlgorithmRepository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var JWTSettings = builder.Configuration.GetSection("JwtSettings");
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings["SecretKey"]));

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


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = JWTSettings["Issuer"],
//        ValidAudience = JWTSettings["Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(key)
//    };
//});

//builder.Configuration
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("ocelot.json")
//    .AddJsonFile("appsettings.json")
//    .Build();
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer("IdentityApiKey", options =>
//{
//    //options.Authority = identityUrl;
//    options.RequireHttpsMetadata = true;
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidIssuer = JWTSettings["Issuer"],
//        ValidateIssuer = true,
//        ValidAudiences = new[] { "AuthenticationService" },
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = key,
//    };
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
