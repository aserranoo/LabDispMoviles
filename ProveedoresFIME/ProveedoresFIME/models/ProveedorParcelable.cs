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
using Java.Interop;
using ProveedoresFIME.models.Parceables;

namespace ProveedoresFIME.models {
    public class ProveedorParcelable : Java.Lang.Object, IParcelable {
        public Proveedor Proveedor { get; set; }

        private static readonly GenericParcelableCreator<ProveedorParcelable> _creator = new GenericParcelableCreator<ProveedorParcelable>((parcel) => new ProveedorParcelable(parcel));
        public ProveedorParcelable() {

        }

        public ProveedorParcelable(Parcel parcel) {
            Proveedor=new Proveedor {
                Nombre=parcel.ReadString(),
                Telefono=parcel.ReadString(),
                Correo=parcel.ReadString()
            };
        }
        [ExportField("CREATOR")]
        public static GenericParcelableCreator<ProveedorParcelable> GetCreator() {
            return _creator;
        }
        public new IntPtr Handle;

        public int DescribeContents() {
            return 0;
        }

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags) {
            dest.WriteString(Proveedor.Nombre);
            dest.WriteString(Proveedor.Telefono);
            dest.WriteString(Proveedor.Correo);
        }

        public new void Dispose() {
            throw new NotImplementedException();
        }
    }
}