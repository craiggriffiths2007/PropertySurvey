using PropertySurvey;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MartControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public static class pet_flap_logic
    {
        public static List<string> magnetic_list = new List<string>() { "...", "None", "Microchip", "Magnetic" };

        public static bool pet_flap_detail_visible(string pet_flap_type)
        {
            return pet_flap_type != ""
                && pet_flap_type != "None";
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PetFlap : StackLayout
	{
        public string pet_flap_binding  { set { pet_flap_picker.TextBinding = value; } }
        public string flap_type_binding { set { pet_flap_type_picker.SetBinding(EditPicker.TextProperty, value); } }
        public string magnetic_binding  { set { magnetic_button.button_binding = value; } }

		public PetFlap ()
		{
			InitializeComponent ();

            pet_flap_picker.SetPickerItems(App.net.pet_flap_size_list);
            pet_flap_type_picker.SetPickerItems(App.net.pet_flap_type_list);
            magnetic_button.set_button_list(pet_flap_logic.magnetic_list);
        }

        public string validation_error_string()
        {
            return (this.IsVisible ?  pet_flap_picker.validation_error_string("Pet flap\n")
                                    + (pet_flap_info_area.IsVisible ?  pet_flap_type_picker.validation_error_string("Pet Type\n")
                                                                     + magnetic_button.validation_error_string("Magnetic/Magnetic\n")
                                                                    : "")
                                   : "");
        }

        private void set_pet_flap_info_visible()
        {
            pet_flap_info_area.IsVisible = pet_flap_logic.pet_flap_detail_visible(pet_flap_picker.Text);
        }

        private void pet_flap_changed(object sender, EventArgs e)
        {
            set_pet_flap_info_visible();
        }
    }
}