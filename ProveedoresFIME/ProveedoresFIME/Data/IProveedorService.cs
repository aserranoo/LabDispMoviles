﻿using System;
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
using ProveedoresFIME.models;
using Refit;

namespace ProveedoresFIME.Data {
    public interface IProveedorService {
        [Get("/Proveedores/")]
        Task<List<Proveedor>> RefreshDataAsync();

        [Post("/Proveedores/")]
        Task SaveTodoItemAsync(Proveedor item, bool isNewItem);

        Task DeleteTodoItemAsync(string id);
    }
}