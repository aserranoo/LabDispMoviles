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
using Refit;

namespace ProveedoresFIME.Data {
    public interface INotificationService {
        [Get("/Notification/SendNotification/{id}/{to}")]
        Task EnviarCorreo(int id, string to);
    }
}