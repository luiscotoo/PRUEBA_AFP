using Microsoft.AspNetCore.Mvc;
using Proyecto.BLL.Services;
using Proyecto.Models.Models;
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;

namespace Proyecto.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoService _service;
        public VehiculoController(IVehiculoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            List<Vehiculo> lista = new List<Vehiculo>();
            var mensaje = "";

            try
            {
                var queryVehiculos = await _service.ObtenerTodos();
                lista = queryVehiculos.ToList();
                mensaje = "ok";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            
            return StatusCode(StatusCodes.Status200OK, new { mensaje = mensaje, response = lista });
        }

        [HttpGet]
        [Route("ObtenerVehiculo/{idVehiculo:int}")]
        public async Task<IActionResult> ObtenerVehiculo(int idVehiculo)
        {
            var vehiculo = new Vehiculo();
            var mensaje = "";

            try
            {
                vehiculo = await _service.Obtener(idVehiculo);

                if (vehiculo == null)
                    return BadRequest("No se encontró el Vehiculo");

                mensaje = "ok";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, new { mensaje = mensaje, response = vehiculo });
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] Vehiculo Vehiculo)
        {
            var mensaje = "";

            try
            {
                var respuesta = await _service.Insertar(Vehiculo);
                if(!respuesta)
                    return BadRequest("Ocurrió un error interno");
                mensaje = "ok";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, new { mensaje = mensaje });
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Vehiculo vehiculoNuevo)
        {
            var mensaje = "";

            try
            {
                var vehiculo = await _service.Obtener(vehiculoNuevo.IdVehiculo);

                if (vehiculo == null)
                    return BadRequest("No se encontró el vehiculo");

                vehiculo.NumeroPlaca = vehiculoNuevo.NumeroPlaca != null ? vehiculoNuevo.NumeroPlaca : vehiculo.NumeroPlaca;
                vehiculo.Vin = vehiculoNuevo.Vin != null ? vehiculoNuevo.Vin : vehiculo.Vin;
                vehiculo.Marca = vehiculoNuevo.Marca != null ? vehiculoNuevo.Marca : vehiculo.Marca;
                vehiculo.Serie = vehiculoNuevo.Serie != null ? vehiculoNuevo.Serie : vehiculo.Serie;
                vehiculo.Anio = vehiculoNuevo.Anio != null ? vehiculoNuevo.Anio : vehiculo.Anio;
                vehiculo.Color = vehiculoNuevo.Color != null ? vehiculoNuevo.Color : vehiculo.Color;
                vehiculo.CantidadPuertas = vehiculoNuevo.CantidadPuertas != null ? vehiculoNuevo.CantidadPuertas : vehiculo.CantidadPuertas;

                var respuesta = await _service.Actualizar(vehiculo);
                if (!respuesta)
                    return BadRequest("Ocurrió un error interno");
                
                mensaje = "ok";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, new { mensaje = mensaje });
        }


        [HttpDelete]
        [Route("Eliminar/{idVehiculo:int}")]
        public async Task<IActionResult> Eliminar(int idVehiculo)
        {
            var mensaje = "";

            try
            {
                var vehiculo = await _service.Obtener(idVehiculo);

                if (vehiculo == null)
                    return BadRequest("No se encontró el Vehiculo");

                var respuesta = await _service.Eliminar(idVehiculo);
                
                if (!respuesta)
                    return BadRequest("Ocurrió un error interno");

                mensaje = "ok";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, new { mensaje = mensaje });
        }

    }
}
