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
	public partial class FitIsComplete : ContentPage
	{
		public FitIsComplete ()
		{
			InitializeComponent ();
		}

        private void OnYes(object sender, EventArgs e)
        {
            App.CurrentApp.HeaderRecord.bfitter_complete = 1;
            Navigation.InsertPageBefore(new FitterCompletion(), this);
            Navigation.PopAsync(false);
        }

        private void OnNo(object sender, EventArgs e)
        {
            App.CurrentApp.HeaderRecord.bfitter_complete = 2;
            Navigation.InsertPageBefore(new FitterCompletion(), this);
            Navigation.PopAsync(false);
        }
    }
}