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
	public partial class SecuritySurvey : ContentPage
	{
		public SecuritySurvey ()
		{
			InitializeComponent ();

            List<string> many_windows = new List<string>() { "", "none", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30" };
            List<string> many_doors = new List<string>() { "", "none", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            List<string> general_condition = new List<string>() { "", "poor", "average", "good", "Other (please detail)" };
            List<string> windows_made_from = new List<string>() { "", "Timber", "Aluminium", "UPVC", "Other (please detail)" };
            List<string> doors_made_from = new List<string>() { "", "Timber", "Aluminium", "UPVC", "Other (please detail)" };
            List<string> windows_locking_system = new List<string>() { "", "Shoot bolt", "Rollercam", "cockspur", "Other (please detail)" };
            List<string> door_locking_system = new List<string>() { "", "5 lever", "Claw lock", "roller cam", "Other (please detail)" };
            List<string> time_required = new List<string>() { "", "None (n/a)", "30 minutes", "1 hour", "1 hour 30 minutes", "2 hours", "2 hours 30 minutes", "3 hours", "3 hours 30 minutes", "4 hours", "4 hours 30 minutes", "5 hours", "5 hours 30 minutes", "6 hours", "6 hours 30 minutes", "7 hours", "7 hours 30 minutes", "8 hours", "8 hours 30 minutes" };

            many_windows_op.SetPickerItems(many_windows);
            many_doors_op.SetPickerItems(many_doors);
            general_condition_op.SetPickerItems(general_condition);
            windows_made_from_op.SetPickerItems(windows_made_from);
            doors_made_from_op.SetPickerItems(doors_made_from);
            window_locking_system_op.SetPickerItems(windows_locking_system);
            door_locking_system_op.SetPickerItems(door_locking_system);
            time_required_op.SetPickerItems(time_required);

            SetSecSurv();

            if (App.net.HeaderRecord.iRecordType > 0)
                page1.IsEnabled = false;

            BindingContext = App.net.HeaderRecord as Header;
        }

        protected override void OnAppearing()
        {
            SetPhotosNum();
            base.OnAppearing();
        }

        private void SetPhotosNum()
        {
            int n;
            bool isNumeric = int.TryParse(App.CurrentApp.HeaderRecord.ss_nodoors, out n);
            bool isNumeric2 = int.TryParse(App.CurrentApp.HeaderRecord.ss_nowindows, out n);

            int num_required = 0;
            if (isNumeric)
            {
                num_required += Convert.ToInt32(App.CurrentApp.HeaderRecord.ss_nodoors);
            }

            if (isNumeric2)
            {
                num_required += Convert.ToInt32(App.CurrentApp.HeaderRecord.ss_nowindows);
            }

            num_required = num_required * 2;

            photos_text.Text = "Photos ( " + App.CurrentApp.HeaderRecord.ss_no_of_photos.ToString() + "/" + num_required.ToString() + " )";
        }

        private void SetAll()
        {
            int n;
            bool isNumeric = int.TryParse(App.CurrentApp.HeaderRecord.ss_nodoors, out n);
            bool isNumeric2 = int.TryParse(App.CurrentApp.HeaderRecord.ss_nowindows, out n);

            if (isNumeric == true)
            {
                door_area1.IsVisible = true;
                door_area2.IsVisible = true;
                door_area3.IsVisible = true;
                door_warning.IsVisible = true;
            }
            else
            {
                door_area1.IsVisible = false;
                door_area2.IsVisible = false;
                door_area3.IsVisible = false;
                door_warning.IsVisible = false;
            }

            if (isNumeric2 == true)
            {
                window_area1.IsVisible = true;
                window_area2.IsVisible = true;
                window_area3.IsVisible = true;
                window_warning.IsVisible = true;
            }
            else
            {
                window_area1.IsVisible = false;
                window_area2.IsVisible = false;
                window_area3.IsVisible = false;
                window_warning.IsVisible = false;
            }

            if (App.net.HeaderRecord.ss_gencondition == "Other (please detail)")
            { general_condition_expain_op.IsVisible = true; }
            else
            { general_condition_expain_op.IsVisible = false; }

            if (App.net.HeaderRecord.ss_matwindows == "Other (please detail)")
            { windows_made_from_explain_op.IsVisible = true; }
            else
            { windows_made_from_explain_op.IsVisible = false; }

            if (App.net.HeaderRecord.ss_matdoors == "Other (please detail)")
            { doors_made_from_explain_op.IsVisible = true; }
            else
            { doors_made_from_explain_op.IsVisible = false; }

            if (App.net.HeaderRecord.ss_lockwindows == "Other (please detail)")
            { window_locking_system_explain_op.IsVisible = true; }
            else
            { window_locking_system_explain_op.IsVisible = false; }

            if (App.net.HeaderRecord.ss_lockdoors == "Other (please detail)")
            { doors_locking_system_explain_op.IsVisible = true; }
            else
            { doors_locking_system_explain_op.IsVisible = false; }

            if (App.net.HeaderRecord.ss_add_window_security == 1)
            { window_sec_explain_op.IsVisible = true; }
            else
            { window_sec_explain_op.IsVisible = false; }

            if (App.net.HeaderRecord.ss_add_door_security == 1)
            { door_sec_explain_op.IsVisible = true; }
            else
            { door_sec_explain_op.IsVisible = false; }
        }

        protected override bool OnBackButtonPressed()
        {
            string error_text = "";

            if (App.net.HeaderRecord.iRecordType == 0)
            {
                if (App.CurrentApp.HeaderRecord.iRecordType > 0)
                {
                    this.Navigation.PopAsync(false);
                    return true;
                }

                if (App.CurrentApp.HeaderRecord.ss_bIsSecuritySurvey == 2)
                {
                    App.CurrentApp.HeaderRecord.ss_bIsComplete = 0;
                    App.data.SaveHeader();
                    this.Navigation.PopAsync(false);
                    return true;
                }

                if (App.CurrentApp.HeaderRecord.ss_nowindows.Length == 0)
                    error_text = error_text + "No of windows\n";

                if (App.CurrentApp.HeaderRecord.ss_nodoors.Length == 0)
                    error_text = error_text + "No of doors\n";

                if (App.CurrentApp.HeaderRecord.ss_gencondition.Length == 0)
                    error_text = error_text + "General condition\n";

                if (App.CurrentApp.HeaderRecord.ss_gencondition == "Other (please detail)" &&
                    App.CurrentApp.HeaderRecord.ss_gencondition_other.Length == 0)
                    error_text = error_text + "General condition other\n";

                if (App.CurrentApp.HeaderRecord.ss_nowindows != "none")
                {
                    if (App.CurrentApp.HeaderRecord.ss_matwindows.Length == 0)
                        error_text = error_text + "Window material\n";

                    if (App.CurrentApp.HeaderRecord.ss_matwindows == "Other (please detail)" &&
                        App.CurrentApp.HeaderRecord.ss_matwindows_other.Length == 0)
                        error_text = error_text + "Window material other\n";
                }

                if (App.CurrentApp.HeaderRecord.ss_nodoors != "none")
                {
                    if (App.CurrentApp.HeaderRecord.ss_matdoors.Length == 0)
                        error_text = error_text + "Door material\n";

                    if (App.CurrentApp.HeaderRecord.ss_matdoors == "Other (please detail)" &&
                        App.CurrentApp.HeaderRecord.ss_matdoors_other.Length == 0)
                        error_text = error_text + "Door material other\n";
                }

                if (App.CurrentApp.HeaderRecord.ss_nowindows != "none")
                {
                    if (App.CurrentApp.HeaderRecord.ss_lockwindows.Length == 0)
                        error_text = error_text + "Window locks\n";

                    if (App.CurrentApp.HeaderRecord.ss_lockwindows == "Other (please detail)" &&
                        App.CurrentApp.HeaderRecord.ss_lockwindows_other.Length == 0)
                        error_text = error_text + "Window locks other\n";
                }

                if (App.CurrentApp.HeaderRecord.ss_nodoors != "none")
                {
                    if (App.CurrentApp.HeaderRecord.ss_lockdoors.Length == 0)
                        error_text = error_text + "Door locks\n";

                    if (App.CurrentApp.HeaderRecord.ss_lockdoors == "Other (please detail)" &&
                        App.CurrentApp.HeaderRecord.ss_lockdoors_other.Length == 0)
                        error_text = error_text + "Door locks other\n";
                }

                if (App.CurrentApp.HeaderRecord.ss_nowindows != "none")
                {
                    if (App.CurrentApp.HeaderRecord.ss_add_window_security == 0)
                        error_text = error_text + "Additional window security\n";
                    if (App.CurrentApp.HeaderRecord.ss_add_window_security == 1 &&
                        App.CurrentApp.HeaderRecord.ss_secwindows_other.Length == 0)
                        error_text = error_text + "Additional security window required\n";
                }

                if (App.CurrentApp.HeaderRecord.ss_nodoors != "none")
                {
                    if (App.CurrentApp.HeaderRecord.ss_add_door_security == 0)
                        error_text = error_text + "Additional door security\n";
                    if (App.CurrentApp.HeaderRecord.ss_add_door_security == 1 &&
                        App.CurrentApp.HeaderRecord.ss_secdoors_other.Length == 0)
                        error_text = error_text + "Additional security doors required\n";
                }

                int n;
                bool isNumeric = int.TryParse(App.CurrentApp.HeaderRecord.ss_nodoors, out n);
                bool isNumeric2 = int.TryParse(App.CurrentApp.HeaderRecord.ss_nowindows, out n);

                int num_required = 0;
                if (isNumeric)
                {
                    num_required += Convert.ToInt32(App.CurrentApp.HeaderRecord.ss_nodoors);
                }

                if (isNumeric2)
                {
                    num_required += Convert.ToInt32(App.CurrentApp.HeaderRecord.ss_nowindows);
                }

                num_required = num_required * 2;

                if (App.CurrentApp.HeaderRecord.ss_no_of_photos < num_required)
                    error_text = error_text + "Photos ( " + App.CurrentApp.HeaderRecord.ss_no_of_photos.ToString() + "/" + num_required.ToString() + " )\n";

                if (App.CurrentApp.HeaderRecord.ss_time_required.Length == 0)
                    error_text = error_text + "Door locks\n";


                if (error_text != "")
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var response = await Application.Current.MainPage.DisplayAlert("Missing information",
                            "Please complete :\n\n" + error_text + "\n\nClose Anyway?\n", "   Yes   ", "   No   ");
                        if (response)
                        {
                            App.CurrentApp.HeaderRecord.ss_bIsComplete = 0;
                            App.data.SaveHeader();
                            await this.Navigation.PopAsync(false);
                        }
                    });
                }
                else
                {
                    App.CurrentApp.HeaderRecord.ss_bIsComplete = 1;
                    App.data.SaveHeader();
                    this.Navigation.PopAsync(false);
                }
            }
            else
            {
                this.Navigation.PopAsync(false);
            }

            base.OnBackButtonPressed();
            return true;
        }

        private void Many_windows_op_OnTextChangedEvent(object sender, EventArgs e)
        {
            SetAll();
            SetPhotosNum();
        }

        private void Many_doors_op_OnTextChangedEvent(object sender, EventArgs e)
        {
            SetAll();
            SetPhotosNum();
        }

        private void SetSecSurv()
        {
            if(App.CurrentApp.HeaderRecord.ss_bIsSecuritySurvey == 1)
                SecSurvey.IsVisible = true;
            else
                SecSurvey.IsVisible = false;
        }

        private void selection_changed(object sender, EventArgs e)
        {
            SetSecSurv();
        }

        private void General_condition_op_OnTextChangedEvent(object sender, EventArgs e)
        {
            SetAll();
        }

        private void Windows_made_from_op_OnTextChangedEvent(object sender, EventArgs e)
        {
            SetAll();
        }

        private void Doors_made_from_op_OnTextChangedEvent(object sender, EventArgs e)
        {
            SetAll();
        }

        private void Window_locking_system_op_OnTextChangedEvent(object sender, EventArgs e)
        {
            SetAll();
        }

        private void Door_locking_system_op_OnTextChangedEvent(object sender, EventArgs e)
        {
            SetAll();
        }

        private void Window_add_OnSelectionChanged(object sender, EventArgs e)
        {
            SetAll();
        }

        private void Door_add_OnSelectionChanged(object sender, EventArgs e)
        {
            SetAll();
        }

        private void Time_required_op_OnTextChangedEvent(object sender, EventArgs e)
        {
            
        }

        private async void OnImagesClicked(object sender, EventArgs e)
        {
            App.data.SaveHeader();

            int n;
            bool isNumeric = int.TryParse(App.CurrentApp.HeaderRecord.ss_nodoors, out n);
            bool isNumeric2 = int.TryParse(App.CurrentApp.HeaderRecord.ss_nowindows, out n);

            int num_required = 0;
            if (isNumeric)
            {
                num_required += Convert.ToInt32(App.CurrentApp.HeaderRecord.ss_nodoors);
            }

            if (isNumeric2)
            {
                num_required += Convert.ToInt32(App.CurrentApp.HeaderRecord.ss_nowindows);
            }
            num_required = num_required * 2;

            //await DisplayAlert("", "Take 2 photos of each item a total of " + num_required.ToString() + " are required.","OK");

            App.CurrentApp.camera_vehicle = 2;
            await Navigation.PushAsync(new Camera(), false);
        }
    }
}