using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.WebEncoders;
using Parcels.MiddlewareComponent;
using Parcels.Services;
using Parcels.Utils;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUsersPortalRepository, UsersPortalRepository>();
builder.Services.AddScoped<AuthorizationFilter>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});
builder.Services.Configure<WebEncoderOptions>(options =>
{
    options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
});
builder.Services.AddMvc();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseMiddleware<ApiKeyMiddleware>();
app.UseCookiePolicy();
app.UseSession();
app.UseStaticFiles();
//Здесь мы включаем поддержку ForwardedHeaders мидлвера из пакета. Microsoft.AspNetCore.HttpOverrides, который будет вставлять в Http-запрос заголовки X-Forwarded-For и X-Forwarded-Proto, использующиеся для определения исходного IP адреса клиента и передачи его прокси-серверу.
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UseStaticFiles();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
