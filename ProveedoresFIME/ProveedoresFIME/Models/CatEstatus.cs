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
    public class CatEstatus {

        public int EstatusId { get; set; }
        public string Descipcion { get; set; }

        public ICollection<Cotizacion> Cotizaciones { get; set; }
    }
}