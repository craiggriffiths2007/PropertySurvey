using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MartControls
{
    public partial class YesNoLabelDouble : Xamarin.Forms.StackLayout
    {
        public delegate void OnSelectionHasChanged(object sender, EventArgs e);
        public event OnSelectionHasChanged OnSelectionChanged;
        public event OnSelectionHasChanged OnSelectionChanged2;
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

        public string button_binding2
        {
            set { TheButton2.SetBinding(YesNo.ButtonStateProperty, value); }
        }

        public YesNoLabelDouble()
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

        public int ButtonState2
        {
            get { return TheButton2.ButtonState; }
            set { TheButton2.ButtonState = value; }
        }

        public string validation_error_string(string error_text)
        {
            // Cannot use the one in TheButton as that has a different IsVisible
            if (this.IsVisible && (TheButton.ButtonState == 0 || TheButton2.ButtonState == 0))
                return error_text;
            else
                return "";
        }

        private void OnButton(object sender, EventArgs e)
        {
            OnSelectionChanged?.Invoke(this, new EventArgs());

            if(TheButton.ButtonState==2)
            {
                TheButton2.set_button_state(2);
            }
        }

        public void set_button_state(int val)
        {
            TheButton.set_button_state(val);
        }

        private void OnButton2(object sender, EventArgs e)
        {
            OnSelectionChanged2?.Invoke(this, new EventArgs());

            if (TheButton2.ButtonState == 1)
            {
                if (TheButton.ButtonState == 0)
                {
                    TheButton.set_button_state(1);
                }
            }

            if (TheButton.ButtonState == 2)
            {
                TheButton2.set_button_state(2);
            }
        }

        public void set_button2_state(int val)
        {
            TheButton2.set_button_state(val);
        }
    }
}