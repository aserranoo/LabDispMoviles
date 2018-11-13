using System;
using System.Collections.Generic;

namespace ProveedoresFimeApi.Models
{
    public partial class Articulos
    {
        public Articulos()
        {
            SolicitudArticulos = new HashSet<SolicitudArticulos>();
        }

        public int ArticuloId { get; set; }
        public int? ProveedorId { get; set; }
        public string Descripcion { get; set; }
        public bool? Status { get; set; }

        public Articulos Articulo { get; set; }
        public Proveedores Proveedor { get; set; }
        public Articulos InverseArticulo { get; set; }
        public ICollection<SolicitudArticulos> SolicitudArticulos { get; set; }
    }
}
