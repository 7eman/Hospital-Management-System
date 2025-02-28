using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Hospital.Models;
using Hospital.Repositories;
using hospitals.Utilities;
using Hospital.Repositories.Interfaces;
using Hospital.Repositories.Implementation;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Hospital.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
             .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();

            builder.Services.AddScoped<IDbInitializer, DbInitiliazer>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IEmailSender,EmailSender>();
            builder.Services.AddRazorPages();
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            DataSedding();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{Area=Patient}/{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            void DataSedding()
            {
                using(var scope = app.Services.CreateScope())
                {
                    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                    dbInitializer.Initialize();
                }
            }


            app.Run();
        }
    }
}
