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

namespace ProveedoresFIME.Resources.layout {
    [Activity(Label = "proveedores_main")]
    public class proveedores_main : AppCompatActivity {
        private ListView listProveedores;
        private List<Proveedor> itemProveedor;
        private ListViewCustomAdapter ProveedoresAdapter;
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.proveedores_main);

            listProveedores=FindViewById<ListView>(Resource.Id.listView1);
            itemProveedor=new List<Proveedor>();
            ProveedoresAdapter = new ListViewCustomAdapter(this, itemProveedor);

            listProveedores.Adapter=ProveedoresAdapter;
            //var emptyText = FindViewById(Resource.Id.service_list_empty);
            //MyServiceListView.EmptyView=emptyText;
            //listProveedores.ItemClick+=Listproveedores_ItemClick;
            //Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            //SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab2);
            fab.Click+=FabOnClick;

            //DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            //ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            //drawer.AddDrawerListener(toggle);
            //toggle.SyncState();
        }
        //private void Listproveedores_ItemClick(object sender, AdapterView.ItemClickEventArgs e) {
        //    throw new NotImplementedException();
        //}
        private void FabOnClick(object sender, EventArgs eventArgs) {
            var myIntent = new Intent(this, typeof(Resources.layout.proveedores_add));
            StartActivityForResult(myIntent, 0);

            //List<proveedores_add> receivedItems = JsonConvert.DeserializeObject<List<proveedores_add>>(Intent.GetStringExtra("greeting"));

            //DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            //drawer.CloseDrawer(GravityCompat.Start);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
            ProveedorParcelable parcelable = (ProveedorParcelable)data.GetParcelableExtra("Proveedor");
            itemProveedor.Add(new Proveedor {
                Nombre=parcelable.Proveedor.Nombre,
                Contacto=parcelable.Proveedor.Contacto,
                Email=parcelable.Proveedor.Email
            });
        }
    }
}