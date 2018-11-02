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

                //var photo = view.FindViewById<ImageView>(Resource.Id.photoImageView);
                holder.Nombre = view.FindViewById<TextView>(Resource.Id.nombreTextView);
                holder.Contacto = view.FindViewById<TextView>(Resource.Id.contactoTextView);

                view.Tag=holder;
            } else {
                holder=view.Tag as ProveedorViewHolder;
            }

            var tempServiceItem = Proveedores[position];
            //var tempServiceItem = new ServiceItem();
            holder.Nombre.Text=tempServiceItem.Nombre;
            holder.Contacto.Text=tempServiceItem.Contacto;
            //holder.Price.Text=String.Format("{0:C}", tempServiceItem.Price);

            //holder..Click+=(object sender, EventArgs e)=&gt;
            //{
            //    ServiceItems.RemoveAt(position);
            //    NotifyDataSetChanged();
            //};
            //holder.EditButton.Click+=(object sender, EventArgs e)=&gt;
            //{
            //    //Todo - implement edit Service
            //};


            //var holder = (ViewHolder)view.Tag;


            //holder.Photo.SetImageDrawable(ImageManager.Get(parent.Context, users[position].ImageUrl));
            //holder.Name.Text=users[position].Name;
            //holder.Department.Text=users[position].Department;


            return view;

        }
    }
}