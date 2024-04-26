using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    public enum t_locking_item_mode { general_lock, multipoint_lock }

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LockItem : CarouselValidate
    {
		public LockItem (t_locking_item_mode lock_type)
		{
			InitializeComponent ();
            BindingContext = App.net.LockingRecord as LockingTable;

            App.net.LockingRecord.bMulti = (lock_type == t_locking_item_mode.multipoint_lock);
            page_number_entry.IsVisible = SurveyFitterSharedLogic.locking_page_number_visible();
            multi_lock_area.IsVisible = SurveyFitterSharedLogic.locking_multi_lock_visible();
            lock_colour_picker.IsVisible = SurveyFitterSharedLogic.locking_lock_colour_visible();

            switch (lock_type)
            {
                case t_locking_item_mode.general_lock:
                    List<string> lock_colour = new List<string>() { "Satin chrome", "Brass", "Nickel plated", "Polished chrome", "White", "Silver", "Brown", "Grey" };
                    lock_colour_picker.SetPickerItems (lock_colour);
                    break;
                default: // multipoint_lock
                    List<string> item_type = new List<string>() { "Door", "Window" };
                    item_picker.SetPickerItems(item_type);
                    lock_make_picker.SetPickerItems(App.net.lock_make);
                    set_locking_mechanism_button();
                    break;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.net.LockingRecord.no_of_pics = SurveyFitterSharedLogic.count_drawings();
            App.net.LockingRecord.no_of_photos = SurveyFitterSharedLogic.count_photos();

            set_locking_mechanism_button();
        }

        private void set_locking_mechanism_button()
        {
            locking_mechanism_button.IsVisible = App.net.LockingRecord.item == "Door" || App.net.LockingRecord.item == "Window";

            if (!locking_mechanism_button.IsVisible)
                locking_mechanism_button.ImageSource = null;
            else if (App.net.LockingRecord.bLockComplete)
                locking_mechanism_button.ImageSource = "green_tick.png";
            else
                locking_mechanism_button.ImageSource = "question.png";
        }

        private void item_picker_changed(object sender, EventArgs e)
        {
            set_locking_mechanism_button();
        }

        private string validate_page_0()
        {
            return cause_of_damage_area.validation_error_string()
                 + page_number_entry.validation_error_string("Page number\n")
                 + (multi_lock_area.IsVisible ? item_picker.validation_error_string("Item type\n")
                                               // locking mechanism validation
                                               + lock_make_picker.validation_error_string("Lock make\n")
                                              : "")
                 + lock_codes_entry.validation_error_string("Locking codes\n")
                 + lock_colour_picker.validation_error_string("Lock colour\n")
                 + supplier_entry.validation_error_string("Supplier/brochure\n")
                 + summary_pto_area.validation_error_string();
        }

        protected override string validate_drawings_and_pictures()
        {
            return cause_of_damage_area.photo_validation_error_string(App.CurrentApp.LockingRecord.no_of_photos)
                 + (App.CurrentApp.LockingRecord.no_of_pics == 0 ? "Drawings\n" : "");
        }

        protected override string validate_page()
        {
                return validate_page_0();
        }

        protected override void save_item(bool complete)
        {
            App.data.SaveLocking(complete);
        }

        protected override bool already_signed()
        {
            return App.CurrentApp.LockingRecord.bDifferentFromOriginal;
        }

        private void locking_mechanism_button_clicked(object sender, EventArgs e)
        {
            switch (App.net.LockingRecord.item)
            {
                case "Door": Navigation.PushAsync(new DoorLock(), false); break;
                case "Window": Navigation.PushAsync(new WindowLock(), false); break;
            }
        }
    }
}