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
    public partial class FitterHeader : ContentPage
    {
        bool bFisrtLoad = true;
        public FitterHeader()
        {
            InitializeComponent();

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

            //Inst.Text = "Delivery\nInstruct";
            if (App.CurrentApp.HeaderRecord.bSurvey == true)
            {
                survey.Text = "View\nSurvey";
            }
            else
            {
                survey.Text = "No\nSurvey";
                survey.IsEnabled = false;
            }

            stock.Text = "Stock\nUsage";
            mandate.Text = "Sign\nMandate";
            photos.Text = "Photos\nOf Job - " + App.CurrentApp.HeaderRecord.no_of_photos.ToString();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            photos.Text = "Photos\nOf Job - " + App.CurrentApp.HeaderRecord.no_of_photos.ToString();
            if (App.CurrentApp.HeaderRecord.no_of_photos >= 7)
            {
                photos.TextColor = Color.DarkGreen;
            }
            if (App.CurrentApp.HeaderRecord.fitter_comments.Length > 0)
            {
                comments_button.TextColor = Color.DarkGreen;
            }
            if (App.CurrentApp.HeaderRecord.fbstockusagecomplete == true)
            {
                stock.TextColor = Color.DarkGreen;
            }
            if (App.CurrentApp.HeaderRecord.fmanimage == true)
            {
                mandate.TextColor = Color.DarkGreen;
            }

            if (App.CurrentApp.HeaderRecord.fname1.Length == 0 ||

                App.CurrentApp.HeaderRecord.fbexcess_paid == 0 ||

                (App.CurrentApp.HeaderRecord.fbexcess_paid == 2 && App.CurrentApp.HeaderRecord.freason_excess_not_paid.Length == 0) ||

                App.CurrentApp.HeaderRecord.fbmandate_signed == 0 ||

                (App.CurrentApp.HeaderRecord.fbmandate_signed == 2 && App.CurrentApp.HeaderRecord.freason_mandate_not_signed.Length == 0) ||

                App.CurrentApp.HeaderRecord.fbadditional_paid == 0 ||

                (App.CurrentApp.HeaderRecord.fbadditional_paid == 1 && App.CurrentApp.HeaderRecord.fhow_mutch_additional_paid.Length == 0) ||

                App.CurrentApp.HeaderRecord.bcompletion_signed == false)
            {

            }
            else
            {
                completion.TextColor = Color.DarkGreen;
            }
            //if (App.CurrentApp.HeaderRecord.bcompletion_signed == true)
            //{
            //    completion.TextColor = Color.DarkGreen;
            //}
            if (App.CurrentApp.HeaderRecord.garage_door_motor == 1)
            {
                if (App.CurrentApp.HeaderRecord.directive_complete == 1)
                {
                    directive.Icon = "directive_complete.png";
                }
                else
                {
                    directive.Icon = "directive.png";
                }
            }
            else
            {
                directive.Icon = "";
            }

            if (bFisrtLoad == true)
                bFisrtLoad = false;
            else
                App.data.SaveHeader();


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

            bool bFin = false;

            bool[] completed = new bool[6];

            completed[0] = true;
            completed[1] = true;
            completed[2] = true;
            completed[3] = true;
            completed[4] = true;
            completed[5] = true;

            if (App.CurrentApp.HeaderRecord.fname1.Length == 0 ||

            App.CurrentApp.HeaderRecord.fbexcess_paid == 0 ||

            (App.CurrentApp.HeaderRecord.fbexcess_paid == 2 && App.CurrentApp.HeaderRecord.freason_excess_not_paid.Length == 0) ||

            App.CurrentApp.HeaderRecord.fbmandate_signed == 0 ||

            (App.CurrentApp.HeaderRecord.fbmandate_signed == 2 && App.CurrentApp.HeaderRecord.freason_mandate_not_signed.Length == 0) ||

            App.CurrentApp.HeaderRecord.fbadditional_paid == 0 ||

            (App.CurrentApp.HeaderRecord.fbadditional_paid == 1 && App.CurrentApp.HeaderRecord.fhow_mutch_additional_paid.Length == 0) ||

            App.CurrentApp.HeaderRecord.bcompletion_signed == false)
            {
                completed[0] = false;
            }

            if (App.CurrentApp.HeaderRecord.bfitter_complete == 2)
            {
                if (
                    App.CurrentApp.HeaderRecord.bfitter_complete == 2 &&
                    (
                    App.CurrentApp.HeaderRecord.funfinished_code.Length == 0 ||
                    App.CurrentApp.HeaderRecord.freason_unfinished.Length == 0 ||
                    App.CurrentApp.HeaderRecord.fparts_required.Length == 0 ||
                    App.CurrentApp.HeaderRecord.bcompletion_signed == false))
                {
                    completed[0] = false;
                }
            }

            if (App.CurrentApp.HeaderRecord.fitter_comments.Length == 0)
            {
                completed[1] = false;
            }

            if (App.CurrentApp.HeaderRecord.fbmandate_signed == 1 && App.CurrentApp.HeaderRecord.fmanimage == false)
            {
                completed[2] = false;
            }

            if (App.CurrentApp.HeaderRecord.fbstockusagecomplete == false)
            {
                completed[3] = false;
            }

            if (App.CurrentApp.HeaderRecord.no_of_photos < 7)
            {
                completed[4] = false;
            }

            if (completed[0] == true &&
                completed[1] == true &&
                completed[3] == true &&
                completed[4] == true)
            {
                //return true;
                bFin = true;
            }
            else
            {
                //return false;
            }

            if (completed[0] == false)
                result = result + "Completion\n";

            if (completed[1] == false)
                result = result + "Comments\n";

            if (App.CurrentApp.HeaderRecord.fbmandate_signed == 1 && completed[2] == false)
                result = result + "Mandate\n";

            if ((App.CurrentApp.HeaderRecord.type_of_equipment.IndexOf("Scaffold") > -1 || App.CurrentApp.HeaderRecord.i_spare3 == 1) && App.CurrentApp.HeaderRecord.isTowerScaff != 3)
            {
                //result = result + "Tower scaffold checklist\n";
            }

            if (App.CurrentApp.HeaderRecord.plaster == 1) // && App.CurrentApp.HeaderRecord.survey_complete == 0)
            {
                /*
                bool bFound = false;

                var query = from p in App.CurrentApp.DB.MotorSheet where p.udi_cont == App.CurrentApp.HeaderRecord.udi_cont select p;
                foreach (var item in query)
                {
                    if (item.i_signed == 0 || item.i_signed_cust == 0)
                    {
                        bFound = true;
                    }
                }

                if (bFound == true)
                {
                    result = result + "Machinery Directive\n";
                }
                */
            }

            if (completed[3] == false)
                result = result + "Stock Usage Complete\n";

            if (completed[4] == false)
                result = result + "10 Photographs\n";

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
                if (App.CurrentApp.HeaderRecord.bfitter_complete == 2 && App.net.HeaderRecord.bDone == false)
                {
                    Navigation.InsertPageBefore(new UnfinishedCode(), this);
                }
                else
                {
                    if (App.net.HeaderRecord.bDone == false)
                    {
                        Navigation.InsertPageBefore(new CustomerCareCard(), this);
                    }

                    App.CurrentApp.HeaderRecord.f_sign_date = DateTime.Today.ToShortDateString();
                    App.CurrentApp.HeaderRecord.bDone = true;

                }
                App.data.SaveHeader();
                this.Navigation.PopAsync(false);
            }


        }



        private void OnMenuChanged(object sender, EventArgs e)
        {

        }

        private void OnSetOnRoute(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EnRoute(), false);
        }

        private void OnDelIns(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new FitterInstructions(), this);
            Navigation.PopAsync(false);
        }

        private void OnPhotos(object sender, EventArgs e)
        {
            App.CurrentApp.fitter_header_photo_type = 1;
            App.data.SaveHeader();

            if (App.CurrentApp.HeaderRecord.si_mpay == "GFD01" ||
                App.CurrentApp.HeaderRecord.si_mpay == "GFD02" ||
                App.CurrentApp.HeaderRecord.si_mpay == "GFD03" ||
                App.CurrentApp.HeaderRecord.si_mpay == "GFD04")
            {
                Navigation.PushAsync(new HingeWarning(), false);
            }
            else
            {
                Navigation.PushAsync(new Camera(), false);
            }

        }

        private void OnComments(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FitterComments(), false);
        }

        private void OnSurvey(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PropertySurveyItemPage(), false);
        }

        private void OnStock(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FitterStockUsage(), false);
        }

        private void OnMandate(object sender, EventArgs e)
        {
            if (App.CurrentApp.HeaderRecord.sn_name.Contains("GFD Trading Ltd"))
                Navigation.PushAsync(new FitterMandateGlobal(), false);
            else
                Navigation.PushAsync(new FitterMandate(), false);
        }

        private void OnCompletion(object sender, EventArgs e)
        {
            if (App.CurrentApp.HeaderRecord.typeB == "Collect and Copy")
            {
                App.CurrentApp.HeaderRecord.bfitter_complete = 1;
                Navigation.PushAsync(new FitterCompletion(), false);
            }
            else
            {
                Navigation.PushAsync(new FitIsComplete(), false);
            }
        }

        private void OnDirectiveClicked(object sender, EventArgs e)
        {
            if (App.CurrentApp.HeaderRecord.garage_door_motor == 1)
            {
                Navigation.PushAsync(new MotorQuest(), false);
            }
        }

        private void OnPhone(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Telephone(), false);
        }
    }
}