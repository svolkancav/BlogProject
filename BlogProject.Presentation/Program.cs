
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


#region API sonrasý kaldýrýlan kodlar
//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
//    builder.Configuration.GetConnectionString("DefaultConnectionVolkan")));

//builder.Services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<AppDbContext>();


////AutoMapper service
////builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddIdentity<AppUser, IdentityRole>
//    (
//    options =>
//    {
//        options.SignIn.RequireConfirmedEmail = false;
//        options.SignIn.RequireConfirmedPhoneNumber = false;
//        options.SignIn.RequireConfirmedAccount = false;
//        options.Password.RequireUppercase = false;
//        options.Password.RequireLowercase = false;
//        options.Password.RequiredLength = 3;
//        options.Password.RequireNonAlphanumeric = false;
//    }
//    )
//    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


////IOC altýnda DependencyResolver class'ýnýn içinde tanýmladýk.
////builder.Services.AddTransient<IGenreService, GenreService>();
////builder.Services.AddTransient<IGenreRepository, GenreRepository>();

////API yazýnca kalktý
////builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
////builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
////{
////    builder.RegisterModule(new DependencyResolver());
////});
#endregion



builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".BlogProject.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["secretKey"];

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        //ValidateLifetime = true,
        //ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["validIssuer"],
        ValidAudience = jwtSettings["validAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
