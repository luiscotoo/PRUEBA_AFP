using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Proyecto.API.Controllers;
using Proyecto.BLL.Services;
using Proyecto.Models.Models;

namespace Proyecto.API.TEST
{
    [TestClass]
    public class TestVehiculoController
    {
        #region Casos correctos
        [TestMethod]
        public async Task Lista_ReturnsOkWhenDataCorrect()
        {
            //Preparacion
            var mockService = new Mock<IVehiculoService>();
            var vehiculosMock = new List<Vehiculo> { new Vehiculo { IdVehiculo = 1, NumeroPlaca = "P123456", Serie = "S", Vin = "12345678901234567", Marca = "Nissan", Color = "Azul", Anio = 2011, CantidadPuertas = 4 } };
            mockService.Setup(s => s.ObtenerTodos()).ReturnsAsync(vehiculosMock.AsQueryable());

            var controlador = new VehiculoController(mockService.Object);

            //Actuar
            var resultado = await controlador.Lista() as ObjectResult;

            //Verificar
            Assert.IsNotNull(resultado);
            Assert.AreEqual(StatusCodes.Status200OK, resultado.StatusCode);

            var modelo = resultado.Value;

            Assert.IsNotNull(modelo);
            Assert.AreEqual("ok", (string)modelo.GetType().GetProperty("mensaje")?.GetValue(modelo));
            Assert.IsNotNull((List<Vehiculo>)modelo.GetType().GetProperty("response")?.GetValue(modelo));

            var vehiculoResultado = ((List<Vehiculo>)modelo.GetType().GetProperty("response")?.GetValue(modelo))[0];
            Assert.AreEqual(1, vehiculoResultado.IdVehiculo);
            Assert.AreEqual("P123456", vehiculoResultado.NumeroPlaca);
            Assert.AreEqual("12345678901234567", vehiculoResultado.Vin);
            Assert.AreEqual("Nissan", vehiculoResultado.Marca);
            Assert.AreEqual("S", vehiculoResultado.Serie);
            Assert.AreEqual(2011, vehiculoResultado.Anio);
            Assert.AreEqual("Azul", vehiculoResultado.Color);
            Assert.AreEqual(4, vehiculoResultado.CantidadPuertas);

        }

        [TestMethod]
        public async Task ObtenerVehiculo_ReturnsOkWhenDataCorrect()
        {
            //Preparacion
            var mockService = new Mock<IVehiculoService>();
            var vehiculoMock = new Vehiculo { IdVehiculo = 1, NumeroPlaca = "P123456", Serie = "S", Vin = "12345678901234567", Marca = "Nissan", Color = "Azul", Anio = 2011, CantidadPuertas = 4 };
            mockService.Setup(s => s.Obtener(It.IsAny<int>())).ReturnsAsync(vehiculoMock);

            var controlador = new VehiculoController(mockService.Object);

            //Actuar
            var resultado = await controlador.ObtenerVehiculo(1) as ObjectResult;

            //Verificar
            Assert.IsNotNull(resultado);
            Assert.AreEqual(StatusCodes.Status200OK, resultado.StatusCode);

            var modelo = resultado.Value;

            Assert.IsNotNull(modelo);
            Assert.AreEqual("ok", (string)modelo.GetType().GetProperty("mensaje")?.GetValue(modelo));
            Assert.IsNotNull((Vehiculo)modelo.GetType().GetProperty("response")?.GetValue(modelo));

            
            var vehiculoResultado = (Vehiculo)modelo.GetType().GetProperty("response")?.GetValue(modelo);
            Assert.AreEqual(1, vehiculoResultado.IdVehiculo);
            Assert.AreEqual("P123456", vehiculoResultado.NumeroPlaca);
            Assert.AreEqual("12345678901234567", vehiculoResultado.Vin);
            Assert.AreEqual("Nissan", vehiculoResultado.Marca);
            Assert.AreEqual("S", vehiculoResultado.Serie);
            Assert.AreEqual(2011, vehiculoResultado.Anio);
            Assert.AreEqual("Azul", vehiculoResultado.Color);
            Assert.AreEqual(4, vehiculoResultado.CantidadPuertas);
        }

        [TestMethod]
        public async Task Guardar_ReturnsOkWhenDataCorrect()
        {
            //Preparacion
            var mockService = new Mock<IVehiculoService>();
            mockService.Setup(s => s.Insertar(It.IsAny<Vehiculo>())).ReturnsAsync(true);

            var controlador = new VehiculoController(mockService.Object);

            //Actuar
            var resultado = await controlador.Guardar(new Vehiculo()) as ObjectResult;

            //Verificar
            Assert.IsNotNull(resultado);
            Assert.AreEqual(StatusCodes.Status200OK, resultado.StatusCode);

            var modelo = resultado.Value;

            Assert.IsNotNull(modelo);
            Assert.AreEqual("ok", (string)modelo.GetType().GetProperty("mensaje")?.GetValue(modelo));
        }

