using Proyecto.DAL.Data;
using Proyecto.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.DAL.Repositories
{
    public class VehiculoRepository : IGenericRepository<Vehiculo>
    {
        private readonly ApplicationDbContext _db;

        public VehiculoRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<bool> Actualizar(Vehiculo modelo)
        {
            _db.Vehiculos.Update(modelo);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(int id)
        {
            Vehiculo Vehiculo = new Vehiculo();
            Vehiculo = _db.Vehiculos.Where(x => x.IdVehiculo == id).FirstOrDefault() ?? new Vehiculo();
            _db.Remove(Vehiculo);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Insertar(Vehiculo modelo)
        {
            _db.Vehiculos.Add(modelo);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Vehiculo> Obtener(int id)
        {
            return await _db.Vehiculos.FindAsync(id);
        }

        public async Task<IQueryable<Vehiculo>> ObtenerTodos()
        {
            IQueryable<Vehiculo> Vehiculos = _db.Vehiculos;
            return Vehiculos;
        }
    }
}
