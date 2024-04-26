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
	public partial class SpotCheckReport : CarouselPage
    {
		public SpotCheckReport ()
		{
            List<string> appearence_list = new List<string>() { "...", "1", "2", "3", "4", "5" };
            InitializeComponent ();

            uspot_qualityofworks.set_button_list(appearence_list);
            uspot_appearence.set_button_list(appearence_list);
            uspot_customersatisfaction.set_button_list(appearence_list);

            BindingContext = App.net.HeaderRecord as Header;
        }

        protected override bool OnBackButtonPressed()
        {
            //App.CurrentApp.HeaderRecord.bcompletion_signed = true;
            App.data.SaveHeader();

            //CheckInAndSave();
            Navigation.PopAsync(false);
            //base.OnBackButtonPressed();
            return true;
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (App.CurrentApp.HeaderRecord.uspot_signed == true)
            {
                sign.Icon = "signature_complete.png";
            }
            else
            {
                sign.Icon = "signature.png";
            }
        }

        private void OnSignature(object sender, EventArgs e)
        {
            string result = "";

            result = "Please complete :\n\n";

            if (App.CurrentApp.HeaderRecord.uspot_appearence_improvements.Length == 0)
                result = result + "Appearence Improvements\n";

            if (App.CurrentApp.HeaderRecord.uspot_qualityofworks_improvements.Length == 0)
                result = result + "Quality of works improvements\n";

            if (App.CurrentApp.HeaderRecord.uspot_customersatisfaction_improvements.Length == 0)
                result = result + "Customer satisfaction improvements\n";

            if (App.CurrentApp.HeaderRecord.uspot_otherobservations.Length == 0)
                result = result + "Other observations\n";

            if (result.Length > 20)
            {
                DisplayAlert("Missing information", "Please complete :\n\n" + result, "   OK   ");
            }
            else
            {
                Navigation.PushAsync(new SpotCheckSignature(), false);
            }
        }
    }
}