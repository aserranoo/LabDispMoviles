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
        public SolicitudCotizacion() {
            this.CotizacionId=0;
            this.ProveedorId=0;
            this.ArticuloId=0;
            this.Cantidad=0;
            this.Descripcion="";
        }
    }
}