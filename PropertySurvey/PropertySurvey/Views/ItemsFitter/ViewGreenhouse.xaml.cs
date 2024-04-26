using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewGreenhouse : ContentPage
	{
		public ViewGreenhouse ()
		{
			InitializeComponent ();
            BindingContext = App.net.GreenRecord as GreenTable;

            repair_replace_answer.set_button_list(SurveyFitterButtonLists.shared_repair_replace_list);
            type_of_opening_answer.set_button_list(SurveyFitterButtonLists.greenhouse_type_of_opening_list);

            replacement_reason_answer.IsVisible = SurveyFitterSharedLogic.greenhouse_replace_reason_visible();
            cause_different_answer.IsVisible = MartControls.cause_of_damage_logic.reason_different_visible(App.net.GreenRecord.cause_of_damage);
            point_of_entry_answer.IsVisible =
                was_it_locked_answer.IsVisible =
                type_of_locking_system_answer.IsVisible = MartControls.cause_of_damage_logic.point_of_entry_visible(App.net.GreenRecord.cause_of_damage);
            type_of_opening_answer.IsVisible = SurveyFitterSharedLogic.greenhouse_type_of_opening_visible();
            replace_glass_area.IsVisible = SurveyFitterSharedLogic.replace_glass_visible(App.net.GreenRecord.repair_or_replace);

            drawings_and_photos.num_drawings = App.net.GreenRecord.no_of_pics;
            drawings_and_photos.num_photos = App.net.GreenRecord.no_of_photos;
        }

        private void view_replace_glass_clicked(object sender, EventArgs e)
        {
            App.net.GlassRecord = App.data.GetGlassByContractGreenhouseItemNo(App.CurrentApp.HeaderRecord.udi_cont, App.CurrentApp.GreenRecord.item_number);
            if (App.net.GlassRecord == null)
            {
                App.net.table_init.CreateGlass();
                App.CurrentApp.GlassRecord.item_number = App.CurrentApp.GreenRecord.item_number;
                App.CurrentApp.GlassRecord.parent_item = 5;
                App.CurrentApp.loaded_item_number = App.CurrentApp.GreenRecord.item_number;
                App.CurrentApp.root_item_number = App.CurrentApp.GreenRecord.item_number;
                App.data.SaveHeader();
                App.data.SaveGlass(false);
            }
            Navigation.PushAsync(new ViewGlass(), false);
        }
    }
}