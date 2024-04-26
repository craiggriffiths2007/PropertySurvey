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
	public partial class HingeWarning : ContentPage
	{
		public HingeWarning ()
		{
			InitializeComponent ();
		}

        private void OnClick(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new Camera(), this);
            Navigation.PopAsync(false);
        }
    }
}