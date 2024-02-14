using Proyecto.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BLL.Services
{
    public interface IVehiculoService
    {
        Task<bool> Insertar(Vehiculo modelo);
        Task<bool> Actualizar(Vehiculo modelo);
        Task<bool> Eliminar(int id);
        Task<Vehiculo> Obtener(int id);
        Task<IQueryable<Vehiculo>> ObtenerTodos();
    }
}
