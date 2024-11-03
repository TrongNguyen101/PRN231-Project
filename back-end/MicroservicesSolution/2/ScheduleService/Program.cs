using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ScheduleBusinessObject.DataContext;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var JWTSettings = builder.Configuration.GetSection("JwtSettings");
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings["SecretKey"]));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddSingleton<ScheduleContext>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
   // options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddAuthentication("IdentityApiKey")
.AddJwtBearer("IdentityApiKey", options =>
{
    options.RequireHttpsMetadata = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = JWTSettings["Issuer"],
        ValidateIssuer = true,
        ValidAudiences = new[] { "AuthenticationService" },
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        RoleClaimType = "Role"
    };
});

var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(options => options
.AllowAnyOrigin()
.AllowAnyHeader()
.AllowAnyMethod());

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
