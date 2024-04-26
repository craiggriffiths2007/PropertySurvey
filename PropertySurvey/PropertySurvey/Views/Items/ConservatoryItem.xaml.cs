using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConservatoryItem : CarouselValidate
    {
		public ConservatoryItem ()
		{
			InitializeComponent ();
            BindingContext = App.net.ConsRecord as ConsTable;
            save_on_pagechange = true;

            // Page 0
            List<string> type_of_roof_list = new List<string>() { "Ultralight 500", "Flat", "Victorian", "Edwardian", "Other" };
            List<string> wall_position_list = new List<string>() { "None", "Left", "Right", "Both" };

            WER_rating_picker.SetPickerItems(App.net.wer_rating);
            type_of_roof_picker.SetPickerItems(type_of_roof_list);
            material_type_button.set_button_list(SurveyFitterButtonLists.conservatory_material_type_list);
            wall_position_picker.SetPickerItems(wall_position_list);

            no_repair_reason_entry.IsVisible = SurveyFitterSharedLogic.reason_not_repaired_visible(App.net.ConsRecord.bRepair);
            replace_glass_area.IsVisible = SurveyFitterSharedLogic.replace_glass_visible(App.net.ConsRecord.bRepair);
            WER_rating_picker.IsVisible = SurveyFitterSharedLogic.WER_rating_visible(App.CurrentApp.ConsRecord.bRepair, App.CurrentApp.ConsRecord.fensa);
            spars_lineup_button.IsVisible = SurveyFitterSharedLogic.conservatory_spars_lineup_visible();
            set_drawing_warning_visible();
            ridge_length_entry.IsVisible = SurveyFitterSharedLogic.conservatory_ridge_length_visible();
            wall_position_picker.IsVisible = SurveyFitterSharedLogic.conservatory_wall_position_visible();
            set_additional_drawing_visible();

            //  Page 1 - Old Cons2 and Cons3 merged
            set_dimensions_visible_and_image();

            // Page 2 - Old Cons4
            roof_sheets_spin_value.IsVisible = SurveyFitterSharedLogic.conservatory_roof_sheets_visible();
            set_pane_dimensions_visible();

            // Page 3 - Old cons6
            List<string> glaze_thickness_list = new List<string>() { "10mm", "16mm", "25mm", "32mm", "35mm", "40mm", "Ultralite" };
            List<string> sheet_colour_list = new List<string>() { "Clear", "Bronze", "Opel", "Bronze on opel", "Heat guard (silver on opel)", "Georgian wire clear", "Georgian wire rough cast" };
            List<string> roof_colour_list = new List<string>() { "White", "Brown", "Woodgrain", "Oak", "Rosewood" };

            roof_glazing_thickness_picker.SetPickerItems(glaze_thickness_list);
            sheet_colour_picker.SetPickerItems(sheet_colour_list);
            roof_colour_picker.SetPickerItems(roof_colour_list);

            roof_glazing_thickness_picker.IsVisible = SurveyFitterSharedLogic.conservatory_roof_glazing_thickness_visible();
            sheet_colour_picker.IsVisible = SurveyFitterSharedLogic.conservatory_sheet_colour_visible();
            flute_size_entry.IsVisible = SurveyFitterSharedLogic.conservatory_flute_size_visible();
            set_brown_warning_visible();
            //SetPageNumber();
        }

        private void set_drawing_warning_visible()
        {
            drawing_warning_text.IsVisible = App.net.ConsRecord.spars_line_up == 1;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.CurrentApp.CurrentItem = "cons";
            App.net.ConsRecord.no_of_pics = SurveyFitterSharedLogic.count_drawings();
            App.net.ConsRecord.no_of_photos = SurveyFitterSharedLogic.count_photos();

            SetGlassButton();
            set_additional_drawing_visible();
        }

        private void SetGlassButton()
        {
            if (App.net.ConsRecord.replace_glass == 1) // Replace glass
            {
                if (App.net.ConsRecord.glass_complete)
                    do_replace_glass_button.ImageSource = "green_tick.png";
                else
                    do_replace_glass_button.ImageSource = "question.png";
            }
            else
                do_replace_glass_button.ImageSource = "na.png";
        }

        private void set_additional_drawing_visible()
        {
            additional_drawing_area.IsVisible = App.net.ConsRecord.does_roof_fit_under == 1;

            if (additional_drawing_area.IsVisible)
            {
                if (App.net.ConsRecord.cons_roof_under_drawn == true)
                    additional_drawing_button.ImageSource = "green_tick.png";
                else
                    additional_drawing_button.ImageSource = "question.png";
            }
            else
                additional_drawing_button.ImageSource = null;
        }

        private void set_dimensions_visible_and_image()
        {
            if (SurveyFitterSharedLogic.conservatory_dimensions_page_visible())
            {
                Page1.IsVisible = true;

                 dimensions_D_to_G_plus_angles.IsVisible = SurveyFitterSharedLogic.conservatory_dimensions_d_to_g_visible();
                dimensions_image.Source = SurveyFitterSharedLogic.conservatory_dimension_image();
            }
            else
                Page1.IsVisible = false;
        }

        private void set_pane_dimensions_visible()
        {
            pane_dimensions_01.IsVisible =
                add_minus_02.IsVisible = App.net.ConsRecord.roof_sheets_quantity_1 > 0;
            pane_dimensions_02.IsVisible =
                add_minus_03.IsVisible = App.net.ConsRecord.roof_sheets_quantity_2 > 0;
            pane_dimensions_03.IsVisible =
                add_minus_04.IsVisible = App.net.ConsRecord.roof_sheets_quantity_3 > 0;
            pane_dimensions_04.IsVisible =
                add_minus_05.IsVisible = App.net.ConsRecord.roof_sheets_quantity_4 > 0;
            pane_dimensions_05.IsVisible =
                add_minus_06.IsVisible = App.net.ConsRecord.roof_sheets_quantity_5 > 0;
            pane_dimensions_06.IsVisible =
                add_minus_07.IsVisible = App.net.ConsRecord.roof_sheets_quantity_6 > 0;
            pane_dimensions_07.IsVisible =
                add_minus_08.IsVisible = App.net.ConsRecord.roof_sheets_quantity_7 > 0;
            pane_dimensions_08.IsVisible =
                add_minus_09.IsVisible = App.net.ConsRecord.roof_sheets_quantity_8 > 0;
            pane_dimensions_09.IsVisible =
                add_minus_10.IsVisible = App.net.ConsRecord.roof_sheets_quantity_9 > 0;
            pane_dimensions_10.IsVisible = App.net.ConsRecord.roof_sheets_quantity_10 > 0;
        }

        private void set_brown_warning_visible()
        {
            brown_warning_text.IsVisible = App.net.ConsRecord.type == "Ultralight 500"
                                        && App.net.ConsRecord.roof_color != ""
                                        && App.net.ConsRecord.roof_color != "White";
        }

        private void replace_repair_button_click(object sender, EventArgs e)
        {
            no_repair_reason_entry.IsVisible = SurveyFitterSharedLogic.reason_not_repaired_visible(App.net.ConsRecord.bRepair);
            replace_glass_area.IsVisible = SurveyFitterSharedLogic.replace_glass_visible(App.net.ConsRecord.bRepair);
        }

        private void type_of_roof_changed(object sender, EventArgs e)
        {
            spars_lineup_button.IsVisible = SurveyFitterSharedLogic.conservatory_spars_lineup_visible();
            ridge_length_entry.IsVisible = SurveyFitterSharedLogic.conservatory_ridge_length_visible();
            wall_position_picker.IsVisible = SurveyFitterSharedLogic.conservatory_wall_position_visible();
            roof_sheets_spin_value.IsVisible = SurveyFitterSharedLogic.conservatory_roof_sheets_visible();
            roof_glazing_thickness_picker.IsVisible = SurveyFitterSharedLogic.conservatory_roof_glazing_thickness_visible();
            sheet_colour_picker.IsVisible = SurveyFitterSharedLogic.conservatory_sheet_colour_visible();
            flute_size_entry.IsVisible = SurveyFitterSharedLogic.conservatory_flute_size_visible();
            set_dimensions_visible_and_image();
            set_brown_warning_visible();
        }

        private void spar_alignment_clicked(object sender, EventArgs e)
        {
            set_drawing_warning_visible();
        }

        private void wall_position_changed(object sender, EventArgs e)
        {
            set_dimensions_visible_and_image();
        }

        private void soffit_clicked(object sender, EventArgs e)
        {
            set_additional_drawing_visible();
        }

        private void replace_glass_changed(object sender, EventArgs e)
        {
            SetGlassButton();
            roof_sheets_spin_value.IsVisible = SurveyFitterSharedLogic.conservatory_roof_sheets_visible();
            roof_glazing_thickness_picker.IsVisible = SurveyFitterSharedLogic.conservatory_roof_glazing_thickness_visible();
            sheet_colour_picker.IsVisible = SurveyFitterSharedLogic.conservatory_sheet_colour_visible();
            flute_size_entry.IsVisible = SurveyFitterSharedLogic.conservatory_flute_size_visible();
        }

        private void replace_glass_clicked(object sender, EventArgs e)
        {
            if (App.net.ConsRecord.replace_glass == 1) // Replace glass?
            {
                int item_no = App.CurrentApp.ConsRecord.item_number;

                App.net.GlassRecord = App.data.GetGlassByContractConservatoryItemNo(App.CurrentApp.HeaderRecord.udi_cont, item_no);

                if (App.net.GlassRecord == null)
                {
                    App.net.table_init.CreateGlass();
                    App.CurrentApp.GlassRecord.item_number = App.CurrentApp.ConsRecord.item_number;
                    App.CurrentApp.GlassRecord.parent_item = 4;
                    App.CurrentApp.loaded_item_number = App.CurrentApp.ConsRecord.item_number;
                    App.CurrentApp.root_item_number = App.CurrentApp.ConsRecord.item_number;
                    App.data.SaveHeader();
                }
                App.CurrentApp.CurrentItem = "glass";
                Navigation.PushAsync(new GlassItem(), false);
            }
        }

        private void additional_drawings_click(object sender, EventArgs e)
        {
            App.net.drawing_type = "cons_roof_under";
            if (App.net.ConsRecord.cons_roof_under_drawn == true)
            {
                App.net.drawing_edit_mode = true;
            }
            else
            {
                App.net.drawing_edit_mode = false;
                App.net.load_template_image = true;
            }
            Navigation.PushAsync(new DrawingPage(), false);
        }

        private void drawings_only_click(object sender, EventArgs e)
        {
            set_dimensions_visible_and_image();
        }

        private void units_clicked(object sender, EventArgs e)
        {
            set_pane_dimensions_visible();
        }

        private void rood_colour_changed(object sender, EventArgs e)
        {
            set_brown_warning_visible();
        }

        private string validate_page_0()
        {
            return no_repair_reason_entry.validation_error_string("Reason cannot be repaired\n")
                 + (replace_glass_area.IsVisible ? replace_glass_button.validation_error_string("Replace glass\n")
                                                   + (App.CurrentApp.ConsRecord.replace_glass == 1 && !App.net.ConsRecord.glass_complete ? "Replace glass details\n" : "")
                                                 : "")
                 + cause_of_damage_area.validation_error_string()
                 + WER_rating_picker.validation_error_string("WER Rating\n")
                 + type_of_roof_picker.validation_error_string("Type of roof\n")
                 + spars_lineup_button.validation_error_string("Spars mullions lineup\n")
                 + roof_condition_button.validation_error_string("Roof condition\n")
                 + material_type_button.validation_error_string("Material type\n")
                 + ridge_length_entry.validation_error_string("Ridge length\n")
                 + wall_position_picker.validation_error_string("Wall position\n")
                 + roof_soffit_button.validation_error_string("Roof under soffit\n")
                 + (additional_drawing_area.IsVisible && !App.net.ConsRecord.cons_roof_under_drawn ? "Additional drawing\n" : "")
                 + preset_drawings_button.validation_error_string("Preset drawings\n");
        }

        private string validate_page_1()
        {
            if (Page1.IsVisible)
            {
                return dimension_A.validation_error_string("Size A\n")
                     + dimension_B.validation_error_string("Size B\n")
                     + dimension_C.validation_error_string("Size C\n")
                     + (dimensions_D_to_G_plus_angles.IsVisible ?  dimension_D.validation_error_string("Size D\n")
                                                                 + dimension_E.validation_error_string("Size E\n")
                                                                 + dimension_F.validation_error_string("Size F\n")
                                                                 + dimension_G.validation_error_string("Size G\n")
                                                                 + angle_1.validation_error_string("Angle 1\n")
                                                                 + angle_2.validation_error_string("Angle 2\n")
                                                                 + angle_3.validation_error_string("Angle 3\n")
                                                                 + angle_4.validation_error_string("Angle 4\n")
                                                                : "");
            }
            else
                return "";
        }

        private string validate_page_2()
        {
            return pitch_height_entry.validation_error_string("Pitch Height\n")
                 + profile_section_size_entry.validation_error_string("Profile Section Size\n")
                 + pitch_degree_entry.validation_error_string("Pitch Degree\n")
                 + overall_length_entry.validation_error_string("Overall length of sheet\n")
                 + pane_dimensions_01.validation_error_string("Sheet width 1\n", "Sheet height 1\n")
                 + pane_dimensions_02.validation_error_string("Sheet width 2\n", "Sheet height 2\n")
                 + pane_dimensions_03.validation_error_string("Sheet width 3\n", "Sheet height 3\n")
                 + pane_dimensions_04.validation_error_string("Sheet width 4\n", "Sheet height 4\n")
                 + pane_dimensions_05.validation_error_string("Sheet width 5\n", "Sheet height 5\n")
                 + pane_dimensions_06.validation_error_string("Sheet width 6\n", "Sheet height 6\n")
                 + pane_dimensions_07.validation_error_string("Sheet width 7\n", "Sheet height 7\n")
                 + pane_dimensions_08.validation_error_string("Sheet width 8\n", "Sheet height 8\n")
                 + pane_dimensions_09.validation_error_string("Sheet width 9\n", "Sheet height 9\n")
                 + pane_dimensions_10.validation_error_string("Sheet width 10\n", "Sheet height 10\n");
        }

        private string validate_page_3()
        {
            return roof_glazing_thickness_picker.validation_error_string("Roof glazing thickness\n")
                 + sheet_colour_picker.validation_error_string("Sheet colour\n")
                 + roof_colour_picker.validation_error_string("Roof colour\n")
                 + flute_size_entry.validation_error_string("Flute size\n")
                 + new_firrings_button.validation_error_string("New firrings req.\n")
                 + new_gutters_button.validation_error_string("New gutters req.\n")
                 + summary_pto_area.validation_error_string();
        }

        protected override string validate_drawings_and_pictures()
        {
            return cause_of_damage_area.photo_validation_error_string(App.CurrentApp.ConsRecord.no_of_photos)
                 + (App.CurrentApp.ConsRecord.no_of_pics == 0 ? "Drawings\n" : "");
        }

        protected override string validate_page()
        {
            return validate_page_0()
            + validate_page_1()
            + validate_page_2()
            + validate_page_3();
        }

        /*
        protected override void set_page(int page_num)
        {
            if (Children.Contains(Page1))
                switch (page_num)
                {
                    case 0: CurrentPage = Page0; break;
                    case 1: CurrentPage = Page1; break;
                    case 2: CurrentPage = Page2; break;
                    case 3: CurrentPage = Page3; break;
                }
            else
                switch (page_num)
                {
                    case 0: CurrentPage = Page0; break;
                    case 1: CurrentPage = Page2; break;
                    case 2: CurrentPage = Page3; break;
                }
        }
        */

        protected override void save_item(bool complete)
        {
            App.data.SaveCons(complete);
        }

        protected override bool already_signed()
        {
            return App.CurrentApp.ConsRecord.bDifferentFromOriginal;
        }
    }
}