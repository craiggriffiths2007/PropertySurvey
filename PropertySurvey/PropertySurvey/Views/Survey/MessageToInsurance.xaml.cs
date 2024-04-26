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
	public partial class MessageToInsurance : ContentPage
	{
		public MessageToInsurance ()
		{
			InitializeComponent ();

            if (App.net.HeaderRecord.is_messagetoinsurer == 0)
                App.net.HeaderRecord.is_messagetoinsurer = 2;

            BindingContext = App.net.HeaderRecord as Header;

            SetMessageArea();
        }

        private void SetMessageArea()
        {
            if (App.net.HeaderRecord.is_messagetoinsurer == 1)
            {
                message_area.IsVisible = true;
                messagetoinsurer.Focus();
            }
            else
            {
                message_area.IsVisible = false;
            }
        }

        private void on_done(object sender, EventArgs e)
        {
            App.data.SaveHeader();
            Navigation.PopAsync(false);
        }

        private void on_changed(object sender, EventArgs e)
        {
            SetMessageArea();
        }

        protected override bool OnBackButtonPressed()
        {
            App.data.SaveHeader();
            Navigation.PopAsync(false);

            base.OnBackButtonPressed();
            return true;
        }

    }
}