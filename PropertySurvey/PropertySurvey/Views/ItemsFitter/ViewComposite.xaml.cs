using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewComposite : ContentPage
	{
		public ViewComposite ()
		{
			InitializeComponent ();
            BindingContext = App.net.CompRecord as CompositeTable;
            special_glass_answer.parent_item = t_current_item.item_composite;

            gaskets_answer.set_button_list(SurveyFitterButtonLists.shared_gasket_button_list);
            is_lock_answer.set_button_list(SurveyFitterButtonLists.composite_is_lock_button_list);
            opens_answer.set_button_list(SurveyFitterButtonLists.shared_in_out_button_list);
            hinged_on_answer.set_button_list(SurveyFitterButtonLists.composite_hinged_on_button_list);
            pet_flap_magnetic_answer.set_button_list(MartControls.pet_flap_logic.magnetic_list);
            handle_operation_answer.set_button_list(SurveyFitterButtonLists.shared_handle_operation_list);
            profile_type_answer.set_button_list(SurveyFitterButtonLists.shared_profile_type_button_list);
            glaze_answer.set_button_list(SurveyFitterButtonLists.shared_internal_external_button_list);

            no_repair_reason_answer.IsVisible = SurveyFitterSharedLogic.reason_not_repaired_visible(App.net.CompRecord.bRepair);
            replace_glass_answer.IsVisible = SurveyFitterSharedLogic.replace_glass_visible(App.net.CompRecord.bRepair);
            handles_required_answer.IsVisible = SurveyFitterSharedLogic.handles_visible(App.net.CompRecord.bRepair);
            handles_text_answer.IsVisible = SurveyFitterSharedLogic.handles_text_visible(App.CurrentApp.CompRecord.bRepair, App.CurrentApp.CompRecord.handles_req);
            gaskets_answer.IsVisible = SurveyFitterSharedLogic.gaskets_visible(App.net.CompRecord.bRepair);
            gasket_detail_answer.IsVisible = SurveyFitterSharedLogic.gaskets_text_visible(App.net.CompRecord.bRepair, App.net.CompRecord.gaskets);
            cause_different_answer.IsVisible = MartControls.cause_of_damage_logic.reason_different_visible(App.net.CompRecord.cause_of_damage);
            point_of_entry_answer.IsVisible =
                was_it_locked_answer.IsVisible =
                type_of_locking_system_answer.IsVisible = MartControls.cause_of_damage_logic.point_of_entry_visible(App.net.CompRecord.cause_of_damage);
            WER_rating_answer.IsVisible = SurveyFitterSharedLogic.WER_rating_visible(App.CurrentApp.CompRecord.bRepair, App.CurrentApp.CompRecord.fensa);
            lock_other_text.IsVisible = SurveyFitterSharedLogic.composite_other_lock_text_visible();
            letter_box_position_answer.IsVisible = MartControls.letter_box_logic.letter_box_position_visible(App.net.CompRecord.letteredit);
            pet_flap_type_answer.IsVisible =
                pet_flap_magnetic_answer.IsVisible = MartControls.pet_flap_logic.pet_flap_detail_visible(App.net.CompRecord.pet_flap);
            addons_width_answer.IsVisible =
                addons_height_answer.IsVisible = SurveyFitterSharedLogic.composite_addon_dimensions_visible();
            special_glass_answer.IsVisible =
                glass_type_answer.IsVisible =
                spacer_thickness_answer.IsVisible =
                spacer_colour_answer.IsVisible =
                document_L_answer.IsVisible = SurveyFitterSharedLogic.composite_glass_questions_visible();

            drawings_and_photos.num_drawings = App.net.CompRecord.no_of_pics;
            drawings_and_photos.num_photos = App.net.CompRecord.no_of_photos;
        }

        private void view_replace_glass_clicked(object sender, EventArgs e)
        {
            App.net.GlassRecord = App.data.GetGlassByContractCompositeItemNo(App.CurrentApp.HeaderRecord.udi_cont, App.CurrentApp.CompRecord.item_number);
            if (App.net.GlassRecord == null)
            {
                App.net.table_init.CreateGlass();
                App.CurrentApp.GlassRecord.item_number = App.CurrentApp.CompRecord.item_number;
                App.CurrentApp.GlassRecord.parent_item = 3;
                App.CurrentApp.loaded_item_number = App.CurrentApp.CompRecord.item_number;
                App.CurrentApp.root_item_number = App.CurrentApp.CompRecord.item_number;
                App.data.SaveHeader();
                App.data.SaveGlass(false);
            }
            Navigation.PushAsync(new ViewGlass(), false);
        }

        private void view_handles_required_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewDrawing(drawing_file_types.dft_handle), false);
        }
    }
}