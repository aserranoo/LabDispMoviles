using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProveedoresFIME.models;
using ProveedoresFIME.Models;
using static Android.Support.V7.Widget.RecyclerView;

namespace ProveedoresFIME.Adapters {
   public class ListViewCustomAdapter : BaseAdapter<Proveedor> {
        protected Activity Context = null;
        protected List<Proveedor> Proveedores;

        public ListViewCustomAdapter(List<Proveedor> Proveedores) {
            this.Proveedores=Proveedores;
        }

        public ListViewCustomAdapter(Activity context, List<Proveedor> proveedores) {
            this.Context=context;
            this.Proveedores=proveedores;
        }
        public override Proveedor this[int position] {
            get {
                return Proveedores[position];
            }
        }

        public override int Count {
            get {
                return Proveedores.Count;
            }
        }

        public override long GetItemId(int position) {
            return position;
        }
        public void Add(Proveedor proveedor) {
            Proveedores.Add(proveedor);
            this.NotifyDataSetChanged();
        }
        public override View GetView(int position, View convertView, ViewGroup parent) {
            ProveedorViewHolder holder = null;
            var view=convertView;

            if (view==null) {
                holder=new ProveedorViewHolder();
                view=LayoutInflater.From(parent.Context).Inflate(Resource.Layout.proveedorRow, parent, false);

                holder.Nombre = view.FindViewById<TextView>(Resource.Id.nombreTextView);
                holder.Telefono = view.FindViewById<TextView>(Resource.Id.telefonoTextView);

                view.Tag=holder;
            } else {
                holder=view.Tag as ProveedorViewHolder;
            }

            var tempServiceItem = Proveedores[position];
            holder.Nombre.Text=tempServiceItem.Nombre;
            holder.Telefono.Text=tempServiceItem.Telefono;
            return view;

        }
    }
}