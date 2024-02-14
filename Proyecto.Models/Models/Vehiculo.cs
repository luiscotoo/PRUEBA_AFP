using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models.Models
{
    [Table("VEHICULO")]
    public class Vehiculo
    {
        [Key]
        [Column("ID_VEHICULO")]
        public int IdVehiculo { get; set; }

        [Column("NUMERO_PLACA")]
        [StringLength(20)]
        public string? NumeroPlaca { get; set; }

        [Column("VIN")]
        [StringLength(17)]
        public string? Vin { get; set; }

        [Column("MARCA")]
        [StringLength(50)]
        public string? Marca { get; set; }

        [Column("SERIE")]
        [StringLength(50)]
        public string? Serie { get; set; }

        [Column("Anio")]
        public int? Anio { get; set; }

        [Column("COLOR")]
        [StringLength(50)]
        public string? Color { get; set; }

        [Column("CANTIDAD_PUERTAS")]
        public int? CantidadPuertas { get; set; }

    }
}
