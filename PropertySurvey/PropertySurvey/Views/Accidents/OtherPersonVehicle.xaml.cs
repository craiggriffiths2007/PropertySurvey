using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OtherPersonVehicle : CarouselValidateNoImage
    {
		public OtherPersonVehicle ()
		{
			InitializeComponent ();
            BindingContext = App.net.AccidentRecord as Accident_sheet;
            changed_question_required = false; // Prevent the ItemChanged screen from being shown.
            SetPageNumber();
        }

        protected override string validate_page(int page_num)
        {
            return name_entry.validation_error_string("Full name\n")
                 + address1_entry.validation_error_string("Address 1\n")
                 + (App.net.AccidentRecord.t_add2 == "" ? "Address 2\n" : "")
                 // + (t_add2 == "" ?  : "") // Not all addresses have 3 lines
                 + postcode_entry.validation_error_string("Postcode\n")
                 + telephone_entry.validation_error_string("Telephone\n")
                 + make_entry.validation_error_string("Make\n")
                 + model_entry.validation_error_string("Model\n")
                 + registration_entry.validation_error_string("Registration\n")
                 + number_of_people_entry.validation_error_string("People in vehicle\n")
                 + insurer_entry.validation_error_string("Insurer\n")
                 + certificate_entry.validation_error_string("Their policy\n");
        }

        protected override string validate_drawings_and_pictures()
        {
            return "";
        }

        protected override void save_item(bool complete)
        {
            App.CurrentApp.AccidentRecord.c_them = complete;
        }
    }
}