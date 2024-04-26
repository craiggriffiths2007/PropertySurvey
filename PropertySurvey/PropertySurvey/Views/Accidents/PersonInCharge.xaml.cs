using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersonInCharge : CarouselValidateNoImage
    {
		public PersonInCharge ()
		{
			InitializeComponent ();
            BindingContext = App.net.AccidentRecord as Accident_sheet;
            changed_question_required = false; // Prevent the ItemChanged screen from being shown.

            SetPageNumber();
        }

        private void signature_clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AccidentSign(accident_signature_type.vehicle_accident_signature), false); 
        }

        private string validate_page_0()
        {
            return make_entry.validation_error_string("Make\n")
                 + model_entry.validation_error_string("Model\n")
                 + registration_entry.validation_error_string("Registration\n")
                 + used_for_entry.validation_error_string("Vehicle used for\n")
                 + name_entry.validation_error_string("Full name\n")
                 // + date_of_birth_control.
                 + address1_entry.validation_error_string("Address 1\n")
                 + (App.net.AccidentRecord.y_address2 == "" ? "Address 2\n" : "")
                 // + (App.net.AccidentRecord.y_address3 == "" ? "Address 3\n" : "") Not all addresses have 3 lines
                 + postcode_entry.validation_error_string("Postcode\n")
                 + occupation_entry.validation_error_string("Occupation\n");
        }

        private string validate_page_1()
        {
            return years_entry.validation_error_string("Years employed\n")
                 + months_entry.validation_error_string("Months employed\n")
                 + involved_entry.validation_error_string("Accidents involved in\n")
                 + convictions_entry.validation_error_string("Prosecutions\n")
                 + infirmity_entry.validation_error_string("Informity or disabilities\n")
                 + vehicle_damaged_entry.validation_error_string("Vehicle damaged\n")
                 + drivable_button.validation_error_string("Vehicle drivable\n")
                 + property_damaged_entry.validation_error_string("Other property damaged\n")
                 + injuries_entry.validation_error_string("Other person injuries\n")
                 + (!App.CurrentApp.AccidentRecord.y_signed ? "Signature" : "");
        }

        protected override string validate_page(int page_num)
        {
            switch (page_num)
            {
                case 0: return validate_page_0();
                case 1: return validate_page_1();
                default: return "";
            }
        }

        protected override void set_page(int page_num)
        {
            switch (page_num)
            {
                case 0: CurrentPage = Page0; break;
                case 1: CurrentPage = Page1; break;
            }
        }

        protected override string validate_drawings_and_pictures()
        {
            return "";
        }

        protected override void save_item(bool complete)
        {
            App.CurrentApp.AccidentRecord.c_you = complete;
        }
    }
}