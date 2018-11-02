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

namespace ProveedoresFIME.Resources.layout {
    [Activity(Label = "proveedores_add")]
    public class proveedores_add : AppCompatActivity {
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.proveedores_add);
            // Create your application here
            Button button = FindViewById<Button>(Resource.Id.button1);

            //ListView ListaProveedores = FindViewById<ListView>(Resource.Id.listView1);

            EditText Nombre = FindViewById<EditText>(Resource.Id.edittext1);
            EditText Contacto = FindViewById<EditText>(Resource.Id.edittext2);
            EditText Correo = FindViewById<EditText>(Resource.Id.edittext3);

            button.Click+=(o, e) => {
                Proveedor NewProveedor = new Proveedor {
                    Nombre=Nombre.Text,
                    Contacto=Contacto.Text,
                    Email=Correo.Text
                };
                Toast.MakeText(this, "Method was called", ToastLength.Short).Show();
                Intent myIntent = new Intent(this, typeof(proveedores_main));
                ProveedorParcelable parcelable = new ProveedorParcelable {
                    Proveedor=NewProveedor
                };
                myIntent.PutExtra("Proveedor", parcelable);
                SetResult(Result.Ok, myIntent);
                Finish();
            };
        }
    }
}