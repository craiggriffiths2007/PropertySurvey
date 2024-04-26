using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace MartControls
{
    public class MenuOptions : Picker
    {
        public MenuOptions()
        {
            this.Title = "MENU";

            this.SelectedIndexChanged += (sender, e) =>
            {
                this.SelectedIndex = -1;
            };
        }
    }
}
