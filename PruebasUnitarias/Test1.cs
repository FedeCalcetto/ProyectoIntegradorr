using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Moq;
using ProyectoIntegrador.LogicaAplication.CasosDeUso;
using ProyectoIntegrador.LogicaAplication.Dtos;
using ProyectoIntegrador.LogicaAplication.Interface;
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
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("test@mail.com")

            };
            esperado.SetPasswordInicial("Password123");

            repo.Setup(r => r.BuscarPorEmail("test@mail.com"))
                .Returns(esperado);

            var caso = new LoginCasoDeUso(repo.Object);

            var resultado = caso.Ejecutar("test@mail.com", "Password123");

            Assert.IsNotNull(resultado);
            Assert.AreEqual("test@mail.com", resultado.email.email);
        }



        [TestMethod]
        public void Ejecutar_LoginIncorrecto_RetornaNull()
        {
            var repo = new Mock<IUsuarioRepositorio>();

            var usuario = new Cliente
            {
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("EmailIncorrecto@mail.com")
            };
            usuario.SetPasswordInicial("Password123");

            repo.Setup(r => r.BuscarPorEmail("test@mail.com"))
                .Returns(usuario);

            var caso = new LoginCasoDeUso(repo.Object);

            var resultado = caso.Ejecutar("test@mail.com", "PasswordIncorrecta");

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
        public void validarContra_ContraseñasNoCoinciden_LanzaNoCoincideException()
        {
            var cliente = new Cliente
            {
                password = BCrypt.Net.BCrypt.HashPassword("Password123"),
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("cliente@mail.com")
            };

            Assert.ThrowsException<NoCoincideException>(() =>
                cliente.validarContra(
                    "PasswordNueva1",
                    "PasswordNueva2",
                    "Password123"
                ));
        }

        [TestMethod]
        public void validarEditar_DireccionInvalida_LanzaExcepcion()
        {
            var cliente = new Cliente
            {
                nombre = "Juan",
                apellido = "Lopez",
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("cliente@mail.com"),
                direccion = new Direccion("", "", "") 
            };

            Assert.ThrowsException<DireccionException>(() => cliente.validarEditar());
        }

        [TestMethod]
        public void Ejecutar_ClienteExiste_LlamaEliminar()
        {
            var repo = new Mock<IClienteRepositorio>();
            var obtener = new Mock<IObtenerCliente>();

            var cliente = new Cliente { id = 10 };

            obtener.Setup(o => o.Ejecutar("cliente@mail.com"))
                   .Returns(cliente);

            var caso = new EliminarClienteCasoDeUso(obtener.Object, repo.Object);

            caso.Ejecutar("cliente@mail.com");

            repo.Verify(r => r.Eliminar(10), Times.Once);
        }

        [TestMethod]
        public void Ejecutar_ClienteNoExiste_LanzaExcepcion()
        {
            var repo = new Mock<IClienteRepositorio>();
            var obtener = new Mock<IObtenerCliente>();

            obtener.Setup(o => o.Ejecutar("noexiste@mail.com"))
                   .Returns((Cliente)null);

            var caso = new EliminarClienteCasoDeUso(obtener.Object, repo.Object);

            Assert.ThrowsException<Exception>(() =>
                caso.Ejecutar("noexiste@mail.com")
            );

            // Verifica que NO se llamó a eliminar ningún ID
            repo.Verify(r => r.Eliminar(It.IsAny<int>()), Times.Never);
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

        [TestMethod] //eliminar artesano ok, explicacion para no olvidarme xd: Mock<> crea una imitación falsa de la interfaz, para que no use la base de datos real. Esto es lo que permite que un test unitario funcione en aislamiento.
        public void Ejecutar_ArtesanoExiste_LlamaEliminar()
        {
            var repo = new Mock<IArtesanoRepositorio>();
            var obtener = new Mock<IObtenerArtesano>();

            var artesano = new Artesano { id = 4 };

            obtener.Setup(o => o.Ejecutar("art@mail.com")).Returns(artesano); //Acá ya se esta usando las clases del sistema, pero con repositorios falsos.

            var caso = new EliminarArtesanoCasoDeUso(repo.Object, obtener.Object);

            caso.Ejecutar("art@mail.com");

            repo.Verify(r => r.Eliminar(4), Times.Once);
        }



        [TestMethod]
        public void Ejecutar_ArtesanoExistePeroIdNoExiste_LanzaExcepcion() //por id
        {
            var repo = new Mock<IArtesanoRepositorio>();
            var obtener = new Mock<IObtenerArtesano>();

            // Artesano existente con ID = 9999
            var artesano = new Artesano { id = 9999 };

            // El email existe → devuelve un artesano válido
            obtener.Setup(o => o.Ejecutar("art@mail.com"))
                   .Returns(artesano);

            // PERO el repositorio lanza excepción porque ese ID no existe en BD
            repo.Setup(r => r.Eliminar(9999))
                .Throws(new Exception("No existe artesano con ese ID"));

            var caso = new EliminarArtesanoCasoDeUso(repo.Object, obtener.Object);

            // Test: el caso de uso debe lanzar excepción
            Assert.ThrowsException<Exception>(() =>
                caso.Ejecutar("art@mail.com")
            );

            // Y verificar que realmente intentó eliminar ese ID
            repo.Verify(r => r.Eliminar(9999), Times.Once);
        }

        [TestMethod]
        public void Ejecutar_ArtesanoNoExiste_LanzaExcepcion() // por email
        {
            var repo = new Mock<IArtesanoRepositorio>();
            var obtener = new Mock<IObtenerArtesano>();

            // Simula que NO existe artesano con ese email
            obtener.Setup(o => o.Ejecutar("noexiste@mail.com"))
                   .Returns((Artesano)null);

            var caso = new EliminarArtesanoCasoDeUso(repo.Object, obtener.Object);

            Assert.ThrowsException<Exception>(() =>
                caso.Ejecutar("noexiste@mail.com")
            );

            // Verifica que NO se llamó a eliminar
            repo.Verify(r => r.Eliminar(It.IsAny<int>()), Times.Never);
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
    [TestClass]
    public class EditarProductoCasoDeUsoTests
    {
        [TestMethod]
        public void Ejecutar_ProductoNoEncontrado_LanzaExcepcion()
        {
            var repo = new Mock<IProductoRepositorio>();

            repo.Setup(r => r.Obtener(1)).Returns((Producto)null);

            var caso = new EditarProductoCasoDeUso(repo.Object);

            var dto = new EditarProductoDto
            {
                Id = 1,
                Nombre = "Nuevo",
                Descripcion = "Desc",
                Precio = 10,
                Stock = 2,
                SubCategoriaId = 1,
                ImagenPrincipal = "img.jpg",
                Fotos = new List<string>()
            };

            Assert.ThrowsException<Exception>(() => caso.Ejecutar(dto));
        }

        [TestMethod]
        public void Ejecutar_ActualizaCamposYLLamaRepositorioEditar()
        {
            var repo = new Mock<IProductoRepositorio>();
            Producto productoGuardado = null;
            List<string> fotosGuardadas = null;

            var productoExistente = new Producto
            {
                id = 1,
                nombre = "Viejo",
                descripcion = "Vieja desc",
                precio = 50,
                stock = 3,
                imagen = "old.jpg",
                SubCategoriaId = 2
            };

            // Mock del Obtener
            repo.Setup(r => r.Obtener(1)).Returns(productoExistente);

            // Mock para capturar lo enviado a Editar()
            repo.Setup(r => r.Editar(It.IsAny<Producto>(), It.IsAny<List<string>>()))
                .Callback<Producto, List<string>>((p, f) =>
                {
                    productoGuardado = p;
                    fotosGuardadas = f;
                });

            var caso = new EditarProductoCasoDeUso(repo.Object);

            var dto = new EditarProductoDto
            {
                Id = 1,
                Nombre = "NuevoNombre",
                Descripcion = "NuevaDesc",
                Precio = 200,
                Stock = 10,
                SubCategoriaId = 5,
                ImagenPrincipal = "nueva.jpg",
                Fotos = new List<string> { "foto1.jpg", "foto2.jpg" }
            };

            caso.Ejecutar(dto);

            // ----------------------------------
            // Verificamos que los campos cambien
            // ----------------------------------
            Assert.AreEqual("NuevoNombre", productoGuardado.nombre);
            Assert.AreEqual("NuevaDesc", productoGuardado.descripcion);
            Assert.AreEqual(200, productoGuardado.precio);
            Assert.AreEqual(10, productoGuardado.stock);
            Assert.AreEqual(5, productoGuardado.SubCategoriaId);
            Assert.AreEqual("nueva.jpg", productoGuardado.imagen);

            // ----------------------------------
            // Verificamos que las fotos se pasen bien
            // ----------------------------------
            Assert.AreEqual(2, fotosGuardadas.Count);
            Assert.AreEqual("foto1.jpg", fotosGuardadas[0]);
            Assert.AreEqual("foto2.jpg", fotosGuardadas[1]);

            // ----------------------------------
            // Verificamos que se llamó al repositorio
            // ----------------------------------
            repo.Verify(r => r.Editar(It.IsAny<Producto>(), It.IsAny<List<string>>()), Times.Once);
        }
    }
    [TestClass]
    public class EliminarArtesanoCasoDeUsoTests
    {
        [TestMethod]
        public void Ejecutar_ArtesanoNoExiste_LanzaExcepcion()
        {
            // Arrange
            var repo = new Mock<IArtesanoRepositorio>();
            var obtener = new Mock<IObtenerArtesano>();

            obtener.Setup(o => o.Ejecutar("correo@test.com"))
                   .Returns((Artesano)null);

            var caso = new EliminarArtesanoCasoDeUso(repo.Object, obtener.Object);

            // Act & Assert
            Assert.ThrowsException<Exception>(() => caso.Ejecutar("correo@test.com"));
        }

        [TestMethod]
        public void Ejecutar_ArtesanoExiste_LlamaRepositorioEliminarConIdCorrecto()
        {
            // Arrange
            var repo = new Mock<IArtesanoRepositorio>();
            var obtener = new Mock<IObtenerArtesano>();

            var artesano = new Artesano
            {
                id = 10,
                nombre = "Juan",
                apellido = "Perez",
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("correo@test.com")
            };

            obtener.Setup(o => o.Ejecutar("correo@test.com"))
                   .Returns(artesano);

            var caso = new EliminarArtesanoCasoDeUso(repo.Object, obtener.Object);

            // Act
            caso.Ejecutar("correo@test.com");

            // Assert
            repo.Verify(r => r.Eliminar(10), Times.Once);
        }
    }
    [TestClass]
    public class EliminarClienteCasoDeUsoTests
    {
        [TestMethod]
        public void Ejecutar_ClienteNoExiste_LanzaExcepcion()
        {
            // Arrange
            var obtener = new Mock<IObtenerCliente>();
            var repo = new Mock<IClienteRepositorio>();

            obtener.Setup(o => o.Ejecutar("test@correo.com"))
                   .Returns((Cliente)null);

            var caso = new EliminarClienteCasoDeUso(obtener.Object, repo.Object);

            // Act & Assert
            Assert.ThrowsException<Exception>(() => caso.Ejecutar("test@correo.com"));
        }


        [TestMethod]
        public void Ejecutar_ClienteExiste_LlamaRepositorioEliminarConIdCorrecto()
        {
            // Arrange
            var obtener = new Mock<IObtenerCliente>();
            var repo = new Mock<IClienteRepositorio>();

            var cliente = new Cliente
            {
                id = 5,
                nombre = "Pedro",
                apellido = "Gomez",
                email = new ProyectoIntegrador.LogicaNegocio.ValueObjects.Email("test@correo.com")
            };

            obtener.Setup(o => o.Ejecutar("test@correo.com"))
                   .Returns(cliente);

            var caso = new EliminarClienteCasoDeUso(obtener.Object, repo.Object);

            // Act
            caso.Ejecutar("test@correo.com");

            // Assert
            repo.Verify(r => r.Eliminar(5), Times.Once);
        }
    }

    [TestClass]
    public class AgregarAlCarritoCasoDeUsoTests
    {
        [TestMethod]
        public void AgregarAlCarrito_ProductoNoExiste_LlamaAgregarItem()
        {
            // Arrange
            var repo = new Mock<ICarritoRepositorio>();

            repo.Setup(r => r.BuscarProducto(1, 10))
                .Returns((CarritoItem)null);

            var caso = new AgregarAlCarritoCasoDeUso(repo.Object);

            // Act
            caso.agregarAlCarrito(1, 10, 3);

            // Assert
            repo.Verify(r => r.AgregarItem(10, 1, 3), Times.Once);
            repo.Verify(r => r.Guardar(), Times.Never);
        }

        [TestMethod]
        public void AgregarAlCarrito_ProductoExiste_SumaCantidadYGuarda()
        {
            // Arrange
            var repo = new Mock<ICarritoRepositorio>();

            var itemExistente = new CarritoItem
            {
                cantidad = 2
            };

            repo.Setup(r => r.BuscarProducto(1, 10))
                .Returns(itemExistente);

            var caso = new AgregarAlCarritoCasoDeUso(repo.Object);

            // Act
            caso.agregarAlCarrito(1, 10, 3);

            // Assert
            Assert.AreEqual(5, itemExistente.cantidad);

            repo.Verify(r => r.Guardar(), Times.Once);
            repo.Verify(r => r.AgregarItem(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }
    }

    [TestClass]
    public class EliminarItemCarritoCasoDeUsoTests
    {
        [TestMethod]
        public void Eliminar_LlamaRepositorioEliminarItem()
        {
            // Arrange
            var repo = new Mock<ICarritoRepositorio>();
            var caso = new EliminarItemCarritoCasoDeUso(repo.Object);

            int carritoItemId = 5;
            int cantidadABorrar = 2;

            // Act
            caso.Eliminar(carritoItemId, cantidadABorrar);

            // Assert
            repo.Verify(
                r => r.EliminarItem(carritoItemId, cantidadABorrar),
                Times.Once
            );
        }
    }
    [TestClass]
    public class AgregarReporteCasoDeUsoTests
    {
        [TestMethod]
        public void Ejecutar_SinArtesanoYSinProducto_LanzaExcepcion()
        {
            // Arrange
            var repo = new Mock<IReporteRepositorio>();
            var caso = new AgregarReporteCasoDeUso(repo.Object);

            var dto = new AgregarReporteDto { razon = "Motivo" };
            var cliente = new Cliente { id = 1 };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
                caso.Ejecutar(dto, null, cliente, null));
        }

        [TestMethod]
        public void Ejecutar_ConArtesanoYProducto_LanzaExcepcion()
        {
            // Arrange
            var repo = new Mock<IReporteRepositorio>();
            var caso = new AgregarReporteCasoDeUso(repo.Object);

            var dto = new AgregarReporteDto { razon = "Motivo" };
            var cliente = new Cliente { id = 1 };
            var artesano = new Artesano { id = 2 };
            var producto = new Producto { id = 3 };

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
                caso.Ejecutar(dto, artesano, cliente, producto));
        }

        [TestMethod]
        public void Ejecutar_ReporteAProducto_LlamaRepositorioAgregar()
        {
            // Arrange
            var repo = new Mock<IReporteRepositorio>();
            Reporte reporteCapturado = null;

            repo.Setup(r => r.Agregar(It.IsAny<Reporte>()))
                .Callback<Reporte>(r => reporteCapturado = r);

            var caso = new AgregarReporteCasoDeUso(repo.Object);

            var dto = new AgregarReporteDto { razon = "Producto defectuoso" };
            var cliente = new Cliente { id = 1 };
            var producto = new Producto { id = 10 };

            // Act
            caso.Ejecutar(dto, null, cliente, producto);

            // Assert
            Assert.IsNotNull(reporteCapturado);
            Assert.AreEqual("Producto defectuoso", reporteCapturado.razon);
            Assert.AreEqual(cliente.id, reporteCapturado.clienteId);
            Assert.AreEqual(producto.id, reporteCapturado.productoId);
            Assert.IsNull(reporteCapturado.artesano);

            repo.Verify(r => r.Agregar(It.IsAny<Reporte>()), Times.Once);
        }

        [TestMethod]
        public void Ejecutar_ReporteAArtesano_LlamaRepositorioAgregar()
        {
            // Arrange
            var repo = new Mock<IReporteRepositorio>();
            Reporte reporteCapturado = null;

            repo.Setup(r => r.Agregar(It.IsAny<Reporte>()))
                .Callback<Reporte>(r => reporteCapturado = r);

            var caso = new AgregarReporteCasoDeUso(repo.Object);

            var dto = new AgregarReporteDto { razon = "Mal servicio" };
            var cliente = new Cliente { id = 1 };
            var artesano = new Artesano { id = 20 };

            // Act
            caso.Ejecutar(dto, artesano, cliente, null);

            // Assert
            Assert.IsNotNull(reporteCapturado);
            Assert.AreEqual("Mal servicio", reporteCapturado.razon);
            Assert.AreEqual(cliente.id, reporteCapturado.clienteId);
            Assert.AreEqual(artesano.id, reporteCapturado.artesanoId);
            Assert.IsNull(reporteCapturado.producto);

            repo.Verify(r => r.Agregar(It.IsAny<Reporte>()), Times.Once);
        }
    }
    [TestClass]
    public class ReporteTests
    {
        [TestMethod]
        public void Validar_RazonVacia_LanzaExcepcion()
        {
            var reporte = new Reporte
            {
                razon = ""
            };

            Assert.ThrowsException<LongitudRazonException>(() => reporte.Validar());
        }

        [TestMethod]
        public void Validar_RazonCorrecta_NoLanza()
        {
            var reporte = new Reporte
            {
                razon = "Motivo válido"
            };

            reporte.Validar();

            Assert.IsTrue(true);
        }
    }
    [TestClass]
    public class EliminarReporteCasoDeUsoTests
    {
        [TestMethod]
        public void Ejecutar_LlamaRepositorioEliminarConIdCorrecto()
        {
            // Arrange
            var repo = new Mock<IReporteRepositorio>();
            var caso = new EliminarReporteCasoDeUso(repo.Object);

            int reporteId = 1;

            // Act
            caso.Ejecutar(reporteId);

            // Assert
            repo.Verify(r => r.Eliminar(reporteId), Times.Once);
        }
    }
    [TestClass]
    public class AgregarArtesanoListaCasoDeUsoTests
    {
        [TestMethod]
        public void AgregarArtesano_LlamaRepositorioAgregarArtesano()
        {
            // Arrange
            var repo = new Mock<IClienteRepositorio>();
            var caso = new AgregarArtesanoListaCasoDeUso(repo.Object);

            var cliente = new Cliente
            {
                id = 1,
                artesanosSeguidos = new List<Artesano>()
            };

            var artesano = new Artesano
            {
                id = 10
            };

            // Act
            caso.agregarArtesano(cliente, artesano);

            // Assert
            repo.Verify(
                r => r.agregarArtesano(cliente, artesano),
                Times.Once
            );
        }
    }
    [TestClass]
    public class EliminarArtesanoListaCasoDeUsoTests
    {
        [TestMethod]
        public void EliminarArtesano_LlamaRepositorioEliminarArtesano()
        {
            // Arrange
            var repo = new Mock<IClienteRepositorio>();
            var caso = new EliminarArtesanoListaCasoDeUso(repo.Object);

            var cliente = new Cliente
            {
                id = 1,
                artesanosSeguidos = new List<Artesano>()
            };

            var artesano = new Artesano
            {
                id = 10
            };

            // Act
            caso.eliminarArtesano(cliente, artesano);

            // Assert
            repo.Verify(
                r => r.eliminarArtesano(cliente, artesano),
                Times.Once
            );
        }
    }
    [TestClass]
    public class BloquearArtesanoCasoDeUsoTests
    {
        [TestMethod]
        public void BloquearArtesano_LlamaRepositorioBloquearArtesano()
        {
            // Arrange
            var repo = new Mock<IArtesanoRepositorio>();
            var caso = new BloquearArtesanoCasoDeUso(repo.Object);
            int artesanoId = 5;

            // Act
            caso.bloquearArtesano(artesanoId);

            // Assert
            repo.Verify(
                r => r.bloquearArtesano(artesanoId),
                Times.Once
            );
        }
    }
    [TestClass]
    public class AgregrComentarioCasoDeUsoTests
    {
        [TestMethod]
        public void Ejecutar_ArtesanoNull_LanzaExcepcion()
        {
            // Arrange
            var repo = new Mock<IComentarioRepositorio>();
            var caso = new AgregrComentarioCasoDeUso(repo.Object);

            var dto = new AgregarComentarioDto
            {
                contenido = "Buen trabajo"
            };

            var cliente = new Cliente { id = 1 };

            // Act + Assert
            Assert.ThrowsException<Exception>(() =>
                caso.Ejecutar(dto, cliente, null)
            );
        }

        [TestMethod]
        public void Ejecutar_ClienteNull_LanzaExcepcion()
        {
            // Arrange
            var repo = new Mock<IComentarioRepositorio>();
            var caso = new AgregrComentarioCasoDeUso(repo.Object);

            var dto = new AgregarComentarioDto
            {
                contenido = "Excelente artesano"
            };

            var artesano = new Artesano { id = 2 };

            // Act + Assert
            Assert.ThrowsException<Exception>(() =>
                caso.Ejecutar(dto, null, artesano)
            );
        }

        [TestMethod]
        public void Ejecutar_DatosValidos_LlamaRepositorioAgregar()
        {
            // Arrange
            var repo = new Mock<IComentarioRepositorio>();
            var caso = new AgregrComentarioCasoDeUso(repo.Object);

            var dto = new AgregarComentarioDto
            {
                contenido = "Muy recomendable"
            };

            var cliente = new Cliente { id = 1 };
            var artesano = new Artesano { id = 2 };

            // Act
            caso.Ejecutar(dto, cliente, artesano);

            // Assert
            repo.Verify(r =>
                r.Agregar(It.Is<Comentario>(c =>
                    c.contenido == "Muy recomendable" &&
                    c.clienteId == 1 &&
                    c.artesanoId == 2
                )),
                Times.Once
            );
        }
    }

}

