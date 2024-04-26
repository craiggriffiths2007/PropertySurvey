using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewConservatory : ContentPage
	{
		public ViewConservatory ()
		{
			InitializeComponent ();
            BindingContext = App.net.ConsRecord as ConsTable;

            material_type_answer.set_button_list(SurveyFitterButtonLists.conservatory_material_type_list);

            no_repair_reason_answer.IsVisible = SurveyFitterSharedLogic.reason_not_repaired_visible(App.net.ConsRecord.bRepair);
            cause_different_answer.IsVisible = MartControls.cause_of_damage_logic.reason_different_visible(App.net.ConsRecord.cause_of_damage);
            point_of_entry_answer.IsVisible =
                was_it_locked_answer.IsVisible =
                type_of_locking_system_answer.IsVisible = MartControls.cause_of_damage_logic.point_of_entry_visible(App.net.ConsRecord.cause_of_damage);
            spars_lineup_answer.IsVisible = SurveyFitterSharedLogic.conservatory_spars_lineup_visible();
            ridge_length_answer.IsVisible = SurveyFitterSharedLogic.conservatory_ridge_length_visible();
            wall_position_answer.IsVisible = SurveyFitterSharedLogic.conservatory_wall_position_visible();
            replace_glass_answer.IsVisible = SurveyFitterSharedLogic.replace_glass_visible(App.net.ConsRecord.bRepair);
            WER_rating_answer.IsVisible = SurveyFitterSharedLogic.WER_rating_visible(App.CurrentApp.ConsRecord.bRepair, App.CurrentApp.ConsRecord.fensa);
            dimensions_image.IsVisible =
                size_A.IsVisible =
                size_B.IsVisible =
                size_C.IsVisible = SurveyFitterSharedLogic.conservatory_dimensions_page_visible();
            size_D.IsVisible =
                size_E.IsVisible =
                size_F.IsVisible =
                size_G.IsVisible =
                angle_1.IsVisible =
                angle_2.IsVisible =
                angle_3.IsVisible =
                angle_4.IsVisible = SurveyFitterSharedLogic.conservatory_dimensions_d_to_g_visible();

            if (dimensions_image.IsVisible)
                dimensions_image.Source = SurveyFitterSharedLogic.conservatory_dimension_image();

            roof_sheets_answer.IsVisible = SurveyFitterSharedLogic.conservatory_roof_sheets_visible();
            roof_sheets_units2_answer.IsVisible =
                roof_sheets_width1_answer.IsVisible =
                roof_sheets_height1_answer.IsVisible = App.net.ConsRecord.roof_sheets_quantity_1 > 0;
            roof_sheets_units3_answer.IsVisible =
                roof_sheets_width2_answer.IsVisible =
                roof_sheets_height2_answer.IsVisible = App.net.ConsRecord.roof_sheets_quantity_2 > 0;
            roof_sheets_units4_answer.IsVisible =
                roof_sheets_width3_answer.IsVisible =
                roof_sheets_height3_answer.IsVisible = App.net.ConsRecord.roof_sheets_quantity_3 > 0;
            roof_sheets_units5_answer.IsVisible =
                roof_sheets_width4_answer.IsVisible =
                roof_sheets_height4_answer.IsVisible = App.net.ConsRecord.roof_sheets_quantity_4 > 0;
            roof_sheets_units6_answer.IsVisible =
                roof_sheets_width5_answer.IsVisible =
                roof_sheets_height5_answer.IsVisible = App.net.ConsRecord.roof_sheets_quantity_5 > 0;
            roof_sheets_units7_answer.IsVisible =
                roof_sheets_width6_answer.IsVisible =
                roof_sheets_height6_answer.IsVisible = App.net.ConsRecord.roof_sheets_quantity_6 > 0;
            roof_sheets_units8_answer.IsVisible =
                roof_sheets_width7_answer.IsVisible =
                roof_sheets_height7_answer.IsVisible = App.net.ConsRecord.roof_sheets_quantity_7 > 0;
            roof_sheets_units9_answer.IsVisible =
                roof_sheets_width8_answer.IsVisible =
                roof_sheets_height8_answer.IsVisible = App.net.ConsRecord.roof_sheets_quantity_8 > 0;
            roof_sheets_units10_answer.IsVisible =
                roof_sheets_width9_answer.IsVisible =
                roof_sheets_height9_answer.IsVisible = App.net.ConsRecord.roof_sheets_quantity_9 > 0;
            roof_sheets_width10_answer.IsVisible =
                roof_sheets_height10_answer.IsVisible = App.net.ConsRecord.roof_sheets_quantity_10 > 0;
            roof_glazing_thickness_answer.IsVisible = SurveyFitterSharedLogic.conservatory_roof_glazing_thickness_visible();
            sheet_colour_answer.IsVisible = SurveyFitterSharedLogic.conservatory_sheet_colour_visible();
            flute_size_answer.IsVisible = SurveyFitterSharedLogic.conservatory_flute_size_visible();

            drawings_and_photos.num_drawings = App.net.ConsRecord.no_of_pics;
            drawings_and_photos.num_photos = App.net.ConsRecord.no_of_photos;
        }

        private void view_replace_glass_clicked(object sender, EventArgs e)
        {
            App.net.GlassRecord = App.data.GetGlassByContractConservatoryItemNo(App.CurrentApp.HeaderRecord.udi_cont, App.CurrentApp.ConsRecord.item_number);
            if (App.net.GlassRecord == null)
            {
                App.net.table_init.CreateGlass();
                App.CurrentApp.GlassRecord.item_number = App.CurrentApp.ConsRecord.item_number;
                App.CurrentApp.GlassRecord.parent_item = 4;
                App.CurrentApp.loaded_item_number = App.CurrentApp.ConsRecord.item_number;
                App.CurrentApp.root_item_number = App.CurrentApp.ConsRecord.item_number;
                App.data.SaveHeader();
                App.data.SaveGlass(false);
            }
            Navigation.PushAsync(new ViewGlass(), false);
        }

        private void view_additional_drawings_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewDrawing(drawing_file_types.dft_conservatory_roof_under), false);
        }
    }
}