using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MartControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewTextLabel : StackLayout
    {
		public ViewTextLabel ()
		{
			InitializeComponent ();
		}

        public string LabelText { set { the_label.Text = value; } }
        public string TextBinding { set { the_text.SetBinding(Label.TextProperty, value); } }
    }
}