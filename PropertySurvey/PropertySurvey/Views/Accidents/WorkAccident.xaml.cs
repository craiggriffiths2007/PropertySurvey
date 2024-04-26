using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SurvAppX
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkAccident : CarouselValidate
    {
		public WorkAccident ()
		{
			InitializeComponent ();
            BindingContext = App.net.FAccidentsRecord as FAccidentsTable;
            changed_question_required = false; // Prevent the ItemChanged screen from being shown.

            near_miss_area.IsVisible = App.CurrentApp.FAccidentsRecord.spare10 == "Nearmiss";
            accident_area.IsVisible = !near_miss_area.IsVisible;
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

        }

        private void send_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SendAccidents(), false);
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