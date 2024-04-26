using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewGarage : ContentPage
	{
		public ViewGarage ()
		{
			InitializeComponent ();
            BindingContext = App.net.GarageRecord as GarageTable;

            subframe_material_answer.set_button_list(SurveyFitterButtonLists.garage_subframe_colour_list);
            new_subframe_answer.set_button_list(SurveyFitterSharedLogic.garage_subframe_list());
            frame_fix_type_answer.set_button_list(SurveyFitterButtonLists.garage_frame_fix_type_list);
            motor_position_answer.set_button_list(SurveyFitterButtonLists.garage_motor_position_list);
            opening_direction_answer.set_button_list(SurveyFitterButtonLists.garage_opening_direction_list);

            cause_different_answer.IsVisible = MartControls.cause_of_damage_logic.reason_different_visible(App.net.GarageRecord.cause_of_damage);
            point_of_entry_answer.IsVisible =
                was_it_locked_answer.IsVisible =
                type_of_locking_system_answer.IsVisible = MartControls.cause_of_damage_logic.point_of_entry_visible(App.net.GarageRecord.cause_of_damage);
            obstruction_outside_detail_answer.IsVisible = SurveyFitterSharedLogic.garage_outside_obstruction_detail_visible();
            obstruction_inside_detail_answer.IsVisible = SurveyFitterSharedLogic.garage_inside_obstruction_detail_visible();
            where_is_garage_answer.IsVisible = SurveyFitterSharedLogic.garage_where_is_garage_visible();
            hard_wired_answer.IsVisible = SurveyFitterSharedLogic.garage_hardwired_visible();
            socket_within_1m_answer.IsVisible = SurveyFitterSharedLogic.garage_socket_within_1m_visible();
            motor_position_answer.IsVisible = SurveyFitterSharedLogic.garage_motor_position_visible();
            safety_release_required_answer.IsVisible = SurveyFitterSharedLogic.garage_safety_release_required_visible();
            roller_door_type_answer.IsVisible = SurveyFitterSharedLogic.garage_roller_door_questions_visible();
            roller_door_box_answer.IsVisible = SurveyFitterSharedLogic.garage_roll_box_visible();
            colour_match_answer.IsVisible = SurveyFitterSharedLogic.garage_colour_match_visible();
            additional_drawings_area.IsVisible = SurveyFitterSharedLogic.garage_additional_drawings_visible();
            opening_direction_answer.IsVisible = SurveyFitterSharedLogic.garage_opening_direction_visible();

            drawings_and_photos.num_drawings = App.net.GarageRecord.no_of_pics;
            drawings_and_photos.num_photos = App.net.GarageRecord.no_of_photos;
        }

        private void additional_drawings_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewDrawing(drawing_file_types.dft_garage_roller_open), false);
        }
    }
}