using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using ProveedoresFIME.Models;

namespace ProveedoresFIME.Adapters {
   public  class ListViewCotizacionAdapter : RecyclerView.Adapter {

        Context context;
        private List<Cotizacion> cotizacion;
        private RecyclerView mRecyclerView;

        public ListViewCotizacionAdapter(Context context, List<Cotizacion> cotizacion, RecyclerView mRecyclerView) {
            this.context=context;
            this.cotizacion=cotizacion;
            this.mRecyclerView=mRecyclerView;
        }
        public override int ItemCount {
            get { return cotizacion.Count; }
        }

        public override long GetItemId(int position) {
            return position;
        }
        public void Add(Cotizacion cotizacion) {
            this.cotizacion.Add(cotizacion);
            NotifyDataSetChanged();
        }
        void mMainView_Click(object sender, EventArgs e) {
            int position = mRecyclerView.GetChildAdapterPosition((View)sender);
            var myIntent = new Intent(context, typeof(Resources.layout.solicitudDetalle));
            var MySerializedObject = JsonConvert.SerializeObject(cotizacion[position]);
            myIntent.PutExtra("Detalle", MySerializedObject);
            context.StartActivity(myIntent);
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
            ListViewCotizacionAdapterViewHolder myHolder = holder as ListViewCotizacionAdapterViewHolder;
            myHolder.NombreSolicitud.Text=cotizacion[position].Proveedor.Nombre;
            myHolder.EstusSolicitud.Text=cotizacion[position].Estatus.Descipcion;
            myHolder.PrecioCotizacion.Text=cotizacion[position].PrecioCotizacion;
            myHolder.mMainView.Click+=mMainView_Click;
            //if (!myHolder.BtnBorrar.HasOnClickListeners) {
            //    myHolder.BtnBorrar.Click+=delegate {
            //        SolicitudArticulo.RemoveAt(position);
            //        NotifyDataSetChanged();
            //    };
            //}
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
            View row = LayoutInflater.From(context).Inflate(Resource.Layout.cotizacionRow, parent, false);

            ListViewCotizacionAdapterViewHolder holder = new ListViewCotizacionAdapterViewHolder(row);
            holder.NombreSolicitud=row.FindViewById<TextView>(Resource.Id.nombreSolicitud);
            holder.EstusSolicitud=row.FindViewById<TextView>(Resource.Id.estatusSolicitud);
            holder.PrecioCotizacion=row.FindViewById<TextView>(Resource.Id.precioCotizacion);
            return holder;
        }
    }

    class ListViewCotizacionAdapterViewHolder : RecyclerView.ViewHolder {
        public TextView NombreSolicitud { get; set; }
        public TextView EstusSolicitud { get; set; }
        public TextView PrecioCotizacion { get; set; }
        public View mMainView { get; set; }

        public ListViewCotizacionAdapterViewHolder(View view) : base(view) {
            mMainView=view;
        }
    }
}