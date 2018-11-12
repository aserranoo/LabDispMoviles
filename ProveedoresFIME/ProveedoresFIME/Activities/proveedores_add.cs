using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.IO;
using Newtonsoft.Json;
using ProveedoresFIME.models;
using ProveedoresFIME.Models;

namespace ProveedoresFIME.Resources.layout {
    [Activity(Label = "Agregar Proveedor")]
    public class proveedores_add : AppCompatActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.proveedores_add);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            //SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            // Create your application here
            Button button = FindViewById<Button>(Resource.Id.button1);

            //ListView ListaProveedores = FindViewById<ListView>(Resource.Id.listView1);

            EditText Nombre = FindViewById<EditText>(Resource.Id.edittext1);
            EditText Telefono = FindViewById<EditText>(Resource.Id.edittext2);
            EditText Correo = FindViewById<EditText>(Resource.Id.edittext3);

            button.Click+=(o, e) => {
                Proveedor NewProveedor = new Proveedor {
                    Nombre=Nombre.Text,
                    Telefono=Telefono.Text,
                    Correo=Correo.Text
                };
                Intent myIntent = new Intent(this, typeof(proveedores_main));
                ProveedorParcelable parcelable = new ProveedorParcelable {
                    Proveedor=NewProveedor
                };
                myIntent.PutExtra("Proveedor", parcelable);
                SetResult(Result.Ok, myIntent);
                Finish();
            };
            //ActionBar.SetHomeButtonEnabled(true);
            //ActionBar.SetDisplayHomeAsUpEnabled(true);
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