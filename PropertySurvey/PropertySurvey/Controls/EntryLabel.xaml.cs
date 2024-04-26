using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MartControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EntryLabel : StackLayout
	{
		public EntryLabel ()
		{
			InitializeComponent ();

            the_entry.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);
        }
        public string text { get { return the_entry.Text; } set { the_entry.Text = value; } }

        public string TextBinding { set { the_entry.SetBinding(Entry.TextProperty, value); } }
        public string LabelText   { set { the_label.Text = value; } }
        public Keyboard EntryKeyboard { set { the_entry.Keyboard = value; } }
        public int max_text_length { set { the_entry.MaxLength = value; } }

        public string validation_error_string(string error_text)
        {
            if (this.IsVisible && (the_entry.Text == null || the_entry.Text.Length == 0))
                return error_text;
            else
                return "";
        }
    }
}