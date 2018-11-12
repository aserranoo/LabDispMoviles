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
    [Activity(Label = "Agregar Articulo")]
    public class articulos_add : AppCompatActivity {
        List<string> Name = new List<string>();
        List<string> ID = new List<string>();
        protected override async void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.articulos_add);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
            ArrayAdapter<string> spinnerAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, Name);
            spinnerAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter=spinnerAdapter;
            spinner.ItemSelected+=spinner_ItemSelected;
            var proveedoresService = NetworkService.GetProveedoresService();
            await proveedoresService.RefreshDataAsync().ContinueWith(post => {
                if (post.IsCompleted&&post.Status==TaskStatus.RanToCompletion) {
                    post.Result.ForEach((Proveedor item) => {
                        spinnerAdapter.Add(item.Nombre);
                        //Name.Add(item.Nombre);
                        ID.Add(item.ProveedorId.ToString());
                    });
                    spinnerAdapter.NotifyDataSetChanged();
                }
            }, TaskScheduler.FromCurrentSynchronizationContext())// execute in main/UI thread.
      .ConfigureAwait(false);// Execute API call on background or worker thread.);
            EditText Descripcion = FindViewById<EditText>(Resource.Id.descripcionArticulo);
            Button button = FindViewById<Button>(Resource.Id.btnGuardarArticulo);
            button.Click+=(o, e) => {
                ArticuloSave NewArticulo = new ArticuloSave {
                    ProveedorId= int.Parse(ID[(int)spinner.SelectedItemId].ToString()),
                    Descripcion=Descripcion.Text,
                    DescripcionProveedor = spinner.SelectedItem.ToString(),
                };
                Intent myIntent = new Intent(this, typeof(articulos_main));
                ArticuloParcelable parcelable = new ArticuloParcelable {
                    Articulo=NewArticulo
                };
                myIntent.PutExtra("Articulo", parcelable);
                SetResult(Result.Ok, myIntent);
                Finish();
            };
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e) {
            //Spinner spinner = (Spinner)sender;
            //string s1 = ID[e.Position].ToString();
            //Toast.MakeText(this, s1, ToastLength.Short).Show();
            //string toast = string.Format("The planet is {0}", spinner.GetItemAtPosition(e.Position));
            //Toast.MakeText(this, toast, ToastLength.Long).Show();
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