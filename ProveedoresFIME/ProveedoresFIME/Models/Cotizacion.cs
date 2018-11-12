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
    public class Cotizacion {
        public int CotizacionId { get; set; }
        public int ProveedorId { get; set; }
        public DateTime Fecha { get; set; }
        public Cotizacion() {
            this.CotizacionId=0;
            this.ProveedorId=0;
            this.Fecha=new DateTime();
        }
    }
}