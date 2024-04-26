using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MartControls
{
    public partial class YesNoLabel : Xamarin.Forms.StackLayout
    {
        public delegate void OnSelectionHasChanged(object sender, EventArgs e);
        public event OnSelectionHasChanged OnSelectionChanged;

        public string LabelText
        {
            get { return TheLabel.Text; }
            set { TheLabel.Text = value; }
        }

        public int ButtonWidth
        {
            get { return (int)TheButton.WidthRequest; }
            set { TheButton.WidthRequest = value; }
        }

        public string button_binding
        {
            set { TheButton.SetBinding(YesNo.ButtonStateProperty, value); }
        }

        public YesNoLabel()
        {
            InitializeComponent();
        }

        public void set_button_list(List<String> items)
        {
            TheButton.set_button_list(items);
        }

        public int ButtonState
        {
            get { return TheButton.ButtonState; }
            set { TheButton.ButtonState = value; }
        }

        public string validation_error_string(string error_text)
        {
            // Cannot use the one in TheButton as that has a different IsVisible
            if (this.IsVisible && TheButton.ButtonState == 0)
                return error_text;
            else
                return "";
        }

        private void OnButton(object sender, EventArgs e)
        {
            OnSelectionChanged?.Invoke(this, new EventArgs());
        }

        public void set_button_state(int val)
        {
            TheButton.set_button_state(val);
        }
    }
}