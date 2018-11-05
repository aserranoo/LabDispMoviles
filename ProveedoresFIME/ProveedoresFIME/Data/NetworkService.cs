using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Refit;

namespace ProveedoresFIME.Data {
    public static class NetworkService {
        public static IArticulosService ArticulosService { get; set; }
        public static IProveedorService ProveedoresService { get; set; }
        public static string baseUrl = "http://labdispmovil.azurewebsites.net/api/";

        public static IArticulosService GetArticulosService() {
            ArticulosService=RestService.For<IArticulosService>(baseUrl);
            return ArticulosService;
        }
        public static IProveedorService GetProveedoresService() {
            ProveedoresService=RestService.For<IProveedorService>(baseUrl);
            return ProveedoresService;
        }
    }
}