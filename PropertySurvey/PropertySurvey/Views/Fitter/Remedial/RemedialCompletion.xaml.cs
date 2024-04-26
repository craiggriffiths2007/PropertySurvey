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
	public partial class RemedialCompletion : CarouselPage
    {
		public RemedialCompletion ()
		{
            List<string> fault_code = new List<string>() { "", "Fitters Fault", "Faulty Parts", "Manu Fault", "Orig. Ins. Fault", "Sorted Itself Out" };
            List<string> complete_time = new List<string>() { "", "15 mins", "30 mins", "45 mins", "1 Hour", "1.5 Hours", "2 Hours", "2.5 Hours", "Half Day", "Full Day" };

            InitializeComponent();

            fault.SetPickerItems(fault_code);
            est_job_time.SetPickerItems(complete_time);

            set_fit_names_visible();

            BindingContext = App.net.HeaderRecord as Header;
        }

        private void SetButtons()
        {
            if(App.CurrentApp.HeaderRecord.r_bsigned == true)
            {
                cust_sign.ImageSource = "green_tick.png";
            }
            else
            {
                cust_sign.ImageSource = "question.png";
            }

            if(App.net.HeaderRecord.bad_image_complete == true)
            {
                add_drawing_button.ImageSource = "green_tick.png";
            }
            else
            {
                add_drawing_button.ImageSource = "question.png";
            }
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetPageNumber();
            SetButtons();
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

        private void ExcessYesNoLabel_OnSelectionChanged(object sender, EventArgs e)
        {
            if(App.net.HeaderRecord.fbexcess_paid==2)
            {
                excesspd.IsVisible = true;
            }
            else
            {
                excesspd.IsVisible = false;
            }
        }

        private void OnAdditionalDrawing(object sender, EventArgs e)
        {
            //if (App.net.HeaderRecord.bad_image_complete == true)
                App.net.drawing_edit_mode = true;
            //else
            //    App.net.drawing_edit_mode = false;
            App.net.HeaderRecord.bad_image_complete = true;
            App.data.SaveHeader();
            App.CurrentApp.drawing_type = "rem_additional";
            Navigation.PushAsync(new DrawingPage(), false);
        }

        private void OnCustomerSignature(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RemedialSign(), false);
        }
    }
}