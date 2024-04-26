using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewAluminium : ContentPage
	{
		public ViewAluminium ()
		{
			InitializeComponent ();
            BindingContext = App.net.AlumRecord as AlumTable;
            special_glass_answer.parent_item = t_current_item.item_aluminium;

            gaskets_button_answer.set_button_list(SurveyFitterButtonLists.shared_gasket_button_list);
            temporary_answer.set_button_list(SurveyFitterButtonLists.shared_temporary_button_list);
            section_type_answer.set_button_list(SurveyFitterButtonLists.aluminium_section_type_list);
            cill_type_answer.set_button_list(SurveyFitterButtonLists.aluminium_cill_type_list);
            frame_type_answer.set_button_list(SurveyFitterButtonLists.aluminium_frame_type_list);
            night_vent_answer.set_button_list(SurveyFitterButtonLists.aluminium_night_vent_list);
            glazed_answer.set_button_list(SurveyFitterButtonLists.aluminium_glazed_button_list);
            bead_type_answer.set_button_list(SurveyFitterButtonLists.aluminium_bead_type_list);
            pet_flap_magnetic_answer.set_button_list (MartControls.pet_flap_logic.magnetic_list);
            opens_answer.set_button_list(SurveyFitterButtonLists.shared_in_out_button_list);
            handle_operation_answer.set_button_list(SurveyFitterButtonLists.shared_handle_operation_list);

            cill_answer.LabelText = SurveyFitterSharedLogic.aluminium_frame_type_label();

            flat_answer.IsVisible = SurveyFitterSharedLogic.aluminium_flat_visible();
            additional_locks_answer.IsVisible = SurveyFitterSharedLogic.aluminium_additional_locks_visible();
            new_lock_answer.IsVisible = SurveyFitterSharedLogic.aluminium_new_lock_visible();
            lock_make_answer.IsVisible =
                lock_code_answer.IsVisible = SurveyFitterSharedLogic.aluminium_new_lock_make_visible();
            handles_required_answer.IsVisible = SurveyFitterSharedLogic.handles_visible(App.net.AlumRecord.bRepair);
            handles_text_answer.IsVisible = SurveyFitterSharedLogic.handles_text_visible(App.net.AlumRecord.bRepair, App.net.AlumRecord.handles_req);
            replace_panel_answer.IsVisible = SurveyFitterSharedLogic.aluminium_replace_panel_visible();
            replace_glass_answer.IsVisible = SurveyFitterSharedLogic.replace_glass_visible(App.net.AlumRecord.bRepair);
            cosmetic_damage_answer.IsVisible =
                gaskets_button_answer.IsVisible = SurveyFitterSharedLogic.gaskets_visible(App.net.AlumRecord.bRepair);
            gasket_text_answer.IsVisible = SurveyFitterSharedLogic.gaskets_text_visible(App.net.AlumRecord.bRepair, App.CurrentApp.AlumRecord.gaskets);
            reason_for_replacement_answer.IsVisible = SurveyFitterSharedLogic.aluminium_reason_for_replacement_visible();
            WER_rating_answer.IsVisible = SurveyFitterSharedLogic.WER_rating_visible(App.net.AlumRecord.bRepair, App.CurrentApp.AlumRecord.bFencer);
            why_not_repaired_answer.IsVisible = SurveyFitterSharedLogic.aluminium_why_not_repaired_visible();
            cause_different_answer.IsVisible = MartControls.cause_of_damage_logic.reason_different_visible(App.net.AlumRecord.cause_of_damage);
            point_of_entry_answer.IsVisible =
                was_it_locked_answer.IsVisible =
                type_of_locking_system_answer.IsVisible = MartControls.cause_of_damage_logic.point_of_entry_visible(App.net.AlumRecord.cause_of_damage);
            temporary_answer.IsVisible = SurveyFitterSharedLogic.temporary_visible(App.net.AlumRecord.collect_and_copy);
            new_subframe_required_answer.IsVisible = SurveyFitterSharedLogic.aluminium_new_sub_frame_visible();
            subframe_depth_answer.IsVisible =
                item_frame_width_answer.IsVisible =
                item_frame_depth_answer.IsVisible = SurveyFitterSharedLogic.aluminium_subframe_details_visible();
            frame_type_answer.IsVisible = SurveyFitterSharedLogic.aluminium_frame_type_visible();
            night_vent_answer.set_button_list(SurveyFitterButtonLists.aluminium_night_vent_list);
            glazed_answer.set_button_list(SurveyFitterButtonLists.aluminium_glazed_button_list);
            bead_type_answer.IsVisible = SurveyFitterSharedLogic.aluminium_bead_type_visible();
            letter_box_answer.IsVisible =
                pet_flap_size_answer.IsVisible = SurveyFitterSharedLogic.aluminium_letterbox_and_petflap_visible();
            letter_box_position_answer.IsVisible = letter_box_answer.IsVisible && MartControls.letter_box_logic.letter_box_position_visible(App.net.AlumRecord.letter_box);
            pet_flap_type_answer.IsVisible =
                pet_flap_magnetic_answer.IsVisible = pet_flap_size_answer.IsVisible && MartControls.pet_flap_logic.pet_flap_detail_visible(App.net.AlumRecord.pet_flap);
            opens_answer.IsVisible = SurveyFitterSharedLogic.aluminium_opens_visible();
            handle_operation_answer.IsVisible = SurveyFitterSharedLogic.aluminium_handle_operation_visible();
            glass_type_answer.IsVisible =
                special_glass_answer.IsVisible = SurveyFitterSharedLogic.aluminium_glass_questions_visible();
            subframe_colour_answer.IsVisible = SurveyFitterSharedLogic.aluminium_sub_frame_visible();
            doc_L_answer.IsVisible = SurveyFitterSharedLogic.aluminium_doc_L_visible();
            spacer_thicknes_answer.IsVisible =
                spacer_colour_answer.IsVisible = SurveyFitterSharedLogic.aluminium_spacer_questions_visible();

            drawings_and_photos.num_drawings = App.net.AlumRecord.no_of_pics;
            drawings_and_photos.num_photos   = App.net.AlumRecord.no_of_photos;
        }

        private void view_new_lock_clicked(object sender, EventArgs e)
        {
            switch (App.net.AlumRecord.type)
            {
                case "Door": Navigation.PushAsync(new ViewLock(view_lock_type.vlt_door), false); break;
                case "Window": Navigation.PushAsync(new ViewLock(view_lock_type.vlt_window), false); break;
            }
        }

        private void view_handles_required_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewDrawing(drawing_file_types.dft_handle), false);
        }

        private void view_replace_panel_clicked(object sender, EventArgs e)
        {
            App.net.PanelRecord = App.data.GetPanelByContractAlumItemNo(App.CurrentApp.HeaderRecord.udi_cont, App.CurrentApp.AlumRecord.item_number);

            if (App.net.PanelRecord == null)
            {
                App.net.table_init.CreatePanel();
                App.CurrentApp.PanelRecord.alum_item_number = App.CurrentApp.AlumRecord.item_number;
                App.CurrentApp.loaded_item_number = App.CurrentApp.AlumRecord.item_number;
                App.CurrentApp.root_item_number = App.CurrentApp.AlumRecord.item_number;
                App.data.SaveHeader();
                App.data.SavePanel();
            }

            Navigation.PushAsync(new ViewPanel(), false);
        }

        private void view_replace_glass_clicked(object sender, EventArgs e)
        {
            App.net.GlassRecord = App.data.GetGlassByContractAlumItemNo(App.CurrentApp.HeaderRecord.udi_cont, App.CurrentApp.AlumRecord.item_number);
            if (App.net.GlassRecord == null)
            {
                App.net.table_init.CreateGlass();
                App.CurrentApp.GlassRecord.item_number = App.CurrentApp.AlumRecord.item_number;
                App.CurrentApp.GlassRecord.parent_item = 1;
                App.CurrentApp.loaded_item_number = App.CurrentApp.AlumRecord.item_number;
                App.CurrentApp.root_item_number = App.CurrentApp.AlumRecord.item_number;
                App.data.SaveHeader();
                App.data.SaveGlass(false);
            }
            Navigation.PushAsync(new ViewGlass(), false);
        }
    }
}