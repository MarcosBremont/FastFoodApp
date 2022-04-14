using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FastFoodApp.ControlesPersonalizados
{
    public class CustomEntry : Entry
    {
        public int MaxLength { get; set; } = 2500;

        protected override void OnTextChanged(string oldValue, string newValue)
        {
            if (this.Text.Length < this.MaxLength)
                base.OnTextChanged(oldValue, newValue);

        }
    }
}
