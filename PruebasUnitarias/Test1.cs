using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Moq;
using ProyectoIntegrador.LogicaAplication.CasosDeUso;
using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaNegocio.Entidades;
using ProyectoIntegrador.LogicaNegocio.Excepciones;
using ProyectoIntegrador.LogicaNegocio.Interface.Repositorio;
using ProyectoIntegrador.LogicaNegocio.ValueObjects;

namespace PruebasUnitarias
{
    // -------------------------------------------------------------------
    // LOGIN CASO DE USO
    // -------------------------------------------------------------------
    [TestClass]
    public class LoginCasoDeUsoTests
    {
        [TestMethod]
        public void Ejecutar_LoginCorrecto_RetornaUsuario()
        {
            var repo = new Mock<IUsuarioRepositorio>();
            var esperado = new Cliente
            {
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("test@mail.com"),
                password = "Password123"
            };

            repo.Setup(r => r.Login("test@mail.com", "Password123")).Returns(esperado);

            var caso = new LoginCasoDeUso(repo.Object);

            var resultado = caso.Ejecutar("test@mail.com", "Password123");

            Assert.IsNotNull(resultado);
            Assert.AreEqual("test@mail.com", resultado.email.email);
        }

        [TestMethod]
        public void Ejecutar_LoginIncorrecto_RetornaNull()
        {
            var repo = new Mock<IUsuarioRepositorio>();
            repo.Setup(r => r.Login("wrong@mail.com", "123")).Returns((Usuario)null);

            var caso = new LoginCasoDeUso(repo.Object);

            var resultado = caso.Ejecutar("wrong@mail.com", "123");

            Assert.IsNull(resultado);
        }
    }

    // -------------------------------------------------------------------
    // OBTENER CLIENTE CASO DE USO
    // -------------------------------------------------------------------
    [TestClass]
    public class ObtenerClienteCasoDeUsoTests
    {
        [TestMethod]
        public void Ejecutar_ClienteExiste_DevuelveCliente()
        {
            var repo = new Mock<IClienteRepositorio>();
            var esperado = new Cliente
            {
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("cliente@mail.com"),
                password = "Password123"
            };

            repo.Setup(r => r.obtenerCliente("cliente@mail.com")).Returns(esperado);

            var caso = new ObtenerClienteCasoDeUso(repo.Object);

            var resultado = caso.Ejecutar("cliente@mail.com");

            Assert.IsNotNull(resultado);
            Assert.AreEqual("cliente@mail.com", resultado.email.email);
        }

        [TestMethod]
        public void Ejecutar_ClienteNoExiste_DevuelveNull()
        {
            var repo = new Mock<IClienteRepositorio>();
            repo.Setup(r => r.obtenerCliente("x@mail.com")).Returns((Cliente)null);

            var caso = new ObtenerClienteCasoDeUso(repo.Object);

            var resultado = caso.Ejecutar("x@mail.com");

            Assert.IsNull(resultado);
        }
    }

    // -------------------------------------------------------------------
    // AGREGAR USUARIO CASO DE USO
    // -------------------------------------------------------------------
    [TestClass]
    public class AgregarUsuarioCasoDeUsoTests
    {
        [TestMethod]
        public void Ejecutar_LlamaRepositorioAgregar()
        {
            var repo = new Mock<IUsuarioRepositorio>();
            var caso = new AgregarUsuarioCasoDeUso(repo.Object);
            var dto = new AgregarUsuarioDto
            {
                Nombre = "Juan",
                Apellido = "Lopez",
                Email = "test@mail.com",
                Password = "Password123"
            };

            caso.Ejecutar(dto, "123456");

            repo.Verify(r => r.Agregar(It.IsAny<Usuario>()), Times.Once);
        }
    }

    // -------------------------------------------------------------------
    // VALIDACIONES DEL USUARIO
    // -------------------------------------------------------------------
    [TestClass]
    public class UsuarioTests
    {
        [TestMethod]
        public void Validar_ContraseñaSinMayuscula_LanzaExcepcion()
        {

            var usuario = new Cliente
            {
                nombre = "Test",
                apellido = "User",
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("t@t.com"),
                password = "password123"
            };

            Assert.ThrowsException<MayusculaPasswordException>(() => usuario.Validar());
        }

        [TestMethod]
        public void Validar_ContraseñaMuyCorta_LanzaExcepcion()
        {
            var usuario = new Cliente
            {
                nombre = "Test",
                apellido = "User",
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("t@t.com"),
                password = "Ab1"
            };

            Assert.ThrowsException<passwordUsuarioException>(() => usuario.Validar());
        }

