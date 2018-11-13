using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProveedoresFIME.Models;
using Refit;

namespace ProveedoresFIME.Data {
    public interface ICotizacionService {
        [Get("/Cotizaciones/")]
        Task<List<Cotizacion>> RefreshDataAsync();
        [Get("/Cotizaciones/{id}")]
        Task<Cotizacion> GetCotizacion(int id);
        [Post("/Cotizaciones/")]
        Task<Cotizacion> SaveTodoItemAsync(Cotizacion item, bool isNewItem);
    }
}