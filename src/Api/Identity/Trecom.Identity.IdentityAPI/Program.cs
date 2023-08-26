using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Trecom.Api.Identity.Application.Features.Rules;
using Trecom.Api.Identity.Application.Helpers.Encryption;
using Trecom.Api.Identity.Application.Helpers.JWT;
using Trecom.Api.Identity.EntityFramework;
using Trecom.Api.Identity.Services.Interfaces;
using Trecom.Shared.Extensions;
using Watchflix.Api.Identity.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenHelper, JwtHelper>();
builder.Services.AddScoped<AuthBusinessRules>();

builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = false,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = tokenOptions.Issuer,
//            ValidAudience = tokenOptions.Audience,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
//        };
//    });

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
