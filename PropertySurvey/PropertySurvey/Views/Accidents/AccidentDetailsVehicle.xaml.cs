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
	public partial class AccidentDetailsVehicle : CarouselValidateNoImage
    {
		public AccidentDetailsVehicle()
		{
			InitializeComponent ();
            BindingContext = App.net.AccidentRecord as Accident_sheet;
            changed_question_required = false; // Prevent the ItemChanged screen from being shown.
            SetPageNumber();
        }

        private void today_button_clicked(object sender, EventArgs e)
        {
            //date_picker.Date = DateTime.Today;
        }

        private void now_button_clicked(object sender, EventArgs e)
        {
            //time_picker.Time = DateTime.Now.TimeOfDay;
        }

        protected override string validate_page(int page_num)
        {
            return (App.net.AccidentRecord.d_description == "" ? "Description\n" : "")
                 + (App.net.AccidentRecord.d_place == "" ? "Place\n" : "")
                 + (App.net.AccidentRecord.d_weather == "" ? "Weather\n" : "")
                 + (App.net.AccidentRecord.d_speed == "" ? "Speed\n" : "");
        }

        protected override string validate_drawings_and_pictures()
        {
            return "";
        }

        protected override void save_item(bool complete)
        {
            App.CurrentApp.AccidentRecord.c_details = complete;
        }
    }
}