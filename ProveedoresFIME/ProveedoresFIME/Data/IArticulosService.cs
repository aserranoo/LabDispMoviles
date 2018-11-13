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
    public interface IArticulosService {
        [Get("/Articulos/")]
        Task<List<Articulo>> RefreshDataAsync();

        [Post("/Articulos/")]
        Task SaveTodoItemAsync(Articulo item, bool isNewItem);
        [Delete("/Articulos/")]
        Task DeleteTodoItemAsync(string id);
    }
}