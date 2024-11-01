using AuthenticationAPI.JWTProvider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories.AccountRepository;
using Repositories.HashAlgorithmRepository;
using System.Text;
using Ultils.AuthorizationToken;
using Ultils.VerifyToken;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddSingleton(typeof(IAccountRepository), typeof(AccountRepository));
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddSingleton(typeof(IHashAlgorithmRepository), typeof(HashAlgorithmRepository));
builder.Services.AddScoped<IHashAlgorithmRepository, HashAlgorithmRepository>();
builder.Services.AddSingleton(typeof(IAuthorizationToken), typeof(AuthorizationToken));
builder.Services.AddScoped<IAuthorizationToken, AuthorizationToken>();
builder.Services.AddSingleton(typeof(IVerifyToken), typeof(VerifyToken));
builder.Services.AddScoped<IVerifyToken, VerifyToken>();
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

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(cors => cors
.AllowAnyOrigin()
.AllowAnyHeader()
.AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
