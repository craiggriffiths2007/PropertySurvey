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
	public partial class MotorQuest : ContentPage
	{
		public MotorQuest ()
		{
			InitializeComponent ();

            BindingContext = App.net.HeaderRecord as Header;
        }

        protected override bool OnBackButtonPressed()
        {
            //CheckInAndSave();
            App.CurrentApp.HeaderRecord.directive_complete = 1;
            //App.data.SaveVanChecksVan();

            if (App.net.HeaderRecord.i_signed == 1 && App.net.HeaderRecord.i_signed_cust == 1)
                App.CurrentApp.HeaderRecord.directive_complete = 1;

            Navigation.PopAsync(false);

            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetMotorSigned();

        }


            private void OnFitterSign(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MotorSign(), false);
        }

        private void OnCustomerSign(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MotorSign2(), false);
        }

        void SetMotorSigned()
        {
            if (App.net.HeaderRecord.i_signed==1)
                fitter_button.ImageSource = "green_tick.png";
            else
                fitter_button.ImageSource = "question.png";

            if (App.net.HeaderRecord.i_signed_cust==1)
                cust_button.ImageSource = "green_tick.png";
            else
                cust_button.ImageSource = "question.png";

        }
    }
}