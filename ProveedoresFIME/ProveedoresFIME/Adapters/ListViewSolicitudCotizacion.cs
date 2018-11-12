using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ProveedoresFIME.Models;

namespace ProveedoresFIME.Adapters {
    public class ListViewSolicitudCotizacion : RecyclerView.Adapter {

        Context context;
        string descripcion;
        List<SolicitudCotizacion> SolicitudArticulo;
        private RecyclerView mRecyclerView;
        
        public ListViewSolicitudCotizacion(Context context, List<SolicitudCotizacion> solicitud, RecyclerView mRecyclerView, string descripcion) {
            this.context=context;
            this.descripcion=descripcion;
            this.SolicitudArticulo=solicitud;
            this.mRecyclerView=mRecyclerView;
        }

        //public override SolicitudCotizacion this[int position] {
        //    get {
        //        return SolicitudArticulo[position];
        //    }
        //}

        //public override int Count {
        //    get {
        //        return SolicitudArticulo.Count;
        //    }
        //}

        public override int ItemCount {
            get { return SolicitudArticulo.Count; }
        }

        public override long GetItemId(int position) {
            return position;
        }
        public void Add(SolicitudCotizacion solicitud, string descripcion) {
            SolicitudArticulo.Add(solicitud);
            this.descripcion=descripcion;
            this.NotifyDataSetChanged();
        }

        //public override View GetView(int position, View convertView, ViewGroup parent) {
        //    var view = convertView;
        //    ListViewSolicitudCotizacionViewHolder holder = null;

        //    if (view!=null)
        //        holder=view.Tag as ListViewSolicitudCotizacionViewHolder;

        //    if (holder==null) {
        //        holder=new ListViewSolicitudCotizacionViewHolder();
        //        view=LayoutInflater.From(context).Inflate(Resource.Layout.solicitudCotizacion, parent, false);
        //        //replace with your item and your holder items
        //        //comment back in
        //        //view = inflater.Inflate(Resource.Layout.item, parent, false);
        //        holder.Descripcion=view.FindViewById<TextView>(Resource.Id.nombreArticulo);
        //        holder.Cantidad=view.FindViewById<TextView>(Resource.Id.cantidadValue);
        //        holder.BtnBorrar=view.FindViewById<Button>(Resource.Id.btnBorrar);
        //        holder.BtnActualizar=view.FindViewById<Button>(Resource.Id.btnActualizar);
        //        view.Tag=holder;
        //    }

        //    //fill in your items
        //    var tempServiceItem = SolicitudArticulo[position];
        //    holder.Descripcion.Text=tempServiceItem.Descripcion;
        //    holder.Cantidad.Text=tempServiceItem.Cantidad.ToString();

        //    holder.BtnBorrar.Click+=(sender, e)=>{
        //        SolicitudArticulo.RemoveAt(0);
        //        NotifyDataSetChanged();
        //    } ;
        //    return view;
        //}

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
            ListViewSolicitudCotizacionViewHolder myHolder = holder as ListViewSolicitudCotizacionViewHolder;
            myHolder.Descripcion.Text=SolicitudArticulo[position].Descripcion;
            myHolder.Cantidad.Text=SolicitudArticulo[position].Cantidad.ToString();
            if (!myHolder.BtnBorrar.HasOnClickListeners) {
                myHolder.BtnBorrar.Click+=delegate {
                    SolicitudArticulo.RemoveAt(position);
                    NotifyDataSetChanged();
                };
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
            View row=LayoutInflater.From(context).Inflate(Resource.Layout.solicitudCotizacion, parent, false);

            ListViewSolicitudCotizacionViewHolder holder = new ListViewSolicitudCotizacionViewHolder(row);
            holder.Descripcion=row.FindViewById<TextView>(Resource.Id.nombreArticulo);
            holder.Cantidad=row.FindViewById<TextView>(Resource.Id.cantidadValue);
            holder.BtnBorrar=row.FindViewById<Button>(Resource.Id.btnBorrar);
            //holder.BtnActualizar=row.FindViewById<Button>(Resource.Id.btnActualizar);
            return holder;
        }
        void mMainView_Click(object sender, EventArgs e) {
            int position = mRecyclerView.GetChildAdapterPosition((View)sender);
        }
    }

    class ListViewSolicitudCotizacionViewHolder : RecyclerView.ViewHolder {
        public TextView Descripcion { get; set; }
        public TextView Cantidad { get; set; }
        public Button BtnBorrar { get; set; }
        //public Button BtnActualizar { get; set; }
        public View mMainView { get; set; }
        public ListViewSolicitudCotizacionViewHolder(View view) : base(view) {
            mMainView=view;
        }
    }
}