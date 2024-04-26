using System;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimberItem : CarouselValidate
    {
        List<string> cols_1;
        List<string> cols_2;

        public TimberItem()
        {
            InitializeComponent();
            BindingContext = App.net.TimberRecord as TimberTable;
            save_on_pagechange = true;

            //Page 0
            List<string> mouldings_list = new List<string>() { "Routed", "Scribed" };

            timber_item_picker.SetPickerItems(App.net.type_of_item);
            replacement_reason_picker.SetPickerItems(App.net.replace_reason);
            additional_locks_picker.SetPickerItems(App.net.additional_locks);
            lock_make_picker.SetPickerItems(App.net.lock_make);
            temporary_button.set_button_list(SurveyFitterButtonLists.shared_temporary_button_list);
            wer_rating_picker.SetPickerItems(App.net.wer_rating);
            mouldings_picker.SetPickerItems(mouldings_list);
            gaskets_button.set_button_list(SurveyFitterButtonLists.shared_gasket_button_list);

            flat_question.IsVisible = SurveyFitterSharedLogic.timber_flat_question_visible();
            set_flat_message_visible();
            replacement_reason_picker.IsVisible = SurveyFitterSharedLogic.reason_not_repaired_visible(App.net.TimberRecord.bRepair);
            cosmetic_damage_button.IsVisible =
                gaskets_button.IsVisible = SurveyFitterSharedLogic.gaskets_visible(App.net.TimberRecord.bRepair);
            temporary_button.IsVisible = SurveyFitterSharedLogic.temporary_visible(App.net.TimberRecord.collect_and_copy);
            preglazed_door_button.IsVisible = SurveyFitterSharedLogic.timber_is_a_door();
            additional_locks_picker.IsVisible = SurveyFitterSharedLogic.timber_additional_locks_visible();
            new_lock_area.IsVisible = SurveyFitterSharedLogic.timber_new_lock_visible();
            new_lock_make_area.IsVisible = SurveyFitterSharedLogic.timber_new_lock_make_visible();
            set_locks_button_image();
            handles_area.IsVisible = SurveyFitterSharedLogic.handles_visible(App.net.TimberRecord.bRepair);
            set_handles_button_image();
            handles_text.IsVisible = SurveyFitterSharedLogic.handles_text_visible(App.net.TimberRecord.bRepair, App.net.TimberRecord.handles_req);
            replace_glass_area.IsVisible = SurveyFitterSharedLogic.replace_glass_visible(App.net.TimberRecord.bRepair);
            wer_rating_picker.IsVisible = SurveyFitterSharedLogic.WER_rating_visible(App.net.TimberRecord.bRepair, App.net.TimberRecord.Fensa);
            reason_explain_label.IsVisible =
            reason_explain_text.IsVisible = SurveyFitterSharedLogic.reason_not_repaired_visible(App.net.TimberRecord.bRepair);
            gasket_text.IsVisible = SurveyFitterSharedLogic.gaskets_text_visible(App.net.TimberRecord.bRepair, App.net.TimberRecord.gaskets);

            // Page 1
            List<string> door_wood_list = new List<string>() { "HW", "SW" };
            List<string> hinge_type_list = new List<string>() { "3 inch", "4 inch", "6 inch" };
            List<string> reas_no_docl_list = new List<string>() { "Internal door", "Into existing frame", "Part of a combi", "Less than 50% glass", "Outbuilding", "Repair only" };

            door_wood_picker.SetPickerItems(door_wood_list);
            frame_picker.SetPickerItems(door_wood_list);
            room_location_picker.SetPickerItems(App.net.room_location);
            hinge_type_picker.SetPickerItems(hinge_type_list);
            reason_not_document_L_picker.SetPickerItems(reas_no_docl_list);

            door_wood_picker.IsVisible = SurveyFitterSharedLogic.timber_door_wood_visible();
            weather_bar_button.IsVisible = SurveyFitterSharedLogic.timber_is_a_door();
            hinge_type_picker.IsVisible = SurveyFitterSharedLogic.timber_hinges_visible();
            reason_not_document_L_picker.IsVisible = SurveyFitterSharedLogic.timber_docL_reason_visible();
            pet_flap_area.IsVisible =
                letter_box_area.IsVisible = SurveyFitterSharedLogic.timber_letterbox_and_petflap_visible();

            // Page 2
            List<string> door_thickness_list = new List<string>() { "34", "40", "44", "54" };

            door_thickness_picker.SetPickerItems(door_thickness_list);
            set_door_size_picker();
            glazed_button.set_button_list(SurveyFitterButtonLists.timber_glazed_button_list);
            opens_button.set_button_list(SurveyFitterButtonLists.shared_in_out_button_list);
            cills_picker.SetPickerItems(App.net.cills);

            door_thickness_picker.IsVisible =
                door_size_picker.IsVisible =
                door_width_height_area.IsVisible =
                glazed_button.IsVisible = SurveyFitterSharedLogic.timber_is_a_door();
            door_size_reason_non_standard.IsVisible = SurveyFitterSharedLogic.timber_reason_door_size_not_standard_visible();

            // Page 3
            glass_pattern_picker.SetPickerItems(App.net.backing_glass);
            single_double_button.set_button_list(SurveyFitterButtonLists.shared_single_or_double_list);
            trickle_vents_picker.SetPickerItems(App.net.t_vents);
            hardware_colour_picker.SetPickerItems(App.net.hard_colour_timb);
            door_slides_button.set_button_list(SurveyFitterButtonLists.shared_inside_outside_button_list);
            locks_picker.SetPickerItems(App.net.locks_choose);

            set_thresher_visible();
            single_double_button.IsVisible = SurveyFitterSharedLogic.timber_single_or_double_visible();
            door_slides_button.IsVisible = SurveyFitterSharedLogic.timber_door_slides_on_visible();
            set_spacer_thickness_visible();
            fire_rated_button.IsVisible = SurveyFitterSharedLogic.timber_fire_rated_visible();

            // Page 4
            List<string> frame_list = new List<string>() { "None", "Stain", "Standard", "Full range" };

            cols_1 = new List<string>()
            {   "Sample sent to MTF",
                "Meranti Mahogany TR1511",
                "Meranti Meranti TR1518",
                "Meranti Natural TR1519",
                "Meranti Teak TR1510",
                "Meranti Chestnut TR1517",
                "Meranti Nuts TR1509",
                "Meranti Rosewood TR1512",
                "Meranti Ebb TR1515",
                "Meranti Dark Oak TR1507",
                "Meranti Light Oak TR1506",
                "Pine Chestnut TR1517",
                "Pine Mahogany TR1511",
                "Pine Dark Oak TR1507",
                "Pine Rosewood TR1512",
                "Pine Essen TR1514",
                "Pine Light Oak TR1506",
                "Pine Nuts TR1509",
                "Pine Teak TR1510",
                "Pine Opal White TR1501",
                "Pine Kiefer TR1505" }; //"Pine", "Dark Oak", "Teak", "Light Oak", "Walnut", "Clear", "Ebony" };

            cols_2 = new List<string>() { "Orange", "Yellow", "Green", "Blue", "Red", "Black", "White" };

            inside_door_colour_picker.IsVisible =
                outside_door_colour_picker.IsVisible = SurveyFitterSharedLogic.timber_door_colours_visible();
            inside_door_colour_picker.SetPickerItems(frame_list);
            set_door_code_picker(App.net.TimberRecord.door_color, inside_door_code_picker, inside_door_colour_picker.IsVisible, "Inside door code/colour");
            outside_door_colour_picker.SetPickerItems(frame_list);
            set_door_code_picker(App.net.TimberRecord.door_color_out, outside_door_code_picker, outside_door_colour_picker.IsVisible, "Outside door code/colour");
            inside_frame_colour_picker.SetPickerItems(frame_list);
            set_door_code_picker(App.net.TimberRecord.frame_color, inside_frame_code_picker, true, "Inside frame code/colour");
            outside_frame_colour_picker.SetPickerItems(frame_list);
            set_door_code_picker(App.net.TimberRecord.frame_color_out, outside_frame_code_picker, true, "Outside frame code/colour");

            // Page 5
            spacer_colour_picker.SetPickerItems(App.net.spacer_colour_new);
            glass_type_picker.SetPickerItems(App.net.glass_type);
            document_L_picker.SetPickerItems(App.net.docl_non_tim);
            special_glass_area.SetPickerItems(App.net.special_glass_back);

            set_spacer_warning_visible();
            glass_type_picker.IsVisible =
                special_glass_area.IsVisible = SurveyFitterSharedLogic.timber_glass_type_visible();
            // DocL visible done with spacer on page 3
            //SetPageNumber();
        }

        /*
        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            SetPageNumber();
        }
        */

        protected override void OnAppearing()
        {
            base.OnAppearing();
            set_handles_button_image();
            set_locks_button_image();
            SetGlassButton();

            App.CurrentApp.CurrentItem = "timber";
            App.net.TimberRecord.no_of_pics = SurveyFitterSharedLogic.count_drawings();
            App.net.TimberRecord.no_of_photos = SurveyFitterSharedLogic.count_photos();

            special_glass_area.set_complete(App.net.TimberRecord.lead_bDiamondComplete,
            App.net.TimberRecord.lead_bGeorgianComplete,
            App.net.TimberRecord.lead_bGeorgianComplete,
            App.net.TimberRecord.lead_bBarComplete);
        }

        private void set_flat_message_visible()
        {
            flat_warning_text.IsVisible = (flat_question.IsVisible && (App.net.TimberRecord.is_a_flat == 1));
        }

        private void set_locks_button_image()
        {
            if (App.net.TimberRecord.bNewLockingMech == 1)
            {
                if (App.net.TimberRecord.bLockComplete)
                    new_lock_button.ImageSource = "green_tick.png";
                else
                    new_lock_button.ImageSource = "question.png";
            }
            else
                new_lock_button.ImageSource = "na.png";
        }

        private void set_handles_button_image()
        {
            if (handles_text.IsVisible)
            {
                if (App.net.TimberRecord.bHandleDrawingComplete)
                    handles_button.ImageSource = "green_tick.png";
                else
                    handles_button.ImageSource = "question.png";
            }
            else
                handles_button.ImageSource = "na.png";
        }

        private void set_thresher_visible()
        {
            thresher_button.IsVisible = SurveyFitterSharedLogic.timber_is_a_door();

            if (!thresher_button.IsVisible)
                App.net.TimberRecord.thresher = 2;
        }

        private void set_spacer_thickness_visible()
        {
            spacer_thickness_picker.IsVisible =
                spacer_colour_picker.IsVisible =
                document_L_picker.IsVisible = SurveyFitterSharedLogic.timber_spacer_visible();

            if (App.net.TimberRecord.single_double == 3)
                spacer_thickness_picker.SetPickerItems(App.net.spacer_thickness3);
            else
                spacer_thickness_picker.SetPickerItems(App.net.spacer_thickness2);
        }

        private void set_spacer_warning_visible()
        {
            if (   spacer_colour_picker.IsVisible
                && (   App.net.TimberRecord.spacer_color == "Grey - Warm Edge - Plastic"
                    || App.net.TimberRecord.spacer_color == "Black - Warm Edge - Plastic"))
            {
                spacer_warning.IsVisible = true;
                spacer_warning.Text = "Limited stock. Try to use super spacer";
            }
            else
                spacer_warning.IsVisible = false;
        }

        private void set_door_size_picker()
        {
            List<string> door_size_list;
            switch (App.net.TimberRecord.door_thickness)
            {
                case "34":
                    // 27x78 added by James 23/7/19
                    door_size_list = new List<string>() { "15x78", "18x78", "21x78", "24x78", "27x78", "28x78", "30x78", "32x80", "33x78", "Non standard" };
                    break;
                case "40":
                case "44":
                case "54":
                    door_size_list = new List<string>() { "30x78", "32x80", "33x78", "33x81", "34x82", "34x86", "36x78", "Non standard" };
                    break;
                default:
                    door_size_list = null;
                    break;
            }

            door_size_picker.SetPickerItems(door_size_list);
        }

        private void set_door_code_picker(string colour, MartControls.EditPickerLabel control, bool parent_visibility, string default_label)
        {
            control.LabelText = default_label;

            switch (colour)
            {
                case "Stain": control.SetPickerItems(cols_1); break;
                case "Standard": control.SetPickerItems(cols_2); break;
                case "Full range":
                    control.SetPickerItems(App.net.ral_string);
                    control.LabelText = "Type in the RAL code";
                    if (control.Text == "")
                        control.Text = "RAL code: ";
                    break;
            }

            control.IsVisible = SurveyFitterSharedLogic.timber_door_colour_code_visible(colour, parent_visibility);
        }

        private void timber_item_changed(object sender, EventArgs e)
        {
            flat_question.IsVisible = SurveyFitterSharedLogic.timber_flat_question_visible();
            set_flat_message_visible();
            preglazed_door_button.IsVisible = SurveyFitterSharedLogic.timber_is_a_door();
            new_lock_area.IsVisible = SurveyFitterSharedLogic.timber_new_lock_visible();
            new_lock_make_area.IsVisible = SurveyFitterSharedLogic.timber_new_lock_make_visible();
            door_wood_picker.IsVisible = SurveyFitterSharedLogic.timber_door_wood_visible();
            weather_bar_button.IsVisible =
                door_thickness_picker.IsVisible =
                door_size_picker.IsVisible =
                door_width_height_area.IsVisible =
                glazed_button.IsVisible = SurveyFitterSharedLogic.timber_is_a_door();
            hinge_type_picker.IsVisible = SurveyFitterSharedLogic.timber_hinges_visible();
            pet_flap_area.IsVisible =
                letter_box_area.IsVisible = SurveyFitterSharedLogic.timber_letterbox_and_petflap_visible();
            door_size_reason_non_standard.IsVisible = SurveyFitterSharedLogic.timber_reason_door_size_not_standard_visible();
            set_thresher_visible();
            door_slides_button.IsVisible = SurveyFitterSharedLogic.timber_door_slides_on_visible();
            fire_rated_button.IsVisible = SurveyFitterSharedLogic.timber_fire_rated_visible();
            inside_door_colour_picker.IsVisible =
                outside_door_colour_picker.IsVisible = SurveyFitterSharedLogic.timber_door_colours_visible();
            set_door_code_picker(App.net.TimberRecord.door_color, inside_door_code_picker, inside_door_colour_picker.IsVisible, "Inside door code/colour");
            set_door_code_picker(App.net.TimberRecord.door_color_out, outside_door_code_picker, outside_door_colour_picker.IsVisible, "Inside frame code/colour");
        }

        private void flat_changed(object sender, EventArgs e)
        {
            set_flat_message_visible();
        }

        private void collect_and_copy_changed(object sender, EventArgs e)
        {
            temporary_button.IsVisible = SurveyFitterSharedLogic.temporary_visible(App.net.TimberRecord.collect_and_copy);
        }

        private void new_lock_required_clicked(object sender, EventArgs e)
        {
            new_lock_make_area.IsVisible = SurveyFitterSharedLogic.timber_new_lock_make_visible();
            set_locks_button_image();
        }

        private void new_lock_clicked(object sender, EventArgs e)
        {
            if (App.net.TimberRecord.bNewLockingMech == 1)
            {
                switch (App.net.TimberRecord.timber_item)
                {
                    case "Door":
                    case "French Doors": Navigation.PushAsync(new DoorLock(), false); break;
                    case "Window": Navigation.PushAsync(new WindowLock(), false); break;
                }
            }
        }

        private void handles_required_changed(object sender, EventArgs e)
        {
            handles_text.IsVisible = SurveyFitterSharedLogic.handles_text_visible(App.net.TimberRecord.bRepair, App.net.TimberRecord.handles_req);
            set_handles_button_image();
        }

        private void handles_info_clicked(object sender, EventArgs e)
        {
            if (App.net.TimberRecord.handles_req == 1)
            {
                App.net.drawing_type = "handles";

                if (App.net.TimberRecord.bHandleDrawingComplete == true)
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

        private void SetGlassButton()
        {
            if (App.net.TimberRecord.replace_glass == 1) // Replace glass?
            {
                if (App.net.TimberRecord.glass_complete)
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
            if (App.net.TimberRecord.replace_glass == 1) // Replace glass?
            {
                int item_no = App.CurrentApp.TimberRecord.item_number;

                App.net.GlassRecord = App.data.GetGlassByContractTimberItemNo(App.CurrentApp.HeaderRecord.udi_cont, item_no);

                if (App.net.GlassRecord == null)
                {
                    App.net.table_init.CreateGlass();
                    App.CurrentApp.GlassRecord.item_number = App.CurrentApp.TimberRecord.item_number;
                    App.CurrentApp.GlassRecord.parent_item = 6;
                    App.CurrentApp.loaded_item_number = App.CurrentApp.TimberRecord.item_number;
                    App.CurrentApp.root_item_number = App.CurrentApp.TimberRecord.item_number;
                    App.data.SaveHeader();
                }
                App.CurrentApp.CurrentItem = "glass";
                Navigation.PushAsync(new GlassItem(), false);
            }
        }

        private void gaskets_changed(object sender, EventArgs e)
        {
            gasket_text.IsVisible = SurveyFitterSharedLogic.gaskets_text_visible(App.net.TimberRecord.bRepair, App.net.TimberRecord.gaskets);
        }

        private void document_L_changed(object sender, EventArgs e)
        {
            reason_not_document_L_picker.IsVisible = SurveyFitterSharedLogic.timber_docL_reason_visible();
        }

        private void door_thickness_changed(object sender, EventArgs e)
        {
            set_door_size_picker();
        }

        private void door_size_changed(object sender, EventArgs e)
        {
            door_size_reason_non_standard.IsVisible = SurveyFitterSharedLogic.timber_reason_door_size_not_standard_visible();
            if (App.net.TimberRecord.standard_sizes.Length == 5)
            {
                door_width_height_area.width_text = App.net.TimberRecord.standard_sizes.Substring(0, 2);
                door_width_height_area.height_text = App.net.TimberRecord.standard_sizes.Substring(3, 2);
            }
        }

        private void glass_pattern_changed(object sender, EventArgs e)
        {
            single_double_button.IsVisible = SurveyFitterSharedLogic.timber_single_or_double_visible();
            set_spacer_thickness_visible();
            fire_rated_button.IsVisible = SurveyFitterSharedLogic.timber_fire_rated_visible();
            set_spacer_warning_visible();
            glass_type_picker.IsVisible =
                special_glass_area.IsVisible = SurveyFitterSharedLogic.timber_glass_type_visible();
        }

        private void single_double_changed(object sender, EventArgs e)
        {
            set_spacer_thickness_visible();
        }

        private void inside_door_colour_changed(object sender, EventArgs e)
        {
            set_door_code_picker(App.net.TimberRecord.door_color, inside_door_code_picker, inside_door_colour_picker.IsVisible, "Inside door code/colour");
        }

        private void outside_door_colour_changed(object sender, EventArgs e)
        {
            set_door_code_picker(App.net.TimberRecord.door_color_out, outside_door_code_picker, outside_door_colour_picker.IsVisible, "Outside door code/colour");
        }

        private void inside_frame_colour_changed(object sender, EventArgs e)
        {
            set_door_code_picker(App.net.TimberRecord.frame_color, inside_frame_code_picker, true, "Inside frame code/colour");
        }

        private void outside_frame_colour_changed(object sender, EventArgs e)
        {
            set_door_code_picker(App.net.TimberRecord.frame_color_out, outside_frame_code_picker, true, "Outside frame code/colour");
        }

        private void space_colour_changed(object sender, EventArgs e)
        {
            set_spacer_warning_visible();
        }

        private string validate_page_0()
        {
            string result = timber_item_picker.validation_error_string("Timber item\n")
                          + flat_question.validation_error_string("Is the property a flat\n")
                          + replacement_reason_picker.validation_error_string("Reason for replacement\n")
                          + cosmetic_damage_button.validation_error_string("Cosmetic Damage\n")
                          + collect_and_copy_button.validation_error_string("Collect and Copy\n")
                          + temporary_button.validation_error_string("Temporary\n")
                          + preglazed_door_button.validation_error_string("Preglazed door\n")
                          + additional_locks_picker.validation_error_string("Additional Locks\n");

            if (new_lock_area.IsVisible)
            {
                if (App.CurrentApp.TimberRecord.bNewLockingMech == 0)
                    result += "New Locking Mechanism req.\n";
                else if (App.CurrentApp.TimberRecord.bNewLockingMech == 1)
                {
                    if (App.CurrentApp.TimberRecord.timber_item == "Door" ||
                        App.CurrentApp.TimberRecord.timber_item == "French Doors")
                    {
                        if (!App.CurrentApp.TimberRecord.bDoorComplete)
                            result += "Locking Mechanism Data\n";
                    }
                    else if (App.CurrentApp.TimberRecord.timber_item == "Window")
                    {
                        if (!App.CurrentApp.TimberRecord.bWindowComplete)
                            result += "Locking Mechanism Data\n";
                    }

                    result = result + lock_make_picker.validation_error_string("Lock Make\n")
                                    + lock_codes_entry.validation_error_string("Lock Codes\n");
                }
            }

            return result + (handles_area.IsVisible ? SurveyFitterSharedLogic.handles_validation_error_text(App.CurrentApp.TimberRecord.handles_req, App.CurrentApp.TimberRecord.handles_text, App.CurrentApp.TimberRecord.bHandleDrawingComplete) : "")
                          + (replace_glass_area.IsVisible ?  replace_glass_button.validation_error_string("Replace glass\n")
                                                           + (App.CurrentApp.TimberRecord.replace_glass == 1 && !App.net.TimberRecord.glass_complete ? "Replace glass details\n" : "")
                                                          : "")
                          + wer_rating_picker.validation_error_string("WER Rating\n")
                          + mouldings_picker.validation_error_string("Moulding\n")
                          + (reason_explain_text.IsVisible && App.CurrentApp.TimberRecord.replace_explain == "" ? "Explaination why item cannot be repaired\n" : "")
                          + gaskets_button.validation_error_string("Gaskets \n")
                          + (gasket_text.IsVisible && App.CurrentApp.TimberRecord.gaskets_text == "" ? "Gaskets text\n" : "");
        }

        private string validate_page_1()
        {
            return cause_of_damage_area.validation_error_string()
                 + door_wood_picker.validation_error_string("Door wood\n")
                 + frame_picker.validation_error_string("Frame type\n")
                 + new_frame_button.validation_error_string("New frame\n")
                 + weather_bar_button.validation_error_string("Weather bar\n")
                 + room_location_picker.validation_error_string("Room location\n")
                 + hinge_type_picker.validation_error_string("Hinge type\n")
                 + docL_compliant_button.validation_error_string("Document 'L'\n")
                 + reason_not_document_L_picker.validation_error_string("Reason not Document 'L'\n")
                 + letter_box_area.validation_error_string()
                 + pet_flap_area.validation_error_string();
        }

        private string validate_page_2()
        {
            return repair_frame_button.validation_error_string("Repair frame\n")
                 + door_thickness_picker.validation_error_string("Door thickness\n")
                 + door_size_picker.validation_error_string("Door size\n")
                 + door_size_reason_non_standard.validation_error_string("Reason non standard\n")
                 + door_width_height_area.validation_error_string("Door width\n", "Door height\n")
                 + glazed_button.validation_error_string("Glazed\n")
                 + opens_button.validation_error_string("Opens\n")
                 + new_sash_button.validation_error_string("New sash reqired\n")
                 + head_drip_button.validation_error_string("Head drip\n")
                 + draught_strip_button.validation_error_string("Draught strip\n")
                 + cills_picker.validation_error_string("Cills\n");
        }

        private string validate_page_3()
        {
            return thresher_button.validation_error_string("Thresher\n")
                 + glass_pattern_picker.validation_error_string("Glass Pattern\n")
                 + single_double_button.validation_error_string("Single or double\n")
                 + trickle_vents_picker.validation_error_string("Trickle vents\n")
                 + hardware_colour_picker.validation_error_string("Hardware Colour\n")
                 + door_slides_button.validation_error_string("Door slides\n")
                 + spacer_thickness_picker.validation_error_string("Spacer thickness\n")
                 + spacer_colour_picker.validation_error_string("Spacer colour\n")
                 + locks_picker.validation_error_string("Locks\n")
                 + brick_dimensions.validation_error_string("Brick Width\n", "Brick Height\n")
                 + internal_dimensions.validation_error_string("Internal Width\n", "Internal Height\n")
                 + fire_rated_button.validation_error_string("Fire rated glass\n");
        }

        private string validate_page_4()
        {
            return inside_door_colour_picker.validation_error_string("Inside door colour\n")
                 + inside_door_code_picker.validation_error_string("Inside door colour code\n")
                 // + (app.CurrentApp.TimberItem.? == "RAL code: " ? "Inside door colour code\n" : "")
                 + outside_door_colour_picker.validation_error_string("Outside door colour\n")
                 + outside_door_code_picker.validation_error_string("Outside door colour code\n")
                 + inside_frame_colour_picker.validation_error_string("Inside frame colour\n")
                 + inside_frame_code_picker.validation_error_string("Inside frame colour code\n")
                 + outside_frame_colour_picker.validation_error_string("Outside frame colour\n")
                 + outside_frame_code_picker.validation_error_string("Outside frame colour code\n");
        }

        private string validate_page_5()
        {
            return glass_type_picker.validation_error_string("Glass Type\n")
                 + special_glass_area.validation_error_string()
                 + document_L_picker.validation_error_string("Document 'L'\n")
                 + summary_pto_area.validation_error_string();
        }

        protected override string validate_drawings_and_pictures()
        {
            return cause_of_damage_area.photo_validation_error_string(App.CurrentApp.TimberRecord.no_of_photos)
                 + (App.CurrentApp.TimberRecord.no_of_pics == 0 ? "Drawings\n" : "")
                 + (App.CurrentApp.TimberRecord.timber_item != "Window" && !App.net.TimberRecord.b_signed ? "Colour signature\n" : "");
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
            App.data.SaveTimber(complete);
        }

        protected override bool already_signed()
        {
            return App.CurrentApp.TimberRecord.bDifferentFromOriginal;
        }
    }
}