using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewTimber : ContentPage
	{
		public ViewTimber ()
		{
			InitializeComponent ();
            BindingContext = App.net.TimberRecord as TimberTable;
            special_glass_answer.parent_item = t_current_item.item_timber;

            temporary_answer.set_button_list(SurveyFitterButtonLists.shared_temporary_button_list);
            glazed_answer.set_button_list(SurveyFitterButtonLists.timber_glazed_button_list);
            opens_answer.set_button_list(SurveyFitterButtonLists.shared_in_out_button_list);
            single_double_answer.set_button_list(SurveyFitterButtonLists.shared_single_or_double_list);
            door_slides_answer.set_button_list(SurveyFitterButtonLists.shared_inside_outside_button_list);

            flat_answer.IsVisible = SurveyFitterSharedLogic.timber_flat_question_visible();
            replacement_reason_answer.IsVisible = SurveyFitterSharedLogic.reason_not_repaired_visible(App.net.TimberRecord.bRepair);
            cosmetic_damage_answer.IsVisible =
                gaskets_answer.IsVisible = SurveyFitterSharedLogic.gaskets_visible(App.net.TimberRecord.bRepair);
            temporary_answer.IsVisible = SurveyFitterSharedLogic.temporary_visible(App.net.TimberRecord.collect_and_copy);
            preglazed_door_answer.IsVisible = weather_bar_answer.IsVisible =
                door_thickness_answer.IsVisible =
                door_size_answer.IsVisible =
                door_width_answer.IsVisible =
                door_height_answer.IsVisible =
                glazed_answer.IsVisible = SurveyFitterSharedLogic.timber_is_a_door();
            additional_locks_answer.IsVisible = SurveyFitterSharedLogic.timber_additional_locks_visible();
            new_locking_mechanism_answer.IsVisible = SurveyFitterSharedLogic.timber_new_lock_visible();
            new_make_answer.IsVisible =
                lock_code_answer.IsVisible = SurveyFitterSharedLogic.timber_new_lock_make_visible();
            handles_required_answer.IsVisible = SurveyFitterSharedLogic.handles_visible(App.net.TimberRecord.bRepair);
            handles_detail_answer.IsVisible = SurveyFitterSharedLogic.handles_text_visible(App.net.TimberRecord.bRepair, App.net.TimberRecord.handles_req);
            replace_glass_answer.IsVisible = SurveyFitterSharedLogic.replace_glass_visible(App.net.TimberRecord.bRepair);
            wer_rating_answer.IsVisible = SurveyFitterSharedLogic.WER_rating_visible(App.net.TimberRecord.bRepair, App.net.TimberRecord.Fensa);
            reason_not_repaired_answer.IsVisible = SurveyFitterSharedLogic.reason_not_repaired_visible(App.net.TimberRecord.bRepair);
            gasket_detail_answer.IsVisible = SurveyFitterSharedLogic.gaskets_text_visible(App.net.TimberRecord.bRepair, App.net.TimberRecord.gaskets);
            cause_different_answer.IsVisible = MartControls.cause_of_damage_logic.reason_different_visible(App.net.TimberRecord.cause_of_damage);
            point_of_entry_answer.IsVisible =
                was_it_locked_answer.IsVisible =
                type_of_locking_system_answer.IsVisible = MartControls.cause_of_damage_logic.point_of_entry_visible(App.net.TimberRecord.cause_of_damage);
            hinge_type_answer.IsVisible = SurveyFitterSharedLogic.timber_hinges_visible();
            reason_not_docL_answer.IsVisible = SurveyFitterSharedLogic.timber_docL_reason_visible();
            letter_box_answer.IsVisible =
                pet_flap_size_answer.IsVisible = SurveyFitterSharedLogic.timber_letterbox_and_petflap_visible();
            letter_box_position_answer.IsVisible = letter_box_answer.IsVisible && MartControls.letter_box_logic.letter_box_position_visible(App.net.TimberRecord.letter_box);
            pet_flap_type_answer.IsVisible =
                pet_flap_magnetic_answer.IsVisible = pet_flap_size_answer.IsVisible && MartControls.pet_flap_logic.pet_flap_detail_visible(App.net.TimberRecord.pet_flap);
            door_size_reason_non_standard_answer.IsVisible = SurveyFitterSharedLogic.timber_reason_door_size_not_standard_visible();
            thresher_answer.IsVisible = SurveyFitterSharedLogic.timber_is_a_door();
            single_double_answer.IsVisible = SurveyFitterSharedLogic.timber_single_or_double_visible();
            door_slides_answer.IsVisible = SurveyFitterSharedLogic.timber_door_slides_on_visible();
            spacer_thickness_answer.IsVisible =
                spacer_colour_answer.IsVisible =
                document_L_answer.IsVisible = SurveyFitterSharedLogic.timber_spacer_visible();
            fire_rated_answer.IsVisible = SurveyFitterSharedLogic.timber_fire_rated_visible();
            inside_door_colour_answer.IsVisible =
                outside_door_colour_answer.IsVisible = SurveyFitterSharedLogic.timber_door_colours_visible();
            inside_door_code_answer.IsVisible = SurveyFitterSharedLogic.timber_door_colour_code_visible(App.net.TimberRecord.door_color, inside_door_colour_answer.IsVisible);
            outside_door_code_answer.IsVisible = SurveyFitterSharedLogic.timber_door_colour_code_visible(App.net.TimberRecord.door_color_out, outside_door_colour_answer.IsVisible);
            inside_frame_code_answer.IsVisible = SurveyFitterSharedLogic.timber_door_colour_code_visible(App.net.TimberRecord.frame_color, true);
            SurveyFitterSharedLogic.timber_door_colour_code_visible(App.net.TimberRecord.frame_color_out, true);
            glass_type_answer.IsVisible =
                special_glass_answer.IsVisible = SurveyFitterSharedLogic.timber_glass_type_visible();
            view_section_button.IsVisible = App.net.TimberRecord.bSectionDrawn;
            view_beading_button.IsVisible = App.net.TimberRecord.bMouldingDrawn;
            view_sash_area.IsVisible = App.net.TimberRecord.new_sash_required == 1;
            view_sash_button.IsVisible = App.net.TimberRecord.bSashDrawn;

            drawings_and_photos.num_drawings = App.net.TimberRecord.no_of_pics;
            drawings_and_photos.num_photos = App.net.TimberRecord.no_of_photos;
            section_label.Text = "Section : " + SurveyFitterSharedLogic.boolean_as_yesno(App.net.TimberRecord.bSectionDrawn);
            beading_label.Text = "Beading : " + SurveyFitterSharedLogic.boolean_as_yesno(App.net.TimberRecord.bMouldingDrawn);
            sash_label.Text = "Sash : " + SurveyFitterSharedLogic.boolean_as_yesno(App.net.TimberRecord.bSashDrawn);
        }

        private void view_locking_mechanism_clicked(object sender, EventArgs e)
        {
            switch (App.net.TimberRecord.timber_item)
            {
                case "Door":
                case "French Doors": Navigation.PushAsync(new ViewLock(view_lock_type.vlt_door), false); break;
                case "Window": Navigation.PushAsync(new ViewLock(view_lock_type.vlt_window), false); break;
            }
        }

        private void view_handles_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewDrawing(drawing_file_types.dft_handle), false);
        }

        private void view_replace_glass_clicked(object sender, EventArgs e)
        {
            App.net.GlassRecord = App.data.GetGlassByContractTimberItemNo(App.CurrentApp.HeaderRecord.udi_cont, App.CurrentApp.TimberRecord.item_number);
            if (App.net.GlassRecord == null)
            {
                App.net.table_init.CreateGlass();
                App.CurrentApp.GlassRecord.item_number = App.CurrentApp.TimberRecord.item_number;
                App.CurrentApp.GlassRecord.parent_item = 6;
                App.CurrentApp.loaded_item_number = App.CurrentApp.TimberRecord.item_number;
                App.CurrentApp.root_item_number = App.CurrentApp.TimberRecord.item_number;
                App.data.SaveHeader();
                App.data.SaveGlass(false);
            }
            Navigation.PushAsync(new ViewGlass(), false);
        }

        private void view_section_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PropertySurvey.ViewDrawing(PropertySurvey.drawing_file_types.dft_timber_section), false);
        }

        private void view_beading_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PropertySurvey.ViewDrawing(PropertySurvey.drawing_file_types.dft_timber_beading), false);
        }

        private void view_sash_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PropertySurvey.ViewDrawing(PropertySurvey.drawing_file_types.dft_timber_sash), false);
        }
    }
}