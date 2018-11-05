using System;
using System.Collections.Generic;

namespace ProveedoresFimeApi.Models
{
    public partial class SolicitudArticulos
    {
        public int TranId { get; set; }
        public int CotizacionId { get; set; }
        public int ProveedorId { get; set; }
        public int ArticuloId { get; set; }
        public int Cantidad { get; set; }

        public Articulos Articulo { get; set; }
        public Cotizaciones Cotizacion { get; set; }
        public Proveedores Proveedor { get; set; }
    }
}
