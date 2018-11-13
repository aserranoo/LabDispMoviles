using System;
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
    public class SolicitudCotizacion {
        public int CotizacionId { get; set; }
        public int ProveedorId { get; set; }
        public int ArticuloId { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public Articulo Articulo { get; set; }
        public Cotizacion Cotizacion { get; set; }
        public Proveedor Proveedor { get; set; }
    }
}