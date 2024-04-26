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
	public partial class RemedialHeader : ContentPage
	{
		public RemedialHeader ()
		{
			InitializeComponent ();

            BindingContext = App.net.HeaderRecord as Header;

            if (App.net.HeaderRecord.fname1.Contains("DLG01") ||
                App.net.HeaderRecord.fname1.Contains("DLG02") ||
                App.net.HeaderRecord.fname1.Contains("DLG03") ||
                App.net.HeaderRecord.fname1.Contains("DLG04") ||
                App.net.HeaderRecord.fname1.Contains("DLG05") ||
                App.net.HeaderRecord.fname1.Contains("DLG06") ||
                App.net.HeaderRecord.fname1.Contains("DLG07") ||
                App.net.HeaderRecord.fname1.Contains("DLG08") ||
                App.net.HeaderRecord.fname1.Contains("DLG09"))
            {
                inf_company.Text = App.net.HeaderRecord.sn_name + " (DLG)".Replace("Legal & General", "L n G");// + App.net.HeaderRecord.fname1+")";
            }
            else
            {
                inf_company.Text = App.net.HeaderRecord.sn_name.Replace("Legal & General", "L n G");
            }

            uc_excess.Text = "£" + ((Header)BindingContext).uc_excess.ToString();

            uc_inciden.Text = ((Header)BindingContext).uc_inceden.Substring(0, 10);

            //Inst.Text = "Delivery\t\nInstruct";

            if (App.CurrentApp.HeaderRecord.si_done == false)
                comments.Text = "";

            other_photos.Text = "Other\t\nPhotos";
        }

        protected override bool OnBackButtonPressed()
        {
            //App.CurrentApp.HeaderRecord.bcompletion_signed = true;
            //App.data.SaveHeader();

            CheckInAndSave();

            //base.OnBackButtonPressed();
            return true;
        }

        private void CheckInAndSave()
        {
            string result = "Please complete :\n\n";

            if (App.CurrentApp.HeaderRecord.r_fault.Length == 0)
                result = result + "Fault\n";

            if (App.CurrentApp.HeaderRecord.rno_hours.Length == 0)
                result = result + "Completion time\n";

            if (App.CurrentApp.HeaderRecord.r_work_txt.Length == 0)
                result = result + "Work carried out\n";

            if (App.CurrentApp.HeaderRecord.readdtxt.Length == 0)
                result = result + "Additional information\n";

            if (App.CurrentApp.HeaderRecord.no_of_photos < 5)
                result = result + "5 Photographs\n";

            if (App.CurrentApp.HeaderRecord.bad_image_complete == false)
                result = result + "Additional drawing\n";

            if (App.CurrentApp.HeaderRecord.si_done == true && App.CurrentApp.HeaderRecord.bRepFin == false)
                result = result + "Quality of work report\n";



            if (App.CurrentApp.HeaderRecord.r_bsigned == false)
                result = result + "Customer signature\n";

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
                if (App.net.HeaderRecord.bDone == false)
                {
                    Navigation.InsertPageBefore(new CustomerCareCard(), this);
                }

                App.CurrentApp.HeaderRecord.f_sign_date = DateTime.Today.ToShortDateString();
                App.CurrentApp.HeaderRecord.bDone = true;
                App.data.SaveHeader();
                this.Navigation.PopAsync(false);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (App.CurrentApp.fitter_header_photo_type != 9)
            {
                photos.Text = "Photos\t\nOf Job - " + App.CurrentApp.HeaderRecord.no_of_photos.ToString();
            }
            if (App.CurrentApp.HeaderRecord.no_of_photos >= 5)
            {
                photos.TextColor = Color.DarkGreen;
            }

            App.data.SaveHeader();
        }

        private void OnOtherPhotos(object sender, EventArgs e)
        {
            App.CurrentApp.fitter_header_photo_type = 9;
            Navigation.PushAsync(new Camera(), false);
        }

        private void OnSetOnRoute(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FitterInstructions(), false);
        }

        private void OnDelIns(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FitterInstructions(), false);
        }

        private void OnPhotos(object sender, EventArgs e)
        {
            App.CurrentApp.fitter_header_photo_type = 1;
            Navigation.PushAsync(new Camera(), false);
        }

        private void OnSuperReport(object sender, EventArgs e)
        {
            if (App.CurrentApp.HeaderRecord.si_done == true)
                Navigation.PushAsync(new RemReport(), false);
        }

        private void OnCompletion(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RemedialCompletion(), false);
        }

        private void OnPhone(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Telephone(), false);
        }
    }
}