using System;
using System.Collections.Generic;

namespace ProveedoresFimeApi.Models
{
    public partial class Proveedores
    {
        public int ProveedorId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Rfc { get; set; }
        public string Correo { get; set; }
    }
}
