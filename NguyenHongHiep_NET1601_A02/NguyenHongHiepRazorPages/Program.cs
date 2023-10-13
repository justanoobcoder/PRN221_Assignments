using NguyenHongHiepRazorPages.Filters;
using Repositories;
using Repositories.Impl;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages()
    .AddMvcOptions(options =>
    {
        options.Filters.Add(new AuthorizationFilter());
    });

// Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

// Add dependency injections
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


app.Run();
