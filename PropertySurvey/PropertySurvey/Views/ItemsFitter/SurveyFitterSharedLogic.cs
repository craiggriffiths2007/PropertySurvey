using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PropertySurvey
{
    public static class SurveyFitterButtonLists
    {
        public static List<string> shared_gasket_button_list = new List<string>() { "...", "Code:-", "Other", "None" };
        public static List<string> shared_handle_operation_list = new List<string>() { "...", "Lever/lever", "Lever pad", "Split spindle", "Long bar" };
        public static List<string> shared_in_out_button_list = new List<string>() { "...", "In", "Out" };
        public static List<string> shared_inside_outside_button_list = new List<string>() { "...", "Inside", "Outside" };
        public static List<string> shared_internal_external_button_list = new List<string>() { "...", "Internal", "External" };
        public static List<string> shared_repair_replace_list = new List<string>() { "...", "Repair", "Replace" };
        public static List<string> shared_temporary_button_list = new List<string>() { "...", "Glass", "Door", "Board", "None" };

        // Lists where the items and send/recieve use a different list - usually one is a version of the other with shortened strings.
        public static List<string> shared_profile_type_button_list = new List<string> { "...", "Chamfered (Standard)", "Rustic (Sculptured)" };
        public static List<string> shortened_profile_type_button_list = new List<string> { "...", "Chamfered", "Rustic" };
        public static List<string> shared_single_or_double_list = new List<string> { "...", "Single glazed", "Double glazed", "Triple glazed", "Encapsulated unit" };
        public static List<string> shortened_single_or_double_list = new List<string> { "...", "Single", "Double", "Triple", "Encapsulated unit" };

        public static List<string> aluminium_cill_type_list = new List<string>() { "...", "150", "180", "Stubb" };
        public static List<string> aluminium_section_type_list = new List<string>() { "...", "Monoframe", "Thermal Break", "Commercial" };
        public static List<string> aluminium_frame_type_list = new List<string>() { "...", "Odd Leg", "Even Leg", "Commercial" };
        public static List<string> aluminium_night_vent_list = new List<string>() { "...", "In Doors", "In Alu Sub-frame", "In Timber Sub-frame", "None" };
        public static List<string> aluminium_glazed_button_list = new List<string>() { "...", "Internal", "External", "Wrap around" };
        public static List<string> aluminium_bead_type_list = new List<string>() { "...", "Chamfered", "Sculpted", "Square" };

        public static List<string> composite_is_lock_button_list = new List<string>() { "...", "Standard", "Other" };
        public static List<string> composite_hinged_on_button_list = new List<string>() { "...", "Left", "Right" };

        public static List<string> conservatory_material_type_list = new List<string>() { "...", "Aluminium", "Timber", "UPVC" };

        public static List<string> garage_subframe_colour_list = new List<string>() { "...", "Timber", "Metal" };
        public static List<string> garage_metal_subframe_list = new List<string>() { "...", "Metal" };
        public static List<string> garage_timber_subframe_list = new List<string>() { "...", "None", "HW", "SW", "Metal" };
        public static List<string> garage_frame_fix_type_list = new List<string>() { "...", "Back Fix", "In-between Fix" };
        public static List<string> garage_motor_position_list = new List<string>() { "...", "None", "Left", "Right", "Centre" };
        public static List<string> garage_opening_direction_list = new List<string>() { "...", "Open In", "Open Out" };

        public static List<string> glass_glaze_button_list = new List<string> { "...", "Internal", "External", "Wraparound" };

        public static List<string> greenhouse_type_of_opening_list = new List<string> { "...", "Automatic", "Manual" };

        public static List<string> timber_glazed_button_list = new List<string>() { "...", "Internal", "External", "No" };

        public static List<string> upvc_double_or_triple_list = new List<string> { "...", "Double glazed", "Triple glazed", "Encapsulated unit" };
        public static List<string> upvc_opens_list = new List<string> { "...", "In", "Out", "Fixed" };
        public static List<string> upvc_trickle_vents_button_list = new List<string> { "...", "None", "Head of frame", "Top of sash" };
        public static List<string> upvc_lock_type_button_list = new List<string> { "...", "Internal", "Internal + external" };
        public static List<string> upvc_extended_lock_type_button_list = new List<string> { "...", "Internal Lock", "Internal + External Lock" }; // For send/receive

        public static List<string> georgian_bar_anti_rattle_button_list = new List<string>() { "", "None", "Square peg", "Round peg" }; // Don't need "..." for view or send.
    }

    public static class SurveyFitterSharedLogic
    {
        private static bool not_blank_or_none(string value)
        {
            return value != "" && value != "None";
        }

        public static string boolean_as_yesno(bool value)
        {
            return value ? "Yes" : "No";
        }

        public static bool addon_dimensions_visible (int addons)
        {
            return addons == 1;
        }

        public static bool gaskets_visible(bool repair)
        {
            return repair;
        }

        public static bool gaskets_text_visible(bool repair, int gaskets)
        {
            return SurveyFitterSharedLogic.gaskets_visible(repair) && gaskets > 0 && gaskets < 3;
        }

        public static bool handles_visible(bool repair)
        {
            return repair;
        }

        public static bool handles_text_visible(bool repair, int handles_required)
        {
            return handles_visible(repair) && handles_required == 1;
        }

        public static string handles_validation_error_text(int handles_required, string handles_text, bool drawing_complete)
        {
            return (handles_required == 0 ? "Handles required\n" : "")
                 + (handles_required == 1 && handles_text == "" ? "Handles required text\n" : "")
                 + (handles_required == 1 && !drawing_complete ? "Handles drawing\n" : "");
        }

        public static bool reason_not_repaired_visible(int repair_replace)
        {
            return repair_replace == 2;
        }

        public static bool reason_not_repaired_visible(bool repair)
        {
            return !repair;
        }

        public static bool replace_glass_visible(bool repair)
        {
            return repair;
        }

        public static bool replace_glass_visible(int repair_or_replace)
        {
            return repair_or_replace == 1;
        }

        public static bool temporary_visible(int copy_and_collect)
        {
            return copy_and_collect == 1;
        }

        public static bool WER_rating_visible(bool repair, bool fensa)
        {
            return (!repair) && fensa;
        }

        public static string drawing_filename(drawing_file_types drawing_parent)
        {
            string filename;

            switch (drawing_parent)
            {
                case drawing_file_types.dft_handle:
                    filename = string.Format("{0:00000000}_dAT", App.net.HeaderRecord.udi_cont)
                             + string.Format("{0:000}lk.jpg", App.net.root_item_number);
                    break;

                case drawing_file_types.dft_garage_roller_open:
                    filename = string.Format("{0:00000000}_dRG", App.net.HeaderRecord.udi_cont)
                             + string.Format("{0:000}00.jpg", App.net.root_item_number);
                    break;

                case drawing_file_types.dft_conservatory_roof_under:
                    filename = string.Format("{0:00000000}_dCR", App.net.HeaderRecord.udi_cont)
                            + string.Format("{0:000}00.jpg", App.net.root_item_number);
                    break;

                case drawing_file_types.dft_timber_section:
                    filename = string.Format("{0:00000000}_dDT", App.net.HeaderRecord.udi_cont)
                             + string.Format("{0:000}00.jpg", App.net.root_item_number);
                    break;

                case drawing_file_types.dft_timber_beading:
                    filename = string.Format("{0:00000000}_dBT", App.net.HeaderRecord.udi_cont)
                             + string.Format("{0:000}00.jpg", App.net.root_item_number);
                    break;
                case drawing_file_types.dft_timber_sash:
                    filename = string.Format("{0:00000000}_dET", App.net.HeaderRecord.udi_cont)
                             + string.Format("{0:000}00.jpg", App.net.root_item_number);
                    break;

                default:
                    return "";
            }

            return "Drawings/" + filename;
        }

        public static int count_drawings()
        {
            string fname = string.Format("{0:00000000}_dAU", App.net.HeaderRecord.udi_cont)
                         + string.Format("{0:000}??.jpg", App.net.root_item_number);

            return App.files.GetFileList("Drawings/", fname).Count;
        }

        public static int count_photos()
        {
            string fname = string.Format("{0:00000000}_cAZ", App.net.HeaderRecord.udi_cont)
                         + string.Format("{0:000}??.jpg", App.net.root_item_number);

            return App.files.GetFileList("Photos/", fname).Count;
        }

        public static bool aluminium_flat_visible()
        {
            return App.net.AlumRecord.bRepair
                && (App.net.AlumRecord.type == "Door" || App.net.AlumRecord.type == "French Doors");
        }

        public static bool aluminium_additional_locks_visible()
        {
            return App.net.AlumRecord.bRepair;
        }

        public static bool aluminium_new_lock_visible()
        {
            return App.net.AlumRecord.bRepair
                && (App.net.AlumRecord.type == "Door"
                    || App.net.AlumRecord.type == "Window"
                    || App.net.AlumRecord.type == "Combi Frame"
                    || App.net.AlumRecord.type == "French Doors");
        }

        public static bool aluminium_new_lock_make_visible()
        {
            // App.net.AlumRecord.bRepair does not need testing here as it's part of aluminium_new_lock_visible()
            return aluminium_new_lock_visible() && (App.net.AlumRecord.bNewLockingMech == 1);
        }

        public static bool aluminium_replace_panel_visible()
        {
            return App.net.AlumRecord.bRepair;
        }

        public static bool aluminium_reason_for_replacement_visible()
        {
            return !App.net.AlumRecord.bRepair;
        }

        public static bool aluminium_why_not_repaired_visible()
        {
            return !App.net.AlumRecord.bRepair;
        }

        public static bool aluminium_new_sub_frame_visible()
        {
            return App.net.AlumRecord.section_type == 2;
        }

        public static bool aluminium_subframe_details_visible()
        {
            return aluminium_new_sub_frame_visible() && App.CurrentApp.AlumRecord.new_timber_sub_frame == 2;
        }

        public static bool aluminium_cill_type_visible()
        {
            return App.CurrentApp.AlumRecord.cill_on_subframe == 1;
        }

        public static bool aluminium_frame_type_visible()
        {
            return App.CurrentApp.AlumRecord.type == "Window" && App.CurrentApp.AlumRecord.section_type == 2;
        }

        public static string aluminium_frame_type_label()
        {
            if (App.CurrentApp.AlumRecord.cill_on_subframe == 1)
                return "Aluminium Subframe";
            else
                return "Cill";
        }

        public static bool aluminium_bead_type_visible()
        {
            return App.net.AlumRecord.glazed == 1  // Glazed == Internal
                || App.net.AlumRecord.glazed == 2; // Glazed == External
        }

        public static bool aluminium_midrail_type_visible()
        {
            return App.net.AlumRecord.type != "Window";
        }

        public static bool aluminium_midrail_height_visible()
        {
            return aluminium_midrail_type_visible()
                && App.CurrentApp.AlumRecord.midrail_type != ""
                && App.CurrentApp.AlumRecord.midrail_type != "None";
        }

        public static bool aluminium_letterbox_and_petflap_visible()
        {
            return App.net.AlumRecord.type != "Window";
        }

        public static bool aluminium_opens_visible()
        {
            return App.CurrentApp.AlumRecord.type != "Conventional Sliding Patio";
        }

        public static bool aluminium_handle_operation_visible()
        {
            return App.CurrentApp.AlumRecord.type == "Door";
        }

        public static bool aluminium_glass_questions_visible()
        {
            return not_blank_or_none(App.CurrentApp.AlumRecord.glass_pattern);
        }

        public static bool aluminium_sub_frame_visible()
        {
            return App.CurrentApp.AlumRecord.section_type == 2;
        }

        public static bool aluminium_doc_L_visible()
        {
            return not_blank_or_none(App.CurrentApp.AlumRecord.glass_pattern);
        }

        public static bool aluminium_spacer_questions_visible()
        {
            return not_blank_or_none(App.CurrentApp.AlumRecord.glass_pattern);
        }

        public static bool bifold_hardware_visible()
        {
            return App.net.BifoldRecord.door_type == "Warmcore";
        }

        public static bool bifold_handle_colour_visible()
        {
            return App.net.BifoldRecord.door_type == "Schuco"
                || App.net.BifoldRecord.door_type == "Smarts"
                || App.net.BifoldRecord.door_type == "KAT PVCu Bifold Doors";
        }

        public static bool glazing_options_visible()
        {
            return App.net.BifoldRecord.door_type == "Warmcore"
                || App.net.BifoldRecord.door_type == "Schuco"
                || App.net.BifoldRecord.door_type == "Smarts";
        }

        public static bool knock_on_visible()
        {
            return App.net.BifoldRecord.door_type == "KAT PVCu Bifold Doors";
        }

        public static void bifold_set_cill_image(Image cill_image)
        {
            bool show_image = true;

            switch (App.net.BifoldRecord.threshold_type)
            {
                case "Part M low threshold": cill_image.Source = "partm.jpg"; break;
                case "Frame - no cill": cill_image.Source = "nocill.jpg"; break;
                case "Frame - 150mm cill": cill_image.Source = "cill.jpg"; break;
                case "XPIO Rebated": cill_image.Source = "xpiorebated.jpg"; break;
                case "XPIO Low Threshold": cill_image.Source = "xpiolowthreshold.jpg"; break;
                case "XPIO Rebate and Cill": cill_image.Source = "xp10rebateandcill.jpg"; break;
                case "XPIO Low Threshold and Cill": cill_image.Source = "xp10lowthresholdandcill.jpg"; break;
                case "XP View Rebate": cill_image.Source = "xpviewrebated.jpg"; break;
                case "XP View Rebate and Cill": cill_image.Source = "xpviewrebateandcill.jpg"; break;
                case "PVC threshold": cill_image.Source = "kat_cill_standard.png"; break;
                case "Aluminium low 30mm": cill_image.Source = "kat_cill_room.png"; break;
                case "Doc. M comp. ramp Internal":
                case "Doc. M comp. ramp External": cill_image.Source = "kat_cill_low.png"; break;
                case "Rebated on a cill": cill_image.Source = "rebate_on_sill.png"; break;
                case "Rebated on a 150 cill": cill_image.Source = "rebate_on_sill.png"; break;
                case "Rebated on a 180 cill": cill_image.Source = "rebate_on_sill.png"; break;
                case "Rebated (Standard)": cill_image.Source = "rebated_standard.png"; break;
                case "Low threshold (Internal)": cill_image.Source = "low_threshold.png"; break;
                case "DV171 Low Threshold": cill_image.Source = "smarts1.png"; break;
                case "DV171 Low Threshold with Cill": cill_image.Source = "smarts2.png"; break;
                case "DV173 Low Threshold": cill_image.Source = "smarts3.png"; break;
                case "DV175 Low Threshold": cill_image.Source = "smarts4.png"; break;
                case "DV173 Low Threshold with Cill": cill_image.Source = "smarts5.png"; break;
                case "Standard Rebated Threshold in": cill_image.Source = "smarts6.png"; break;
                case "Standard Rebated Threshold with Cill in": cill_image.Source = "smarts7.png"; break;
                case "Standard Rebated Threshold out": cill_image.Source = "smarts8.png"; break;
                case "Standard Rebated Threshold with Cill out": cill_image.Source = "smarts9.png"; break;
                default: show_image = false; break;
            }

            cill_image.IsVisible = show_image;
        }

        public static void bifold_set_door_text_and_image(Label no_of_doors_text, Image type_image)
        {
            switch (App.CurrentApp.BifoldRecord.number_of_doors_text)
            {
                case "1a": no_of_doors_text.Text = "1 Traffic Door Right to Left"; type_image.Source = "bf1a.jpg"; break;
                case "1b": no_of_doors_text.Text = "1 Traffic Door Left to Right"; type_image.Source = "bf1b.jpg"; break;
                case "2a": no_of_doors_text.Text = "2 Section Left to Right"; type_image.Source = "bf2a.jpg"; break;
                case "2b": no_of_doors_text.Text = "2 Section Right to Left"; type_image.Source = "bf2b.jpg"; break;
                case "2c": no_of_doors_text.Text = "2 Section Centre Opening"; type_image.Source = "bf2c.jpg"; break;
                case "3a": no_of_doors_text.Text = "3 Section Left to Right"; type_image.Source = "bf3a.jpg"; break;
                case "3a2": no_of_doors_text.Text = "3 Section Left to Right"; type_image.Source = "bf3a2.jpg"; break;
                case "3b": no_of_doors_text.Text = "3 Section Right to Left"; type_image.Source = "bf3b.jpg"; break;
                case "3b2": no_of_doors_text.Text = "3 Section Right to Left"; type_image.Source = "bf3b2.jpg"; break;
                case "4a": no_of_doors_text.Text = "4 Section Left to Right"; type_image.Source = "bf4a.jpg"; break;
                case "4b": no_of_doors_text.Text = "4 Section Right to Left"; type_image.Source = "bf4b.jpg"; break;
                case "4c": no_of_doors_text.Text = "4 Section (1 to Left, 3 to Right)"; type_image.Source = "bf4c.jpg"; break;
                case "4d": no_of_doors_text.Text = "4 Section (1 to Right, 3 to Left)"; type_image.Source = "bf4d.jpg"; break;
                case "5a": no_of_doors_text.Text = "5 Section Left to Right"; type_image.Source = "bf5a.jpg"; break;
                case "5a2": no_of_doors_text.Text = "5 Section Left to Right"; type_image.Source = "bf5a2.jpg"; break;
                case "5b": no_of_doors_text.Text = "5 Section Right to Left"; type_image.Source = "bf5b.jpg"; break;
                case "5b2": no_of_doors_text.Text = "5 Section Right to Left"; type_image.Source = "bf5b2.jpg"; break;
                case "5c": no_of_doors_text.Text = "5 Section (2 to Left, 3 to Right)"; type_image.Source = "bf5c.jpg"; break;
                case "5d": no_of_doors_text.Text = "5 Section (3 to Left, 2 to Right)"; type_image.Source = "bf5d.jpg"; break;
                case "6a": no_of_doors_text.Text = "6 Section Left to Right"; type_image.Source = "bf6a.jpg"; break;
                case "6b": no_of_doors_text.Text = "6 Section Right to Left"; type_image.Source = "bf6b.jpg"; break;
                case "6c": no_of_doors_text.Text = "6 Section (1 to Left, 5 to Right)"; type_image.Source = "bf6c.jpg"; break;
                case "6d": no_of_doors_text.Text = "6 Section (5 to Left, 1 to Right)"; type_image.Source = "bf6d.jpg"; break;
                case "6e": no_of_doors_text.Text = "6 Section (3 to Left, 3 to Right)"; type_image.Source = "bf6e.jpg"; break;
                case "7a": no_of_doors_text.Text = "7 Section Left to Right"; type_image.Source = "bf7a.jpg"; break;
                case "7a2": no_of_doors_text.Text = "7 Section Left to Right"; type_image.Source = "bf7a2.jpg"; break;
                case "7b": no_of_doors_text.Text = "7 Section Right to Left"; type_image.Source = "bf7b.jpg"; break;
                case "7b2": no_of_doors_text.Text = "7 Section Right to Left"; type_image.Source = "bf7b2.jpg"; break;
                case "7c": no_of_doors_text.Text = "7 Section (2 to Left, 5 to Right)"; type_image.Source = "bf7c.jpg"; break;
                case "7d": no_of_doors_text.Text = "7 Section (5 to Left, 2 to Right)"; type_image.Source = "bf7d.jpg"; break;
                case "7e": no_of_doors_text.Text = "7 Section (3 to Left, 4 to Right)"; type_image.Source = "bf7e.jpg"; break;
                case "7f": no_of_doors_text.Text = "7 Section (4 to Left, 3 to Right)"; type_image.Source = "bf7f.jpg"; break;

                case "202": no_of_doors_text.Text = "2.0.2"; type_image.Source = "kat_202.png"; break;
                case "202b": no_of_doors_text.Text = "2.0.2b"; type_image.Source = "kat_202b.png"; break;
                case "321": no_of_doors_text.Text = "3.2.1"; type_image.Source = "kat_321.png"; break;
                case "330": no_of_doors_text.Text = "3.3.0"; type_image.Source = "kat_330.png"; break;
                case "321b": no_of_doors_text.Text = "3.2.1b"; type_image.Source = "kat_321b.png"; break;
                case "330b": no_of_doors_text.Text = "3.3.0b"; type_image.Source = "kat_330b.png"; break;
                case "413": no_of_doors_text.Text = "4.1.3"; type_image.Source = "kat_413.png"; break;
                case "422": no_of_doors_text.Text = "4.2.2"; type_image.Source = "kat_422.png"; break;
                case "440": no_of_doors_text.Text = "4.4.0"; type_image.Source = "kat_440.png"; break;
                case "413b": no_of_doors_text.Text = "4.1.3b"; type_image.Source = "kat_413b.png"; break;
                case "422b": no_of_doors_text.Text = "4.2.2b"; type_image.Source = "kat_422b.png"; break;
                case "440b": no_of_doors_text.Text = "4.4.0b"; type_image.Source = "kat_440b.png"; break;
                case "532": no_of_doors_text.Text = "5.3.2"; type_image.Source = "kat_532.png"; break;
                case "541": no_of_doors_text.Text = "5.4.1"; type_image.Source = "kat_541.png"; break;
                case "550": no_of_doors_text.Text = "5.5.0"; type_image.Source = "kat_550.png"; break;
                case "532b": no_of_doors_text.Text = "5.3.2b"; type_image.Source = "kat_532b.png"; break;
                case "541b": no_of_doors_text.Text = "5.4.1b"; type_image.Source = "kat_541b.png"; break;
                case "550b": no_of_doors_text.Text = "5.5.0b"; type_image.Source = "kat_550bpng"; break;
                case "615": no_of_doors_text.Text = "6.1.5"; type_image.Source = "kat_615.png"; break;
                case "624": no_of_doors_text.Text = "6.2.4"; type_image.Source = "kat_642.png"; break;
                case "633": no_of_doors_text.Text = "6.3.3"; type_image.Source = "kat_633.png"; break;
                case "660": no_of_doors_text.Text = "6.6.0"; type_image.Source = "kat_660.png"; break;
                case "615b": no_of_doors_text.Text = "6.1.5b"; type_image.Source = "kat_615b.png"; break;
                case "624b": no_of_doors_text.Text = "6.2.4b"; type_image.Source = "kat_624b.png"; break;
                case "633b": no_of_doors_text.Text = "6.3.3b"; type_image.Source = "kat_633b.png"; break;
                case "660b": no_of_doors_text.Text = "6.6.0b"; type_image.Source = "kat_660b.png"; break;
                case "770": no_of_doors_text.Text = "7.7.0"; type_image.Source = "kat_770.png"; break;
                case "770b": no_of_doors_text.Text = "7.7.0b"; type_image.Source = "kat_770b.png"; break;

                default: no_of_doors_text.Text = ""; type_image.Source = null; break;
            }
        }

        public static bool composite_other_lock_text_visible()
        {
            return App.net.CompRecord.is_lock == 2;
        }

        public static bool composite_addon_dimensions_visible()
        {
            return App.net.CompRecord.addons == 1;
        }

        public static bool composite_glass_questions_visible()
        {
            return not_blank_or_none(App.net.CompRecord.glass_pattern);
        }

        public static bool conservatory_wall_position_visible()
        {
            return App.net.ConsRecord.type == "Ultralight 500"
                || App.net.ConsRecord.type == "Flat"
                || App.net.ConsRecord.type == "Edwardian";
        }

        public static bool conservatory_dimensions_page_visible()
        {
            return App.net.ConsRecord.bDrawingsOnly != 2
                && (App.net.ConsRecord.type == "Flat"
                    || App.net.ConsRecord.type == "Ultralight 500"
                    || App.net.ConsRecord.type == "Victorian"
                    || App.net.ConsRecord.type == "Edwardian");
        }

        public static bool conservatory_dimensions_d_to_g_visible()
        {
            return App.net.ConsRecord.bDrawingsOnly != 2 && App.net.ConsRecord.type == "Victorian";
        }

        public static bool conservatory_spars_lineup_visible()
        {
            return App.CurrentApp.ConsRecord.type != "Ultralight 500";
        }

        public static bool conservatory_flute_size_visible()
        {
            return App.net.ConsRecord.replace_glass != 1 && App.CurrentApp.ConsRecord.type != "Ultralight 500";
        }

        public static bool conservatory_roof_glazing_thickness_visible()
        {
            return App.net.ConsRecord.replace_glass != 1 && App.CurrentApp.ConsRecord.type != "Ultralight 500";
        }

        public static bool conservatory_roof_sheets_visible()
        {
            return App.net.ConsRecord.replace_glass != 1 && App.CurrentApp.ConsRecord.type != "Ultralight 500";
        }

        public static bool conservatory_sheet_colour_visible()
        {
            return App.net.ConsRecord.replace_glass != 1 && App.CurrentApp.ConsRecord.type != "Ultralight 500";
        }

        public static string conservatory_dimension_image()
        {
            if (App.net.ConsRecord.type == "Flat"
                || App.net.ConsRecord.type == "Ultralight 500")
            {
                switch (App.net.ConsRecord.wall_pos)
                {
                    case "Left": return "Edward_wallleft.jpg";
                    case "Right": return "Edward_wallright.jpg";
                    case "Both": return "Edward_both.jpg";
                        // default: let it default to Edward.jpg
                }
            }
            else if (SurveyFitterSharedLogic.conservatory_dimensions_d_to_g_visible())
                return "Victorian.jpg";

            return "Edward.jpg";
        }

        public static bool conservatory_ridge_length_visible()
        {
            return App.CurrentApp.ConsRecord.type != "Ultralight 500";
        }

        public static bool garage_outside_obstruction_detail_visible()
        {
            return App.net.GarageRecord.obstruction_outside_b == 1;
        }

        public static bool garage_inside_obstruction_detail_visible()
        {
            return App.net.GarageRecord.obstruction_inside_b == 1;
        }

        public static List<string> garage_subframe_list()
        {
            if (App.net.GarageRecord.door_fits_into == 2)
                return SurveyFitterButtonLists.garage_metal_subframe_list;
            else
                return SurveyFitterButtonLists.garage_timber_subframe_list;
        }

        public static bool garage_hardwired_visible()
        {
            return App.net.GarageRecord.new_electric_operator_req == "Yes";
        }

        public static bool garage_socket_within_1m_visible()
        {
            return garage_hardwired_visible() && App.net.GarageRecord.wire_type == "Hard Wired";
        }

        public static bool garage_motor_position_visible()
        {
            return App.net.GarageRecord.electric_door == 1;
        }

        public static bool garage_safety_release_required_visible()
        {
            return App.net.GarageRecord.electric_door == 1 && App.net.GarageRecord.other_access == 2;
        }

        public static bool garage_roller_door_questions_visible()
        {
            return App.net.GarageRecord.opening_type == "Roller Door";
        }

        public static bool garage_roll_box_visible()
        {
            return garage_roller_door_questions_visible() && App.net.GarageRecord.roller_door_type == "Thermaglide 77";
        }

        public static bool garage_colour_match_visible()
        {
            return garage_roll_box_visible()
                && (App.net.GarageRecord.roller_box_type == "Full roll box" || App.net.GarageRecord.roller_box_type == "Half roll box");
        }

        public static bool garage_additional_drawings_visible()
        {
            return App.CurrentApp.GarageRecord.opening_type == "Roller Door"
                && (App.net.GarageRecord.roller_door_type == "Thermaglide 77" || App.net.GarageRecord.roller_door_type == "Thermaglide 55");
        }

        public static bool garage_opening_direction_visible()
        {
            return App.net.GarageRecord.opening_type == "Side Hung";
        }

        public static bool garage_where_is_garage_visible()
        {
            return App.net.GarageRecord.door_within_perimeter == 2;
        }

        public static bool glass_internal_dimensions_visible()
        {
            return App.net.GlassRecord.stepped_unit == 1;
        }

        public static bool glass_only_items_visible()
        {
            return App.net.GlassRecord.parent_item == 0;
        }

        public static bool glass_glazing_type_visible()
        {
            return App.net.GlassRecord.ProductInto == "Timber";
        }

        public static bool glass_seal_visible()
        {
            return App.net.GlassRecord.ProductInto == "UPVC";
        }

        public static bool glass_stepped_unit_visible()
        {
            // Keep for glass only and for glass within timber, hide for everything else
            return App.net.GlassRecord.parent_item == 0 ||  App.net.GlassRecord.parent_item == 6;
        }

        public static bool glass_docL_and_spacer_visible()
        {
            return App.net.GlassRecord.single_or_double >= 2;
        }

        public static bool glass_special_glass_visible()
        {
            return App.net.GlassRecord.units_required < 2;
        }

        public static bool greenhouse_type_of_glass_visible()
        {
            return App.net.GreenRecord.glaze_type == "Glass";
        }

        public static bool greenhouse_replace_reason_visible()
        {
            return App.net.GreenRecord.repair_or_replace == 2;
        }

        public static bool greenhouse_type_of_opening_visible()
        {
            return App.net.GreenRecord.roof_opening_lights == 1;
        }

        public static bool locking_page_number_visible()
        {
            return !App.net.LockingRecord.bMulti;
        }

        public static bool locking_multi_lock_visible()
        {
            return App.net.LockingRecord.bMulti;
        }

        public static bool locking_lock_colour_visible()
        {
            return !App.net.LockingRecord.bMulti;
        }

        public static bool panel_knocker_colour_visible()
        {
            return not_blank_or_none(App.CurrentApp.PanelRecord.knockedit);
        }

        public static bool panel_only_visible()
        {
            return App.net.PanelRecord.upvc_item_number == 0
                && App.net.PanelRecord.alum_item_number == 0;
        }

        public static bool panel_glass_and_spacer_visible()
        {
            return not_blank_or_none(App.CurrentApp.PanelRecord.backgedit);
        }

        public static bool timber_flat_question_visible()
        {
            return App.net.TimberRecord.timber_item == "Door" || App.net.TimberRecord.timber_item == "French Doors";
        }

        public static bool timber_additional_locks_visible()
        {
            return App.net.TimberRecord.bRepair;
        }

        public static bool timber_new_lock_visible()
        {
            return App.net.TimberRecord.bRepair
                && (App.net.TimberRecord.timber_item == "Door"
                    || App.net.TimberRecord.timber_item == "Window"
                    || App.net.TimberRecord.timber_item == "French Doors");
        }

        public static bool timber_new_lock_make_visible()
        {
            return timber_new_lock_visible() && (App.net.TimberRecord.bNewLockingMech == 1);
        }

        public static bool timber_door_wood_visible()
        {
            return App.net.TimberRecord.timber_item != "" && App.net.TimberRecord.timber_item != "Window";
        }

        public static bool timber_is_a_door()
        {
            return App.net.TimberRecord.timber_item == "Door"
                || App.net.TimberRecord.timber_item == "French Doors"
                || App.net.TimberRecord.timber_item == "Combi Frame";
        }

        public static bool timber_hinges_visible()
        {
            return App.net.TimberRecord.timber_item == "Door";
        }

        public static bool timber_docL_reason_visible()
        {
            return App.net.TimberRecord.doc_l_compliant == 2;
        }

        public static bool timber_letterbox_and_petflap_visible()
        {
            return App.net.TimberRecord.timber_item == "Door" || App.net.TimberRecord.timber_item == "French Doors";
        }

        public static bool timber_reason_door_size_not_standard_visible()
        {
            return timber_is_a_door() && (App.net.TimberRecord.standard_sizes == "Non standard");
        }

        public static bool timber_single_or_double_visible()
        {
            return not_blank_or_none(App.net.TimberRecord.glass_pattern);
        }

        public static bool timber_door_slides_on_visible()
        {
            return App.net.TimberRecord.timber_item == "Conventional Sliding Patio";
        }

        public static bool timber_spacer_visible()
        {
            return not_blank_or_none(App.net.TimberRecord.glass_pattern) && App.net.TimberRecord.single_double > 1;
        }

        public static bool timber_fire_rated_visible()
        {
            return App.net.TimberRecord.glass_pattern != "None"
                && (App.net.TimberRecord.timber_item == "Door"
                    || App.net.TimberRecord.timber_item == "French Doors");
        }

        public static bool timber_door_colours_visible()
        {
            return App.net.TimberRecord.timber_item != "" && App.net.TimberRecord.timber_item != "Window";
        }

        public static bool timber_door_colour_code_visible(string colour, bool parent_visibility)
        {
            return parent_visibility
                && (  colour == "Stain"
                   || colour == "Standard"
                   || colour == "Full range");
        }

        public static bool timber_glass_type_visible()
        {
            return not_blank_or_none(App.net.TimberRecord.glass_pattern);
        }

        public static bool upvc_collect_and_copy_visible()
        {
            return App.net.UPVCRecord.upvc_item == "Door" || App.net.UPVCRecord.upvc_item == "French Doors";
        }

        public static bool upvc_additional_locks_visible()
        {
            return App.CurrentApp.UPVCRecord.bRepair;
        }

        public static bool upvc_new_locking_mechanism_visible()
        {
            return App.CurrentApp.UPVCRecord.bRepair
                && (   App.net.UPVCRecord.upvc_item == "Door"
                    || App.net.UPVCRecord.upvc_item == "Window"
                    || App.net.UPVCRecord.upvc_item == "French Doors"
                    || App.net.UPVCRecord.upvc_item == "Porch"
                    || App.net.UPVCRecord.upvc_item == "Conservatory"
                    || App.net.UPVCRecord.upvc_item == "Combi Frame"
                    || App.net.UPVCRecord.upvc_item == "Bay Window");
        }

        public static bool upvc_new_lock_make_visible()
        {
            return upvc_new_locking_mechanism_visible() && (App.net.UPVCRecord.bNewLockingMech == 1);
        }

        public static bool upvc_replace_panel_visible()
        {
            return App.CurrentApp.UPVCRecord.bRepair;
        }

        public static bool upvc_cosmetic_damage_visible()
        {
            return App.CurrentApp.UPVCRecord.bRepair;
        }

        public static bool upvc_hinge_colour_visible()
        {
            return App.CurrentApp.UPVCRecord.upvc_item == "Door"
                || App.CurrentApp.UPVCRecord.upvc_item == "French Doors"
                || App.CurrentApp.UPVCRecord.upvc_item == "Combi Frame"
                || App.CurrentApp.UPVCRecord.upvc_item == "Porch"
                || App.CurrentApp.UPVCRecord.upvc_item == "Conservatory";
        }

        public static bool upvc_handle_operation_visible()
        {
            return App.CurrentApp.UPVCRecord.upvc_item == "Door"
                || App.CurrentApp.UPVCRecord.upvc_item == "Combi Frame"
                || App.CurrentApp.UPVCRecord.upvc_item == "Porch";
        }

        public static bool upvc_midrail_visible()
        {
            return App.CurrentApp.UPVCRecord.upvc_item != "Window"
                && App.CurrentApp.UPVCRecord.upvc_item != "Bay Window";
        }

        public static bool upvc_midrail_height_visible()
        {
            return upvc_midrail_visible() && App.CurrentApp.UPVCRecord.midrail == 1;
        }

        public static bool upvc_locking_type_visible()
        {
            return App.net.UPVCRecord.upvc_item == "Door"
                || App.net.UPVCRecord.upvc_item == "Combi Frame"
                || App.net.UPVCRecord.upvc_item == "Porch"
                || App.net.UPVCRecord.upvc_item == "Conservatory";
        }

        public static bool upvc_patio_lock_type_visible()
        {
            return App.CurrentApp.UPVCRecord.upvc_item == "Tilt & Slide Patio";
        }

        public static bool upvc_letterbox_and_petflap_visible()
        {
            return App.net.UPVCRecord.upvc_item == "Door"
                || App.net.UPVCRecord.upvc_item == "French Doors"
                || App.net.UPVCRecord.upvc_item == "Combi Frame"
                || App.net.UPVCRecord.upvc_item == "Porch"
                || App.net.UPVCRecord.upvc_item == "Conservatory";
        }

        public static bool upvc_glass_questions_visible()
        {
            return not_blank_or_none(App.CurrentApp.UPVCRecord.glass_pattern);
        }

        public static bool upvc_threshold_visible()
        {
            return App.net.UPVCRecord.upvc_item == "Door"
                || App.net.UPVCRecord.upvc_item == "Combi Frame"
                || App.net.UPVCRecord.upvc_item == "Porch"
                || App.net.UPVCRecord.upvc_item == "French Doors"
                || App.net.UPVCRecord.upvc_item == "Conventional Sliding Patio";
        }

        public static bool upvc_spacer_questions_visible()
        {
            return not_blank_or_none(App.CurrentApp.UPVCRecord.glass_pattern);
        }

        public static bool upvc_slide_position_visible()
        {
            return App.CurrentApp.UPVCRecord.upvc_item == "Conventional Sliding Patio"
                || App.CurrentApp.UPVCRecord.upvc_item == "Tilt & Slide Patio";
        }
    }
}
