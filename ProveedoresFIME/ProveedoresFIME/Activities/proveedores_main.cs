using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ProveedoresFIME.models;
using Newtonsoft.Json;
using ProveedoresFIME.Adapters;
using System.Threading.Tasks;
using ProveedoresFIME.Data;
using ProveedoresFIME.Models;

namespace ProveedoresFIME.Resources.layout {
    [Activity(Label = "Proveedores")]
    public class proveedores_main : AppCompatActivity {
        private ListView listProveedores;
        private List<Proveedor> itemProveedor;
        private ListViewCustomAdapter ProveedoresAdapter;
        protected override async void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.proveedores_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab2);
            fab.Click+=FabOnClick;

            listProveedores=FindViewById<ListView>(Resource.Id.listView1);
            itemProveedor=new List<Proveedor>();
            ProveedoresAdapter=new ListViewCustomAdapter(this, itemProveedor);
            listProveedores.Adapter=ProveedoresAdapter;
            var proveedoresService = NetworkService.GetProveedoresService();
            await proveedoresService.RefreshDataAsync().ContinueWith(post => {
                if (post.IsCompleted&&post.Status==TaskStatus.RanToCompletion) {
                    post.Result.ForEach((Proveedor item) => {
                        itemProveedor.Add(new Proveedor {
                            Nombre=item.Nombre,
                            Direccion=item.Direccion,
                            Correo=item.Correo,
                            Rfc=item.Rfc,
                            Telefono=item.Telefono,
                            ProveedorId=0
                        });
                    });
                    ProveedoresAdapter.NotifyDataSetChanged();
                } else if (post.IsFaulted) {

                } else if (post.IsCanceled) {

                }
            }, TaskScheduler.FromCurrentSynchronizationContext())// execute in main/UI thread.
      .ConfigureAwait(false);// Execute API call on background or worker thread.);
        }

        private void FabOnClick(object sender, EventArgs eventArgs) {
            var myIntent = new Intent(this, typeof(Resources.layout.proveedores_add));
            StartActivityForResult(myIntent, 0);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
            try {
                ProveedorParcelable parcelable = (ProveedorParcelable)data.GetParcelableExtra("Proveedor");
                itemProveedor.Add(new Proveedor {
                    Nombre=parcelable.Proveedor.Nombre,
                    Telefono=parcelable.Proveedor.Telefono,
                    Correo=parcelable.Proveedor.Correo,
                    Direccion="",
                    Rfc="",
                    ProveedorId=0
                });
                if (parcelable.Proveedor!=null) {
                    NetworkService.ProveedoresService.SaveTodoItemAsync(parcelable.Proveedor, true);
                }
            } catch (Exception) {

                //throw;
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