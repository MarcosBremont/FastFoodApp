using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FastFoodApp.Modelo
{
    public class ToastConfigClass
    {
        public void MostrarNotificacion(string mensaje, ToastPosition position = ToastPosition.Top, int duracionSegundos = 6, string color = "#51C560")
        {
            ToastConfig toastConfig = new ToastConfig(mensaje)
            { BackgroundColor = Color.FromHex(color), MessageTextColor = Color.White };
            toastConfig.SetPosition(position);
            toastConfig.Duration = new TimeSpan(0, 0, duracionSegundos);
            UserDialogs.Instance.Toast(toastConfig);
        }
    }
}
