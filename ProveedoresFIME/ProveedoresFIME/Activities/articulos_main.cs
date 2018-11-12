using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using ProveedoresFIME.Adapters;
using ProveedoresFIME.Data;
using ProveedoresFIME.Models;

namespace ProveedoresFIME.Resources.layout {
    [Activity(Label = "Articulos")]
    public class articulos_main : AppCompatActivity {

        Expandable_ProveedorArticulo listAdapter;
        ExpandableListView expListView;
        List<string> listDataHeader = new List<string>();
        Dictionary<string, List<string>> listDataChild = new Dictionary<string, List<string>>();
        //List<string> listDataHeader;
        //Dictionary<string, List<string>> listDataChild;
        int previousGroup = -1;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.articulos_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fabArticulos);
            fab.Click+=FabOnClick;


            expListView=FindViewById<ExpandableListView>(Resource.Id.lvExp);
            listAdapter=new Expandable_ProveedorArticulo(this, listDataHeader, listDataChild);
            expListView.SetAdapter(listAdapter);
            GetData();
            FnClickEvents();

        }
        void FnClickEvents() {
            //Listening to child item selection
            expListView.ChildClick+=delegate (object sender, ExpandableListView.ChildClickEventArgs e) {
                //Toast.MakeText(this, "child clicked", ToastLength.Short).Show();
            };

            //Listening to group expand
            //modified so that on selection of one group other opened group has been closed
            expListView.GroupExpand+=delegate (object sender, ExpandableListView.GroupExpandEventArgs e) {

                if (e.GroupPosition!=previousGroup)
                    expListView.CollapseGroup(previousGroup);
                previousGroup=e.GroupPosition;
            };

            //Listening to group collapse
            expListView.GroupCollapse+=delegate (object sender, ExpandableListView.GroupCollapseEventArgs e) {
                //Toast.MakeText(this, "group collapsed", ToastLength.Short).Show();
            };

        }

        public async void GetData() {
            var proveedoresService = NetworkService.GetProveedoresService();
            await proveedoresService.GetProveedoresArticulos().ContinueWith(post => {
                if (post.IsCompleted&&post.Status==TaskStatus.RanToCompletion) {
                    post.Result.ForEach((Proveedor item) => {
                        listDataHeader.Add(item.Nombre);
                        var lstCS = new List<string>();
                        foreach (Articulo articulo in item.Articulos) {
                            lstCS.Add(articulo.Descripcion);
                        }
                        listDataChild.Add(item.Nombre, lstCS);
                    });
                    listAdapter.NotifyDataSetChanged();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext())// execute in main/UI thread.
                .ConfigureAwait(false);// Execute API call on background or worker thread
        }

        private void FabOnClick(object sender, EventArgs eventArgs) {
            var myIntent = new Intent(this, typeof(articulos_add));
            StartActivityForResult(myIntent, 0);
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
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
            try {
                ArticuloParcelable parcelable = (ArticuloParcelable)data.GetParcelableExtra("Articulo");
                listDataChild[parcelable.Articulo.DescripcionProveedor].Add(parcelable.Articulo.Descripcion);
                if (parcelable.Articulo!=null) {
                    Articulo newArt = new Articulo {
                        Descripcion=parcelable.Articulo.Descripcion,
                        ProveedorId=parcelable.Articulo.ProveedorId,
                    };
                    var net = NetworkService.GetArticulosService();
                    net.SaveTodoItemAsync(newArt, true);
                }
            } catch (Exception e) {
                e.ToString();
                //throw;
            }
        }
    }
}
