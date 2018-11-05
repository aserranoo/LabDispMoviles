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

namespace ProveedoresFIME.Adapters {
    class ProveedorViewHolder :Java.Lang.Object{
        public TextView Nombre { get; set; }
        public TextView Telefono { get; set; }

        public TextView Correo{ get; set; }
    }
}