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
            LblTotal.Text = App.Precio.ToString();
        }

        void BtnagregarCantidad_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void BtnCancelar_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void TxtCantidad_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {

            if (string.IsNullOrEmpty(TxtCantidad.Text))
            {
                LblTotal.Text = App.Precio.ToString();
            }
            else
            {
                int totalcantidad = Convert.ToInt32(TxtCantidad.Text);
                LblTotal.Text = (App.Precio * totalcantidad).ToString();
            }
        }
    }
}
