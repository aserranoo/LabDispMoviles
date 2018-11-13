using System;
using System.Collections.Generic;

namespace ProveedoresFimeApi.Models
{
    public partial class Cotizaciones
    {
        public Cotizaciones()
        {
            SolicitudArticulos = new HashSet<SolicitudArticulos>();
        }

        public int CotizacionId { get; set; }
        public int ProveedorId { get; set; }
        public string PrecioCotizacion { get; set; }
        public int EstatusId { get; set; }
        public DateTime Fecha { get; set; }

        public CatEstatus Estatus { get; set; }
        public Proveedores Proveedor { get; set; }
        public ICollection<SolicitudArticulos> SolicitudArticulos { get; set; }
    }
}
