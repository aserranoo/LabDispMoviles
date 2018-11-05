using System;
using System.Collections.Generic;

namespace ProveedoresFimeApi.Models
{
    public partial class Proveedores
    {
        public Proveedores()
        {
            Articulos = new HashSet<Articulos>();
            Cotizaciones = new HashSet<Cotizaciones>();
            SolicitudArticulos = new HashSet<SolicitudArticulos>();
        }

        public int ProveedorId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Rfc { get; set; }
        public string Correo { get; set; }

        public ICollection<Articulos> Articulos { get; set; }
        public ICollection<Cotizaciones> Cotizaciones { get; set; }
        public ICollection<SolicitudArticulos> SolicitudArticulos { get; set; }
    }
}
