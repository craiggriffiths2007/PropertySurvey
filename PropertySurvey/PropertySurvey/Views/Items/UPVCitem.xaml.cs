using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UPVCitem : CarouselValidate
    {
        public UPVCitem()
        {
            InitializeComponent();
            BindingContext = App.net.UPVCRecord as UPVCTable;
            save_on_pagechange = true;

            //Page 0
            upvc_item_picker.SetPickerItems(App.net.type_of_item2);
            additional_locks_picker.SetPickerItems(App.net.additional_locks);
            lock_make_picker.SetPickerItems(App.net.lock_make);
            temporary_button.set_button_list(SurveyFitterButtonLists.shared_temporary_button_list);

            flat_question.IsVisible = SurveyFitterSharedLogic.upvc_collect_and_copy_visible();
            set_flat_message_visible();
            temporary_button.IsVisible = SurveyFitterSharedLogic.temporary_visible(App.net.UPVCRecord.collect_and_copy);
            additional_locks_picker.IsVisible = SurveyFitterSharedLogic.upvc_additional_locks_visible();
            new_lock_area.IsVisible = SurveyFitterSharedLogic.upvc_new_locking_mechanism_visible();
            lock_make_picker.IsVisible =
                lock_codes_entry.IsVisible = SurveyFitterSharedLogic.upvc_new_lock_make_visible();
            set_locks_button_image();
            handles_area.IsVisible = SurveyFitterSharedLogic.handles_visible(App.CurrentApp.UPVCRecord.bRepair);
            handles_entry.IsVisible = SurveyFitterSharedLogic.handles_text_visible(App.CurrentApp.UPVCRecord.bRepair, App.CurrentApp.UPVCRecord.handles_req);
            replace_panel_area.IsVisible = SurveyFitterSharedLogic.upvc_replace_panel_visible();
            replace_glass_area.IsVisible = SurveyFitterSharedLogic.replace_glass_visible(App.CurrentApp.UPVCRecord.bRepair);

            // Page 1
            gaskets_button.set_button_list(SurveyFitterButtonLists.shared_gasket_button_list);
            room_location_picker.SetPickerItems(App.net.room_location);
            hinge_colour_picker.SetPickerItems(App.net.handle_colour);
            replacement_reason_picker.SetPickerItems(App.net.replace_reason);
            WER_rating_picker.SetPickerItems(App.net.wer_rating);

            cosmetic_damage_button.IsVisible = SurveyFitterSharedLogic.upvc_cosmetic_damage_visible();
            gaskets_button.IsVisible = SurveyFitterSharedLogic.gaskets_visible(App.CurrentApp.UPVCRecord.bRepair);
            gasket_text.IsVisible = SurveyFitterSharedLogic.gaskets_text_visible(App.CurrentApp.UPVCRecord.bRepair, App.net.UPVCRecord.gaskets);
            hinge_colour_picker.IsVisible = SurveyFitterSharedLogic.upvc_hinge_colour_visible();
            WER_rating_picker.IsVisible = SurveyFitterSharedLogic.WER_rating_visible(App.CurrentApp.UPVCRecord.bRepair, App.CurrentApp.UPVCRecord.fensa);
            replacement_reason_picker.IsVisible =
                replace_explain_entry.IsVisible = SurveyFitterSharedLogic.reason_not_repaired_visible(App.CurrentApp.UPVCRecord.bRepair);

            // Page 2
            List<string> outer_section_list = new List<string>() { "56mm", "70mm" };
            List<string> frame_depth_list = new List<string>() { "60mm", "70mm" };

            cills_picker.SetPickerItems(App.net.cills);
            handle_colour_picker.SetPickerItems(App.net.handle_colour);
            handle_operation_button.set_button_list(SurveyFitterButtonLists.shared_handle_operation_list);
            outer_section_picker.SetPickerItems(outer_section_list);
            frame_depth_picker.SetPickerItems(frame_depth_list);
            set_colour_list();

            handle_operation_button.IsVisible = SurveyFitterSharedLogic.upvc_handle_operation_visible();
            set_frame_depth_warning_visible();
            set_sidelights_warning_visible();

            // Page 3
            List<string> locking_type_list = new List<string>() { "Hooks", "Hooks + Shootbolts" };

            locking_type_picker.SetPickerItems(locking_type_list);

            addon_width_edit.IsVisible =
                addon_height_edit.IsVisible = SurveyFitterSharedLogic.addon_dimensions_visible (App.CurrentApp.UPVCRecord.addons);
            midrail_button.IsVisible = SurveyFitterSharedLogic.upvc_midrail_visible();
            midrail_height_edit.IsVisible = SurveyFitterSharedLogic.upvc_midrail_height_visible();
            locking_type_picker.IsVisible = SurveyFitterSharedLogic.upvc_locking_type_visible();
            letter_box_area.IsVisible =
                pet_flap_area.IsVisible = SurveyFitterSharedLogic.upvc_letterbox_and_petflap_visible();

            // Page 4
            bead_type_picker.SetPickerItems(App.net.bead_type);
            glass_pattern_picker.SetPickerItems(App.net.backing_glass);
            double_or_triple_button.set_button_list(SurveyFitterButtonLists.upvc_double_or_triple_list);
            glass_type_picker.SetPickerItems(App.net.glass_type);
            special_glass_area.SetPickerItems(App.net.special_glass_back);
            opens_button.set_button_list(SurveyFitterButtonLists.upvc_opens_list);
            glaze_button.set_button_list(SurveyFitterButtonLists.shared_internal_external_button_list);
            trickle_vents_button.set_button_list(SurveyFitterButtonLists.upvc_trickle_vents_button_list);

            set_glass_questions_picker_and_visible();

            // Page 5
            set_threshold_list();
            set_spacer_thickness_list();
            spacer_colour_picker.SetPickerItems(App.net.spacer_colour_new);
            lock_type_button.set_button_list(SurveyFitterButtonLists.upvc_lock_type_button_list);
            slide_position_button.set_button_list(SurveyFitterButtonLists.shared_inside_outside_button_list);
            profile_type_button.set_button_list(SurveyFitterButtonLists.shared_profile_type_button_list); // ToDo Needs an alert when clicking on rustic(sculptured)

            spacer_thickness_picker.IsVisible =
                spacer_colour_picker.IsVisible = SurveyFitterSharedLogic.upvc_spacer_questions_visible();
            set_spacer_message();
            lock_type_button.IsVisible = SurveyFitterSharedLogic.upvc_patio_lock_type_visible();
            slide_position_button.IsVisible = SurveyFitterSharedLogic.upvc_slide_position_visible();
            set_profile_message_visible();

            //SetPageNumber();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.CurrentApp.CurrentItem = "upvc";
            App.net.UPVCRecord.no_of_pics = SurveyFitterSharedLogic.count_drawings();
            App.net.UPVCRecord.no_of_photos = SurveyFitterSharedLogic.count_photos();

            SetPanelButton();
            SetGlassButton();
            set_handles_button_image();
            set_locks_button_image();

            special_glass_area.set_complete(App.net.UPVCRecord.lead_bDiamondComplete,
                App.net.UPVCRecord.lead_bGeorgianComplete,
                App.net.UPVCRecord.lead_bGeorgianComplete,
                App.net.UPVCRecord.lead_bBarComplete);
        }

        private void set_flat_message_visible()
        {
            flat_warning_text.IsVisible = flat_question.IsVisible && (App.net.UPVCRecord.is_a_flat == 1);
        }

        private void set_locks_button_image()
        {
            if (new_lock_area.IsVisible)
            {
                if (App.net.UPVCRecord.bNewLockingMech == 1)
                {
                    if (App.net.UPVCRecord.bLockComplete)
                        new_lock_button.ImageSource = "green_tick.png";
                    else
                        new_lock_button.ImageSource = "question.png";
                }
                else
                    new_lock_button.ImageSource = "na.png";
            }
        }

        private void set_handles_button_image()
        {
            if (App.net.UPVCRecord.handles_req == 1)
            {
                if (App.net.UPVCRecord.bHandleDrawingComplete)
                    handles_button.ImageSource = "green_tick.png";
                else
                    handles_button.ImageSource = "question.png";
            }
            else
                handles_button.ImageSource = "na.png";
        }

        private void set_frame_depth_warning_visible()
        {
            frame_depth_warning.IsVisible = App.CurrentApp.UPVCRecord.frame_depth == "60mm";
        }

        private void set_colour_list()
        {
            List<string> colour_list = null;

            if (frame_depth_picker.Text == "60mm")
                colour_list = new List<string>() { "Oak", "Oak on White", "Rosewood", "Rosewood on White", "White", "Woodgrain 021", "Woodgrain 021 on White" };
            else if (frame_depth_picker.Text == "70mm")
                colour_list = new List<string>() { "White", "Rosewood", "Rosewood on White", "Oak", "Oak on White", "",
                    "Woodgrain 013", "Woodgrain 021", "Woodgrain 013 on White", "Woodgrain 021 on White",
                    "White foil on white foil", "White foil on white smooth", "Cream foil", "Cream foil on white",
                    "Anthracite grey foil 7016", "Anth' grey foil on white", "Anthracite grey smooth 7016", "Anth' grey smooth on white",
                    "Chartwell green foil", "Chartwell green foil on white",
                    "Black foil", "Black foil on white", "Other coloured foil", "Other coloured smooth" };
            // else leave it as null;

            colour_picker.SetPickerItems(colour_list);
        }

        private void set_sidelights_warning_visible()
        {
            // Use doubles so that if they e.g. type 1900.00 it still works and doesn't cause the message to vanish just because they type .00
            double brick_width;
            sidelights_warning.IsVisible = ((App.net.UPVCRecord.upvc_item == "French Doors")
                                                 && (Double.TryParse(App.CurrentApp.UPVCRecord.brick_width, out brick_width))
                                                 && (brick_width > 1800));
        }

        private void set_glass_questions_picker_and_visible()
        {
            double_or_triple_button.IsVisible =
                glass_type_picker.IsVisible =
                special_glass_area.IsVisible =
                document_L_picker.IsVisible = SurveyFitterSharedLogic.upvc_glass_questions_visible();

            if (App.CurrentApp.UPVCRecord.glass_type.Contains("6.4mm")
                || App.CurrentApp.UPVCRecord.glass_pattern != "Clear")
            {
                if (App.CurrentApp.UPVCRecord.bRepair)
                    document_L_picker.SetPickerItems(App.net.docl_comp_noa);
                else
                    document_L_picker.SetPickerItems(App.net.docl_non_noa);
            }
            else if (App.CurrentApp.UPVCRecord.bRepair)
                document_L_picker.SetPickerItems(App.net.docl_comp);
            else
                document_L_picker.SetPickerItems(App.net.docl_non);
        }

        private void set_threshold_list()
        {
            threshold_type_picker.IsVisible = SurveyFitterSharedLogic.upvc_threshold_visible();
            if (threshold_type_picker.IsVisible)
            {
                List<string> threshold_list = null;

                if (App.net.UPVCRecord.upvc_item == "Door"
                    || App.net.UPVCRecord.upvc_item == "Combi Frame"
                    || App.net.UPVCRecord.upvc_item == "Porch")
                {
                    threshold_list = new List<string>() { "56mm uPVC outer", "44 F/Door Ali thresher", "70mm uPVC outer", "15mm disabled access thresher" };
                }
                else if (App.net.UPVCRecord.upvc_item == "French Doors")
                    threshold_list = new List<string>() { "56mm uPVC outer", "44 F/Door Ali thresher", "70mm uPVC outer" };
                else if (App.net.UPVCRecord.upvc_item == "Conventional Sliding Patio")
                    threshold_list = new List<string>() { "70mm", "Aluminium" };

                threshold_type_picker.SetPickerItems(threshold_list);
            }
        }

        private void set_spacer_thickness_list()
        {
            if (App.net.UPVCRecord.double_tripple == 2) // Tripple
                spacer_thickness_picker.SetPickerItems(App.net.spacer_thickness3);
            else
                spacer_thickness_picker.SetPickerItems(App.net.spacer_thickness2);
        }

        private void set_spacer_message()
        {
            if (spacer_colour_picker.IsVisible)
            {
                switch (App.net.UPVCRecord.spacer_colour)
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
            else
                spacer_message.IsVisible = false;
        }

        private void set_profile_message_visible()
        {
            profile_message.IsVisible = App.CurrentApp.UPVCRecord.profile_type == 2;
        }

        private void flat_changed(object sender, EventArgs e)
        {
            set_flat_message_visible();
        }

        private void upvc_item_changed(object sender, EventArgs e)
        {
            flat_question.IsVisible = SurveyFitterSharedLogic.upvc_collect_and_copy_visible();
            set_flat_message_visible();
            new_lock_area.IsVisible = SurveyFitterSharedLogic.upvc_new_locking_mechanism_visible();
            lock_make_picker.IsVisible =
                lock_codes_entry.IsVisible = SurveyFitterSharedLogic.upvc_new_lock_make_visible();
            hinge_colour_picker.IsVisible = SurveyFitterSharedLogic.upvc_hinge_colour_visible();
            set_locks_button_image();
            handle_operation_button.IsVisible = SurveyFitterSharedLogic.upvc_handle_operation_visible();
            set_sidelights_warning_visible();
            midrail_button.IsVisible = SurveyFitterSharedLogic.upvc_midrail_visible();
            midrail_height_edit.IsVisible = SurveyFitterSharedLogic.upvc_midrail_height_visible();
            locking_type_picker.IsVisible = SurveyFitterSharedLogic.upvc_locking_type_visible();
            letter_box_area.IsVisible =
                pet_flap_area.IsVisible = SurveyFitterSharedLogic.upvc_letterbox_and_petflap_visible();
            set_threshold_list();
            lock_type_button.IsVisible = SurveyFitterSharedLogic.upvc_patio_lock_type_visible();
            slide_position_button.IsVisible = SurveyFitterSharedLogic.upvc_slide_position_visible();
        }

        private void flat_button_click(object sender, EventArgs e)
        {
            set_flat_message_visible();
        }

        private void collect_and_copy_changed(object sender, EventArgs e)
        {
            temporary_button.IsVisible = SurveyFitterSharedLogic.temporary_visible(App.net.UPVCRecord.collect_and_copy);
        }

        private void new_lock_required_clicked(object sender, EventArgs e)
        {
            lock_make_picker.IsVisible =
                lock_codes_entry.IsVisible = SurveyFitterSharedLogic.upvc_new_lock_make_visible();
            set_locks_button_image();
        }

        private void new_lock_clicked(object sender, EventArgs e)
        {
            if (App.CurrentApp.UPVCRecord.bNewLockingMech == 1)
            {
                switch (App.net.UPVCRecord.upvc_item)
                {
                    case "Door":
                    case "French Doors":
                    case "Combi Frame":
                    case "Porch": Navigation.PushAsync(new DoorLock(), false); break;
                    case "Window":
                    case "Bay Window": Navigation.PushAsync(new WindowLock(), false); break;
                }
            }
        }

        private void handles_required_changed(object sender, EventArgs e)
        {
            handles_entry.IsVisible = SurveyFitterSharedLogic.handles_text_visible(App.CurrentApp.UPVCRecord.bRepair, App.CurrentApp.UPVCRecord.handles_req);
            set_handles_button_image();
        }

        private void handles_clicked(object sender, EventArgs e)
        {
            if (App.net.UPVCRecord.handles_req == 1)
            {
                App.net.drawing_type = "handles";

                if (App.net.UPVCRecord.bHandleDrawingComplete == true)
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

        private void gaskets_changed(object sender, EventArgs e)
        {
            gasket_text.IsVisible = SurveyFitterSharedLogic.gaskets_text_visible(App.CurrentApp.UPVCRecord.bRepair, App.net.UPVCRecord.gaskets);
        }

        private void frame_depth_changed(object sender, EventArgs e)
        {
            set_colour_list();
            set_frame_depth_warning_visible();
        }

        private void brick_width_changed(object sender, EventArgs e) // Gets called when brick_height changes also, but doesn't break anything
        {
            set_sidelights_warning_visible();
        }

        private void addons_button_clicked(object sender, EventArgs e)
        {
            addon_width_edit.IsVisible =
                addon_height_edit.IsVisible = SurveyFitterSharedLogic.addon_dimensions_visible (App.CurrentApp.UPVCRecord.addons);
        }

        private void midrail_clicked(object sender, EventArgs e)
        {
            midrail_height_edit.IsVisible = SurveyFitterSharedLogic.upvc_midrail_height_visible();
        }

        private void glass_pattern_or_type_changed(object sender, EventArgs e) // Used for both glass pattern and glass type
        {
            set_glass_questions_picker_and_visible();
            spacer_thickness_picker.IsVisible =
                spacer_colour_picker.IsVisible = SurveyFitterSharedLogic.upvc_spacer_questions_visible();
            set_spacer_message();
        }

        private void double_triple_clicked(object sender, EventArgs e)
        {
            set_spacer_thickness_list();
        }

        private void spacer_colour_changed(object sender, EventArgs e)
        {
            set_spacer_message();
        }

        private void profile_type_clicked(object sender, EventArgs e)
        {
            set_profile_message_visible();
        }

        private string validate_page_0()
        {
            string result = upvc_item_picker.validation_error_string("UPVC item\n")
                          + flat_question.validation_error_string("Is the property a flat\n")
                          + collect_and_copy_button.validation_error_string("Collect and Copy\n")
                          + temporary_button.validation_error_string("Temporary\n")
                          + additional_locks_picker.validation_error_string("Additional Locks\n");

            if (new_lock_area.IsVisible)
            {
                if (App.CurrentApp.UPVCRecord.bNewLockingMech == 0)
                    result += "New Locking Mechanism req.\n";

                if (App.CurrentApp.UPVCRecord.bNewLockingMech == 1)
                {
                    if (App.CurrentApp.UPVCRecord.upvc_item == "Door" ||
                        App.CurrentApp.UPVCRecord.upvc_item == "French Doors" ||
                        App.CurrentApp.UPVCRecord.upvc_item == "Combi Frame" ||
                        App.CurrentApp.UPVCRecord.upvc_item == "Porch")
                    {
                        if (!App.CurrentApp.UPVCRecord.bDoorComplete)
                            result += "Locking Mechanism Data\n";
                    }
                    else if (App.CurrentApp.UPVCRecord.upvc_item == "Window" || App.CurrentApp.UPVCRecord.upvc_item == "Bay Window")
                    {
                        if (!App.CurrentApp.UPVCRecord.bWindowComplete)
                            result += "Locking Mechanism Data\n";
                    }

                    result = result + lock_make_picker.validation_error_string("Lock Make\n")
                                    + lock_codes_entry.validation_error_string("Lock Codes\n");
                }
            }

            if (handles_area.IsVisible)
                result += SurveyFitterSharedLogic.handles_validation_error_text(App.CurrentApp.UPVCRecord.handles_req, App.CurrentApp.UPVCRecord.handles_text, App.CurrentApp.UPVCRecord.bHandleDrawingComplete);

            if (replace_panel_area.IsVisible)
                result = result + (App.CurrentApp.UPVCRecord.replace_panel == 0 ? "Replace Panel\n" : "")
                                + (App.CurrentApp.UPVCRecord.replace_panel == 1 && !App.CurrentApp.UPVCRecord.bPanelComplete ? "Panel data\n" : "");

            if (replace_glass_area.IsVisible)
                result = result + replace_glass_button.validation_error_string("Replace glass\n")
                                + (App.CurrentApp.UPVCRecord.replace_glass == 1 && !App.net.UPVCRecord.glass_complete ? "Replace glass details\n" : "");

            return result;
        }

        private string validate_page_1()
        {
            return cause_of_damage_area.validation_error_string()
                 + cosmetic_damage_button.validation_error_string("Cosmetic damage\n")
                 + gaskets_button.validation_error_string("Gaskets\n")
                 + (gasket_text.IsVisible && App.CurrentApp.UPVCRecord.gaskets_text == "" ? "Gaskets text\n" : "")
                 + room_location_picker.validation_error_string("Room location\n")
                 + hinge_colour_picker.validation_error_string("Hinge colour\n")
                 + WER_rating_picker.validation_error_string("WER Rating\n")
                 + replacement_reason_picker.validation_error_string("Reason for replacement\n")
                 + replace_explain_entry.validation_error_string("Explaination why item cannot be repaired\n");
        }

        private string validate_page_2()
        {
            return cills_picker.validation_error_string("Cills\n")
                 + handle_colour_picker.validation_error_string("Handle colour\n")
                 + handle_operation_button.validation_error_string("Handle operation\n")
                 + outer_section_picker.validation_error_string("Outer section size\n")
                 + frame_depth_picker.validation_error_string("Frame Depth\n")
                 + colour_picker.validation_error_string("Colour\n")
                 + brick_dimensions.validation_error_string("Brick width\n", "Brick height\n")
                 + internal_dimensions.validation_error_string("Internal width\n", "Internal height\n")
                 + addons_button.validation_error_string("Addons\n")
                 + addon_width_edit.validation_error_string("Addon width\n")
                 + addon_height_edit.validation_error_string("Addon height\n");
        }

        private string validate_page_3()
        {
            return midrail_button.validation_error_string("Midrail\n")
                 + midrail_height_edit.validation_error_string("Midrail Height\n")
                 + head_drip_button.validation_error_string("Head Drip\n")
                 + locking_type_picker.validation_error_string("Locking Type\n")
                 + letter_box_area.validation_error_string()
                 + pet_flap_area.validation_error_string();
        }

        private string validate_page_4()
        {
            return bead_type_picker.validation_error_string("Bead Type\n")
                 + glass_pattern_picker.validation_error_string("Glass Pattern\n")
                 + double_or_triple_button.validation_error_string("Double or triple\n")
                 + glass_type_picker.validation_error_string("Glass Type\n")
                 + special_glass_area.validation_error_string()
                 + document_L_picker.validation_error_string("Document 'L'\n")
                 + opens_button.validation_error_string("Opens\n")
                 + glaze_button.validation_error_string("Glaze\n")
                 + trickle_vents_button.validation_error_string("Trickle Vents\n");
        }

        private string validate_page_5()
        {
            return threshold_type_picker.validation_error_string("Threshold type\n")
                  + spacer_thickness_picker.validation_error_string("Spacer Thickness\n")
                  + spacer_colour_picker.validation_error_string("Spacer Colour\n")
                  + lock_type_button.validation_error_string("Lock Type\n")
                  + slide_position_button.validation_error_string("Door Slide Position\n")
                  + profile_type_button.validation_error_string("Profile type\n")
                  + summary_pto_area.validation_error_string();
        }

        protected override string validate_drawings_and_pictures()
        {
            return cause_of_damage_area.photo_validation_error_string(App.CurrentApp.UPVCRecord.no_of_photos)
                 + (App.CurrentApp.UPVCRecord.no_of_pics == 0 ? "Drawings\n" : "");
        }

        protected override string validate_page()
        {
            return validate_page_0()
                + validate_page_1()
                + validate_page_2()
                + validate_page_3()
                + validate_page_4()
                + validate_page_5();
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
                case 5: CurrentPage = Page5; break;
            }
        }
        */

        protected override void save_item(bool complete)
        {
            App.data.SaveUPVC(complete);
        }

        protected override bool already_signed()
        {
            return App.CurrentApp.UPVCRecord.bDifferentFromOriginal;
        }

        private void replace_panel_clicked(object sender, EventArgs e)
        {
            if (App.net.UPVCRecord.replace_panel == 1)
            {
                App.net.PanelRecord = App.data.GetPanelByContractUPVCItemNo(App.CurrentApp.HeaderRecord.udi_cont, App.CurrentApp.UPVCRecord.item_number);

                if (App.net.PanelRecord == null)
                {
                    App.net.table_init.CreatePanel();

                    App.CurrentApp.PanelRecord.upvc_item_number = App.CurrentApp.UPVCRecord.item_number;
                    App.CurrentApp.PanelRecord.item_number = App.CurrentApp.UPVCRecord.item_number;
                    App.data.SaveHeader();
                }

                App.CurrentApp.root_item_number = App.CurrentApp.UPVCRecord.item_number;
                App.CurrentApp.loaded_item_number = App.CurrentApp.PanelRecord.item_number;
                App.CurrentApp.CurrentItem = "panel";
                Navigation.PushAsync(new Panel(), false);
            }
        }

        private void SetPanelButton()
        {
            if (App.net.UPVCRecord.replace_panel == 1)
            {
                if (App.net.UPVCRecord.bPanelComplete == true)
                    do_replace_panel_button.ImageSource = "green_tick.png";
                else
                    do_replace_panel_button.ImageSource = "question.png";
            }
            else
                do_replace_panel_button.ImageSource = "na.png";
        }

        private void Replace_panel_button_OnSelectionChanged(object sender, EventArgs e)
        {
            SetPanelButton();
        }

        private void SetGlassButton()
        {
            if (App.net.UPVCRecord.replace_glass == 1) // Replace glass?
            {
                if (App.net.UPVCRecord.glass_complete)
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
            if (App.net.UPVCRecord.replace_glass == 1) // Replace glass?
            {
                int item_no = App.CurrentApp.UPVCRecord.item_number;

                App.net.GlassRecord = App.data.GetGlassByContractUPVCItemNo(App.CurrentApp.HeaderRecord.udi_cont, item_no);

                if (App.net.GlassRecord == null)
                {
                    App.net.table_init.CreateGlass();
                    App.CurrentApp.GlassRecord.item_number = App.CurrentApp.UPVCRecord.item_number;
                    App.CurrentApp.GlassRecord.parent_item = 7;
                    App.CurrentApp.loaded_item_number = App.CurrentApp.UPVCRecord.item_number;
                    App.CurrentApp.root_item_number = App.CurrentApp.UPVCRecord.item_number;
                    App.data.SaveHeader();
                }
                App.CurrentApp.CurrentItem = "glass";
                Navigation.PushAsync(new GlassItem(), false);
            }
        }
    }
}