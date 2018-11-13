using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using ProveedoresFIME.Adapters;
using ProveedoresFIME.Data;
using ProveedoresFIME.Models;
using ProveedoresFIME.Resources.layout;

namespace ProveedoresFIME {
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener {
        private RecyclerView RecycleView;
        private LinearLayoutManager layoutManager;
        private ListViewCotizacionAdapter listAdapterCot;
        private List<Cotizacion> cotizacines = new List<Cotizacion>();

        protected override async void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click+=FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            RecycleView=FindViewById<RecyclerView>(Resource.Id.cotizaciones);
            RecycleView.HasFixedSize=true;
            layoutManager=new LinearLayoutManager(this);
            RecycleView.SetLayoutManager(layoutManager);
            listAdapterCot=new ListViewCotizacionAdapter(this, cotizacines, RecycleView);
            RecycleView.SetAdapter(listAdapterCot);

            await NetworkService.GetCotizacionService().RefreshDataAsync().ContinueWith(post => {
                if (post.IsCompleted&&post.Status==TaskStatus.RanToCompletion) {
                    post.Result.ForEach((Cotizacion item) => {
                        cotizacines.Add(item);
                    });
                    listAdapterCot.NotifyDataSetChanged();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext())// execute in main/UI thread.
      .ConfigureAwait(true);// Execute API call on background or worker thread
        }

        public override void OnBackPressed() {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start)) {
                drawer.CloseDrawer(GravityCompat.Start);
            } else {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu) {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item) {
            int id = item.ItemId;
            if (id==Resource.Id.action_settings) {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs) {
            var myIntent = new Intent(this, typeof(cotizacion_add));
            StartActivityForResult(myIntent, 0);
        }

        public bool OnNavigationItemSelected(IMenuItem item) {
            int id = item.ItemId;

            if (id==Resource.Id.nav_person_add) {
                StartActivity(typeof(proveedores_main));
            } else if (id==Resource.Id.nav_articulos) {
                StartActivity(typeof(articulos_main));
            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
            Cotizacion DetalleArticulo = new Cotizacion();
            var DetalleArticuloStr = data.GetStringExtra("Detalle");
            DetalleArticulo = JsonConvert.DeserializeObject<Cotizacion>(DetalleArticuloStr);
            cotizacines.Add(DetalleArticulo);
            listAdapterCot.NotifyDataSetChanged();
        }
    }
}

