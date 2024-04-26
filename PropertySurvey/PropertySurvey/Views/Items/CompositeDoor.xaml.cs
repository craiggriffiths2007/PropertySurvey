using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompositeDoor : CarouselValidate
    {
        public CompositeDoor()
        {
            InitializeComponent();
            BindingContext = App.net.CompRecord as CompositeTable;
            save_on_pagechange = true;

            // Page 0
            // Combo boxes
            List<string> door_make_list = new List<string>() { "", "Rockdoor", "Solidoor", "Fusion/Global Door", "Steel Skin", "Mart Door" };
            List<string> frame_list = new List<string>() { "", "HW", "SW", "UPVC" };
            List<string> frame_colour_inside_list = new List<string>() { "", "White", "Other" };
            List<string> frame_colour_outside_list = new List<string>() { "", "White", "Mahogany", "Oak", "Whitegrain", "Cherrywood", "Black-Brown", "Other" };
            List<string> door_colour_inside_list = new List<string>() { "", "White", "Other" };
            List<string> door_colour_outside_list = new List<string>() { "", "White", "Red", "Green", "Black", "Oak", "Dark Oak", "Light Oak", "Other" };

            gaskets_button.set_button_list(SurveyFitterButtonLists.shared_gasket_button_list);
            door_make_picker.SetPickerItems(door_make_list);
            WER_rating_picker.SetPickerItems(App.net.wer_rating);
            frame_picker.SetPickerItems(frame_list);
            trickle_vents_picker.SetPickerItems(App.net.t_vents);
            frame_colour_inside_picker.SetPickerItems(frame_colour_inside_list);
            frame_colour_outside_picker.SetPickerItems(frame_colour_outside_list);
            door_colour_inside_picker.SetPickerItems(door_colour_inside_list);
            door_colour_outside_picker.SetPickerItems(door_colour_outside_list);

            is_lock_button.set_button_list(SurveyFitterButtonLists.composite_is_lock_button_list);
            opens_button.set_button_list(SurveyFitterButtonLists.shared_in_out_button_list);
            hinged_on_button.set_button_list(SurveyFitterButtonLists.composite_hinged_on_button_list);

            no_repair_reason_entry.IsVisible = SurveyFitterSharedLogic.reason_not_repaired_visible(App.net.CompRecord.bRepair);
            replace_glass_area.IsVisible = SurveyFitterSharedLogic.replace_glass_visible(App.net.CompRecord.bRepair);
            handles_area.IsVisible = SurveyFitterSharedLogic.handles_visible(App.net.CompRecord.bRepair);
            handles_entry.IsVisible = SurveyFitterSharedLogic.handles_text_visible(App.CurrentApp.CompRecord.bRepair, App.CurrentApp.CompRecord.handles_req);
            gaskets_button.IsVisible = SurveyFitterSharedLogic.gaskets_visible(App.net.CompRecord.bRepair);
            gasket_text.IsVisible = SurveyFitterSharedLogic.gaskets_text_visible(App.net.CompRecord.bRepair, App.net.CompRecord.gaskets);
            WER_rating_picker.IsVisible = SurveyFitterSharedLogic.WER_rating_visible(App.CurrentApp.CompRecord.bRepair, App.CurrentApp.CompRecord.fensa);
            set_please_survey_visible();
            set_flat_warning_visible();
            lock_other_text.IsVisible = SurveyFitterSharedLogic.composite_other_lock_text_visible();

            // Page 1 - Has no comboboxes or buttons

            // Page 2 - All comboboxes and buttons are within other controls

            // Page 3
            handle_colour_picker.SetPickerItems(App.net.handle_colour);
            set_threshold_picker_items();

            addons_width_entry.IsVisible =
                addons_height_entry.IsVisible = SurveyFitterSharedLogic.composite_addon_dimensions_visible();
            special_glass_area.IsVisible =
            glass_type_picker.IsVisible =
            spacer_thickness_picker.IsVisible =
            spacer_colour_picker.IsVisible =
            document_L_picker.IsVisible = SurveyFitterSharedLogic.composite_glass_questions_visible();

            // Page 4
            glass_pattern_picker.SetPickerItems(App.net.backing_glass);
            special_glass_area.SetPickerItems(App.net.special_glass_back);
            glass_type_picker.SetPickerItems(App.net.glass_type);
            spacer_thickness_picker.SetPickerItems(App.net.spacer_thickness2);
            spacer_colour_picker.SetPickerItems(App.net.spacer_colour_new);
            document_L_picker.SetPickerItems(App.net.docl_non);
            room_location_picker.SetPickerItems(App.net.room_location);

            handle_operation_button.set_button_list(SurveyFitterButtonLists.shared_handle_operation_list);
            profile_type_button.set_button_list(SurveyFitterButtonLists.shared_profile_type_button_list); // ToDo Needs an alert when clicking on rustic(sculptured)

            // Page 5
            List<string> cills_list = new List<string>() { "", "150", "180", "Stubb", "None" };

            cills_picker.SetPickerItems(cills_list);

            glaze_button.set_button_list(SurveyFitterButtonLists.shared_internal_external_button_list);
            //SetPageNumber();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.CurrentApp.CurrentItem = "comp";
            App.net.CompRecord.no_of_pics = SurveyFitterSharedLogic.count_drawings();
            App.net.CompRecord.no_of_photos = SurveyFitterSharedLogic.count_photos();

            SetGlassButton();
            set_handles_button_image();
        }

        private void SetGlassButton()
        {
            if (App.net.CompRecord.replace_glass == 1) // Replace glass
            {
                if (App.net.CompRecord.glass_complete)
                    do_replace_glass_button.ImageSource = "green_tick.png";
                else
                    do_replace_glass_button.ImageSource = "question.png";
            }
            else
                do_replace_glass_button.ImageSource = "na.png";
        }

        private void set_handles_button_image()
        {
            if (App.net.CompRecord.handles_req == 1)
            {
                if (App.net.CompRecord.bHandleDrawingComplete)
                    handles_button.ImageSource = "green_tick.png";
                else
                    handles_button.ImageSource = "question.png";
            }
            else
                handles_button.ImageSource = "na.png";
        }

        private void gaskets_changed(object sender, EventArgs e)
        {
            gasket_text.IsVisible = SurveyFitterSharedLogic.gaskets_text_visible(App.CurrentApp.CompRecord.bRepair, App.net.CompRecord.gaskets);
        }

        private void set_threshold_picker_items()
        {
            List<string> threshold_list1;
            if (App.net.CompRecord.door_make == "Fusion/Global Door")
                threshold_list1 = new List<string>() { "70mm uPVC outer", "Low Ali threshold 15mm clearance" };
            else
                threshold_list1 = new List<string>() { "56mm uPVC outer", "70mm uPVC outer", "Low threshold 5mm clearance" };

            threshold_type_picker.SetPickerItems(threshold_list1);
        }

        private void set_please_survey_visible()
        {
            please_survey_text.IsVisible = (App.net.CompRecord.fire_door == 1);
        }

        private void set_flat_warning_visible()
        {
            flat_warning_text.IsVisible = (App.net.CompRecord.is_a_flat == 1);
        }

        public void door_make_changed(object sender, EventArgs e)
        {
            set_threshold_picker_items();
        }

        public void fire_door_button_click(object sender, EventArgs e)
        {
            set_please_survey_visible();
        }

        public void flat_button_click(object sender, EventArgs e)
        {
            set_flat_warning_visible();
        }

        private void is_lock_button_click(object sender, EventArgs e)
        {
            lock_other_text.IsVisible = SurveyFitterSharedLogic.composite_other_lock_text_visible();
        }

        private void replace_repair_button_click(object sender, EventArgs e)
        {
            no_repair_reason_entry.IsVisible = SurveyFitterSharedLogic.reason_not_repaired_visible(App.net.CompRecord.bRepair);
            replace_glass_area.IsVisible = SurveyFitterSharedLogic.replace_glass_visible(App.net.CompRecord.bRepair);
        }

        private void replace_glass_changed(object sender, EventArgs e)
        {
            SetGlassButton();
        }

        private void replace_glass_clicked(object sender, EventArgs e)
        {
            if (App.net.CompRecord.replace_glass == 1) // Replace glass?
            {
                int item_no = App.CurrentApp.CompRecord.item_number;

                App.net.GlassRecord = App.data.GetGlassByContractCompositeItemNo(App.CurrentApp.HeaderRecord.udi_cont, item_no);

                if (App.net.GlassRecord == null)
                {
                    App.net.table_init.CreateGlass();
                    App.CurrentApp.GlassRecord.item_number = App.CurrentApp.CompRecord.item_number;
                    App.CurrentApp.GlassRecord.parent_item = 3;
                    App.CurrentApp.loaded_item_number = App.CurrentApp.CompRecord.item_number;
                    App.CurrentApp.root_item_number = App.CurrentApp.CompRecord.item_number;
                    App.data.SaveHeader();
                }
                App.CurrentApp.CurrentItem = "glass";
                Navigation.PushAsync(new GlassItem(), false);
            }
        }

        private void handles_required_changed(object sender, EventArgs e)
        {
            handles_entry.IsVisible = SurveyFitterSharedLogic.handles_text_visible(App.CurrentApp.CompRecord.bRepair, App.CurrentApp.CompRecord.handles_req);
            set_handles_button_image();
        }

        private void handles_clicked(object sender, EventArgs e)
        {
            if (App.net.CompRecord.handles_req == 1)
            {
                App.net.drawing_type = "handles";

                if (App.net.CompRecord.bHandleDrawingComplete == true)
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

        private void addons_button_click(object sender, EventArgs e)
        {
            addons_width_entry.IsVisible =
                addons_height_entry.IsVisible = SurveyFitterSharedLogic.composite_addon_dimensions_visible();
        }

        private void glass_pattern_changed(object sender, EventArgs e)
        {
            special_glass_area.IsVisible =
            glass_type_picker.IsVisible =
            spacer_thickness_picker.IsVisible =
            spacer_colour_picker.IsVisible =
            document_L_picker.IsVisible = SurveyFitterSharedLogic.composite_glass_questions_visible();
        }

        private string validate_page_0()
        {
            return no_repair_reason_entry.validation_error_string("Reason cannot be repaired\n")
                 + (replace_glass_area.IsVisible ? replace_glass_button.validation_error_string("Replace glass\n")
                                                  + (App.CurrentApp.CompRecord.replace_glass == 1 && !App.net.CompRecord.glass_complete ? "Replace glass details\n" : "")
                                                 : "")
                 + (handles_area.IsVisible ? SurveyFitterSharedLogic.handles_validation_error_text(App.CurrentApp.CompRecord.handles_req, App.CurrentApp.CompRecord.handles_text, App.CurrentApp.CompRecord.bHandleDrawingComplete) : "")
                 + gaskets_button.validation_error_string("Gaskets\n")
                 + (gasket_text.IsVisible && App.CurrentApp.CompRecord.gaskets_text == "" ? "Gaskets text\n" : "")
                 + door_make_picker.validation_error_string("Door make\n")
                 + fire_door_button.validation_error_string("Fire door\n")
                 + flat_button.validation_error_string("Is the property a flat\n")
                 + cause_of_damage_area.validation_error_string()
                 + WER_rating_picker.validation_error_string("WER Rating\n")
                 + frame_picker.validation_error_string("Frame\n")
                 + is_lock_button.validation_error_string("Lock\n")
                 + (lock_other_text.IsVisible && App.CurrentApp.CompRecord.lock_other_text == "" ? "Lock other text\n" : "")
                 + opens_button.validation_error_string("Opens\n")
                 + hinged_on_button.validation_error_string("Hinged on\n")
                 + trickle_vents_picker.validation_error_string("Trickle vents\n")
                 + frame_colour_inside_picker.validation_error_string("Frame colour inside\n")
                 + frame_colour_outside_picker.validation_error_string("Frame colour outside\n")
                 + door_colour_inside_picker.validation_error_string("Door colour inside\n")
                 + door_colour_outside_picker.validation_error_string("Door colour outside\n");
        }

        private string validate_page_1()
        {
            return head_drip_button.validation_error_string("Head drip\n")
                 + door_design_entry.validation_error_string("Door design\n")
                 + glass_design_entry.validation_error_string("Glass design\n");
        }

        private string validate_page_2()
        {
            return letter_box_area.validation_error_string()
                 + pet_flap_area.validation_error_string();
        }

        private string validate_page_3()
        {
            return brick_dimensions.validation_error_string("Brick width\n", "Brick height\n")
                 + internal_dimensions.validation_error_string("Internal height\n", "Internal width\n")
                 + addons_button.validation_error_string("Addons\n")
                 + addons_width_entry.validation_error_string("Addons width\n")
                 + addons_height_entry.validation_error_string("Addons height\n")
                 + handle_colour_picker.validation_error_string("Handle colour\n")
                 + threshold_type_picker.validation_error_string("Threshold type\n");
        }

        private string validate_page_4()
        {
            return handle_operation_button.validation_error_string("Handle operation\n")
                 + glass_pattern_picker.validation_error_string("Glass pattern\n")
                 + glass_type_picker.validation_error_string("Glass type\n")
                 + spacer_thickness_picker.validation_error_string("Spacer thickness\n")
                 + spacer_colour_picker.validation_error_string("Spacer colour\n")
                 + document_L_picker.validation_error_string("Document L\n")
                 + profile_type_button.validation_error_string("Profile type\n")
                 + room_location_picker.validation_error_string("Room location\n")
                 + special_glass_area.validation_error_string();
        }

        private string validate_page_5()
        {
            return glaze_button.validation_error_string("Glaze\n")
                 + cills_picker.validation_error_string("Cills\n")
                 + summary_pto_area.validation_error_string();
        }

        protected override string validate_drawings_and_pictures()
        {
            return cause_of_damage_area.photo_validation_error_string(App.CurrentApp.CompRecord.no_of_photos)
                 + (App.CurrentApp.CompRecord.no_of_pics == 0 ? "Drawings\n" : "");
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
            App.data.SaveComp(complete);
        }

        protected override bool already_signed()
        {
            return App.CurrentApp.CompRecord.bDifferentFromOriginal;
        }
    }
}