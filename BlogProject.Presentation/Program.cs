using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogProject.Application.IoC;
using BlogProject.Domain.Entities;
using BlogProject.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnectionVolkan")));

builder.Services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<AppDbContext>();


//AutoMapper service
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddIdentity<AppUser, IdentityRole>
    (
    options =>
    {
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequiredLength = 3;
        options.Password.RequireNonAlphanumeric = false;
    }
    )
    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


//IOC altýnda DependencyResolver class'ýnýn içinde tanýmladýk.
//builder.Services.AddTransient<IGenreService, GenreService>();
//builder.Services.AddTransient<IGenreRepository, GenreRepository>();


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new DependencyResolver());
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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
