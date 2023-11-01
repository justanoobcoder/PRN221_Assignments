using NguyenHongHiepSignalR.Filters;
using NguyenHongHiepSignalR.Hubs;
using Repositories;
using Repositories.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()
    .AddMvcOptions(options =>
    {
        options.Filters.Add(new AuthorizationFilter());
    });
builder.Services.AddSignalR();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IRentingRepository, RentingRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRequestLocalization("vi-VN");

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<CarHub>("/car-hub");
app.MapHub<CustomerHub>("/customer-hub");

app.Run();
