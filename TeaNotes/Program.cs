using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TeaNotes.Auth.Jwt;
using TeaNotes.Database;
using TeaNotes.Email;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddSingleton(new JwtTokenGenerator(builder.Configuration));
builder.Services.AddSingleton(await EmailClient.Create(builder.Configuration));
builder.Services.AddSingleton(new RegisterEmailBuilder(builder.Configuration));
builder.Services.AddSingleton(new ConfirmationCodeGenerator());

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("Main")));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithExposedHeaders()
            .WithOrigins("http://localhost", "http://localhost:5173", "http://localhost:4173")
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
builder.Services
    .AddControllers(options => options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider()))
    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var app = builder.Build();

app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();


