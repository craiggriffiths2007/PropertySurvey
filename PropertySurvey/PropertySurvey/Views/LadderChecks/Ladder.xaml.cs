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
	public partial class Ladder : ContentPage
	{
		public Ladder ()
		{
            BindingContext = App.CurrentApp.LadderRecord as LaddersTable;

            InitializeComponent();

            List<string> ladder_types = new List<string>() { "Extending Ladder", "Small Steps", "Tall Steps", "Conservatory Access", "Surveyor Telescopic" };

            List<string> button_list = new List<string>() { "...", "Yes", "No", "n/a" };

            if (App.CurrentApp.LadderRecord.branch == "")
            {
                App.CurrentApp.LadderRecord.branch = App.net.App_Settings.set_branchcode;
            }

            ladder_type.SetPickerItems(ladder_types);

            in_reasonable_condition.set_button_list(button_list);
            rungs_missing_or_loose.set_button_list(button_list);
            stiles_damaged_or_bent.set_button_list(button_list);
            any_cracks.set_button_list(button_list);
            any_corrosion.set_button_list(button_list);
            rubber_plastic_feet.set_button_list(button_list);
            sharp_or_metal_splinters.set_button_list(button_list);
            painted_or_decorated.set_button_list(button_list);
            hooks_sit_properly.set_button_list(button_list);
            ladders_been_repaired.set_button_list(button_list);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetCompletion();
        }

        protected override bool OnBackButtonPressed()
        {
            App.data.SaveLadderRecord();

            base.OnBackButtonPressed();
            return false;
        }

        public bool ValidateName(string name)
        {
            int num_words = 0;
            bool bad_length = false;

            string[] words = name.Split(' ');
            foreach (string word in words)
            {
                if (word.Length < 3 && word.Length != 0)
                    bad_length = true;
                num_words++;
            }

            if (bad_length == true || num_words < 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void CheckInAndSend()
        {
            string result = "";

            result = "Please complete :\n\n";

            if (App.CurrentApp.LadderRecord.branch.Length == 0)
                result = result + "Branch\n";
            if (App.CurrentApp.LadderRecord.ladder_number.Length == 0)
                result = result + "Ladder number\n";

            if (App.CurrentApp.LadderRecord.s_spare5.Length == 0)
                result = result + "Ladder type\n";



            if (App.CurrentApp.LadderRecord.registration.Length == 0)
                result = result + "Registration\n";

            if (App.CurrentApp.LadderRecord.fitter_surveyor_name.Length == 0 || ValidateName(App.CurrentApp.LadderRecord.fitter_surveyor_name) == false)
                result = result + "Fitter/Surveyor name\n";

            if(App.CurrentApp.LadderRecord.signature_filename == null)
                result = result + "Fitter Singature\n";

            if (App.CurrentApp.LadderRecord.managers_name.Length == 0 || ValidateName(App.CurrentApp.LadderRecord.managers_name) == false)
                result = result + "Manager name\n";

            if (App.CurrentApp.LadderRecord.s_spare4 == null)
                result = result + "Manager Singature\n";

            if (App.CurrentApp.LadderRecord.in_reasonable_condition == 0)
                result = result + "Reasonable condition\n";

            if (App.CurrentApp.LadderRecord.rungs_missing_or_loose == 0)
                result = result + "Rungs missing or loose\n";

            if (App.CurrentApp.LadderRecord.stiles_damaged_or_bent == 0)
                result = result + "Stiles damaged\n";

            if (App.CurrentApp.LadderRecord.any_cracks == 0)
                result = result + "Cracks\n";

            if (App.CurrentApp.LadderRecord.any_corrosion == 0)
                result = result + "Currosion\n";

            if (App.CurrentApp.LadderRecord.rubber_plastic_feet == 0)
                result = result + "Plastic feet\n";

            if (App.CurrentApp.LadderRecord.sharp_or_metal_splinters == 0)
                result = result + "Edges or metal splinters\n";

            if (App.CurrentApp.LadderRecord.painted_or_decorated == 0)
                result = result + "Painted or decorated\n";

            if (App.CurrentApp.LadderRecord.ladders_been_repaired == 0)
                result = result + "Reasonable Condition\n";

            if (App.CurrentApp.LadderRecord.comments.Length == 0)
                result = result + "Comments\n";



            if (App.CurrentApp.LadderRecord.i_spare4 == 1 && App.CurrentApp.LadderRecord.total_photos < 5)
                result = result + "5 Photographs\n";


            if (result.Length > 20)
            {
                DisplayAlert("Missing information", result, "OK");
                return ;
            }
            else
            {
                Navigation.PushAsync(new LadderSend(), false);
            }

        }

        private void Send_Clicked(object sender, EventArgs e)
        {
            CheckInAndSend();
            //if(App.CurrentApp.LadderRecord.signature_filename != null &&
            //    App.CurrentApp.LadderRecord.s_spare4 != null)
            //    Navigation.PushAsync(new LadderSend(), false);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LadderSend(), false);
        }

        private void Sign1_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LadderSign(), false);
        }

        private void Sign2_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LadderSign2(), false);
        }

        private void SetCompletion()
        {
            if(App.CurrentApp.LadderRecord.bSigned == true)
                sign1_button.ImageSource = "green_tick.png";
            else
                sign1_button.ImageSource = "question.png";

            if (App.CurrentApp.LadderRecord.bSigned2 == true)
                sign2_button.ImageSource = "green_tick.png";
            else
                sign2_button.ImageSource = "question.png";

            if(App.net.LadderRecord.total_photos>4)
                photos_button.ImageSource = "green_tick.png";
            else
                photos_button.ImageSource = "question.png";
            //App.CurrentApp.LadderRecord.any_corrosion
            if (App.CurrentApp.LadderRecord.bSent == true)
                green_tick.IsVisible = true;
            else
                green_tick.IsVisible = false;


        }

        private void Photos_Clicked(object sender, EventArgs e)
        {
            App.net.HeaderRecord = new Header();
            //App.net.table_init.CreateHeader();
            App.net.HeaderRecord.iRecordType = 0;
            App.CurrentApp.camera_vehicle = 3;
            Navigation.PushAsync(new Camera(), false);
        }

        private void I_spare4_OnSelectionChanged(object sender, EventArgs e)
        {
            if(App.net.LadderRecord.i_spare4 ==1)
            {
                photos_button_area.IsVisible = true;
            }
            else
            {
                photos_button_area.IsVisible = false;
            }
        }
    }
}