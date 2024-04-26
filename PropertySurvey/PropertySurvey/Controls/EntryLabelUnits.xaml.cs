using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MartControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryLabelUnits : StackLayout
    {
        public delegate void OnChanged(object sender, EventArgs e);
        public event OnChanged OnEntryChanged;

        public EntryLabelUnits()
        {
            InitializeComponent();
        }

        public string TextBinding { set { the_entry.SetBinding(Entry.TextProperty, value); } get { return the_entry.Text; } }
        public string LabelText { set { the_label.Text = value; } }
        public double LabelWidth { set { the_label.WidthRequest = value; } }
        public string UnitsText { set { the_units.Text = value; } }
        public Keyboard EntryKeyboard { set { the_entry.Keyboard = value; } }
        public int max_text_length { set { the_entry.MaxLength = value; } }

        public string validation_error_string(string error_text)
        {
            if (this.IsVisible && (the_entry.Text == null || the_entry.Text.Length == 0))
                return error_text;
            else
                return "";
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            OnEntryChanged?.Invoke(this, new EventArgs());
        }
    }
}