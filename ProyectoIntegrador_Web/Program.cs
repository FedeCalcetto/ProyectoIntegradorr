using Microsoft.EntityFrameworkCore;
using ProyectoIntegrador.EntityFrameWork;

namespace ProyectoIntegrador_Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ProyectoDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ProyectoDB")));


            //Repositorios
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ProyectoDBContext>();

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

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
