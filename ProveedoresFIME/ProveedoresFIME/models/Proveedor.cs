﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ProveedoresFIME.Models {
    public class Proveedor {
        public int ProveedorId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Rfc { get; set; }
        public string Correo { get; set; }
        public ICollection<Articulo> Articulos { get; set; }
        public ICollection<Cotizacion> Cotizaciones { get; set; }
        public ICollection<SolicitudCotizacion> SolicitudArticulos { get; set; }
    }
}