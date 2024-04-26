using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PoliceDetails : CarouselValidateNoImage
    {
		public PoliceDetails ()
		{
			InitializeComponent ();
            BindingContext = App.net.AccidentRecord as Accident_sheet;
            changed_question_required = false; // Prevent the ItemChanged screen from being shown.
            SetPageNumber();
        }
        protected override string validate_page(int page_num)
        {
            return officers_name_entry.validation_error_string("Officer's name\n")
                 + officers_number_entry.validation_error_string("Number\n")
                 + officers_station_entry.validation_error_string("Station\n");
        }
        protected override string validate_drawings_and_pictures()
        {
            return "";
        }

        protected override void save_item(bool complete)
        {
            App.CurrentApp.AccidentRecord.c_police = complete;
        }
    }
}