        [TestMethod]
        public void Validar_NombreConNumero_LanzaExcepcion()
        {
            var usuario = new Cliente
            {
                nombre = "Juan2",
                apellido = "Perez",
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("t@t.com"),
                password = "Password123"
            };

            Assert.ThrowsException<ValidarNumeroEnNombreException>(() => usuario.Validar());
        }
    }

    // -------------------------------------------------------------------
    // VALIDACIONES DEL CLIENTE
    // -------------------------------------------------------------------
    [TestClass]
    public class ClienteTests
    {
        [TestMethod]
        public void validarContra_ContraseñasNoCoinciden_LanzaExcepcion()
        {
            var cliente = new Cliente
            {
                password = "Password123",
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("cliente@mail.com" )
            };

            Assert.ThrowsException<NoCoincideException>(() =>
                cliente.validarContra("NuevaPass123", "OtraPass123", "Password123"));
        }

        [TestMethod]
        public void validarEditar_DireccionInvalida_LanzaExcepcion()
        {
            var cliente = new Cliente
            {
                nombre = "Juan",
                apellido = "Lopez",
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("cliente@mail.com"),
                direccion = new Direccion { barrio = "", departamento = "", domicilio = "" }
            };

            Assert.ThrowsException<DireccionException>(() => cliente.validarEditar());
        }
    }

    // -------------------------------------------------------------------
    // VALIDACIONES DEL ARTESANO
    // -------------------------------------------------------------------
    [TestClass]
    public class ArtesanoTests
    {
        [TestMethod]
        public void ValidarTelefono_ContieneLetras_LanzaExcepcion()
        {
            var artesano = new Artesano
            {
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("art@mail.com")
            };

            Assert.ThrowsException<TelefonoUsuarioException>(() =>
                artesano.ValidarTelefono("123A5678"));
        }

        [TestMethod]
        public void ValidarTelefono_Correcto_NoLanza()
        {
            var artesano = new Artesano
            {
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("art@mail.com")
            };

            artesano.ValidarTelefono("091234567");

            Assert.IsTrue(true);
        }
    }
    // -------------------------------------------------------------------
    // VALIDACIONES DEL ARTESANO
    // -------------------------------------------------------------------
    [TestClass]
    public class EliminarProductoCasoDeUsoTests
    {
        [TestMethod]
        public void Ejecutar_LlamaRepositorioEliminar()
        {
            var repo = new Mock<IProductoRepositorio>();
            var caso = new EliminarProductoCasoDeUso(repo.Object);

            caso.Ejecutar(1);

            repo.Verify(r => r.Eliminar(1), Times.Once);
        }
    }
    [TestClass]
    public class ObtenerProductoCasoDeUsoTests
    {
        [TestMethod]
        public void Ejecutar_ProductoExiste_DevuelveProducto()
        {
            var repo = new Mock<IProductoRepositorio>();

            var esperado = new Producto
            {
                id = 1,
                nombre = "Mesa",
                descripcion = "Mesa",
                precio = 100,
                imagen = "img.jpg",
                stock = 10,
                SubCategoriaId = 1
            };

            repo.Setup(r => r.Obtener(1)).Returns(esperado);

            var caso = new ObtenerProductoCasoDeUso(repo.Object);

            var resultado = caso.obtener(1);

            Assert.IsNotNull(resultado);
            Assert.AreEqual("Mesa", resultado.nombre);
        }

        [TestMethod]
        public void Ejecutar_ProductoNoExiste_RetornaNull()
        {
            var repo = new Mock<IProductoRepositorio>();
            repo.Setup(r => r.Obtener(2)).Returns((Producto)null);

            var caso = new ObtenerProductoCasoDeUso(repo.Object);

            var resultado = caso.obtener(2);

            Assert.IsNull(resultado);
        }
    }
    [TestClass]
    public class ProductoTests
    {
        [TestMethod]
        public void Validar_StockONPrecioNegativo_LanzaExcepcion()
        {
            var producto = new Producto
            {
                nombre = "Mesa",
                descripcion = "Madera",
                imagen = "img.jpg",
                stock = 0,
                precio = -5
            };

            Assert.ThrowsException<PrecioStockException>(() => producto.Validar());
        }

