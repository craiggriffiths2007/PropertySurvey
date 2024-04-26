using PropertySurvey;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MartControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public static class letter_box_logic
    {
        public static bool letter_box_position_visible(string letter_box_position)
        {
            return letter_box_position != ""
                && letter_box_position != "None";
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Letterbox : StackLayout
    {
        public string letter_box_binding { set { letter_box_picker.TextBinding = value; } }
        public string letter_box_position_binding { set { letter_box_position_picker.TextBinding = value; } }

        public Letterbox ()
		{
			InitializeComponent ();

            letter_box_picker.SetPickerItems(App.net.letter_box_colour_list);
            letter_box_position_picker.SetPickerItems(App.net.letter_box_pos_list);
        }

        public string validation_error_string()
        {
            string result = "";

            if (this.IsVisible)
            {
                if (letter_box_picker.Text == null || letter_box_picker.Text.Length == 0)
                    result = "Letter Box\n";

                if (letter_box_position_picker.IsVisible
                    && (letter_box_position_picker.Text == null || letter_box_position_picker.Text.Length == 0))
                    result = result + "Letter Box Position\n";
            }

            return result;
        }

        private void set_letter_box_position_visible()
        {
            letter_box_position_picker.IsVisible = letter_box_logic.letter_box_position_visible(letter_box_picker.Text);
        }

        private void letter_box_changed(object sender, EventArgs e)
        {
            set_letter_box_position_visible();
        }
    }
}