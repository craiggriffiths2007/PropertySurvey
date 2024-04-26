using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Greenhouse : CarouselValidate
    {
		public Greenhouse()
		{
			InitializeComponent ();
            BindingContext = App.net.GreenRecord as GreenTable;

            // Combo boxes
            List<string> material_types = new List<string>() { "Aluminium", "Timber", "UPVC", "Metal" };
            List<string> types_of_glaze = new List<string>() { "Glass", "Polycarb" };
            List<string> glass_type = new List<string>() { "Horticultural", "Toughened" };
            List<string> base_sizes = new List<string>() { "4l x 8w ft", "4w x 8l ft", "6l x 4w ft", "6w x 4l ft", "6 x 6 ft", "6l x 8w ft", "6w x 8l ft", "6l x 10w ft", "6w x 10l ft", "6l x 12w ft", "6w x 12l ft", "8l x 10w ft", "8w x 10l ft", "8l x 12w ft", "8w x 12l ft", "8l x 14w ft", "8w x 14l ft", "Non standard ( mm )" };
            List<string> doors_open = new List<string>() { "In", "Out", "Sliding" };
            List<string> windows_open = new List<string>() { "Standard", "Louver", "Both", "None" };

            item_material_picker.SetPickerItems(material_types);                                  // Material type
            set_available_colours();                                                              // Sets item_colour based on the material type
            glazing_type_picker.SetPickerItems(types_of_glaze);                                   // Glazing type
            type_of_glass_picker.SetPickerItems(glass_type);                                      // Type of glass
            base_sizes_picker.SetPickerItems(base_sizes);                                         // Base size
            doors_open_picker.SetPickerItems(doors_open);                                         // Doors open
            window_opener_picker.SetPickerItems(windows_open);                                    // Windows open

            repair_replace_button.set_button_list(SurveyFitterButtonLists.shared_repair_replace_list);
            type_of_opening_button.set_button_list(SurveyFitterButtonLists.greenhouse_type_of_opening_list);

            type_of_glass_picker.IsVisible = SurveyFitterSharedLogic.greenhouse_type_of_glass_visible();
            type_of_opening_button.IsVisible = SurveyFitterSharedLogic.greenhouse_type_of_opening_visible();
            set_width_height_values();
            reason_for_replacement_entry.IsVisible = SurveyFitterSharedLogic.greenhouse_replace_reason_visible();
            replace_glass_area.IsVisible = SurveyFitterSharedLogic.replace_glass_visible(App.net.GreenRecord.repair_or_replace);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.CurrentApp.CurrentItem = "green";
            App.net.GreenRecord.no_of_pics = SurveyFitterSharedLogic.count_drawings();
            App.net.GreenRecord.no_of_photos = SurveyFitterSharedLogic.count_photos();

            SetGlassButton();
        }

        private void SetGlassButton()
        {
            if (App.net.GreenRecord.replace_glass == 1) // Replace glass
            {
                if (App.net.GreenRecord.glass_complete)
                    do_replace_glass_button.ImageSource = "green_tick.png";
                else
                    do_replace_glass_button.ImageSource = "question.png";
            }
            else
                do_replace_glass_button.ImageSource = "na.png";
        }

        private void set_available_colours()
        {
            //cb3.IsEnabled = true;

            if (App.net.GreenRecord.material_type == "Aluminium")
            {
                item_colour_picker.set_picker_enabled (true);
                List<string> ali_cols = new List<string>() { "", "Silver", "Green" };
                item_colour_picker.SetPickerItems(ali_cols);
            }
            else if (App.net.GreenRecord.material_type == "Timber")
            {
                item_colour_picker.set_picker_enabled (true);
                item_colour_picker.SetPickerItems(App.net.standard_and_stain);
            }
            else
                item_colour_picker.set_picker_enabled (false);
        }

        private void set_width_height_values()
        {
            t_units units = t_units.units_feet;

            switch (App.net.GreenRecord.base_size)
            {
                case "4l x 8w ft": width_height_area.width_text = "8"; width_height_area.height_text = "4"; break;
                case "4w x 8l ft": width_height_area.width_text = "4"; width_height_area.height_text = "8"; break;
                case "6l x 4w ft": width_height_area.width_text = "4"; width_height_area.height_text = "6"; break;
                case "6w x 4l ft": width_height_area.width_text = "6"; width_height_area.height_text = "4"; break;
                case "6 x 6 ft": width_height_area.width_text = "6"; width_height_area.height_text = "6"; break;
                case "6l x 8w ft": width_height_area.width_text = "8"; width_height_area.height_text = "6"; break;
                case "6w x 8l ft": width_height_area.width_text = "6"; width_height_area.height_text = "8"; break;
                case "6l x 10w ft": width_height_area.width_text = "10"; width_height_area.height_text = "6"; break;
                case "6w x 10l ft": width_height_area.width_text = "6"; width_height_area.height_text = "10"; break;
                case "6l x 12w ft": width_height_area.width_text = "12"; width_height_area.height_text = "6"; break;
                case "6w x 12l ft": width_height_area.width_text = "6"; width_height_area.height_text = "12"; break;
                case "8l x 10w ft": width_height_area.width_text = "10"; width_height_area.height_text = "8"; break;
                case "8w x 10l ft": width_height_area.width_text = "8"; width_height_area.height_text = "10"; break;
                case "8l x 12w ft": width_height_area.width_text = "12"; width_height_area.height_text = "8"; break;
                case "8w x 12l ft": width_height_area.width_text = "8"; width_height_area.height_text = "12"; break;
                case "8l x 14w ft": width_height_area.width_text = "14"; width_height_area.height_text = "8"; break;
                case "8w x 14l ft": width_height_area.width_text = "8"; width_height_area.height_text = "14"; break;
                default: units = t_units.units_mm; break;
            }

            width_height_area.units = units;
        }

        private void replace_repair_button_click(object sender, EventArgs e)
        {
            reason_for_replacement_entry.IsVisible = SurveyFitterSharedLogic.greenhouse_replace_reason_visible();
            replace_glass_area.IsVisible = SurveyFitterSharedLogic.replace_glass_visible(App.net.GreenRecord.repair_or_replace);
        }

        private void material_changed(object sender, EventArgs e)
        {
            set_available_colours(); // Sets item_colour based on the material type
        }

        private void glazing_type_changed(object sender, EventArgs e)
        {
            type_of_glass_picker.IsVisible = SurveyFitterSharedLogic.greenhouse_type_of_glass_visible();
        }

        private void base_size_changed(object sender, EventArgs e)
        {
            set_width_height_values();
        }

        private void roof_opening_changed(object sender, EventArgs e)
        {
            type_of_opening_button.IsVisible = SurveyFitterSharedLogic.greenhouse_type_of_opening_visible();
        }

        private void replace_glass_changed(object sender, EventArgs e)
        {
            SetGlassButton();
        }

        private void replace_glass_clicked(object sender, EventArgs e)
        {
            if (App.net.GreenRecord.replace_glass == 1) // Replace glass?
            {
                int item_no = App.CurrentApp.GreenRecord.item_number;

                App.net.GlassRecord = App.data.GetGlassByContractGreenhouseItemNo(App.CurrentApp.HeaderRecord.udi_cont, item_no);

                if (App.net.GlassRecord == null)
                {
                    App.net.table_init.CreateGlass();
                    App.CurrentApp.GlassRecord.item_number = App.CurrentApp.GreenRecord.item_number;
                    App.CurrentApp.GlassRecord.parent_item = 5;
                    App.CurrentApp.loaded_item_number = App.CurrentApp.GreenRecord.item_number;
                    App.CurrentApp.root_item_number = App.CurrentApp.GreenRecord.item_number;
                    App.data.SaveHeader();
                }
                App.CurrentApp.CurrentItem = "glass";
                Navigation.PushAsync(new GlassItem(), false);
            }
        }

        private string validate_page_0()
        {
            return repair_replace_button.validation_error_string("Repair/Replace")
                 + reason_for_replacement_entry.validation_error_string("Reason for replace\n")
                 + (replace_glass_area.IsVisible ? replace_glass_button.validation_error_string("Replace glass\n")
                                                   + (App.CurrentApp.GreenRecord.replace_glass == 1 && !App.net.GreenRecord.glass_complete ? "Replace glass details\n" : "")
                                                 : "")
                 + cause_of_damage_area.validation_error_string()
                 + item_material_picker.validation_error_string("Material type\n")
                 + item_colour_picker.validation_error_string("Colour\n")
                 + glazing_type_picker.validation_error_string("Glazing type\n")
                 + type_of_glass_picker.validation_error_string("Type of glass\n")
                 + base_sizes_picker.validation_error_string("Base sizes\n")
                 + width_height_area.validation_error_string("Base width\n", "Base length\n")
                 + doors_open_picker.validation_error_string("Doos opening\n")
                 + window_opener_picker.validation_error_string("Windows opening\n")
                 + roof_opening_button.validation_error_string("Roof opening lights\n")
                 + type_of_opening_button.validation_error_string("Automatic or Manual\n")
                 + overall_height_entry.validation_error_string("Overall height\n")
                 + summary_pto_area.validation_error_string();
        }

        protected override string validate_drawings_and_pictures()
        {
            return cause_of_damage_area.photo_validation_error_string(App.CurrentApp.GreenRecord.no_of_photos)
                 + (App.CurrentApp.GreenRecord.no_of_pics == 0 ? "Drawings\n" : "");
        }

        protected override string validate_page()
        {
                return validate_page_0();
        }

        protected override void save_item(bool complete)
        {
            App.data.SaveGreen(complete);
        }

        protected override bool already_signed()
        {
            return App.CurrentApp.GreenRecord.bDifferentFromOriginal;
        }
    }
}