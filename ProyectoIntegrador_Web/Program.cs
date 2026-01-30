using Microsoft.EntityFrameworkCore;
using ProyectoIntegrador.EntityFrameWork;
using ProyectoIntegrador.EntityFrameWork.Repositorios;
using ProyectoIntegrador.LogicaAplication.CasosDeUso;
using ProyectoIntegrador.LogicaAplication.Interface;
using ProyectoIntegrador.LogicaAplication.Servicios;
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
            builder.Services.AddScoped<ICarritoRepositorio, CarritoEFRepositorio>();
            builder.Services.AddScoped<IReporteRepositorio, ReporteEFRepositorio>();
            builder.Services.AddScoped<IOrdenRepositorio, OrdenEFRepositorio>();
            builder.Services.AddScoped<IFacturaRepositorio, FacturaEFRepsoitorio>();
            builder.Services.AddScoped<IPedidoPersonalizadoRepsoitorio, PedidoPersonalizadoEFRepositorio>();

            //Casos de uso
            builder.Services.AddScoped<IAgregarProducto, AgregarProductoCasoDeUso>();
            builder.Services.AddScoped<IAgregarUsuario, AgregarUsuarioCasoDeUso>();
            builder.Services.AddScoped<ICambiarPassword, CambiarPasswordCasoDeUso>();
            builder.Services.AddScoped<ILogin, LoginCasoDeUso>();
            builder.Services.AddScoped<IEditarArtesano, EditarArtesanoCasoDeUso>();
            builder.Services.AddScoped<IEditarCliente, EditarClienteCasoDeUso>();
            builder.Services.AddScoped<IObtenerCategorias, ObtenerCategoriasCasoDeUso>();
            builder.Services.AddScoped<IObtenerSubcategorias, ObtenerSubCategoriasCasoDeUso>();
            builder.Services.AddScoped<IObtenerCliente, ObtenerClienteCasoDeUso>();
            builder.Services.AddScoped<IObtenerArtesano, ObtenerArtesanoCasoDeUso>();
            builder.Services.AddScoped<IObtenerUsuario, ObtenerUsuarioCasoDeUso>();
            builder.Services.AddScoped<IObtenerProducto, ObtenerProductoCasoDeUso>();
            builder.Services.AddScoped<IObtenerProductoArtesano, ObtenerProductoArtesanoCasoDeUso>();
            builder.Services.AddScoped<IEliminarProducto, EliminarProductoCasoDeUso>();
            builder.Services.AddScoped<IEditarProducto, EditarProductoCasoDeUso>();
            builder.Services.AddScoped<IEliminarUsuario, EliminarUsuarioCasoDeUso>();
            builder.Services.AddScoped<IProductoEstaEnCarrito, ProductoEstaEnCarritoCasoDeUso>();
            builder.Services.AddScoped<IObtenerFacturaCliente, ObtenerFacturaClienteCasoDeUso>();
            builder.Services.AddScoped<IObtenerFacturaClientePorOrden, ObtenerFacturaClientePorOrdenCasoDeUso>();
            builder.Services.AddScoped<IObtenerFacturaArtesano, ObtenerFacturaArtesanoCasoDeUso>();
            builder.Services.AddScoped<IObtenerFacturasDeUnCliente, ObtenerFacturasDeUnClienteCasoDeUso>();


            builder.Services.AddScoped<ICatalogoService, CatalogoService>();
            builder.Services.AddScoped<ISubCategoriaRepositorio, SubCategoriaEFRepositorio>();
            builder.Services.AddScoped<IProductoRepositorio, ProductoEFRepositorio>();
            builder.Services.AddScoped<IBusquedaDeUsuarios, BusquedaDeUsuariosCasoDeUso>();
            builder.Services.AddScoped<IProductosFiltrados, ProductosFiltradosCasoDeUso>();
            builder.Services.AddScoped<IEliminarArtesano, EliminarArtesanoCasoDeUso>();
            builder.Services.AddScoped<IEliminarCliente, EliminarClienteCasoDeUso>();
            builder.Services.AddScoped<IObtenerTodosLosProductos, ObtenerTodosLosProductosCasoDeUso>();
            builder.Services.AddScoped<IAgragarAlCarrito, AgregarAlCarritoCasoDeUso>();
            builder.Services.AddScoped<IMostrarProductosCarrito, MostrarProductoCarritoCasoDeUso>();
            builder.Services.AddScoped<IEliminarItemDelCarrito, EliminarItemCarritoCasoDeUso>();
            builder.Services.AddScoped<IObtenerProductosDeInteres, ObtenerProductosDeInteresCasoDeUso>();
            builder.Services.AddScoped<IObtenerSubCategoria, ObtenerSubCategoriaCasoDeUso>();
            builder.Services.AddScoped<IAgregarReporte, AgregarReporteCasoDeUso>();
            builder.Services.AddScoped<IAgregarOrden, AgregarOrdenCasoDeUso>();
            builder.Services.AddScoped<IListadoDeReportes, ListadoDeReportesCasoDeUso>();
            builder.Services.AddScoped<IEliminarReporte, EliminarReporteCasoDeUso>();
            builder.Services.AddScoped<IObtenerReporte, ObtenerReporteCasoeUso>();
            builder.Services.AddScoped<IObtenerArtesanoId, ObtenerArtesanoIdCasoDeUso>();
            // ?? Pedidos Personalizados
            builder.Services.AddScoped<ICrearPedidoPersonalizado, CrearPedidoPersonalizadoCasoDeUso>();
            builder.Services.AddScoped<IObtenerPedidosPendientes, ObtenerPedidosPendientesCasoDeUso>();
            builder.Services.AddScoped<IAceptarPedidoPersonalizado, AceptarPedidoPersonalizadoCasoDeUso>();
            builder.Services.AddScoped<IObtenerMisEncargos, ObtenerMisEncargosCasoDeUso>();
            builder.Services.AddScoped<IFinalizarPedidoPersonalizado, FinalizarPedidoPersonalizadoCasoDeUso>();


            builder.Services.AddScoped<PdfClienteService>();



            builder.Services.AddScoped<IBloquearArtesano, BloquearArtesanoCasoDeUso>();
            builder.Services.AddScoped<IAgregarArtesanoLista, AgregarArtesanoListaCasoDeUso>();
            builder.Services.AddScoped<IEliminarArtesanoLista, EliminarArtesanoListaCasoDeUso>();
            //REGISTRO DEL SERVICIO DE EMAIL
            builder.Services.AddScoped<EmailService>();
            builder.Services.AddTransient<EmailService>();
            builder.Services.AddScoped<ICarritoService, CarritoService>();
            builder.Services.AddScoped<IEmailService, EmailService>();



            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            }
             );

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
