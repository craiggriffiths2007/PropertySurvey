using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewPanel : ContentPage
	{
		public ViewPanel ()
		{
			InitializeComponent ();
            BindingContext = App.net.PanelRecord as PanelTable;

            pet_flap_magnetic_answer.set_button_list(MartControls.pet_flap_logic.magnetic_list);

            cause_of_damage_answer.IsVisible =
                room_location_answer.IsVisible =
                item_summary_answer.IsVisible =
                parts_to_order_answer.IsVisible = SurveyFitterSharedLogic.panel_only_visible();
            cause_different_answer.IsVisible = SurveyFitterSharedLogic.panel_only_visible()
                                            && MartControls.cause_of_damage_logic.reason_different_visible(App.net.PanelRecord.cause_of_damage);
            point_of_entry_answer.IsVisible =
                was_it_locked_answer.IsVisible =
                type_of_locking_system_answer.IsVisible = SurveyFitterSharedLogic.panel_only_visible()
                                                       && MartControls.cause_of_damage_logic.point_of_entry_visible(App.net.PanelRecord.cause_of_damage);
            knocker_colour_answer.IsVisible = SurveyFitterSharedLogic.panel_knocker_colour_visible();
            letter_box_position_answer.IsVisible = MartControls.letter_box_logic.letter_box_position_visible(App.net.PanelRecord.letteredit);
            pet_flap_type_answer.IsVisible =
                pet_flap_magnetic_answer.IsVisible = MartControls.pet_flap_logic.pet_flap_detail_visible(App.net.PanelRecord.pet_flap);
            glass_design_answer.IsVisible =
                spacer_colour_answer.IsVisible = SurveyFitterSharedLogic.panel_glass_and_spacer_visible();

            drawings_and_photos.num_drawings = App.net.PanelRecord.no_of_pics;
            drawings_and_photos.num_photos = App.net.PanelRecord.no_of_photos;
        }
    }
}