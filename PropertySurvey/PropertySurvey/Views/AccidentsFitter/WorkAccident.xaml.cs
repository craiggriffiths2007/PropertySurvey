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
    public partial class WorkAccident : CarouselValidateNoImage
    {
        public WorkAccident()
        {
            InitializeComponent();
            BindingContext = App.net.FAccidentsRecord as FAccidentsTable;
            changed_question_required = false; // Prevent the ItemChanged screen from being shown.

            near_miss_area.IsVisible = App.CurrentApp.FAccidentsRecord.spare10 == "Nearmiss";
            accident_area.IsVisible = !near_miss_area.IsVisible;

            if(App.CurrentApp.FAccidentsRecord.spare10 == "Nearmiss")
            {
                Title = "Near Miss Report";
            }
        }

        private void signature_clicked1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AccidentSign(accident_signature_type.work_accident_signature), false);
        }

        private void signature_clicked2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AccidentSign(accident_signature_type.work_supervisor_signature), false);
        }

        private void photos_clicked(object sender, EventArgs e)
        {
            App.net.HeaderRecord = new Header();
            //App.net.table_init.CreateHeader();
            App.net.HeaderRecord.iRecordType = 0;
            App.CurrentApp.camera_vehicle = 4;
            Navigation.PushAsync(new Camera(), false);
        }

        private void CheckInAndSend()
        {
            string result = "";

            result = "Please complete :\n\n";

            if (App.CurrentApp.FAccidentsRecord.spare10 == "Nearmiss")
            {
                if (App.CurrentApp.FAccidentsRecord.spare11.Length == 0)
                    result = result + "What happened\n";
                if (App.CurrentApp.FAccidentsRecord.spare12.Length == 0)
                    result = result + "Where abouts?\n";
                if (App.CurrentApp.FAccidentsRecord.spare13.Length == 0)
                    result = result + "Date happened?\n";
                if (App.CurrentApp.FAccidentsRecord.spare14.Length == 0)
                    result = result + "Anonymouse or name?\n";

            }
            else
            {


                if (App.CurrentApp.FAccidentsRecord.full_name.Length == 0)
                    result = result + "Name\n";
                if (App.CurrentApp.FAccidentsRecord.add1.Length == 0)
                    result = result + "Address\n";
                if (App.CurrentApp.FAccidentsRecord.pcode.Length == 0)
                    result = result + "Postcode\n";
                if (App.CurrentApp.FAccidentsRecord.occupation.Length == 0)
                    result = result + "Occupation\n";
                if (App.CurrentApp.FAccidentsRecord.filer_full_name.Length == 0)
                    result = result + "Name\n";
                if (App.CurrentApp.FAccidentsRecord.filer_add1.Length == 0)
                    result = result + "Address\n";
                if (App.CurrentApp.FAccidentsRecord.filer_pcode.Length == 0)
                    result = result + "Postcode\n";
                if (App.CurrentApp.FAccidentsRecord.filer_occupation.Length == 0)
                    result = result + "Occupation\n";
                if (App.CurrentApp.FAccidentsRecord.how_did_accident_happen.Length == 0)
                    result = result + "How happened\n";
                if (App.CurrentApp.FAccidentsRecord.materials_used_in_treatment.Length == 0)
                    result = result + "Materials used in treatment\n";

            }

            //if(App.CurrentApp.FAccidentsRecord.spare4.Length==0)
            //{
            //    result = result + "Injuries\n";
            //}

            if (App.CurrentApp.FAccidentsRecord.num_of_photographs < 1)
                result = result + "Photograph(s)\n";

            if (result.Length > 20)
            {
                DisplayAlert("Missing information", result, "OK");
                return;
            }
            else
            {
                Navigation.PushAsync(new SendWorkAccidents(), false);
            }
        }

        private void send_clicked(object sender, EventArgs e)
        {
            CheckInAndSend();
            //Navigation.PushAsync(new SendAccidents(), false);
        }

        protected override string validate_page(int page_num)
        {
            if (App.CurrentApp.FAccidentsRecord.spare10 == "Nearmiss")
                return what_happened_entry.validation_error_string("What happened\n")
                     + where_did_near_miss_happen_entry.validation_error_string("Where abouts?\n")
                     + anon_or_name_entry.validation_error_string("Anonymouse or name?\n");
            else
                return name_entry.validation_error_string("Full name\n")
                     + address1_entry.validation_error_string("Address 1\n")
                     + (App.net.FAccidentsRecord.add2 == "" ? "Address 2\n" : "")
                     // + (App.net.AccidentRecord.y_address3 == "" ? "Address 3\n" : "") Not all addresses have 3 lines
                     + postcode_entry.validation_error_string("Postcode\n")
                     + occupation_entry.validation_error_string("Occupation\n")
                     + filer_name_entry.validation_error_string("Filer full name\n")
                     + filer_address1_entry.validation_error_string("Filer address 1\n")
                     + (App.net.FAccidentsRecord.filer_add2 == "" ? "Filer address 2\n" : "")
                     // + (App.net.AccidentRecord.y_address3 == "" ? "Address 3\n" : "") Not all addresses have 3 lines
                     + filer_postcode_entry.validation_error_string("Filer postcode\n")
                     + filer_occupation_entry.validation_error_string("Filer occupation\n")
                     + how_did_it_happen_entry.validation_error_string("How did it happen\n")
                     + where_did_it_happen_entry.validation_error_string("Where did it happen\n")
                     + materials_for_treatment_entry.validation_error_string("Materials for treatement\n")
                     + injuries_entry.validation_error_string("Injuries\n")
                     + (App.CurrentApp.FAccidentsRecord.person_signed == 0 ? "Signature of who had accident\n" : "")
                     + (App.CurrentApp.FAccidentsRecord.supervisor_signed == 0 ? "Signature of person reporting\n" : "");
        }

        protected override string validate_drawings_and_pictures()
        {
            return "";
        }

        protected override void save_item(bool complete)
        {
            App.data.SaveWorkAccident();
        }
    }
}