        [TestMethod]
        public void Validar_DatosCorrectos_NoLanza()
        {
            var producto = new Producto
            {
                nombre = "Silla",
                descripcion = "Comoda",
                precio = 100,
                imagen = "img.jpg",
                stock = 5,
                SubCategoriaId = 1
            };

            producto.Validar();

            Assert.IsTrue(true);
        }
    }
    [TestClass]
    public class AgregarProductoCasoDeUsoTests
    {
        [TestMethod]
        public void Ejecutar_ArtesanoNull_LanzaExcepcion()
        {
            var repo = new Mock<IProductoRepositorio>();
            var caso = new AgregarProductoCasoDeUso(repo.Object);

            var dto = new AgregarProductoDto
            {
                Nombre = "Producto1",
                Descripcion = "Desc",
                Precio = 100,
                Stock = 5,
                Imagen = "img.jpg",
                SubCategoriaId = 1
            };

            Assert.ThrowsException<Exception>(() => caso.Ejecutar(dto, null));
        }

        [TestMethod]
        public void Ejecutar_LlamaRepositorioAgregar()
        {
            var repo = new Mock<IProductoRepositorio>();
            var caso = new AgregarProductoCasoDeUso(repo.Object);

            var artesano = new Artesano
            {
                id = 10,
                nombre = "Juan",
                apellido = "Perez",
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("art@mail.com")
            };

            var dto = new AgregarProductoDto
            {
                Nombre = "Mesa",
                Descripcion = "Mesa de madera",
                Precio = 200,
                Stock = 3,
                Imagen = "img.jpg",
                SubCategoriaId = 1
            };

            caso.Ejecutar(dto, artesano);

            repo.Verify(r => r.Agregar(It.IsAny<Producto>()), Times.Once);
        }

        [TestMethod]
        public void Ejecutar_MapeaFotosCorrectamente()
        {
            var repo = new Mock<IProductoRepositorio>();
            Producto productoCapturado = null;

            repo
                .Setup(r => r.Agregar(It.IsAny<Producto>()))
                .Callback<Producto>(p => productoCapturado = p);

            var caso = new AgregarProductoCasoDeUso(repo.Object);

            var artesano = new Artesano
            {
                id = 10,
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("art@mail.com")
            };

            var dto = new AgregarProductoDto
            {
                Nombre = "Mesa",
                Descripcion = "Madera",
                Precio = 100,
                Stock = 5,
                Imagen = "main.jpg",
                SubCategoriaId = 1,
                Fotos = new List<string> { "f1.jpg", "f2.jpg" }
            };

            caso.Ejecutar(dto, artesano);

            Assert.IsNotNull(productoCapturado);
            Assert.AreEqual(2, productoCapturado.Fotos.Count);
            Assert.AreEqual("f1.jpg", productoCapturado.Fotos[0].UrlImagen);
            Assert.AreEqual("f2.jpg", productoCapturado.Fotos[1].UrlImagen);
        }

        [TestMethod]
        public void Ejecutar_FotosNull_NoLanzaYNoAsignaFotos()
        {
            var repo = new Mock<IProductoRepositorio>();
            Producto capturado = null;

            repo
                .Setup(r => r.Agregar(It.IsAny<Producto>()))
                .Callback<Producto>(p => capturado = p);

            var caso = new AgregarProductoCasoDeUso(repo.Object);

            var artesano = new Artesano
            {
                id = 1,
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("art@mail.com")
            };

            var dto = new AgregarProductoDto
            {
                Nombre = "Producto",
                Descripcion = "Desc",
                Precio = 50,
                Stock = 3,
                Imagen = "img.jpg",
                SubCategoriaId = 1,
                Fotos = null
            };

            caso.Ejecutar(dto, artesano);

            Assert.IsNotNull(capturado);
            Assert.AreEqual(0, capturado.Fotos.Count);
        }

        [TestMethod]
        public void Ejecutar_FotosVacio_NoLanza()
        {
            var repo = new Mock<IProductoRepositorio>();
            Producto capturado = null;

            repo
                .Setup(r => r.Agregar(It.IsAny<Producto>()))
                .Callback<Producto>(p => capturado = p);

            var caso = new AgregarProductoCasoDeUso(repo.Object);

            var artesano = new Artesano
            {
                id = 1,
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("art@mail.com")
            };

            var dto = new AgregarProductoDto
            {
                Nombre = "Producto",
                Descripcion = "Desc",
                Precio = 50,
                Stock = 3,
                Imagen = "img.jpg",
                SubCategoriaId = 1,
                Fotos = new List<string>()
            };

            caso.Ejecutar(dto, artesano);

            Assert.IsNotNull(capturado);
            Assert.AreEqual(0, capturado.Fotos.Count);
        }
    }
}

