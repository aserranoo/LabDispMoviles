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
    public class Articulo {
        public int ArticuloId { get; set; }
        public int? ProveedorId { get; set; }
        public string Descripcion { get; set; }
        public bool? Status { get; set; }
        public Proveedor Proveedor { get; set; }
        public ICollection<SolicitudCotizacion> SolicitudArticulos { get; set; }
    }
}