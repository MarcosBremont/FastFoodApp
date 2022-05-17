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
            TxtCantidad.Text = "1";
        }

        void BtnagregarCantidad_Clicked(System.Object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtCantidad.Text) || string.IsNullOrEmpty(TxtDinero.Text))
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert("Por favor rellene todos los campos");
            }
        }

        async void BtnCancelar_Clicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.PopAsync();
            TxtCantidad.Text = "";
            TxtDinero.Text = "";
        }

        void TxtCantidad_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            TxtCantidad.Text.Replace(".", " ");
            TxtCantidad.Text.Replace("#", "");
            TxtCantidad.Text.Replace("$", "");
            TxtCantidad.Text.Replace(",", "");
            TxtCantidad.Text.Replace(" ", "");
            if (!string.IsNullOrEmpty(TxtCantidad.Text.Replace(".","")))
            {
                if (TxtCantidad.Text == "0")
                {
                    LblTotal.Text = "0";
                }
                else
                {

                    int totalcantidad = Convert.ToInt32(TxtCantidad.Text);
                    LblTotal.Text = (App.Precio * totalcantidad + App.envio).ToString();

                    if (!string.IsNullOrEmpty(TxtDinero.Text))
                    {
                        int Dinerotxt = Convert.ToInt32(TxtDinero.Text);

                        int TotalTxt = Convert.ToInt32(LblTotal.Text.Replace("RD$ ", ""));



                        if (string.IsNullOrEmpty(TxtCantidad.Text))
                        {
                            LblTotal.Text = App.Precio.ToString();
                            lbldevuelta.Text = "0";
                        }
                        else
                        {

                            int total = Convert.ToInt32(LblTotal.Text.Replace("RD$ ", ""));
                            int dinero = Convert.ToInt32(TxtDinero.Text);
                            int devuelta = dinero - total;
                            if (TxtCantidad.Text != "0")
                            {
                                lbldevuelta.Text = devuelta.ToString();
                            }
                            else
                            {
                                lbldevuelta.Text = "0";
                            }
                        }
                        if (Dinerotxt < TotalTxt)
                        {
                            TxtDinero_TextChanged(sender, e);

                        }
                    }

                }

            }
            else
            {
                LblTotal.Text = "0";
            }



        }

        private void TxtDinero_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtDinero.Text))
            {
                int Dinerotxt = Convert.ToInt32(TxtDinero.Text);
                int TotalTxt = Convert.ToInt32(LblTotal.Text.Replace("RD$ ", ""));

                if (Dinerotxt < TotalTxt)
                {
                    //int total = Convert.ToInt32(LblTotal.Text.Replace("RD$ ", ""));
                    //int dinero = Convert.ToInt32(TxtDinero.Text);
                    //int devuelta = dinero - total;
                    lbldevuelta.Text = "0";
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
}
