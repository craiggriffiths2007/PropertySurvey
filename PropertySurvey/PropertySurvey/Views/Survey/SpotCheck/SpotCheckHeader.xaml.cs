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
	public partial class SpotCheckHeader : ContentPage
	{
		public SpotCheckHeader()
		{
			InitializeComponent ();

            BindingContext = App.net.HeaderRecord as Header;

            uc_excess.Text = "£" + ((Header)BindingContext).uc_excess.ToString();

            uc_inciden.Text = ((Header)BindingContext).uc_inceden.Substring(0, 10);

            damage_button.Text = App.net.HeaderRecord.COD_String.Replace(" ", "\t\n");
        }

        protected override bool OnBackButtonPressed()
        {
            //App.CurrentApp.HeaderRecord.bcompletion_signed = true;
            // App.net.HeaderRecord.bDone = true;
            // App.data.SaveHeader();

            CheckInAndSave();

            //CheckInAndSave();
            //Navigation.PopAsync(false);
            //base.OnBackButtonPressed();
            return true;
        }

        private void CheckInAndSave()
        {
            string result = "";

            result = "Please complete :\n\n";

            if (App.CurrentApp.HeaderRecord.uspot_signed == false)
                result = result + "Completion and Signature\n";

            if (App.CurrentApp.HeaderRecord.no_of_photos<5)
                result = result + "Photos\n";

            if (result.Length > 20)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var response = await Application.Current.MainPage.DisplayAlert("Missing information",
                        "Please complete :\n\n" + result + "\n\nClose Anyway?\n", "   Yes   ", "   No   ");
                    if (response)
                    {
                        App.CurrentApp.HeaderRecord.bDone = false;
                        App.data.SaveHeader();
                        await this.Navigation.PopAsync(false);
                    }
                    else
                    {
                        App.CurrentApp.HeaderRecord.bDone = false;
                    }
                });
            }
            else
            {
                App.CurrentApp.HeaderRecord.bDone = true;
                App.data.SaveHeader();
                this.Navigation.PopAsync(false);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            photos.Text = "Photos - " + App.CurrentApp.HeaderRecord.no_of_photos.ToString();

            if (App.CurrentApp.HeaderRecord.no_of_photos >= 5)
            {
                photos.TextColor = Color.DarkGreen;
            }
            if(App.net.HeaderRecord.bInfoSeen == true)
            {
                instructions.TextColor = Color.DarkGreen;
            }
            if(App.CurrentApp.HeaderRecord.uspot_signed==true)
            {
                report.TextColor = Color.DarkGreen;
            }
        }

        private void OnDamage(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new Damage(), this);
            Navigation.PopAsync(false);
        }

        private void OnInstructions(object sender, EventArgs e)
        {
            App.net.HeaderRecord.bInfoSeen = true;
            Navigation.PushAsync(new SurveyInstructions(), false);
        }

        private void OnPhotos(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Camera(), false);
        }

        private void OnReport(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SpotCheckReport(), false);
        }
    }
}