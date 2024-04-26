using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewGlass : ContentPage
	{
		public ViewGlass ()
		{
			InitializeComponent ();
            BindingContext = App.net.GlassRecord as GlassTable;
            special_glass_answer.parent_item = t_current_item.item_glass;

            temporary_answer.set_button_list(SurveyFitterButtonLists.shared_temporary_button_list);
            glaze_answer.set_button_list(SurveyFitterButtonLists.glass_glaze_button_list);
            single_or_double_answer.set_button_list(SurveyFitterButtonLists.shared_single_or_double_list);

            cause_of_damage_answer.IsVisible =
                collect_and_copy_answer.IsVisible =
                item_summary_answer.IsVisible =
                parts_to_order_answer.IsVisible =
                room_location_answer.IsVisible = SurveyFitterSharedLogic.glass_only_items_visible();
            cause_different_answer.IsVisible = MartControls.cause_of_damage_logic.reason_different_visible(App.net.GlassRecord.cause_of_damage);
            point_of_entry_answer.IsVisible =
                was_it_locked_answer.IsVisible =
                type_of_locking_system_answer.IsVisible = SurveyFitterSharedLogic.glass_only_items_visible()
                                                       && MartControls.cause_of_damage_logic.point_of_entry_visible(App.net.GlassRecord.cause_of_damage);
            temporary_answer.IsVisible = SurveyFitterSharedLogic.glass_only_items_visible()
                                      && SurveyFitterSharedLogic.temporary_visible(App.net.GlassRecord.collect_and_copy);
            glass_width_2.IsVisible =
                glass_height_2.IsVisible = (App.net.GlassRecord.units_required >= 2);
            glass_width_3.IsVisible =
                glass_height_3.IsVisible = (App.net.GlassRecord.units_required >= 3);
            glass_width_4.IsVisible =
                glass_height_4.IsVisible = (App.net.GlassRecord.units_required >= 4);
            glass_width_5.IsVisible =
                glass_height_5.IsVisible = (App.net.GlassRecord.units_required >= 5);
            glass_width_6.IsVisible =
                glass_height_6.IsVisible = (App.net.GlassRecord.units_required >= 6);
            glass_width_7.IsVisible =
                glass_height_7.IsVisible = (App.net.GlassRecord.units_required >= 7);
            glass_width_8.IsVisible =
                glass_height_8.IsVisible = (App.net.GlassRecord.units_required >= 8);
            stepped_unit_answer.IsVisible = SurveyFitterSharedLogic.glass_stepped_unit_visible();
            internal_width_answer.IsVisible =
                internal_height_answer.IsVisible = SurveyFitterSharedLogic.glass_internal_dimensions_visible();
            glazing_type_answer.IsVisible = SurveyFitterSharedLogic.glass_glazing_type_visible();
            seal_answer.IsVisible = SurveyFitterSharedLogic.glass_seal_visible();
            document_L_answer.IsVisible =
                spacer_colour_answer.IsVisible =
                spacer_thickness_answer.IsVisible = SurveyFitterSharedLogic.glass_docL_and_spacer_visible();
            special_glass_answer.IsVisible = SurveyFitterSharedLogic.glass_special_glass_visible();

            drawings_and_photos.num_drawings = App.net.GlassRecord.no_of_pics;
            drawings_and_photos.num_photos = App.net.GlassRecord.no_of_photos;
        }
    }
}