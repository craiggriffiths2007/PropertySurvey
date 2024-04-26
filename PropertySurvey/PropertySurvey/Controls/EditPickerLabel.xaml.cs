using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MartControls
{
    public partial class EditPickerLabel : StackLayout
    {
        public delegate void OnTextChanged(object sender, EventArgs e);
        public event OnTextChanged OnTextChangedEvent;

        public EditPickerLabel()
        {
            InitializeComponent();
        }

        public string LabelText
        {
            get { return TheLabel.Text; }
            set { TheLabel.Text = value; }
        }

        public string Text
        {
            get { return ThePicker.Text; }
            set { ThePicker.Text = value; }
        }

        public string TextBinding { set { ThePicker.TextBinding = value; } }
        public string picker_title { set { ThePicker.picker_title = value; } }
        public int max_text_length { set { ThePicker.max_text_length = value; } }

        public void SetPickerItems(List<String> items)
        {
            ThePicker.SetPickerItems (items);
        }

        public void set_picker_enabled(bool enable)
        {
            ThePicker.set_picker_enabled(enable);
        }

        public string validation_error_string(string error_text)
        {
            if (this.IsVisible && (ThePicker.Text == null || ThePicker.Text.Length == 0))
                return error_text;
            else
                return "";
        }

        private void picker_changed (object sender, EventArgs e)
        {
            OnTextChangedEvent?.Invoke(this, new EventArgs());
        }
    }
}