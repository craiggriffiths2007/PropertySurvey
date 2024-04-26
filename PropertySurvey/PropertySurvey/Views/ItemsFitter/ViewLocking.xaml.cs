using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewLocking : ContentPage
	{
		public ViewLocking ()
		{
			InitializeComponent ();
            BindingContext = App.net.LockingRecord as LockingTable;

            cause_different_answer.IsVisible = MartControls.cause_of_damage_logic.reason_different_visible(App.net.LockingRecord.cause_of_damage);
            point_of_entry_answer.IsVisible =
                was_it_locked_answer.IsVisible =
                type_of_locking_system_answer.IsVisible = MartControls.cause_of_damage_logic.point_of_entry_visible(App.net.LockingRecord.cause_of_damage);
            page_number_answer.IsVisible = SurveyFitterSharedLogic.locking_page_number_visible();
            item_answer.IsVisible =
                locking_mechanism_area.IsVisible =
                lock_make_answer.IsVisible = SurveyFitterSharedLogic.locking_multi_lock_visible();
            lock_colour_answer.IsVisible = SurveyFitterSharedLogic.locking_lock_colour_visible();

            drawings_and_photos.num_drawings = App.net.LockingRecord.no_of_pics;
            drawings_and_photos.num_photos = App.net.LockingRecord.no_of_photos;
        }

        private void locking_mechanism_button_clicked(object sender, EventArgs e)
        {
            switch (App.net.LockingRecord.item)
            {
                case "Door": Navigation.PushAsync(new ViewLock(view_lock_type.vlt_door), false); break;
                case "Window": Navigation.PushAsync(new ViewLock(view_lock_type.vlt_window), false); break;
            }
        }
    }
}