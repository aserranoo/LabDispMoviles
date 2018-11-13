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
using ProveedoresFIME.Adapters;
using ProveedoresFIME.Models;

namespace ProveedoresFIME.Resources.layout {
    [Activity(Label = "Solicitud Detalle")]
    public class solicitudDetalle : AppCompatActivity {
        private RecyclerView RecycleView;
        private LinearLayoutManager layoutManager;
        private ListViewDetalleSolicitudAdapter listAdapterCot;
        private List<SolicitudCotizacion> cotizacines = new List<SolicitudCotizacion>();

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.solicitudDetalle);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            var DetalleArticuloStr = Intent.GetStringExtra("Detalle");
            Cotizacion DetalleArticulo = JsonConvert.DeserializeObject<Cotizacion>(DetalleArticuloStr);

            RecycleView=FindViewById<RecyclerView>(Resource.Id.articulosSolicitados);
            RecycleView.HasFixedSize=true;
            layoutManager=new LinearLayoutManager(this);
            RecycleView.SetLayoutManager(layoutManager);
            listAdapterCot=new ListViewDetalleSolicitudAdapter(this, cotizacines, RecycleView);
            RecycleView.SetAdapter(listAdapterCot);
            TextView nombreCotizacion = FindViewById<TextView>(Resource.Id.nombreProveedor);
            nombreCotizacion.Text=DetalleArticulo.Proveedor.Nombre;
            foreach (SolicitudCotizacion solicitud in DetalleArticulo.SolicitudArticulos) {
                cotizacines.Add(solicitud);
            }
        }
        public override bool OnOptionsItemSelected(IMenuItem item) {
            switch (item.ItemId) {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}