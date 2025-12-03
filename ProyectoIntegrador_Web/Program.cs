using Microsoft.EntityFrameworkCore;
using ProyectoIntegrador.EntityFrameWork;
using ProyectoIntegrador.EntityFrameWork.Repositorios;
using ProyectoIntegrador.LogicaAplication.CasosDeUso;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador_Web.Services;


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
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioEFRepsoitorio>();
            builder.Services.AddScoped<IAdminRepositorio, AdminEFRepositorio>();
            builder.Services.AddScoped<ISubCategoriaRepositorio, SubCategoriaEFRepositorio>();
            builder.Services.AddScoped<IArtesanoRepositorio, ArtesanoEFRepositorio>();
            builder.Services.AddScoped<IProductoRepositorio, ProductoEFRepositorio>();
            builder.Services.AddScoped<IClienteRepositorio, ClienteEFRepositorio>();
            builder.Services.AddScoped<ICategoriaRepositorio, CategoriaEFRepsoitorio>();
            builder.Services.AddScoped<IProductoFotoRepsoitorio, ProductoFotoEFRepositorio>();
            //Casos de uso
            builder.Services.AddScoped<IAgregarProducto, AgregarProductoCasoDeUso>();
            builder.Services.AddScoped<IAgregarUsuario, AgregarUsuarioCasoDeUso>();
            builder.Services.AddScoped<ICambiarPassword, CambiarPasswordCasoDeUso>();
            builder.Services.AddScoped<ILogin, LoginCasoDeUso>();
            builder.Services.AddScoped<IEditarArtesano, EditarArtesanoCasoDeUso>();
            builder.Services.AddScoped<IObtenerCategorias, ObtenerCategoriasCasoDeUso>();
            builder.Services.AddScoped<IObtenerCliente, ObtenerClienteCasoDeUso>();
            builder.Services.AddScoped<IObtenerArtesano, ObtenerArtesanoCasoDeUso>();

            builder.Services.AddScoped<IEliminarProducto, EliminarProductoCasoDeUso>();
            builder.Services.AddScoped<IEditarProducto, EditarProductoCasoDeUso>();

            //REGISTRO DEL SERVICIO DE EMAIL
            builder.Services.AddScoped<EmailService>();
            builder.Services.AddTransient<EmailService>();



            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            }
             );

            var app = builder.Build();

            builder.Services.AddDistributedMemoryCache();
            


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            app.UseSession();

            app.UseStaticFiles();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
