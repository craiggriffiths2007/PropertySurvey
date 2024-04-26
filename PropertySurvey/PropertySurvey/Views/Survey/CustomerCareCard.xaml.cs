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
	public partial class CustomerCareCard : ContentPage
	{
		public CustomerCareCard ()
		{
			InitializeComponent ();
		}

        private void go_back(object sender, EventArgs e)
        {
            Navigation.PopAsync(false);
        }
    }
}