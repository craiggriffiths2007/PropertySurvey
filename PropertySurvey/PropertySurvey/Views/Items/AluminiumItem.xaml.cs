using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AluminiumItem : CarouselValidate
    {
        public AluminiumItem()
        {
            InitializeComponent();
            BindingContext = App.net.AlumRecord as AlumTable;
            save_on_pagechange = true;
            //SetPageNumber();

            // Page 0
            aluminium_item_picker.SetPickerItems(App.CurrentApp.type_of_item);
            additional_locks_picker.SetPickerItems(App.CurrentApp.additional_locks_alum);
            lock_make_picker.SetPickerItems(App.CurrentApp.lock_make);
            reason_for_replacement_picker.SetPickerItems(App.CurrentApp.replace_reason);
            WER_rating_picker.SetPickerItems(App.CurrentApp.wer_rating);
            gaskets_button.set_button_list(SurveyFitterButtonLists.shared_gasket_button_list);

            set_patio_warning_visible();
            flat_question.IsVisible = SurveyFitterSharedLogic.aluminium_flat_visible();
            set_flat_message_visible();
            additional_locks_picker.IsVisible = SurveyFitterSharedLogic.aluminium_additional_locks_visible();
            new_lock_area.IsVisible = SurveyFitterSharedLogic.aluminium_new_lock_visible();
            lock_make_picker.IsVisible =
            lock_codes_entry.IsVisible = SurveyFitterSharedLogic.aluminium_new_lock_make_visible();
            set_locks_button_image();
            handles_area.IsVisible = SurveyFitterSharedLogic.handles_visible(App.net.AlumRecord.bRepair);
            handles_text.IsVisible = SurveyFitterSharedLogic.handles_text_visible(App.net.AlumRecord.bRepair, App.net.AlumRecord.handles_req);
            set_handles_button_image();
            replace_panel_area.IsVisible = SurveyFitterSharedLogic.aluminium_replace_panel_visible();
            replace_glass_area.IsVisible = SurveyFitterSharedLogic.replace_glass_visible(App.net.AlumRecord.bRepair);
            cosmetic_gaskets_area.IsVisible = SurveyFitterSharedLogic.gaskets_visible(App.net.AlumRecord.bRepair);
            gasket_text.IsVisible = SurveyFitterSharedLogic.gaskets_text_visible(App.net.AlumRecord.bRepair, App.CurrentApp.AlumRecord.gaskets);
            reason_for_replacement_picker.IsVisible = SurveyFitterSharedLogic.aluminium_reason_for_replacement_visible();
            WER_rating_picker.IsVisible = SurveyFitterSharedLogic.WER_rating_visible(App.net.AlumRecord.bRepair, App.CurrentApp.AlumRecord.bFencer);
            why_not_repaired_area.IsVisible = SurveyFitterSharedLogic.aluminium_why_not_repaired_visible();

            // Page 1
            List<string> aluminium_colour_list = new List<string>() { "White", "Silver", "Brown", "Black" };

            temporary_button.set_button_list(SurveyFitterButtonLists.shared_temporary_button_list);
            colour_picker.SetPickerItems(aluminium_colour_list);
            section_type_button.set_button_list(SurveyFitterButtonLists.aluminium_section_type_list);
            cill_type_button.set_button_list(SurveyFitterButtonLists.aluminium_cill_type_list);

            temporary_button.IsVisible = SurveyFitterSharedLogic.temporary_visible(App.net.AlumRecord.collect_and_copy);
            new_subframe_required_button.IsVisible = SurveyFitterSharedLogic.aluminium_new_sub_frame_visible();
            subframe_depth_entry.IsVisible =
            item_frame_dimensions_area.IsVisible = SurveyFitterSharedLogic.aluminium_subframe_details_visible();
            cill_type_button.IsVisible = SurveyFitterSharedLogic.aluminium_cill_type_visible();

            // Page 2
            List<string> midrail_type_list = new List<string>() { "A Type", "B Type", "None" };

            frame_type_button.set_button_list(SurveyFitterButtonLists.aluminium_frame_type_list);
            night_vent_button.set_button_list(SurveyFitterButtonLists.aluminium_night_vent_list);
            glazed_button.set_button_list(SurveyFitterButtonLists.aluminium_glazed_button_list);
            bead_type_button.set_button_list(SurveyFitterButtonLists.aluminium_bead_type_list);

            frame_type_button.IsVisible = SurveyFitterSharedLogic.aluminium_frame_type_visible();
            cill_or_subframe_button.LabelText = SurveyFitterSharedLogic.aluminium_frame_type_label();
            bead_type_button.IsVisible = SurveyFitterSharedLogic.aluminium_bead_type_visible();
            midrail_type_picker.SetPickerItems(midrail_type_list);
            midrail_type_picker.IsVisible = SurveyFitterSharedLogic.aluminium_midrail_type_visible();
            midrail_height_entry.IsVisible = SurveyFitterSharedLogic.aluminium_midrail_height_visible();
            set_locking_type_picker();
            letter_box_area.IsVisible =
                pet_flap_area.IsVisible = SurveyFitterSharedLogic.aluminium_letterbox_and_petflap_visible();

            // Page 3
            List<string> subframe_colour_list = new List<string>() { "Stained", "Painted" };

            opens_button.set_button_list(SurveyFitterButtonLists.shared_in_out_button_list);
            handle_colour_picker.SetPickerItems(App.net.handle_colour);
            handle_operation_button.set_button_list(SurveyFitterButtonLists.shared_handle_operation_list);
            glass_pattern_picker.SetPickerItems(App.CurrentApp.backing_glass);
            set_glass_type_picker();
            subframe_colour_picker.SetPickerItems(subframe_colour_list);
            special_glass_area.SetPickerItems(App.CurrentApp.special_glass_back);

            opens_button.IsVisible = SurveyFitterSharedLogic.aluminium_opens_visible();
            handle_operation_button.IsVisible = SurveyFitterSharedLogic.aluminium_handle_operation_visible();
            glass_type_picker.IsVisible =
                special_glass_area.IsVisible = SurveyFitterSharedLogic.aluminium_glass_questions_visible();
            subframe_colour_picker.IsVisible = SurveyFitterSharedLogic.aluminium_sub_frame_visible();
            set_document_L_picker_and_visible();

            // Page 4
            spacer_thickness_picker.SetPickerItems(App.CurrentApp.spacer_thickness);
            spacer_colour_picker.SetPickerItems(App.CurrentApp.spacer_colour_new);
            set_spacer_message();
            room_location_picker.SetPickerItems(App.CurrentApp.room_location);

            spacer_thickness_picker.IsVisible =
                spacer_colour_picker.IsVisible = SurveyFitterSharedLogic.aluminium_spacer_questions_visible();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            set_handles_button_image();
            set_locks_button_image();
            SetPanelButton();
            SetGlassButton();
            App.CurrentApp.CurrentItem = "alum";
            App.net.AlumRecord.no_of_pics = SurveyFitterSharedLogic.count_drawings();
            App.net.AlumRecord.no_of_photos = SurveyFitterSharedLogic.count_photos();

            special_glass_area.set_complete(App.net.AlumRecord.lead_bDiamondComplete,
                App.net.AlumRecord.lead_bGeorgianComplete,
                App.net.AlumRecord.lead_bGeorgianComplete,
                App.net.AlumRecord.lead_bBarComplete);
        }

        private void set_patio_warning_visible()
        {
            patio_warning.IsVisible = App.net.AlumRecord.type == "Conventional Sliding Patio";
        }

        private void set_flat_message_visible()
        {
            // App.net.AlumRecord.bRepair does not need testing here as it's part of flat_question.IsVisible
            flat_warning_text.IsVisible = flat_question.IsVisible && App.net.AlumRecord.is_a_flat == 1;
        }

        private void set_locks_button_image()
        {
            if (new_lock_area.IsVisible)
            {
                if (App.net.AlumRecord.bNewLockingMech == 1)
                {
                    if (   App.CurrentApp.AlumRecord.type == "Door"
                        || App.CurrentApp.AlumRecord.type == "French Doors"
                        || App.CurrentApp.AlumRecord.type == "Combi Frame"
                        || App.CurrentApp.AlumRecord.type == "Porch")
                    {
                        if (App.net.AlumRecord.bDoorComplete)
                            new_lock_details_button.ImageSource = "green_tick.png";
                        else
                            new_lock_details_button.ImageSource = "question.png";
                    }
                    else if (   App.CurrentApp.AlumRecord.type == "Window"
                             || App.CurrentApp.AlumRecord.type == "Bay Window")
                    {
                        if (App.net.AlumRecord.bWindowComplete)
                            new_lock_details_button.ImageSource = "green_tick.png";
                        else
                            new_lock_details_button.ImageSource = "question.png";
                    }
                    else
                        new_lock_details_button.ImageSource = "na.png";
                }
                else
                    new_lock_details_button.ImageSource = "na.png";
            }
        }

        private void set_handles_button_image()
        {
            if (handles_text.IsVisible)
            {
                if (App.net.AlumRecord.bHandleDrawingComplete)
                    handles_button.ImageSource = "green_tick.png";
                else
                    handles_button.ImageSource = "question.png";
            }
            else
                handles_button.ImageSource = "na.png";
        }

        private void set_locking_type_picker()
        {
            List<string> lock_type_list;

            if (App.CurrentApp.AlumRecord.type == "Window"
                && App.CurrentApp.AlumRecord.section_type != 0
                && App.CurrentApp.AlumRecord.section_type < 3)
            {
                if (App.CurrentApp.AlumRecord.section_type == 1)
                    lock_type_list = new List<string>() { "", "Cockspur", "Espag", "Shootbolts" };
                else // App.CurrentApp.AlumRecord.section_type is 2
                    lock_type_list = new List<string>() { "", "Cockspur", "Espag" };
            }
            else
                lock_type_list = new List<string>() { "", "Standard", "With Shootbolts" };

            locking_type_picker.SetPickerItems(lock_type_list);
        }

        private void set_glass_type_picker()
        {
            if (App.CurrentApp.AlumRecord.section_type == 3)
            {
                glass_type_picker.SetPickerItems(App.CurrentApp.glass_type);
            }
            else
            {
                List<string> type_list;

                if (App.CurrentApp.AlumRecord.bRepair)
                    type_list = new List<string>() { "", "4mm", "6mm", "4mm Tough", "6mm Tough", "6.4mm Laminated" };
                else
                    type_list = new List<string>() { "", "4mm", "4mm Tough" };

                glass_type_picker.SetPickerItems(type_list);
            }
        }

        private void set_document_L_picker_and_visible()
        {
            document_L_picker.IsVisible = SurveyFitterSharedLogic.aluminium_doc_L_visible();

            if (document_L_picker.IsVisible)
            {
                if (App.CurrentApp.AlumRecord.glass_type.Contains("6.4mm") || (App.CurrentApp.AlumRecord.glass_type != "None" && App.CurrentApp.AlumRecord.glass_type != "Clear"))
                    document_L_picker.SetPickerItems(App.CurrentApp.docl_comp_noa);
                else
                    document_L_picker.SetPickerItems(App.CurrentApp.docl_comp);
            }
        }

        private void set_spacer_message()
        {
            switch (App.net.AlumRecord.spacer_color)
            {
                case "Black, Warm Edge, Super Spacer":
                    spacer_message.IsVisible = true;
                    spacer_message.Text = "This is our preferred spacer, available in 12-20mm";
                    break;
                case "Grey - Warm Edge - Plastic":
                case "Black - Warm Edge - Plastic":
                    spacer_message.IsVisible = true;
                    spacer_message.Text = "Limited stock. Try to use super spacer";
                    break;
                default:
                    spacer_message.IsVisible = false;
                    break;
            }
        }

        private void aluminium_item_changed(object sender, EventArgs e)
        {
            set_patio_warning_visible();
            flat_question.IsVisible = SurveyFitterSharedLogic.aluminium_flat_visible();
            set_flat_message_visible();
            new_lock_area.IsVisible = SurveyFitterSharedLogic.aluminium_new_lock_visible();
            set_locking_type_picker();
            frame_type_button.IsVisible = SurveyFitterSharedLogic.aluminium_frame_type_visible();
            midrail_type_picker.IsVisible = SurveyFitterSharedLogic.aluminium_midrail_type_visible();
            midrail_height_entry.IsVisible = SurveyFitterSharedLogic.aluminium_midrail_height_visible();
            letter_box_area.IsVisible =
                pet_flap_area.IsVisible = SurveyFitterSharedLogic.aluminium_letterbox_and_petflap_visible();
            opens_button.IsVisible = SurveyFitterSharedLogic.aluminium_opens_visible();
            handle_operation_button.IsVisible = SurveyFitterSharedLogic.aluminium_handle_operation_visible();
        }

        public void flat_button_click(object sender, EventArgs e)
        {
            set_flat_message_visible();
        }

        private void new_lock_required_clicked(object sender, EventArgs e)
        {
            lock_make_picker.IsVisible =
            lock_codes_entry.IsVisible = SurveyFitterSharedLogic.aluminium_new_lock_make_visible();
            set_locks_button_image();
        }

        private void new_lock_clicked(object sender, EventArgs e)
        {
            if (App.net.AlumRecord.bNewLockingMech == 1)
            {
                switch (App.net.AlumRecord.type)
                {
                    case "Combi Frame":
                    case "Door":
                    case "French Doors": Navigation.PushAsync(new DoorLock(), false); break;
                    case "Window": Navigation.PushAsync(new WindowLock(), false); break;
                }
            }
        }

        private void handles_required_changed(object sender, EventArgs e)
        {
            handles_text.IsVisible = SurveyFitterSharedLogic.handles_text_visible(App.net.AlumRecord.bRepair, App.net.AlumRecord.handles_req);
            set_handles_button_image();
        }

        private void handles_info_clicked(object sender, EventArgs e)
        {
            if (App.net.AlumRecord.handles_req == 1)
            {
                App.net.drawing_type = "handles";

                if (App.net.AlumRecord.bHandleDrawingComplete == true)
                {
                    App.net.drawing_edit_mode = true;
                    Navigation.PushAsync(new DrawingPage(), false);
                }
                else
                {
                    App.net.drawing_edit_mode = false;
                    App.net.load_template_image = true;
                    Navigation.PushAsync(new TemplateHandles(), false);
                }
            }
        }

        private void gaskets_clicked(object sender, EventArgs e)
        {
            gasket_text.IsVisible = SurveyFitterSharedLogic.gaskets_text_visible(App.net.AlumRecord.bRepair, App.CurrentApp.AlumRecord.gaskets);
        }

        private void collect_and_copy_changed(object sender, EventArgs e)
        {
            temporary_button.IsVisible = SurveyFitterSharedLogic.temporary_visible(App.net.AlumRecord.collect_and_copy);
        }

        private void section_type_clicked(object sender, EventArgs e)
        {
            new_subframe_required_button.IsVisible = SurveyFitterSharedLogic.aluminium_new_sub_frame_visible();
            subframe_depth_entry.IsVisible =
            item_frame_dimensions_area.IsVisible = SurveyFitterSharedLogic.aluminium_subframe_details_visible();
            set_locking_type_picker();
            frame_type_button.IsVisible = SurveyFitterSharedLogic.aluminium_frame_type_visible();
            subframe_colour_picker.IsVisible = SurveyFitterSharedLogic.aluminium_sub_frame_visible();
            set_glass_type_picker();
        }

        private void new_subframe_required_clicked(object sender, EventArgs e)
        {
            subframe_depth_entry.IsVisible =
            item_frame_dimensions_area.IsVisible = SurveyFitterSharedLogic.aluminium_subframe_details_visible();
        }

        private void cill_on_subframe_clicked(object sender, EventArgs e)
        {
            cill_type_button.IsVisible = SurveyFitterSharedLogic.aluminium_cill_type_visible();
            cill_or_subframe_button.LabelText = SurveyFitterSharedLogic.aluminium_frame_type_label();
        }

        private void glazed_changed(object sender, EventArgs e)
        {
            bead_type_button.IsVisible = SurveyFitterSharedLogic.aluminium_bead_type_visible();
        }

        private void midrail_type_changed(object sender, EventArgs e)
        {
            midrail_height_entry.IsVisible = SurveyFitterSharedLogic.aluminium_midrail_height_visible();
        }

        private void glass_type_pattern_changed(object sender, EventArgs e)
        {
            glass_type_picker.IsVisible =
                special_glass_area.IsVisible = SurveyFitterSharedLogic.aluminium_glass_questions_visible();
            set_document_L_picker_and_visible();
            spacer_thickness_picker.IsVisible =
                spacer_colour_picker.IsVisible = SurveyFitterSharedLogic.aluminium_spacer_questions_visible();
        }

        private void glass_type_changed(object sender, EventArgs e)
        {
            set_document_L_picker_and_visible();
        }

        private void spacer_colour_changed(object sender, EventArgs e)
        {
            set_spacer_message();
        }

        private string validate_page_0()
        {
            string result = aluminium_item_picker.validation_error_string("Aluminium item\n")
                          + flat_question.validation_error_string("Is the property a flat\n")
                          + additional_locks_picker.validation_error_string("Additional Locks\n")
                          + (new_lock_area.IsVisible && App.CurrentApp.AlumRecord.bNewLockingMech == 0 ? "New locking mechanism" : "")
                          + lock_make_picker.validation_error_string("Lock make\n")
                          + lock_codes_entry.validation_error_string("Lock codes\n")
                          + (handles_area.IsVisible ? SurveyFitterSharedLogic.handles_validation_error_text(App.CurrentApp.AlumRecord.handles_req, App.CurrentApp.AlumRecord.handles_text, App.CurrentApp.AlumRecord.bHandleDrawingComplete) : "");

            if (replace_panel_area.IsVisible)
                result = result + (App.CurrentApp.AlumRecord.replace_panel == 0 ? "Replace Panel\n" : "")
                                + (App.CurrentApp.AlumRecord.replace_panel == 1 && !App.CurrentApp.AlumRecord.bPanelComplete ? "Panel data\n" : "");

            if (replace_glass_area.IsVisible)
                result = result + replace_glass_button.validation_error_string("Replace glass\n")
                                + (App.CurrentApp.AlumRecord.replace_glass == 1 && !App.net.AlumRecord.glass_complete ? "Replace glass details\n" : "");

            if (cosmetic_gaskets_area.IsVisible)
                result = result + cosmetic_damage_button.validation_error_string("Cosmetic Damage\n")
                                + gaskets_button.validation_error_string("Gaskets\n")
                                + (gasket_text.IsVisible && App.CurrentApp.AlumRecord.gaskets_text == "" ? "Gaskets text\n" : "");

            // Replace
            result = result + reason_for_replacement_picker.validation_error_string("Reason for replacement\n")
                            + WER_rating_picker.validation_error_string("WER Rating\n")
                            + (why_not_repaired_area.IsVisible && App.CurrentApp.AlumRecord.replace_explain == "" ? "Explaination why item cannot be repaired\n" : "");

            return result;
        }

        private string validate_page_1()
        {
            return cause_of_damage_area.validation_error_string()
                 + collect_and_copy_button.validation_error_string("Collect and Copy\n")
                 + temporary_button.validation_error_string("Temporary\n")
                 + colour_picker.validation_error_string("Colour\n")
                 + section_type_button.validation_error_string("Section type\n")
                 + new_subframe_required_button.validation_error_string("New subframe\n")
                 + cill_on_subframe_button.validation_error_string("Cill on subframe\n")
                 + cill_type_button.validation_error_string("Cill type\n")
                 + subframe_depth_entry.validation_error_string("Subframe Depth\n")
                 + item_frame_dimensions_area.validation_error_string("Item Frame Width\n", "Item Frame Height\n")
                 + brick_dimensions_area.validation_error_string("Brick Width\n", "Brick Height\n")
                 + internal_dimensions_area.validation_error_string("Internal Width\n", "Internal Height\n")
                 + outer_section_dimensions_area.validation_error_string("Outer section width\n", "Outer section depth\n");
        }

        private string validate_page_2()
        {
            return frame_type_button.validation_error_string("Frame Type\n")
                 + cill_or_subframe_button.validation_error_string(cill_or_subframe_button.LabelText + "\n") // Use whichever label we display as the error text
                 + drip_button.validation_error_string("Drip\n")
                 + night_vent_button.validation_error_string("Night Vent\n")
                 + glazed_button.validation_error_string("Glazed\n")
                 + bead_type_button.validation_error_string("Bead type\n")
                 + midrail_type_picker.validation_error_string("Midrail Type\n")
                 + midrail_height_entry.validation_error_string("Midrail height\n")
                 + locking_type_picker.validation_error_string("Locking Type\n")
                 + letter_box_area.validation_error_string()
                 + pet_flap_area.validation_error_string();
        }

        private string validate_page_3()
        {
            string result = opens_button.validation_error_string("Opens\n")
                          + handle_colour_picker.validation_error_string("Handle Colour\n")
                          + handle_operation_button.validation_error_string("Handle operation\n")
                          + glass_pattern_picker.validation_error_string("Glass Pattern\n")
                          + glass_type_picker.validation_error_string("Glass Type\n")
                          + subframe_colour_picker.validation_error_string("Subframe Colour\n")
                          + document_L_picker.validation_error_string("Document 'L'\n");

            if (special_glass_area.IsVisible)
            {
                if (App.CurrentApp.AlumRecord.special_glass == "")
                    result += "Special Glass\n";
                else if (App.CurrentApp.AlumRecord.special_glass == "Diamond Leaded" && !App.CurrentApp.AlumRecord.lead_bDiamondComplete)
                    result += "Diamond lead details\n";
                else if (App.CurrentApp.AlumRecord.special_glass == "Georgian Leaded" && !App.CurrentApp.AlumRecord.lead_bGeorgianComplete)
                    result += "Georgian lead details\n";
                else if (App.CurrentApp.AlumRecord.special_glass == "Back to Back Spacer" && !App.CurrentApp.AlumRecord.lead_bGeorgianComplete)
                    result += "Back to Back Spacer\n";
                else if (App.CurrentApp.AlumRecord.special_glass == "Georgian Bar" && !App.CurrentApp.AlumRecord.lead_bBarComplete)
                    result += "Georgian bar details\n";
            }

            return result;
        }

        private string validate_page_4()
        {
            return spacer_thickness_picker.validation_error_string("Spacer Thickness\n")
                 + spacer_colour_picker.validation_error_string("Spacer Colour\n")
                 + room_location_picker.validation_error_string("Room Location\n")
                 + summary_pto_area.validation_error_string();
        }

        protected override string validate_drawings_and_pictures()
        {
            return cause_of_damage_area.photo_validation_error_string (App.CurrentApp.AlumRecord.no_of_photos)
                 + (App.CurrentApp.AlumRecord.no_of_pics == 0 ? "Drawings\n" : "");
        }

        protected override string validate_page()
        {
            return validate_page_0()
                + validate_page_1()
                + validate_page_2()
                + validate_page_3()
                + validate_page_4();
        }

        /*
        protected override void set_page(int page_num)
        {
            switch (page_num)
            {
                case 0: CurrentPage = Page0; break;
                case 1: CurrentPage = Page1; break;
                case 2: CurrentPage = Page2; break;
                case 3: CurrentPage = Page3; break;
                case 4: CurrentPage = Page4; break;
            }
        }
        */

        protected override void save_item(bool complete)
        {
            App.data.SaveAlum(complete);
        }

        protected override bool already_signed()
        {
            return App.CurrentApp.AlumRecord.bDifferentFromOriginal;
        }

        private void SetPanelButton()
        {
            if (App.net.AlumRecord.replace_panel == 1)
            {
                if (App.net.AlumRecord.bPanelComplete == true)
                    do_replace_panel_button.ImageSource = "green_tick.png";
                else
                    do_replace_panel_button.ImageSource = "question.png";
            }
            else
                do_replace_panel_button.ImageSource = "na.png";
        }

        private void replace_panel_changed(object sender, EventArgs e)
        {
            SetPanelButton();
        }

        private void replace_panel_clicked(object sender, EventArgs e)
        {
            if (App.net.AlumRecord.replace_panel == 1)
            {
                App.net.PanelRecord = App.data.GetPanelByContractAlumItemNo(App.CurrentApp.HeaderRecord.udi_cont, App.CurrentApp.AlumRecord.item_number);

                if (App.net.PanelRecord == null)
                {
                    App.net.table_init.CreatePanel();

                    App.CurrentApp.PanelRecord.alum_item_number = App.CurrentApp.AlumRecord.item_number;
                    App.CurrentApp.PanelRecord.item_number = App.CurrentApp.AlumRecord.item_number;

                    App.data.SaveHeader();
                }

                App.CurrentApp.loaded_item_number = App.CurrentApp.AlumRecord.item_number;
                App.CurrentApp.root_item_number = App.CurrentApp.AlumRecord.item_number;
                App.CurrentApp.CurrentItem = "panel";
                Navigation.PushAsync(new Panel(), false);
            }
        }

        private void SetGlassButton()
        {
            if (App.net.AlumRecord.replace_glass == 1) // Replace glass
            {
                if (App.net.AlumRecord.glass_complete)
                    do_replace_glass_button.ImageSource = "green_tick.png";
                else
                    do_replace_glass_button.ImageSource = "question.png";
            }
            else
                do_replace_glass_button.ImageSource = "na.png";
        }

        private void replace_glass_changed(object sender, EventArgs e)
        {
            SetGlassButton();
        }

        private void replace_glass_clicked(object sender, EventArgs e)
        {
            if (App.net.AlumRecord.replace_glass == 1) // Replace glass?
            {
                int item_no = App.CurrentApp.AlumRecord.item_number;

                App.net.GlassRecord = App.data.GetGlassByContractAlumItemNo(App.CurrentApp.HeaderRecord.udi_cont, item_no);

                if (App.net.GlassRecord == null)
                {
                    App.net.table_init.CreateGlass();
                    App.CurrentApp.GlassRecord.item_number = App.CurrentApp.AlumRecord.item_number;
                    App.CurrentApp.GlassRecord.parent_item = 1;
                    App.CurrentApp.loaded_item_number = App.CurrentApp.AlumRecord.item_number;
                    App.CurrentApp.root_item_number = App.CurrentApp.AlumRecord.item_number;
                    App.data.SaveHeader();
                }
                App.CurrentApp.CurrentItem = "glass";
                Navigation.PushAsync(new GlassItem(), false);
            }
        }
    }
}