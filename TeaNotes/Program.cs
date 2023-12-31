using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TeaNotes.Auth.Jwt;
using TeaNotes.Cors;
using TeaNotes.Database;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddSingleton(new JwtTokenGenerator(builder.Configuration));
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("Main")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicies.Client, policy =>
    {
        policy
            .WithExposedHeaders()
            .WithOrigins("http://localhost", "http://127.0.0.1:5173")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services    
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration[JwtConfigurationKeys.Issuer],
            ValidAudience = builder.Configuration[JwtConfigurationKeys.Audience],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[JwtConfigurationKeys.Secret]!)),
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(CorsPolicies.Client);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();


