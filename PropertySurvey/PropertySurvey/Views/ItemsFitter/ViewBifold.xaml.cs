using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewBifold : ContentPage
	{
		public ViewBifold ()
		{
			InitializeComponent ();
            BindingContext = App.net.BifoldRecord as BifoldTable;

            gaskets_answer.set_button_list(SurveyFitterButtonLists.shared_gasket_button_list);
            opens_answer.set_button_list(SurveyFitterButtonLists.shared_in_out_button_list);

            reason_not_repaired_answer.IsVisible = SurveyFitterSharedLogic.reason_not_repaired_visible(App.net.BifoldRecord.bRepair);
            replace_glass_answer.IsVisible = SurveyFitterSharedLogic.replace_glass_visible(App.net.BifoldRecord.bRepair);
            handles_required_answer.IsVisible = SurveyFitterSharedLogic.handles_visible(App.net.BifoldRecord.bRepair);
            handles_text_answer.IsVisible = SurveyFitterSharedLogic.handles_text_visible(App.CurrentApp.BifoldRecord.bRepair, App.CurrentApp.BifoldRecord.handles_req);
            gaskets_answer.IsVisible = SurveyFitterSharedLogic.gaskets_visible(App.net.BifoldRecord.bRepair);
            gasket_detail_answer.IsVisible = SurveyFitterSharedLogic.gaskets_text_visible(App.net.BifoldRecord.bRepair, App.net.BifoldRecord.gaskets);
            cause_different_answer.IsVisible = MartControls.cause_of_damage_logic.reason_different_visible(App.net.BifoldRecord.cause_of_damage);
            point_of_entry_answer.IsVisible =
                was_it_locked_answer.IsVisible =
                type_of_locking_system_answer.IsVisible = MartControls.cause_of_damage_logic.point_of_entry_visible(App.net.BifoldRecord.cause_of_damage);
            addon_width_answer.IsVisible =
                addon_height_answer.IsVisible = SurveyFitterSharedLogic.addon_dimensions_visible (App.CurrentApp.BifoldRecord.addons);
            WER_rating_answer.IsVisible = SurveyFitterSharedLogic.WER_rating_visible(App.CurrentApp.BifoldRecord.bRepair, App.CurrentApp.BifoldRecord.fensa);
            hardware_answer.IsVisible =
                external_frame_colour_answer.IsVisible =
                internal_frame_colour_answer.IsVisible = SurveyFitterSharedLogic.bifold_hardware_visible();
            handle_colour_answer.IsVisible =
                door_colour_answer.IsVisible =
                internal_door_colour_answer.IsVisible = SurveyFitterSharedLogic.bifold_handle_colour_visible();
            glazing_options_answer.IsVisible = SurveyFitterSharedLogic.glazing_options_visible();
            knock_on_answer.IsVisible = SurveyFitterSharedLogic.knock_on_visible();

            SurveyFitterSharedLogic.bifold_set_cill_image(cill_image);
            SurveyFitterSharedLogic.bifold_set_door_text_and_image(no_of_doors_text, type_image);

            drawings_and_photos.num_drawings = App.net.BifoldRecord.no_of_pics;
            drawings_and_photos.num_photos = App.net.BifoldRecord.no_of_photos;
        }

        private void view_replace_glass_clicked(object sender, EventArgs e)
        {
            App.net.GlassRecord = App.data.GetGlassByContractBifoldItemNo(App.CurrentApp.HeaderRecord.udi_cont, App.CurrentApp.BifoldRecord.item_number);
            if (App.net.GlassRecord == null)
            {
                App.net.table_init.CreateGlass();
                App.CurrentApp.GlassRecord.item_number = App.CurrentApp.BifoldRecord.item_number;
                App.CurrentApp.GlassRecord.parent_item = 2;
                App.CurrentApp.loaded_item_number = App.CurrentApp.BifoldRecord.item_number;
                App.CurrentApp.root_item_number = App.CurrentApp.BifoldRecord.item_number;
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