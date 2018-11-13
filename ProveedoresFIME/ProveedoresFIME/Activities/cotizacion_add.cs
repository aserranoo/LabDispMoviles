using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ProveedoresFIME.Adapters;
using ProveedoresFIME.Data;
using ProveedoresFIME.Models;
using Newtonsoft.Json;

namespace ProveedoresFIME.Resources.layout {
    [Activity(Label = "Solicitar Cotización")]
    public class cotizacion_add : AppCompatActivity {
        List<string> NameProveedor = new List<string>();
        List<string> IDProveedor = new List<string>();
        List<string> NameArticulo = new List<string>();
        List<string> IDArticulo = new List<string>();
        ArrayAdapter<string> spinnerAdapterProveedor;
        ArrayAdapter<string> spinnerAdapterArticulo;
        Spinner spinnerProveedor;
        Spinner spinnerArticulo;
        ListViewSolicitudCotizacion listAdapterCot;
        List<SolicitudCotizacion> articulos = new List<SolicitudCotizacion>();
        RecyclerView RecycleView;
        RecyclerView.LayoutManager layoutManager;
        protected override async void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.cotizaciones_add);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            spinnerProveedor=FindViewById<Spinner>(Resource.Id.spinnerProveedor);
            spinnerArticulo=FindViewById<Spinner>(Resource.Id.spinnerArticulo);
            spinnerAdapterProveedor=new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, NameProveedor);
            spinnerAdapterArticulo=new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, NameArticulo);


            spinnerAdapterProveedor.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerProveedor.Adapter=spinnerAdapterProveedor;
            spinnerProveedor.ItemSelected+=spinnerProveedor_ItemSelectedAsync;

            spinnerAdapterArticulo.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerArticulo.Adapter=spinnerAdapterArticulo;
            spinnerArticulo.ItemSelected+=spinnerArticulo_ItemSelectedAsync;


            var proveedoresService = NetworkService.GetProveedoresService();
            await proveedoresService.RefreshDataAsync().ContinueWith(post => {
                if (post.IsCompleted&&post.Status==TaskStatus.RanToCompletion) {
                    post.Result.ForEach((Proveedor item) => {
                        spinnerAdapterProveedor.Add(item.Nombre);
                        IDProveedor.Add(item.ProveedorId.ToString());
                    });
                    spinnerAdapterProveedor.NotifyDataSetChanged();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext())// execute in main/UI thread.
      .ConfigureAwait(false);// Execute API call on background or worker thread.);

            RecycleView=FindViewById<RecyclerView>(Resource.Id.preCotizacion);
            RecycleView.HasFixedSize=true;
            layoutManager=new LinearLayoutManager(this);
            RecycleView.SetLayoutManager(layoutManager);
            listAdapterCot=new ListViewSolicitudCotizacion(this, articulos, RecycleView, "");
            RecycleView.SetAdapter(listAdapterCot);
            Button btnAgregarArt = FindViewById<Button>(Resource.Id.btnAgregarArticulo);
            btnAgregarArt.Click+=buttonclick;
        }
        public override bool OnCreateOptionsMenu(IMenu menu) {
            MenuInflater.Inflate(Resource.Menu.menu_solicitud, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item) {
            switch (item.ItemId) {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;
                case Resource.Id.saveArticulo:
                    SaveCotizacion();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private async void SaveCotizacion() {
            Cotizacion cotizacion = new Cotizacion {
                ProveedorId=int.Parse(IDProveedor[(int)spinnerProveedor.SelectedItemId]),
                Fecha=DateTime.Now,
                EstatusId = 1
            };
            try {
                cotizacion = await NetworkService.GetCotizacionService().SaveTodoItemAsync(cotizacion, true);
            } catch (Exception e) {

                throw;
            }
            foreach (SolicitudCotizacion solicitud in articulos) {
                solicitud.CotizacionId=cotizacion.CotizacionId;
                await NetworkService.GetSolicitudCotizacionService().SaveSolicitud(solicitud);
            }
            Cotizacion newCot = await NetworkService.GetCotizacionService().GetCotizacion(cotizacion.CotizacionId);
            Intent myIntent = new Intent(this, typeof(MainActivity));
            var MySerializedObject = JsonConvert.SerializeObject(newCot);
            myIntent.PutExtra("Detalle", MySerializedObject);
            SetResult(Result.Ok, myIntent);
            Finish();
        }

        private void buttonclick(object sender, EventArgs e) {
            EditText cantidad = FindViewById<EditText>(Resource.Id.cantidadSolicitada);
            articulos.Add(new SolicitudCotizacion {
                ProveedorId=int.Parse(IDProveedor[(int)spinnerProveedor.SelectedItemId]),
                ArticuloId=int.Parse(IDArticulo[(int)spinnerArticulo.SelectedItemId]),
                Cantidad=int.Parse(cantidad.Text),
                Descripcion=spinnerArticulo.SelectedItem.ToString()
            });
            listAdapterCot.NotifyDataSetChanged();
        }

        private async void spinnerArticulo_ItemSelectedAsync(object sender, AdapterView.ItemSelectedEventArgs e) {
            //throw new NotImplementedException();
        }


        private async void spinnerProveedor_ItemSelectedAsync(object sender, AdapterView.ItemSelectedEventArgs e) {
            if (spinnerAdapterArticulo.Count>0) {
                spinnerAdapterArticulo.Clear();
                NameArticulo.Clear();
                IDArticulo.Clear();
                spinnerAdapterArticulo.NotifyDataSetChanged();
            }
            try {
                await NetworkService.GetProveedoresService().GetProveedor(int.Parse(IDProveedor[e.Position].ToString())).ContinueWith(post => {
                    if (post.IsCompleted&&post.Status==TaskStatus.RanToCompletion) {
                        post.Result.Articulos.ToList().ForEach(x => {
                            spinnerAdapterArticulo.Add(x.Descripcion);
                            IDArticulo.Add(x.ArticuloId.ToString());
                        });
                        spinnerAdapterArticulo.NotifyDataSetChanged();
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext()).ConfigureAwait(false);
            } catch (Exception) {

                throw;
            }
        }
    }
}