        [TestMethod]
        public async Task Editar_ReturnsOkWhenDataCorrect()
        {
            //Preparacion
            var mockService = new Mock<IVehiculoService>();
            mockService.Setup(s => s.Obtener(It.IsAny<int>())).ReturnsAsync(new Vehiculo());
            mockService.Setup(s => s.Actualizar(It.IsAny<Vehiculo>())).ReturnsAsync(true);

            var controlador = new VehiculoController(mockService.Object);

            //Actuar
            var resultado = await controlador.Editar(new Vehiculo()) as ObjectResult;

            //Verificar
            Assert.IsNotNull(resultado);
            Assert.AreEqual(StatusCodes.Status200OK, resultado.StatusCode);

            var modelo = resultado.Value;

            Assert.IsNotNull(modelo);
            Assert.AreEqual("ok", (string)modelo.GetType().GetProperty("mensaje")?.GetValue(modelo));
        }

        [TestMethod]
        public async Task Eliminar_ReturnsOkWhenDataCorrect()
        {
            //Preparacion
            var mockService = new Mock<IVehiculoService>();
            mockService.Setup(s => s.Obtener(It.IsAny<int>())).ReturnsAsync(new Vehiculo());
            mockService.Setup(s => s.Eliminar(It.IsAny<int>())).ReturnsAsync(true);

            var controlador = new VehiculoController(mockService.Object);

            //Actuar
            var resultado = await controlador.Eliminar(1) as ObjectResult;

            //Verificar
            Assert.IsNotNull(resultado);
            Assert.AreEqual(StatusCodes.Status200OK, resultado.StatusCode);

            var modelo = resultado.Value;

            Assert.IsNotNull(modelo);
            Assert.AreEqual("ok", (string)modelo.GetType().GetProperty("mensaje")?.GetValue(modelo));
        }
        #endregion

        #region Casos de error
        [TestMethod]
        public async Task ObtenerVehiculo_DeberiaRetornarBadRequestCuandoNoSeEncuentraElVehiculo()
        {
            //Preparacion
            var mockService = new Mock<IVehiculoService>();
            mockService.Setup(s => s.Obtener(It.IsAny<int>())).ReturnsAsync((Vehiculo)null);

            var controlador = new VehiculoController(mockService.Object);

            //Actuar
            var resultado = await controlador.ObtenerVehiculo(1) as ObjectResult;

            //Verificar
            Assert.IsNotNull(resultado);
            Assert.AreEqual(StatusCodes.Status400BadRequest, resultado.StatusCode);

            var mensaje = resultado.Value as string;
            Assert.IsNotNull(mensaje);
            Assert.AreEqual("No se encontró el Vehiculo", mensaje);
        }

        [TestMethod]
        public async Task Guardar_DeberiaRetornarBadRequestCuandoOcurreUnError()
        {
            //Preparacion
            var mockService = new Mock<IVehiculoService>();
            mockService.Setup(s => s.Insertar(It.IsAny<Vehiculo>())).ReturnsAsync(false);

            var controlador = new VehiculoController(mockService.Object);

            //Actuar
            var resultado = await controlador.Guardar(new Vehiculo()) as ObjectResult;

            //Verificar
            Assert.IsNotNull(resultado);
            Assert.AreEqual(StatusCodes.Status400BadRequest, resultado.StatusCode);

            var mensaje = resultado.Value as string;
            Assert.IsNotNull(mensaje);
            Assert.AreEqual("Ocurrió un error interno", mensaje);
        }

        [TestMethod]
        public async Task Editar_DeberiaRetornarBadRequestCuandoNoSeEncuentraElVehiculo()
        {
            //Preparacion
            var mockService = new Mock<IVehiculoService>();
            mockService.Setup(s => s.Obtener(It.IsAny<int>())).ReturnsAsync((Vehiculo)null);

            var controlador = new VehiculoController(mockService.Object);

            //Actuar
            var resultado = await controlador.Editar(new Vehiculo()) as ObjectResult;

            //Verificar
            Assert.IsNotNull(resultado);
            Assert.AreEqual(StatusCodes.Status400BadRequest, resultado.StatusCode);

            var mensaje = resultado.Value as string;
            Assert.IsNotNull(mensaje);
            Assert.AreEqual("No se encontró el vehiculo", mensaje);
        }

        [TestMethod]
        public async Task Eliminar_DeberiaRetornarBadRequestCuandoNoSeEncuentraElVehiculo()
        {
            //Preparacion
            var mockService = new Mock<IVehiculoService>();
            mockService.Setup(s => s.Obtener(It.IsAny<int>())).ReturnsAsync((Vehiculo)null);

            var controlador = new VehiculoController(mockService.Object);

            //Actuar
            var resultado = await controlador.Eliminar(1) as ObjectResult;

            //Verificar
            Assert.IsNotNull(resultado);
            Assert.AreEqual(StatusCodes.Status400BadRequest, resultado.StatusCode);

            var mensaje = resultado.Value as string;
            Assert.IsNotNull(mensaje);
            Assert.AreEqual("No se encontró el Vehiculo", mensaje);
        }
        #endregion
    }
}