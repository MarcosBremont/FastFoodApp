using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FastFoodApp.Pantallas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalCantidad : Rg.Plugins.Popup.Pages.PopupPage
    {
        public event EventHandler OnLLamarOtraPantalla;

        public ModalCantidad()
        {
            InitializeComponent();
            LblTotal.Text = "RD$ " + (App.Precio + App.envio).ToString();
            lblprecioenvio.Text = "RD$" + App.envio.ToString() + " Pesos";
            lblprecioenvio.Text.Trim();
        }

        void BtnagregarCantidad_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        async void BtnCancelar_Clicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.PopAsync();
            TxtCantidad.Text = "";
            TxtDinero.Text = "";
        }

        void TxtCantidad_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            int Dinerotxt = Convert.ToInt32(TxtDinero.Text);

            int TotalTxt = Convert.ToInt32(LblTotal.Text.Replace("RD$ ", ""));

            if (Dinerotxt < TotalTxt)
            {
                TxtDinero_TextChanged(sender, e);

            }

            if (string.IsNullOrEmpty(TxtCantidad.Text))
            {
                LblTotal.Text = App.Precio.ToString();
            }
            else
            {
                int totalcantidad = Convert.ToInt32(TxtCantidad.Text);
                LblTotal.Text = (App.Precio * totalcantidad + App.envio).ToString();
            }
        }

        private void TxtDinero_TextChanged(object sender, TextChangedEventArgs e)
        {

            int Dinerotxt = Convert.ToInt32(TxtDinero.Text);
            int TotalTxt = Convert.ToInt32(LblTotal.Text.Replace("RD$ ", ""));

            if (Dinerotxt < TotalTxt)
            {
                int total = Convert.ToInt32(LblTotal.Text.Replace("RD$ ", ""));
                int dinero = Convert.ToInt32(TxtDinero.Text);
                int devuelta = dinero - total;
                lbldevuelta.Text = devuelta.ToString();
            }
            else
            {
                if (string.IsNullOrEmpty(TxtDinero.Text))
                {
                    lbldevuelta.Text = "0";
                }
                else
                {
                    int total = Convert.ToInt32(LblTotal.Text.Replace("RD$ ", ""));
                    int dinero = Convert.ToInt32(TxtDinero.Text);
                    int devuelta = dinero - total;
                    lbldevuelta.Text = devuelta.ToString();
                }
            }

        }
    }
}
