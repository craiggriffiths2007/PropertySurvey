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
	public partial class Telephone : ContentPage
	{
		public Telephone ()
		{
			InitializeComponent ();

            BindingContext = App.net.HeaderRecord as Header;
        }

        private void OnCallHome(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:" + App.net.HeaderRecord.uc_h_phone));
        }

        private void OnCallWork(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:" + App.net.HeaderRecord.uc_h_phone2));
        }

        private void OnCallMobile(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:" + App.net.HeaderRecord.uc_h_phone3));
        }

        private void OnCallAdd1(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:" + App.net.HeaderRecord.add_phone_1));
        }

        private void OnCallAdd2(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:" + App.net.HeaderRecord.add_phone_2));
        }
    }
}