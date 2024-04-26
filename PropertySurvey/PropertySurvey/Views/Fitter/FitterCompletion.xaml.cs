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
	public partial class FitterCompletion : CarouselPage
    {
        bool bGarage = false;

        public FitterCompletion ()
		{
			InitializeComponent ();

            List<string> unfinished_code_options = new List<string>() { "", "Fitters", "Surveyors", "Production", "Order", "Customer", "Delivery", "Copied" };

            BindingContext = App.net.HeaderRecord as Header;

            List<GarageTable> garages = App.data.GetGaragesByContract(App.net.HeaderRecord.udi_cont);

            int no_of = App.net.HeaderRecord.no_of_fitters;

            if (garages.Count() > 0)
            {
                bGarage = true;
                garage_motor.IsVisible = true;
            }
            else
            {
                bGarage = false;
                garage_motor.IsVisible = false;
            }
            set_fit_names_visible();
            SetOptionsVisible();

            unfinished_code.SetPickerItems(unfinished_code_options);

            if (App.CurrentApp.HeaderRecord.bfitter_complete == 1)
            {
                Children.Remove(content1);
                CurrentPage = content2;
            }
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            SetPageNumber();
        }

        private void SetPageNumber()
        {
            page_num.Text = (this.Children.IndexOf(CurrentPage) + 1).ToString() + "/" + this.Children.Count().ToString();
        }

        private void set_fit_names_visible()
        {
            fname2.IsVisible = (App.net.HeaderRecord.no_of_fitters >= 2);
            fname3.IsVisible = (App.net.HeaderRecord.no_of_fitters >= 3);
            fname4.IsVisible = (App.net.HeaderRecord.no_of_fitters >= 4);
            fname5.IsVisible = (App.net.HeaderRecord.no_of_fitters >= 5);
            fname6.IsVisible = (App.net.HeaderRecord.no_of_fitters >= 6);
            fname7.IsVisible = (App.net.HeaderRecord.no_of_fitters >= 7);
            fname8.IsVisible = (App.net.HeaderRecord.no_of_fitters >= 8);
        }

        private void OnFittersChanged(object sender, EventArgs e)
        {
            set_fit_names_visible();
        }

        protected override bool OnBackButtonPressed()
        {
            App.CurrentApp.HeaderRecord.bcompletion_signed = true;
            App.data.SaveHeader();

            base.OnBackButtonPressed();
            return false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetPageNumber();

            if (App.CurrentApp.HeaderRecord.uspot_p3 == 1)
            {
                lintel_sign_button.ImageSource = "green_tick.png";
            }
            else
            {
                lintel_sign_button.ImageSource = "question.png";
            }

            if(App.CurrentApp.HeaderRecord.bcompletion_signed == true)
            {
                fitter_sign_button.ImageSource = "green_tick.png";
            }
            else
            {
                fitter_sign_button.ImageSource = "question.png";
            }
            if(App.CurrentApp.HeaderRecord.bad_image_complete==true)
            {
                add_drawing_button.ImageSource = "green_tick.png";
            }
            else
            {
                add_drawing_button.ImageSource = "question.png";
            }
        }

        private void additional_drawing1_clicked(object sender, EventArgs e)
        {
            App.CurrentApp.drawing_type = "FitterParts";
            App.CurrentApp.HeaderRecord.bad_image_complete = true;
            App.net.drawing_edit_mode = true;
            Navigation.PushAsync(new DrawingPage(), false);
        }

        private void YesNoLabel_OnSelectionChanged(object sender, EventArgs e)
        {

        }

        private void ExcessYesNoLabel_OnSelectionChanged(object sender, EventArgs e)
        {
            SetOptionsVisible();
        }

        private void MandateYesNoLabel_OnSelectionChanged(object sender, EventArgs e)
        {
            SetOptionsVisible();
        }

        private void SetOptionsVisible()
        {
            if (App.CurrentApp.HeaderRecord.fbexcess_paid == 2)
                excesspd.IsVisible = true;
            else
                excesspd.IsVisible = false;

            if (App.CurrentApp.HeaderRecord.fbmandate_signed == 2)
                mandatesigned.IsVisible = true;
            else
                mandatesigned.IsVisible = false;

            if (App.CurrentApp.HeaderRecord.fbadditional_paid == 1)
                addpaid.IsVisible = true;
            else
                addpaid.IsVisible = false;

            if (App.CurrentApp.HeaderRecord.uspot_p1 == 2)
                lintel_area.IsVisible = true;
            else
                lintel_area.IsVisible = false;
        }

        private void OnSign(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FitterSign(), false);
        }

        private void AdditionalYesNoLabel_OnSelectionChanged(object sender, EventArgs e)
        {
            SetOptionsVisible();
        }

        private void LintelYesNoLabel_OnSelectionChanged(object sender, EventArgs e)
        {
            SetOptionsVisible();
        }

        private void OnLintelSign(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FitterSignLintel(), false);
        }

        private void TowerYesNoLabel_OnSelectionChanged(object sender, EventArgs e)
        {

        }
    }
}