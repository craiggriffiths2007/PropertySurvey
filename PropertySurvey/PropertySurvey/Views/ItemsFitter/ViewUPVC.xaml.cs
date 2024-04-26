using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewUPVC : ContentPage
	{
		public ViewUPVC ()
		{
			InitializeComponent ();
            BindingContext = App.net.UPVCRecord as UPVCTable;
            special_glass_answer.parent_item = t_current_item.item_upvc;

            temporary_answer.set_button_list(SurveyFitterButtonLists.shared_temporary_button_list);
            gaskets_answer.set_button_list(SurveyFitterButtonLists.shared_gasket_button_list);
            handle_operation_answer.set_button_list(SurveyFitterButtonLists.shared_handle_operation_list);
            pet_flap_magnetic_answer.set_button_list(MartControls.pet_flap_logic.magnetic_list);
            double_or_triple_answer.set_button_list(SurveyFitterButtonLists.upvc_double_or_triple_list);
            opens_answer.set_button_list(SurveyFitterButtonLists.upvc_opens_list);
            glaze_answer.set_button_list(SurveyFitterButtonLists.shared_internal_external_button_list);
            trickle_vents_answer.set_button_list(SurveyFitterButtonLists.upvc_trickle_vents_button_list);
            lock_type_answer.set_button_list(SurveyFitterButtonLists.upvc_lock_type_button_list);
            slide_position_answer.set_button_list(SurveyFitterButtonLists.shared_inside_outside_button_list);
            profile_type_answer.set_button_list(SurveyFitterButtonLists.shared_profile_type_button_list);

            flat_answer.IsVisible = SurveyFitterSharedLogic.upvc_collect_and_copy_visible();
            temporary_answer.IsVisible = SurveyFitterSharedLogic.temporary_visible(App.net.UPVCRecord.collect_and_copy);
            additional_locks_answer.IsVisible = SurveyFitterSharedLogic.upvc_additional_locks_visible();
            new_lock_answer.IsVisible = SurveyFitterSharedLogic.upvc_new_locking_mechanism_visible();
            lock_make_answer.IsVisible =
                lock_codes_answer.IsVisible = SurveyFitterSharedLogic.upvc_new_lock_make_visible();
            handles_required_answer.IsVisible = SurveyFitterSharedLogic.handles_visible(App.CurrentApp.UPVCRecord.bRepair);
            handles_text_answer.IsVisible = SurveyFitterSharedLogic.handles_text_visible(App.CurrentApp.UPVCRecord.bRepair, App.CurrentApp.UPVCRecord.handles_req);
            replace_panel_answer.IsVisible = SurveyFitterSharedLogic.upvc_replace_panel_visible();
            replace_glass_answer.IsVisible = SurveyFitterSharedLogic.replace_glass_visible(App.CurrentApp.UPVCRecord.bRepair);
            cause_different_answer.IsVisible = MartControls.cause_of_damage_logic.reason_different_visible(App.net.UPVCRecord.cause_of_damage);
            point_of_entry_answer.IsVisible =
                was_it_locked_answer.IsVisible =
                type_of_locking_system_answer.IsVisible = MartControls.cause_of_damage_logic.point_of_entry_visible(App.net.UPVCRecord.cause_of_damage);
            cosmetic_damage_answer.IsVisible = SurveyFitterSharedLogic.upvc_cosmetic_damage_visible();
            gaskets_answer.IsVisible = SurveyFitterSharedLogic.gaskets_visible(App.CurrentApp.UPVCRecord.bRepair);
            gasket_detail_answer.IsVisible = SurveyFitterSharedLogic.gaskets_text_visible(App.CurrentApp.UPVCRecord.bRepair, App.net.UPVCRecord.gaskets);
            hinge_colour_answer.IsVisible = SurveyFitterSharedLogic.upvc_hinge_colour_visible();
            WER_rating_answer.IsVisible = SurveyFitterSharedLogic.WER_rating_visible(App.CurrentApp.UPVCRecord.bRepair, App.CurrentApp.UPVCRecord.fensa);
            replacement_reason_answer.IsVisible =
                replace_explain_answer.IsVisible = SurveyFitterSharedLogic.reason_not_repaired_visible(App.CurrentApp.UPVCRecord.bRepair);
            handle_operation_answer.IsVisible = SurveyFitterSharedLogic.upvc_handle_operation_visible();
            addon_width_answer.IsVisible =
                addon_height_answer.IsVisible = SurveyFitterSharedLogic.addon_dimensions_visible (App.CurrentApp.UPVCRecord.addons);
            midrail_answer.IsVisible = SurveyFitterSharedLogic.upvc_midrail_visible();
            midrail_height_answer.IsVisible = SurveyFitterSharedLogic.upvc_midrail_height_visible();
            locking_type_answer.IsVisible = SurveyFitterSharedLogic.upvc_locking_type_visible();
            letter_box_answer.IsVisible =
                pet_flap_size_answer.IsVisible = SurveyFitterSharedLogic.upvc_letterbox_and_petflap_visible();
            letter_box_position_answer.IsVisible = letter_box_answer.IsVisible && MartControls.letter_box_logic.letter_box_position_visible(App.net.UPVCRecord.letter_box);
            pet_flap_type_answer.IsVisible =
                pet_flap_magnetic_answer.IsVisible = pet_flap_size_answer.IsVisible && MartControls.pet_flap_logic.pet_flap_detail_visible(App.net.UPVCRecord.pet_flap);
            double_or_triple_answer.IsVisible =
                glass_type_answer.IsVisible =
                special_glass_answer.IsVisible =
                document_L_answer.IsVisible = SurveyFitterSharedLogic.upvc_glass_questions_visible();
            threshold_type_answer.IsVisible = SurveyFitterSharedLogic.upvc_threshold_visible();
            spacer_thickness_answer.IsVisible =
                spacer_colour_answer.IsVisible = SurveyFitterSharedLogic.upvc_spacer_questions_visible();
            lock_type_answer.IsVisible = SurveyFitterSharedLogic.upvc_patio_lock_type_visible();
            slide_position_answer.IsVisible = SurveyFitterSharedLogic.upvc_slide_position_visible();

            drawings_and_photos.num_drawings = App.net.UPVCRecord.no_of_pics;
            drawings_and_photos.num_photos = App.net.UPVCRecord.no_of_photos;
        }

        private void view_new_lock_clicked(object sender, EventArgs e)
        {
            switch (App.net.UPVCRecord.upvc_item)
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
            App.net.PanelRecord = App.data.GetPanelByContractUPVCItemNo(App.CurrentApp.HeaderRecord.udi_cont, App.CurrentApp.UPVCRecord.item_number);

            if (App.net.PanelRecord == null)
            {
                App.net.table_init.CreatePanel();
                App.CurrentApp.PanelRecord.upvc_item_number = App.CurrentApp.UPVCRecord.item_number;
                App.CurrentApp.loaded_item_number = App.CurrentApp.UPVCRecord.item_number;
                App.CurrentApp.root_item_number = App.CurrentApp.UPVCRecord.item_number;
                App.data.SaveHeader();
                App.data.SavePanel();
            }

            Navigation.PushAsync(new ViewPanel(), false);
        }

        private void view_replace_glass_clicked(object sender, EventArgs e)
        {
            App.net.GlassRecord = App.data.GetGlassByContractUPVCItemNo(App.CurrentApp.HeaderRecord.udi_cont, App.CurrentApp.UPVCRecord.item_number);
            if (App.net.GlassRecord == null)
            {
                App.net.table_init.CreateGlass();
                App.CurrentApp.GlassRecord.item_number = App.CurrentApp.UPVCRecord.item_number;
                App.CurrentApp.GlassRecord.parent_item = 7;
                App.CurrentApp.loaded_item_number = App.CurrentApp.UPVCRecord.item_number;
                App.CurrentApp.root_item_number = App.CurrentApp.UPVCRecord.item_number;
                App.data.SaveHeader();
                App.data.SaveGlass(false);
            }

            Navigation.PushAsync(new ViewGlass(), false);
        }
    }
}