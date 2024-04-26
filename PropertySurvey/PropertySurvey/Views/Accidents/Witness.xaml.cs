using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Witness : CarouselValidateNoImage
    {
        public Witness()
        {
            InitializeComponent();
            BindingContext = App.net.WitnessRecord as WhitnessesData;
            changed_question_required = false; // Prevent the ItemChanged screen from being shown.
            SetPageNumber();
        }

        protected override string validate_page(int page_num)
        {
            return name_entry.validation_error_string("Full name\n")
                 + address1_entry.validation_error_string("Address 1\n")
                 + (App.net.WitnessRecord.p_add2 == "" ? "Address 2\n" : "")
                 // + (p_add2 == "" ?  : "") // Not all addresses have 3 lines
                 + postcode_entry.validation_error_string("Postcode\n")
                 + telephone_entry.validation_error_string("Telephone\n");
        }

        protected override string validate_drawings_and_pictures()
        {
            return "";
        }

        protected override void save_item(bool complete)
        {
            App.CurrentApp.WitnessRecord.complete = complete;
            App.data.SaveVehicleWitness();
        }
    }
}