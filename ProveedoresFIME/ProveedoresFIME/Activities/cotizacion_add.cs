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
using Android.Views;
using Android.Widget;
using ProveedoresFIME.Data;
using ProveedoresFIME.Models;

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
                }, TaskScheduler.FromCurrentSynchronizationContext()).ConfigureAwait(false);// Execute API call on background or worker thread.
                                                                                            //           await proveedoresService.GetProveedor(1).ContinueWith(post => {
                                                                                            //              if (post.IsCompleted&&post.Status==TaskStatus.RanToCompletion) {
                                                                                            //                  //post.Result.ForEach((Proveedor item) => {
                                                                                            //                  //    item.Articulos.ToList().ForEach(x => {
                                                                                            //                  //        spinnerAdapterArticulo.Add(x.Descripcion);
                                                                                            //                  //        IDArticulo.Add(x.ArticuloId.ToString());
                                                                                            //                  //    });
                                                                                            //                  //});
                                                                                            //                  //spinnerAdapterArticulo.NotifyDataSetChanged();
                                                                                            //              }
                                                                                            //          }, TaskScheduler.FromCurrentSynchronizationContext())// execute in main/UI thread.
                                                                                            //.ConfigureAwait(false);// Execute API call on background or worker thread.);
            } catch (Exception) {

                throw;
            }
        }
    }
}