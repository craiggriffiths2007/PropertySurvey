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
    public partial class Repudiation : ContentPage
    {
        public Repudiation()
        {
            InitializeComponent();
            List<string> rep_list;
            rep_list = new List<string>() { "", "Zurich commercial", "Not Dele", "Insured not present", "Sheilas wheels", "Esure", "DCGOS" };

            r_not_rep_on_site.SetPickerItems(rep_list);

            if (App.net.DoRep() == false)
            {
                //mess.Text = "Do not repudiate as the insurance is " + App.net.HeaderRecord.fname1;
                mess.IsVisible = true;
            }

            SetRepVis();



            BindingContext = App.net.HeaderRecord as Header;
        }

        private void SetRepVis()
        {
            if (App.net.HeaderRecord.i_spare2 == 1)
            {
                r_not_rep_on_site.IsVisible = false;
            }
            else
            {
                r_not_rep_on_site.IsVisible = true;
            }

        }
        private void R_rep_on_site_OnSelectionChanged(object sender, EventArgs e)
        {
            SetRepVis();
        }

        private void R_not_rep_on_site_OnTextChangedEvent(object sender, EventArgs e)
        {

        }

        protected override bool OnBackButtonPressed()
        {
            string error_text = "";

            if (App.CurrentApp.HeaderRecord.i_spare2 == 0 &&
            App.CurrentApp.HeaderRecord.s_spare3.Length == 0)
                error_text = error_text + "Reason did not repudiate\n";

            if (App.CurrentApp.HeaderRecord.s_spare1.Length == 0)
                error_text = error_text + "Explaination for being different\n";

            if (App.CurrentApp.HeaderRecord.s_spare2.Length == 0)
                error_text = error_text + "Other information\n";

            if (error_text != "")
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var response = await Application.Current.MainPage.DisplayAlert("Missing information",
                        "Please complete :\n\n" + error_text + "\n\nClose Anyway?\n", "   Yes   ", "   No   ");
                    if (response)
                    {
                        App.CurrentApp.HeaderRecord.i_spare1 = 0;
                        App.data.SaveHeader();
                        await this.Navigation.PopAsync(false);
                    }
                });
            }
            else
            {
                App.CurrentApp.HeaderRecord.i_spare1 = 1;
                App.data.SaveHeader();
                this.Navigation.PopAsync(false);
            }

            base.OnBackButtonPressed();
            return true;
        }
    }
}