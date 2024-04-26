using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SecuritySurveyMessage : ContentPage
	{
		public SecuritySurveyMessage ()
		{
            InitializeComponent ();
            if (App.net.HeaderRecord.bSSTicked == true)
                next_button.IsEnabled = true;
            else
                next_button.IsEnabled = false;

            App.net.HeaderRecord.bSSTicked = false;

            BindingContext = App.net.HeaderRecord as Header;
        }

        private void OnNext(object sender, EventArgs e)
        {
            if(App.net.HeaderRecord.bSSTicked==true)
            {
                Navigation.PopAsync(false);
            }
        }

        private void TickButtonLabel_Clicked(object sender, EventArgs e)
        {
            if (App.net.HeaderRecord.bSSTicked == true)
                next_button.IsEnabled = true;
            else
                next_button.IsEnabled = false;
        }
    }
}