using Proyecto.DAL.Repositories;
using Proyecto.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BLL.Services
{
    public class VehiculoService : IVehiculoService
    {
        private readonly IGenericRepository<Vehiculo> _repository;
        
        public VehiculoService(IGenericRepository<Vehiculo> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Actualizar(Vehiculo modelo)
        {
            return await _repository.Actualizar(modelo);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _repository.Eliminar(id);
        }

        public async Task<bool> Insertar(Vehiculo modelo)
        {
            return await _repository.Insertar(modelo);
        }

        public async Task<Vehiculo> Obtener(int id)
        {
            return await _repository.Obtener(id);
        }

        public async Task<IQueryable<Vehiculo>> ObtenerTodos()
        {
            return await _repository.ObtenerTodos();
        }
    }
}
