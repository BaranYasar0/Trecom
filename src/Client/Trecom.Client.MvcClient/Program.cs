using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Trecom.Client.MvcClient.Controllers;
using Trecom.Client.MvcClient.Extensions;
using Trecom.Client.MvcClient.Handlers;
using Trecom.Client.MvcClient.Middlewares;
using Trecom.Client.MvcClient.Services;
using Trecom.Client.MvcClient.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.RegisterHttpServices(builder.Configuration);
builder.Services.RegisterRequiredServices();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Auth/SignIn";
        opt.LogoutPath = "/Auth/SignOut";
        opt.AccessDeniedPath = "/Home/Error";
        opt.ExpireTimeSpan = TimeSpan.FromDays(60);
        opt.SlidingExpiration = true;
        opt.Cookie.Name = "authcookie";
    });
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<ClientExceptionHandler>();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
