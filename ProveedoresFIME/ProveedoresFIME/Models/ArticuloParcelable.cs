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

namespace ProveedoresFIME.Models {
    class ArticuloParcelable : Java.Lang.Object, IParcelable {
        public ArticuloSave Articulo { get; set; }
        private static readonly GenericParcelableCreator<ArticuloParcelable> _creator = new GenericParcelableCreator<ArticuloParcelable>((parcel) => new ArticuloParcelable(parcel));
        public ArticuloParcelable() {

        }
        public ArticuloParcelable(Parcel parcel) {
            Articulo=new ArticuloSave {
                ProveedorId=parcel.ReadInt(),
                Descripcion=parcel.ReadString(),
                DescripcionProveedor=parcel.ReadString()
            };
        }

        [ExportField("CREATOR")]
        public static GenericParcelableCreator<ArticuloParcelable> GetCreator() {
            return _creator;
        }

        public int DescribeContents() {
            return 0;
        }

        public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags) {
            dest.WriteInt((int)Articulo.ProveedorId);
            dest.WriteString(Articulo.Descripcion);
            dest.WriteString(Articulo.DescripcionProveedor);
        }

        public new void Dispose() {
            throw new NotImplementedException();
        }
    }
}