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
    public class ListViewDetalleSolicitudAdapter : RecyclerView.Adapter {

        Context context;
        private List<SolicitudCotizacion> cotizacion;
        private RecyclerView mRecyclerView;

        public ListViewDetalleSolicitudAdapter(Context context, List<SolicitudCotizacion> cotizacion, RecyclerView mRecyclerView) {
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
        public void Add(SolicitudCotizacion cotizacion) {
            this.cotizacion.Add(cotizacion);
            NotifyDataSetChanged();
        }
        void mMainView_Click(object sender, EventArgs e) {
          
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
            ListViewDetalleSolicitudAdapterViewHolder myHolder = holder as ListViewDetalleSolicitudAdapterViewHolder;
            myHolder.NombreArticulo.Text=cotizacion[position].Articulo.Descripcion;
            myHolder.CantidadSolicitada.Text=cotizacion[position].Cantidad.ToString();
            myHolder.mMainView.Click+=mMainView_Click;
            //if (!myHolder.BtnBorrar.HasOnClickListeners) {
            //    myHolder.BtnBorrar.Click+=delegate {
            //        SolicitudArticulo.RemoveAt(position);
            //        NotifyDataSetChanged();
            //    };
            //}
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
            View row = LayoutInflater.From(context).Inflate(Resource.Layout.detalleRow, parent, false);

            ListViewDetalleSolicitudAdapterViewHolder holder = new ListViewDetalleSolicitudAdapterViewHolder(row);
            holder.NombreArticulo=row.FindViewById<TextView>(Resource.Id.nombreArticuloSolicitado);
            holder.CantidadSolicitada=row.FindViewById<TextView>(Resource.Id.cantidadSolicitada);
            return holder;
        }
    }

    class ListViewDetalleSolicitudAdapterViewHolder : RecyclerView.ViewHolder {
        public TextView NombreArticulo { get; set; }
        public TextView CantidadSolicitada { get; set; }
        public View mMainView { get; set; }

        public ListViewDetalleSolicitudAdapterViewHolder(View view) : base(view) {
            mMainView=view;
        }
    }
}