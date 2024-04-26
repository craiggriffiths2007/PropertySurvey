using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GlassItem : CarouselValidate
    {
        public GlassItem()
        {
            InitializeComponent();
            BindingContext = App.net.GlassRecord as GlassTable;
            save_on_pagechange = true;
            show_image_button = App.CurrentApp.GlassRecord.parent_item == 0;

            // Page 0
            List<string> glass_installed_into = new List<string>() { "Aluminium", "Timber", "UPVC" };
            List<string> glass_instinto = new List<string>() { "Putted", "Beaded" };
            List<string> tape_or_gasket = new List<string>() { "Tape", "Gasket" };

            temporary_button.set_button_list(SurveyFitterButtonLists.shared_temporary_button_list);
            glaze_button.set_button_list(SurveyFitterButtonLists.glass_glaze_button_list);
            single_or_double_button.set_button_list(SurveyFitterButtonLists.shared_single_or_double_list);
            if (App.net.GlassRecord.parent_item == 5) // Replace glass within greenhouse
                glass_type_picker.SetPickerItems(App.net.glass_type_greenhouse);
            else
                glass_type_picker.SetPickerItems(App.net.glass_type);
            installed_into_picker.SetPickerItems(glass_installed_into);
            glazing_type_picker.SetPickerItems(glass_instinto);
            seal_picker.SetPickerItems(tape_or_gasket);

            cause_of_damage_area.IsVisible =
                copy_and_collect_button.IsVisible =
                summary_pto_area.IsVisible =
                room_location_picker.IsVisible = SurveyFitterSharedLogic.glass_only_items_visible();
            temporary_button.IsVisible = SurveyFitterSharedLogic.glass_only_items_visible() 
                                      && SurveyFitterSharedLogic.temporary_visible(App.net.GlassRecord.collect_and_copy);
            set_width_and_heights_visible();
            stepped_unit_button.IsVisible = SurveyFitterSharedLogic.glass_stepped_unit_visible();
            internal_width_height_area.IsVisible = SurveyFitterSharedLogic.glass_internal_dimensions_visible();
            glazing_type_picker.IsVisible = SurveyFitterSharedLogic.glass_glazing_type_visible();
            seal_picker.IsVisible = SurveyFitterSharedLogic.glass_seal_visible();

            // Page 1
            set_glass_pattern_picker_list();
            set_document_L_picker_list();
            spacer_colour_picker.SetPickerItems(App.net.spacer_colour_new);
            set_spacer_and_DocL_visibility_and_picker();
            special_glass_area.SetPickerItems(App.net.special_glass_back);
            room_location_picker.SetPickerItems(App.net.room_location);

            set_special_glass_visible();

            //SetPageNumber();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.net.CurrentItem = "glass";
            App.net.GlassRecord.no_of_pics = SurveyFitterSharedLogic.count_drawings();
            App.net.GlassRecord.no_of_photos = SurveyFitterSharedLogic.count_photos();

            special_glass_area.set_complete(App.net.GlassRecord.lead_bDiamondComplete,
                App.net.GlassRecord.lead_bGeorgianComplete,
                App.net.GlassRecord.lead_bGeorgianComplete,
                App.net.GlassRecord.lead_bBarComplete);
        }

        private void set_width_and_heights_visible()
        {
            units_warning.IsVisible =
                width_height_area2.IsVisible = (App.net.GlassRecord.units_required >= 2);
            width_height_area3.IsVisible = (App.net.GlassRecord.units_required >= 3);
            width_height_area4.IsVisible = (App.net.GlassRecord.units_required >= 4);
            width_height_area5.IsVisible = (App.net.GlassRecord.units_required >= 5);
            width_height_area6.IsVisible = (App.net.GlassRecord.units_required >= 6);
            width_height_area7.IsVisible = (App.net.GlassRecord.units_required >= 7);
            width_height_area8.IsVisible = (App.net.GlassRecord.units_required >= 8);
        }

        private void set_glass_pattern_picker_list()
        {
            if (App.net.GlassRecord.glass_type == "6.4mm Laminated")
                glass_pattern_picker.SetPickerItems(App.net.backing_glass_6mm_lami);
            else if (App.net.GlassRecord.glass_type == "6mm" || App.net.GlassRecord.glass_type == "6mm Tough")
                glass_pattern_picker.SetPickerItems(App.net.backing_glass_6mm);
            else
                glass_pattern_picker.SetPickerItems(App.net.backing_glass);
        }

        private void set_document_L_picker_list()
        {
            if (App.net.GlassRecord.glass_type.Contains("6.4mm") || (App.net.GlassRecord.glass_pattern != "None" && App.net.GlassRecord.glass_pattern != "Clear"))
                document_L_picker.SetPickerItems(App.net.docl_comp_noa);
            else
                document_L_picker.SetPickerItems(App.net.docl_comp);
        }

        private void set_spacer_and_DocL_visibility_and_picker()
        {
            document_L_picker.IsVisible =
                spacer_colour_picker.IsVisible =
                spacer_thickness_picker.IsVisible = SurveyFitterSharedLogic.glass_docL_and_spacer_visible();

            if (spacer_thickness_picker.IsVisible)
                if (App.net.GlassRecord.single_or_double == 3)
                    spacer_thickness_picker.SetPickerItems(App.net.spacer_thickness3);
                else
                    spacer_thickness_picker.SetPickerItems(App.net.spacer_thickness2);
        }

        private void set_spacer_message_text()
        {
            if ((App.net.GlassRecord.single_or_double >= 2) && (App.net.GlassRecord.single_or_double <= 4))
            {
                switch (App.net.GlassRecord.spacer_color)
                {
                    case "Black, Warm Edge, Super Spacer":
                        spacer_warning.IsVisible = true;
                        spacer_warning.Text = "This is our preferred spacer, available in 12-20mm";
                        break;
                    case "Grey - Warm Edge - Plastic":
                    case "Black - Warm Edge - Plastic":
                        spacer_warning.IsVisible = true;
                        spacer_warning.Text = "Limited stock. Try to use super spacer";
                        break;
                    default:
                        spacer_warning.IsVisible = false;
                        break;
                }
            }
            else
                spacer_warning.IsVisible = false;
        }

        private void check_thickness_mismatch()
        {
            double spacer_thickness = 0;
            double glass_thickness = 0;
            int spacer_multiplier = 1;
            bool skip_multiplier = false;

            string spacer_str = App.net.GlassRecord.spacer_thickness;
            if (spacer_str.Contains("2x"))
            {
                spacer_multiplier = 2;
                spacer_str = spacer_str.Replace("2x ", "");
            }
            spacer_str = spacer_str.Replace(" Ali", "");
            spacer_str = spacer_str.Replace("mm", "");

            // For now, calculate the thickness of a single pane. Double/triple multipliers are applied later
            switch (App.net.GlassRecord.glass_type)
            {
                case "4mm Tough + 6.4mm Lami": glass_thickness = 10.4; skip_multiplier = true; break;
                case "6mm Tough + 6.4mm Lami": glass_thickness = 12.4; skip_multiplier = true; break;

                default:
                    string glass_str = App.net.GlassRecord.glass_type;
                    glass_str = glass_str.Replace(" Tough", "");
                    glass_str = glass_str.Replace(" Laminated", "");
                    glass_str = glass_str.Replace(" Horticultural", "");
                    glass_str = glass_str.Replace("mm", "");

                    try   { glass_thickness = Convert.ToDouble(glass_str);  }
                    catch { glass_thickness = 0; }
                    break;
            }

            string overall_str = App.net.GlassRecord.docl_old;
            overall_str = overall_str.Replace("mm", "");

            if ((spacer_str != "") && (App.net.GlassRecord.glass_type != "") && (overall_str != ""))
            {
                double overall_thickness;

                try { spacer_thickness = Convert.ToDouble(spacer_str) * spacer_multiplier; }
                catch { spacer_thickness = 0; }

                try { overall_thickness = Convert.ToDouble(overall_str); }
                catch { overall_thickness = 0; }

                if (!skip_multiplier)
                {
                    // Ignore single glazed, there is no spacer question for single
                    if (App.net.GlassRecord.single_or_double == 3) // Triple glazed
                        glass_thickness = 3 * glass_thickness;
                    else // Double glazed, encapsulated unit
                        glass_thickness = 2 * glass_thickness;
                }

                if (Math.Abs(spacer_thickness + glass_thickness- overall_thickness) > 0.05) // Allow for rounding errors when using decimals.
                {
                    spacer_thickness_warning.IsVisible = true;
                    spacer_thickness_warning.Text = "The spacer thickness you have chosen does not match the overall thickness less the thickness of the glass.\t\n"
                                                 + "Glass thickness : " + String.Format("{0:0.#}", glass_thickness) + "mm\t\n"
                                                 + "Overall thickness : " + String.Format("{0:0.#}", overall_thickness) + "mm\t\n"
                                                 + "The spacer should be : " + String.Format("{0:0.#}", overall_thickness - glass_thickness) + "mm";
                }
                else
                    spacer_thickness_warning.IsVisible = false;
            }
            else
                spacer_thickness_warning.IsVisible = false;
        }

        private void set_special_glass_visible()
        {
            special_glass_area.IsVisible = SurveyFitterSharedLogic.glass_special_glass_visible();
        }

        private void copy_and_collected_changed(object sender, EventArgs e)
        {
            temporary_button.IsVisible = SurveyFitterSharedLogic.temporary_visible(App.net.GlassRecord.collect_and_copy);
        }

        private void units_clicked(object sender, EventArgs e)
        {
            set_width_and_heights_visible();
            set_special_glass_visible();
        }

        private void stepped_unit_click(object sender, EventArgs e)
        {
            internal_width_height_area.IsVisible = SurveyFitterSharedLogic.glass_internal_dimensions_visible();
        }

        private void single_or_double_picker_changed(object sender, EventArgs e)
        {
            set_spacer_and_DocL_visibility_and_picker();
            check_thickness_mismatch();
        }

        private void glass_type_picker_changed(object sender, EventArgs e)
        {
            set_glass_pattern_picker_list();
            set_document_L_picker_list();
            check_thickness_mismatch();
        }

        private void overall_thickness_changed(object sender, EventArgs e)
        {
            check_thickness_mismatch();
        }

        private void installed_into_picker_changed(object sender, EventArgs e)
        {
            glazing_type_picker.IsVisible = SurveyFitterSharedLogic.glass_glazing_type_visible();
            seal_picker.IsVisible = SurveyFitterSharedLogic.glass_seal_visible();
        }

        private void glass_pattern_picker_changed(object sender, EventArgs e)
        {
            set_document_L_picker_list();
        }

        private void document_L_picker_changed(object sender, EventArgs e)
        {
            // Todo
        }

        private void spacer_colour_picker_changed (object sender, EventArgs e)
        {
            set_spacer_message_text();
        }
        
        private void spacer_thickness_picker_changed (object sender, EventArgs e)
        {
            check_thickness_mismatch();
        }

        private string validate_page_0()
        {
            return cause_of_damage_area.validation_error_string()
                 + copy_and_collect_button.validation_error_string("Collect and Copy\n")
                 + temporary_button.validation_error_string("Temporary\n")
                 + width_height_area1.validation_error_string("Glass width\n", "Glass height\n")
                 + width_height_area2.validation_error_string("Glass width 2\n", "Glass height 2\n")
                 + width_height_area3.validation_error_string("Glass width 3\n", "Glass height 3\n")
                 + width_height_area4.validation_error_string("Glass width 4\n", "Glass height 4\n")
                 + width_height_area5.validation_error_string("Glass width 5\n", "Glass height 5\n")
                 + width_height_area6.validation_error_string("Glass width 6\n", "Glass height 6\n")
                 + width_height_area7.validation_error_string("Glass width 7\n", "Glass height 7\n")
                 + width_height_area8.validation_error_string("Glass width 8\n", "Glass height 8\n")
                 + stepped_unit_button.validation_error_string("Stepped unit\n")
                 + internal_width_height_area.validation_error_string("Internal width\n", "Internal height\n")
                 + glaze_button.validation_error_string("Glazing type\n")
                 + single_or_double_button.validation_error_string("Single or double\n")
                 + glass_type_picker.validation_error_string("Glass type\n")
                 + overall_thickness_entry.validation_error_string("Overall Thickness\n")
                 + installed_into_picker.validation_error_string("Installed into\n")
                 + glazing_type_picker.validation_error_string("Glazing type\n")
                 + seal_picker.validation_error_string("Seal\n");
        }

        private string validate_page_1()
        {
            return glass_pattern_picker.validation_error_string("Glass pattern\n")
                 + document_L_picker.validation_error_string("Document 'L'\n")
                 + spacer_colour_picker.validation_error_string("Spacer Colour\n")
                 + spacer_thickness_picker.validation_error_string("Spacer Thickness\n")
                 + special_glass_area.validation_error_string()
                 + room_location_picker.validation_error_string("Room Location\n")
                 + summary_pto_area.validation_error_string();
        }

        protected override string validate_drawings_and_pictures()
        {
            return cause_of_damage_area.photo_validation_error_string(App.CurrentApp.GlassRecord.no_of_photos)
                 + (App.CurrentApp.GlassRecord.no_of_pics == 0 ? "Drawings\n" : "");
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
            switch (App.net.GlassRecord.parent_item)
            {
                case 1 : App.net.AlumRecord.glass_complete = complete; break;
                case 2 : App.net.BifoldRecord.glass_complete = complete; break;
                case 3 : App.net.CompRecord.glass_complete = complete; break;
                case 4 : App.net.ConsRecord.glass_complete = complete; break;
                case 5 : App.net.GreenRecord.glass_complete = complete; break;
                case 6 : App.net.TimberRecord.glass_complete = complete; break;
                case 7 : App.net.UPVCRecord.glass_complete = complete; break;
        }

            App.data.SaveGlass(complete);
        }

        protected override bool already_signed()
        {
            return App.CurrentApp.GlassRecord.bDifferentFromOriginal;
        }
    }
}