using Microsoft.EntityFrameworkCore;
using ProyectoIntegrador.EntityFrameWork;
using ProyectoIntegrador.EntityFrameWork.Repositorios;
using ProyectoIntegrador.LogicaAplication.CasosDeUso;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;

namespace ProyectoIntegrador_Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ProyectoDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ProyectoDB3")));


            //Repositorios
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ProyectoDBContext>();
            builder.Services.AddScoped<IUsuarioRepositorio, AdminEFRepsoitorio>();
            builder.Services.AddScoped<IAdminRepositorio, AdminEFRepositorio>();
            builder.Services.AddScoped<IArtesanoRepositorio, ArtesanoEFRepositorio>();
            builder.Services.AddScoped<IProductoRepositorio, ProductoEFRepositorio>();
            builder.Services.AddScoped<IClienteRepositorio, ClienteEFRepositorio>();

            //Casos de uso
            builder.Services.AddScoped<ILogin, LoginCasoDeUso>();

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
