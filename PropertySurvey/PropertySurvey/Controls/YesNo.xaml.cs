using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MartControls
{
    public static class YesNo_button_default_list
    {
        public static List<string> button_list = new List<string>() { "...", "Yes", "No" };
    }

    public partial class YesNo : Xamarin.Forms.StackLayout
    {
        public delegate void OnSelectionHasChanged(object sender, EventArgs e);
        public event OnSelectionHasChanged OnSelectionChanged;
        private List<string> button_list = YesNo_button_default_list.button_list;

        public int ButtonWidth
        {
            get { return (int)TheButton.WidthRequest; }
            set { TheButton.WidthRequest = value; }
        }

        public static readonly BindableProperty ButtonStateProperty = BindableProperty.Create("ButtonState", typeof(int), typeof(YesNo), default(int), BindingMode.TwoWay);

        public int ButtonState
        {
            get { return (int)GetValue(ButtonStateProperty); }
            set { SetValue(ButtonStateProperty, value); }
        }

        public YesNo()
        {
            InitializeComponent();
        }

        public void set_button_list(List<String> items)
        {
            button_list = items;
        }

        public string validation_error_string(string error_text)
        {
            if (this.IsVisible && ButtonState == 0)
                return error_text;
            else
                return "";
        }

        private void SetButton()
        {
            if(ButtonState<button_list.Count)
                TheButton.Text = button_list[ButtonState];
        }

        private void OnButton(object sender, EventArgs e)
        {
            ButtonState++;
            if (ButtonState >= button_list.Count)
                ButtonState = 1;

            SetButton();
            OnSelectionChanged?.Invoke(this, new EventArgs());
        }

        public void set_button_state(int val)
        {
            ButtonState = val;
            SetButton();
        }

        private void layout_changed(object sender, EventArgs e)
        {
            // This is needed when we go back into an existing item.
            // It causes the button to display the value set when we previously created/edited the item.
            SetButton();
        }
    }
}

