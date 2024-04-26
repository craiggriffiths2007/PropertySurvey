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
    public partial class JobSummary : ContentPage
    {
        public JobSummary()
        {
            if (App.net.HeaderRecord.nsn == "")
            {
                App.net.HeaderRecord.nsn = App.net.App_Settings.set_ownercode.ToUpper();
            }
            if (App.net.HeaderRecord.new_ispare9 == 0)
                App.net.HeaderRecord.new_ispare9 = 2;

            InitializeComponent();

            SetSumVis();

            BindingContext = App.net.HeaderRecord as Header;

            List<string> time_list = new List<string>() { "", "1 hour", "1 hour 30 minutes", "2 hours", "2 hours 30 minutes", "3 hours", "3 hours 30 minutes", "4 hours", "4 hours 30 minutes", "5 hours", "5 hours 30 minutes", "6 hours", "6 hours 30 minutes", "7 hours", "7 hours 30 minutes", "8 hours", "8 hours 30 minutes", "Half day", "Full day", "One and a half days", "Two days", "Two and a half days", "Three days", "Three and a half days", "Four days", "Four and a half days", "Five days" };
            List<string> no_complete_reas = new List<string>() { "", "No one in", "Done at securing", "Cancelled" };

            List<string> grade_list = new List<string>() { "...", "A", "B", "C", "D" };
            List<string> size_list = new List<string>() { "...", "A", "B", "C" };

            est_job_time.SetPickerItems(time_list);

            reason_not_complete.SetPickerItems(no_complete_reas);

            App.CurrentApp.HeaderRecord.nsn = App.net.App_Settings.set_ownercode;

            job_grade.set_button_list(grade_list);
            njs.set_button_list(size_list);

            switch (App.net.HeaderRecord.job_grade)
            {
                case "":
                    job_grade.set_button_state(0); break;
                case "A":
                    job_grade.set_button_state(1); break;
                case "B":
                    job_grade.set_button_state(2); break;
                case "C":
                    job_grade.set_button_state(3); break;
                case "D":
                    job_grade.set_button_state(4); break;
            }
            switch (App.net.HeaderRecord.njs)
            {
                case "":
                    njs.set_button_state(0); break;
                case "A":
                    njs.set_button_state(1); break;
                case "B":
                    njs.set_button_state(2); break;
                case "C":
                    njs.set_button_state(3); break;
            }

            if (App.net.HeaderRecord.iRecordType > 0)
            {
                page1.IsEnabled = false;
            }
            reason_not_booked.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);
            //items_matching_up.set_button_list(items_matching);
        }


        private void SetSumVis()
        {
            if (App.net.HeaderRecord.new_ispare9 == 1)
            {
                sum_area.IsVisible = true;
                summary_area.Focus();
            }
            else
            {
                sum_area.IsVisible = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (App.net.HeaderRecord.iRecordType == 0)
            {
                //summary_area.Focus();
            }

            SetReasonNotCompleteArea();
            SetReasonNotBookedArea();
        }

        private void SetReasonNotCompleteArea()
        {
            if (App.CurrentApp.HeaderRecord.survey_complete == 2)
                ReasonSurveyIncompleteArea.IsVisible = true;
            else
                ReasonSurveyIncompleteArea.IsVisible = false;
        }

        protected override bool OnBackButtonPressed()
        {
            if (App.net.HeaderRecord.iRecordType == 0)
            {
                if ((App.CurrentApp.HeaderRecord.survey_complete == 1 && (/*App.CurrentApp.HeaderRecord.summ_text.Length == 0 ||*/
                    App.CurrentApp.HeaderRecord.time_to_complete.Length == 0)) ||
                    (App.CurrentApp.HeaderRecord.survey_complete == 2 && App.CurrentApp.HeaderRecord.reason_not_complete.Length == 0))
                {
                    App.CurrentApp.HeaderRecord.bSumFin = false;
                }
                else
                {
                    App.CurrentApp.HeaderRecord.bSumFin = true;
                }
                switch (job_grade.ButtonState)
                {
                    case 0:
                        App.CurrentApp.HeaderRecord.job_grade = ""; break;
                    case 1:
                        App.CurrentApp.HeaderRecord.job_grade = "A"; break;
                    case 2:
                        App.CurrentApp.HeaderRecord.job_grade = "B"; break;
                    case 3:
                        App.CurrentApp.HeaderRecord.job_grade = "C"; break;
                    case 4:
                        App.CurrentApp.HeaderRecord.job_grade = "D"; break;
                }
                switch (njs.ButtonState)
                {
                    case 0:
                        App.CurrentApp.HeaderRecord.njs = ""; break;
                    case 1:
                        App.CurrentApp.HeaderRecord.njs = "A"; break;
                    case 2:
                        App.CurrentApp.HeaderRecord.njs = "B"; break;
                    case 3:
                        App.CurrentApp.HeaderRecord.njs = "C"; break;
                }
            }
            Navigation.PopAsync(false);
            //this.
            //RaiseBackButtonPressed();
            return true;
        }

        private void Survey_complete_OnSelectionChanged(object sender, EventArgs e)
        {
            SetReasonNotCompleteArea();
        }

        private void SetReasonNotBookedArea()
        {
            if (App.CurrentApp.HeaderRecord.booked == 2)
            {
                ReasonArea.IsVisible = true;
            }
            else
            {
                ReasonArea.IsVisible = false;
            }
        }

        private void Booked_OnSelectionChanged(object sender, EventArgs e)
        {
            SetReasonNotBookedArea();
        }

        private void Yes_or_no_OnSelectionChanged(object sender, EventArgs e)
        {
            SetSumVis();
        }
    }
}