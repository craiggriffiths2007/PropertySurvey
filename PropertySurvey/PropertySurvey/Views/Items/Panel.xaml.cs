using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Panel : CarouselValidate
    {
        public Panel()
        {
            InitializeComponent();
            BindingContext = App.net.PanelRecord as PanelTable;
            save_on_pagechange = true;
            show_image_button = App.CurrentApp.PanelRecord.upvc_item_number == 0 && App.CurrentApp.PanelRecord.alum_item_number == 0;
            //SetPageNumber();

            List<string> nock_list = new List<string>() { "None", "Lions Head", "Urn Type", "Urn Type With Spyhole" };
            knocker_required_picker.SetPickerItems(nock_list);

            List<string> nock_col = new List<string>() { "White", "Gold", "Silver", "Black", "None" };
            knocker_colour_picker.SetPickerItems(nock_col);

            cause_of_damage_area.IsVisible =
                room_location_picker.IsVisible =
                summary_pto_area.IsVisible = SurveyFitterSharedLogic.panel_only_visible();
            knocker_colour_picker.IsVisible = SurveyFitterSharedLogic.panel_knocker_colour_visible();

            // Page 2
            List<string> thick_list = new List<string>() { "20mm", "24mm", "28mm" };
            thickness_picker.SetPickerItems(thick_list);
            backing_glass_picker.SetPickerItems(App.net.backing_glass);
            panel_colour_picker.SetPickerItems(App.net.panel_colour);

            set_profile_colour_warning_visible();
            glass_design_area.IsVisible =
                spacer_colour_picker.IsVisible = SurveyFitterSharedLogic.panel_glass_and_spacer_visible();
            spacer_colour_picker.SetPickerItems(App.net.spacer_colour_new);
            room_location_picker.SetPickerItems(App.net.room_location);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.net.PanelRecord.no_of_pics = SurveyFitterSharedLogic.count_drawings();
            App.net.PanelRecord.no_of_photos = SurveyFitterSharedLogic.count_photos();
        }

        private void set_profile_colour_warning_visible()
        {
            profile_colour_warning.IsVisible = App.CurrentApp.PanelRecord.coledit == "Woodgrain 013 on White"
                                            || App.CurrentApp.PanelRecord.coledit == "Woodgrain 021 on White"
                                            || App.CurrentApp.PanelRecord.coledit == "Oak on White"
                                            || App.CurrentApp.PanelRecord.coledit == "Rosewood on White";
        }

        private void knocker_required_changed(object sender, EventArgs e)
        {
            knocker_colour_picker.IsVisible = SurveyFitterSharedLogic.panel_knocker_colour_visible();
        }

        private void backing_glass_changed(object sender, EventArgs e)
        {
            glass_design_area.IsVisible =
            spacer_colour_picker.IsVisible = SurveyFitterSharedLogic.panel_glass_and_spacer_visible();
        }

        private void panel_colour_changed(object sender, EventArgs e)
        {
            set_profile_colour_warning_visible();
        }

        private string validate_page_0()
        {
            return cause_of_damage_area.validation_error_string()
                 + knocker_required_picker.validation_error_string("Knocker required\n")
                 + knocker_colour_picker.validation_error_string("Knocker color\n")
                 + width_height_area.validation_error_string("Panel width\n", "Panel height\n")
                 + letter_box_area.validation_error_string()
                 + pet_flap_area.validation_error_string()
                 + panel_type_entry.validation_error_string("Panel type\n");
        }

        private string validate_page_1()
        {
            return thickness_picker.validation_error_string("Panel thickness\n")
                 + backing_glass_picker.validation_error_string("Backing glass\n")
                 + panel_colour_picker.validation_error_string("Panel Color\n")
                 + (glass_design_area.IsVisible && App.CurrentApp.PanelRecord.gltext == "" ? "Glass design\n" : "")
                 + spacer_colour_picker.validation_error_string("Spacer Bar Color\n")
                 + room_location_picker.validation_error_string("Room Location\n")
                 + summary_pto_area.validation_error_string();
        }

        protected override string validate_drawings_and_pictures()
        {
            return cause_of_damage_area.photo_validation_error_string(App.CurrentApp.PanelRecord.no_of_photos)
                 + (App.CurrentApp.PanelRecord.no_of_pics == 0 ? "Drawings\n" : "");
        }

        protected override string validate_page()
        {
                return validate_page_0()
                + validate_page_1();
        }

        /*
        protected override void set_page(int page_num)
        {
            switch (page_num)
            {
                case 0: CurrentPage = Page0; break;
                case 1: CurrentPage = Page1; break;
            }
        }
        */

        protected override void save_item(bool complete)
        {
            if(App.net.PanelRecord.upvc_item_number!=0)
            {
                App.net.UPVCRecord.bPanelComplete = complete;
            }
            if (App.net.PanelRecord.alum_item_number != 0)
            {
                App.net.AlumRecord.bPanelComplete = complete;
            }
            App.data.SavePanel(complete);
        }

        protected override bool already_signed()
        {
            return App.CurrentApp.PanelRecord.bDifferentFromOriginal;
        }
    }
}