using System;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GarageItem : CarouselValidate
    {
        public GarageItem()
        {
            InitializeComponent();
            BindingContext = App.net.GarageRecord as GarageTable;

            // Page 0
            List<string> garage_colour_list = new List<string>() { "White", "Black", "Green", "Red", "Blue", "Brown", "Mahogany" };

            colour_picker.SetPickerItems(garage_colour_list);
            subframe_material_button.set_button_list(SurveyFitterButtonLists.garage_subframe_colour_list);
            new_subframe_button.set_button_list(SurveyFitterSharedLogic.garage_subframe_list());

            obstruction_outside_text.IsVisible = SurveyFitterSharedLogic.garage_outside_obstruction_detail_visible();
            obstruction_inside_text.IsVisible = SurveyFitterSharedLogic.garage_inside_obstruction_detail_visible();

            // Page 1
            
            List<string> type_of_garage_list = new List<string>() { "Brick", "Timber", "Concrete Prefab", "Asbestos" };
            List<string> new_electric_operator_list = new List<string>() { "Yes", "No" };
            List<string> hard_wired_list = new List<string>() { "Hard Wired", "240v Plug" };

            frame_fix_type_button.set_button_list(SurveyFitterButtonLists.garage_frame_fix_type_list);
            motor_position_button.set_button_list(SurveyFitterButtonLists.garage_motor_position_list);
            type_of_garage_picker.SetPickerItems(type_of_garage_list);
            new_electric_operator_picker.SetPickerItems(new_electric_operator_list);
            hard_wired_picker.SetPickerItems(hard_wired_list);

            where_is_garage_entry.IsVisible = SurveyFitterSharedLogic.garage_where_is_garage_visible();
            hard_wired_picker.IsVisible = SurveyFitterSharedLogic.garage_hardwired_visible();
            socket_within_1m_button.IsVisible = SurveyFitterSharedLogic.garage_socket_within_1m_visible();
            set_electrician_warning_visible();
            motor_position_button.IsVisible = SurveyFitterSharedLogic.garage_motor_position_visible();
            safety_box_warning_message.IsVisible =
            safety_release_required_button.IsVisible = SurveyFitterSharedLogic.garage_safety_release_required_visible();

            // Page 4
            List<string> opening_type_list = new List<string>() { "Canopy", "Retractable", "Roller Door", "Sectional Door", "Side Hung" };
            List<string> roller_door_type_list = new List<string>() { "Thermaglide 77", "Thermaglide 55", "Steel Line" };
            List<string> roller_door_box_list = new List<string>() { "No roll box", "Full roll box", "Half roll box" };
            List<string> finish_list = new List<string>() { "Metal Painted", "Woodgrain Plastic", "Fibre Glass", "UPVC Finish" };

            opening_type_picker.SetPickerItems(opening_type_list);
            roller_door_type_picker.SetPickerItems(roller_door_type_list);
            roller_door_box_picker.SetPickerItems(roller_door_box_list);
            finish_picker.SetPickerItems(finish_list);
            opening_direction_button.set_button_list(SurveyFitterButtonLists.garage_opening_direction_list);

            roller_door_type_picker.IsVisible = SurveyFitterSharedLogic.garage_roller_door_questions_visible();
            roller_door_box_picker.IsVisible = SurveyFitterSharedLogic.garage_roll_box_visible();
            colour_match_button.IsVisible = SurveyFitterSharedLogic.garage_colour_match_visible();
            additional_drawings_area.IsVisible = SurveyFitterSharedLogic.garage_additional_drawings_visible();
            opening_direction_button.IsVisible = SurveyFitterSharedLogic.garage_opening_direction_visible();
            //SetPageNumber();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.net.GarageRecord.no_of_pics = SurveyFitterSharedLogic.count_drawings();
            App.net.GarageRecord.no_of_photos = SurveyFitterSharedLogic.count_photos();
            SetAdditionalButton();
        }

        private void set_electrician_warning_visible()
        {
            electrician_warning_message.IsVisible = socket_within_1m_button.IsVisible && App.net.GarageRecord.socket_within_1m == 2;
        }

        private void set_motor_position_information()
        {
            motor_position_message.IsVisible = App.net.GarageRecord.motor_position >= 2;
            switch (App.net.GarageRecord.motor_position)
            {
                case 2: motor_position_message.Text = "MOTOR ON LEFT HAND SIDE VIEWED INTERNALLY"; break;
                case 3: motor_position_message.Text = "MOTOR ON RIGHT HAND SIDE VIEWED INTERNALLY"; break;
                case 4: motor_position_message.Text = "MOTOR IN CENTRE VIEWED INTERNALLY"; break;
            }
        }

        private void obstruction_outside_clicked(object sender, EventArgs e)
        {
            obstruction_outside_text.IsVisible = SurveyFitterSharedLogic.garage_outside_obstruction_detail_visible();
        }

        private void obstruction_inside_clicked(object sender, EventArgs e)
        {
            obstruction_inside_text.IsVisible = SurveyFitterSharedLogic.garage_inside_obstruction_detail_visible();
        }

        private void new_electric_operator_clicked(object sender, EventArgs e)
        {
            hard_wired_picker.IsVisible = SurveyFitterSharedLogic.garage_hardwired_visible();
            socket_within_1m_button.IsVisible = SurveyFitterSharedLogic.garage_socket_within_1m_visible();
            set_electrician_warning_visible();
        }

        private void hard_wired_change(object sender, EventArgs e)
        {
            socket_within_1m_button.IsVisible = SurveyFitterSharedLogic.garage_socket_within_1m_visible();
            set_electrician_warning_visible();
        }

        private void socket_within_1m_changed(object sender, EventArgs e)
        {
            set_electrician_warning_visible();
        }

        private void electric_door_clicked(object sender, EventArgs e)
        {
            motor_position_button.IsVisible = SurveyFitterSharedLogic.garage_motor_position_visible();
            safety_box_warning_message.IsVisible =
            safety_release_required_button.IsVisible = SurveyFitterSharedLogic.garage_safety_release_required_visible();
        }

        private void other_access_clicked(object sender, EventArgs e)
        {
            safety_box_warning_message.IsVisible =
            safety_release_required_button.IsVisible = SurveyFitterSharedLogic.garage_safety_release_required_visible();
        }

        private void opening_type_clicked(object sender, EventArgs e)
        {
            roller_door_type_picker.IsVisible = SurveyFitterSharedLogic.garage_roller_door_questions_visible();
            roller_door_box_picker.IsVisible = SurveyFitterSharedLogic.garage_roll_box_visible();
            colour_match_button.IsVisible = SurveyFitterSharedLogic.garage_colour_match_visible();
            additional_drawings_area.IsVisible = SurveyFitterSharedLogic.garage_additional_drawings_visible();
            opening_direction_button.IsVisible = SurveyFitterSharedLogic.garage_opening_direction_visible();
        }

        private void roller_door_type_changed(object sender, EventArgs e)
        {
            roller_door_box_picker.IsVisible = SurveyFitterSharedLogic.garage_roll_box_visible();
            colour_match_button.IsVisible = SurveyFitterSharedLogic.garage_colour_match_visible();
            additional_drawings_area.IsVisible = SurveyFitterSharedLogic.garage_additional_drawings_visible();
        }

        private void roll_box_type_changed(object sender, EventArgs e)
        {
            colour_match_button.IsVisible = SurveyFitterSharedLogic.garage_colour_match_visible();
        }

        private void SetAdditionalButton()
        {
            if (additional_drawings_area.IsVisible)
            {
                if (App.net.GarageRecord.additional_drawn)
                    additional_drawing_button.ImageSource = "green_tick.png";
                else
                    additional_drawing_button.ImageSource = "question.png";
            }
            else
                additional_drawing_button.ImageSource = null;
        }

        private void additional_drawing1_clicked(object sender, EventArgs e)
        {
            App.net.drawing_type = "garage_roller_open";

            if (App.net.GarageRecord.additional_drawn == true)
                App.net.drawing_edit_mode = true;
            else
            {
                App.net.drawing_edit_mode = false;
                App.net.load_template_image = true;
            }

            Navigation.PushAsync(new DrawingPage(), false);
        }

        private void motor_position_clicked(object sender, EventArgs e)
        {
            set_motor_position_information();
        }

        private void subframe_material_clicked(object sender, EventArgs e)
        {
            new_subframe_button.set_button_list(SurveyFitterSharedLogic.garage_subframe_list());
            if (App.net.GarageRecord.door_fits_into == 2) // Metal
                new_subframe_button.set_button_state (1);
            else
                new_subframe_button.set_button_state(0); // Timber
        }

        protected override string validate_drawings_and_pictures()
        {
            return cause_of_damage_area.photo_validation_error_string(App.CurrentApp.GarageRecord.no_of_photos)
                 + (App.CurrentApp.GarageRecord.no_of_pics == 0 ? "Drawings\n" : "");
        }

        protected override string validate_page()
        {
            return cause_of_damage_area.validation_error_string()
                 + colour_picker.validation_error_string("Colour\n")
                 + subframe_material_button.validation_error_string("Subframe Material\n")
                 + new_subframe_button.validation_error_string("New Subframe\n")
                 + obstruction_outside_button.validation_error_string("Obstruction outside\n")
                 + (obstruction_outside_text.IsVisible && App.net.GarageRecord.obstruction_outside == "" ? "Obstruction outside text\n" : "")
                 + obstruction_inside_button.validation_error_string("Obstruction inside\n")
                 + (obstruction_inside_text.IsVisible && App.net.GarageRecord.obstruction_inside == "" ? "Obstruction inside text\n" : "")
                 + door_dimensions.validation_error_string("Actual Door Width\n", "Actual Door Height\n")
                 + frame_fix_type_button.validation_error_string("Frame fix type\n")
                 + perimiter_button.validation_error_string("Door in perimeter\n")
                 + type_of_garage_picker.validation_error_string("Type of garage\n")
                 + new_electric_operator_picker.validation_error_string("New Electric op.\n")
                 + hard_wired_picker.validation_error_string("Wire type\n")
                 + socket_within_1m_button.validation_error_string("Socket available\n")
                 + electric_door_button.validation_error_string("Electric Door\n")
                 + motor_position_button.validation_error_string("Motor position\n")
                 + other_access_button.validation_error_string("Other access\n")
                 + safety_release_required_button.validation_error_string("Need safety release\n")
                 + handle_outside_button.validation_error_string("Handle Outside\n")
                 + door_stuck_button.validation_error_string("Door stuck shut\n")
                 + side_dimension_A.validation_error_string("Size A\n")
                 + side_dimension_B.validation_error_string("Size B\n")
                 + side_dimension_C.validation_error_string("Size C\n")
                 + side_dimension_D.validation_error_string("Size D\n")
                 + side_dimension_E.validation_error_string("Size E\n")
                 + side_dimension_F.validation_error_string("Size F\n")
                 + side_dimension_G.validation_error_string("Size G\n")
                 + side_dimension_1.validation_error_string("Timber Size 1\n")
                 + side_dimension_2.validation_error_string("Timber Size 2\n")
                 + plan_dimension_A.validation_error_string("Size A\n")
                 + plan_dimension_B.validation_error_string("Size B\n")
                 + plan_dimension_C1.validation_error_string("Size C1\n")
                 + plan_dimension_C2.validation_error_string("Size C2\n")
                 + plan_dimension_D.validation_error_string("Size D\n")
                 + plan_dimension_1.validation_error_string("Timber Size 1\n")
                 + plan_dimension_2.validation_error_string("Timber Size 2\n")
                 + opening_type_picker.validation_error_string("Opening type\n")
                 + roller_door_type_picker.validation_error_string("Roller Door Type\n")
                 + roller_door_box_picker.validation_error_string("Roll Box Type\n")
                 + colour_match_button.validation_error_string("Colour match\n")
                 + (additional_drawings_area.IsVisible && !App.CurrentApp.GarageRecord.additional_drawn ? "Additional drawing\n" : "")
                 + finish_picker.validation_error_string("Finish\n")
                 + opening_direction_button.validation_error_string("Opening direction\n")
                 + power_points_button.validation_error_string("Power Points\n")
                 + insulated_button.validation_error_string("Insulated\n")
                 + summary_pto_area.validation_error_string();
        }

        protected override void save_item(bool complete)
        {
            App.data.SaveGarage(complete);
        }

        protected override bool already_signed()
        {
            return App.CurrentApp.GarageRecord.bDifferentFromOriginal;
        }

        private void door_within_changed(object sender, EventArgs e)
        {
            where_is_garage_entry.IsVisible = SurveyFitterSharedLogic.garage_where_is_garage_visible();
        }
    }
}