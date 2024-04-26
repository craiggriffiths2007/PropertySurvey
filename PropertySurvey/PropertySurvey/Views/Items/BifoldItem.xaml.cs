using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BifoldItem : CarouselValidate
    {
        List<string> cill_requirements_list = new List<string>() { "150 x 25mm", "190 x 25mm", "225 x 25mm", "No cill required" };
        List<string> handle_colour_list = new List<string>() { "White", "Black", "Brush Chrome" };
        List<string> door_colour_list = new List<string>() { "Standard white 9016 Gloss", "Standard Grey 7016 Matt", "Standard Black 9005 Matt", "Other RAL(specify)..", "Dual RAL(specify).." };
        List<string> glaze_options_list = new List<string>() { "Standard DG - 28mm", "Standard DG laminated - 28mm" };

        public BifoldItem()
        {
            InitializeComponent();
            BindingContext = App.net.BifoldRecord as BifoldTable;
            changed_question_required = false; // Prevent the ItemChanged screen from being shown.

            // Page 0
            List<string> door_type_list = new List<string>() { "Warmcore", "Schuco", "Smarts", "KAT PVCu Bifold Doors" };
            List<string> hardware_list = new List<string>() { "Anodised Silver (matt fin)", "Anodised Black (matt fin)", "Brushed Graphite" };
            List<string> kat_knock_on_list = new List<string>() { "15mm", "25mm", "50mm" };
            List<string> frame_colour_list = new List<string>() { "White", "Cream", "Grey", "Black" };
            List<string> internal_colour_of_doors_list = new List<string>() { "Same as above", "White" };

            gaskets_button.set_button_list(SurveyFitterButtonLists.shared_gasket_button_list);
            door_type_picker.SetPickerItems(door_type_list);
            WER_rating_picker.SetPickerItems(App.net.wer_rating);
            opens_button.set_button_list(SurveyFitterButtonLists.shared_in_out_button_list);
            hardware_picker.SetPickerItems(hardware_list);
            knock_on_picker.SetPickerItems(kat_knock_on_list);
            internal_colour_picker.SetPickerItems(frame_colour_list);
            external_colour_picker.SetPickerItems(frame_colour_list);
            internal_door_colour_picker.SetPickerItems(internal_colour_of_doors_list);
            glazing_options_picker.SetPickerItems(glaze_options_list);

            SurveyFitterSharedLogic.bifold_set_cill_image(cill_image);

            no_repair_reason_entry.IsVisible = SurveyFitterSharedLogic.reason_not_repaired_visible(App.net.BifoldRecord.bRepair);
            replace_glass_area.IsVisible = SurveyFitterSharedLogic.replace_glass_visible(App.net.BifoldRecord.bRepair);
            handles_area.IsVisible = SurveyFitterSharedLogic.handles_visible(App.net.BifoldRecord.bRepair);
            handles_entry.IsVisible = SurveyFitterSharedLogic.handles_text_visible(App.CurrentApp.BifoldRecord.bRepair, App.CurrentApp.BifoldRecord.handles_req);
            gaskets_button.IsVisible = SurveyFitterSharedLogic.gaskets_visible(App.net.BifoldRecord.bRepair);
            gasket_text.IsVisible = SurveyFitterSharedLogic.gaskets_text_visible(App.net.BifoldRecord.bRepair, App.net.BifoldRecord.gaskets);
            addon_width_edit.IsVisible =
                addon_height_edit.IsVisible = SurveyFitterSharedLogic.addon_dimensions_visible (App.CurrentApp.BifoldRecord.addons);
            WER_rating_picker.IsVisible = SurveyFitterSharedLogic.WER_rating_visible(App.CurrentApp.BifoldRecord.bRepair, App.CurrentApp.BifoldRecord.fensa);
        }

        private void set_door_type_questions()
        {
            hardware_picker.IsVisible =
                internal_colour_picker.IsVisible =
                external_colour_picker.IsVisible = SurveyFitterSharedLogic.bifold_hardware_visible();
            handle_colour_picker.IsVisible =
                door_colour_picker.IsVisible =
                internal_door_colour_picker.IsVisible = SurveyFitterSharedLogic.bifold_handle_colour_visible();
            glazing_options_picker.IsVisible = SurveyFitterSharedLogic.glazing_options_visible();
            knock_on_picker.IsVisible = SurveyFitterSharedLogic.knock_on_visible();

            switch (App.net.BifoldRecord.door_type)
            {
                case "Warmcore":
                    List<string> sill_type_list = new List<string>() { "Part M low threshold", "Frame - no cill", "Frame - 150mm cill" };
                    threshold_picker.SetPickerItems(sill_type_list);
                    cill_picker.SetPickerItems(cill_requirements_list);
                    break;

                case "Schuco":
                    List<string> schuco_cill_list = new List<string>() { "XP View Rebate", "XP View Rebate and Cill" };
                    threshold_picker.SetPickerItems(schuco_cill_list);
                    cill_picker.SetPickerItems(cill_requirements_list);
                    handle_colour_picker.SetPickerItems(handle_colour_list);
                    door_colour_picker.SetPickerItems(door_colour_list);
                    break;

                case "Smarts":
                    List<string> smarts_cill_list = new List<string>() { "Rebated on a cill", "Rebated (Standard)", "Low threshold (Internal)" };
                    threshold_picker.SetPickerItems(smarts_cill_list);
                    cill_picker.SetPickerItems(cill_requirements_list);
                    handle_colour_picker.SetPickerItems(handle_colour_list);
                    door_colour_picker.SetPickerItems(door_colour_list);
                    break;

                case "KAT PVCu Bifold Doors":
                    List<string> kat_threshold = new List<string>() { "PVC threshold", "Aluminium low 30mm", "Doc. M comp. ramp Internal", "Doc. M comp. ramp External" };
                    List<string> kat_cill_requirements = new List<string>() { "85mm", "150mm", "180mm", "No cill required" };
                    List<string> kat_handle_colour_list = new List<string>() { "White", "Black", "Satin", "Champagne" };
                    List<string> kat_door_colour_list = new List<string>() { "White", "Cream", "Golden oak", "Rosewood", "Irish oak", "White ash", "Golden oak on white", "Rosewood on white", "Irish oak on white", "Black ash on white", "Grey ash on white" };

                    threshold_picker.SetPickerItems(kat_threshold);
                    cill_picker.SetPickerItems(kat_cill_requirements);
                    handle_colour_picker.SetPickerItems(kat_handle_colour_list);
                    door_colour_picker.SetPickerItems(kat_door_colour_list);
                    break;

                default :
                    threshold_picker.SetPickerItems(null);
                    cill_picker.SetPickerItems(null);
                    break;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.CurrentApp.CurrentItem = "bifold";

            set_handles_button_image();
            SetGlassButton();
            SurveyFitterSharedLogic.bifold_set_door_text_and_image(no_of_doors_text, type_image);

            App.net.BifoldRecord.no_of_pics = SurveyFitterSharedLogic.count_drawings();
            App.net.BifoldRecord.no_of_photos = SurveyFitterSharedLogic.count_photos();
            // App.net.BifoldRecord.no_of_vids ???

            if (App.CurrentApp.BifoldRecord.number_of_doors_text != "")
                bifold_type_button.ImageSource = "green_tick.png";
            else
                bifold_type_button.ImageSource = "question.png";

            if (App.CurrentApp.BifoldRecord.bifold_signed == 1)
                signature_button.ImageSource = "green_tick.png";
            else
                signature_button.ImageSource = "question.png";
        }

        private void SetGlassButton()
        {
                if (App.net.BifoldRecord.replace_glass == 1) // Replace glass
                {
                    if (App.net.BifoldRecord.glass_complete)
                        do_replace_glass_button.ImageSource = "green_tick.png";
                    else
                        do_replace_glass_button.ImageSource = "question.png";
                }
                else
                    do_replace_glass_button.ImageSource = "na.png";
        }

        private void set_handles_button_image()
        {
            if (App.net.BifoldRecord.handles_req == 1)
            {
                if (App.net.BifoldRecord.bHandleDrawingComplete)
                    handles_button.ImageSource = "green_tick.png";
                else
                    handles_button.ImageSource = "question.png";
            }
            else
                handles_button.ImageSource = "na.png";
        }

        private void repair_or_replace_changed(object sender, EventArgs e)
        {
            no_repair_reason_entry.IsVisible = SurveyFitterSharedLogic.reason_not_repaired_visible(App.net.BifoldRecord.bRepair);
            replace_glass_area.IsVisible = SurveyFitterSharedLogic.replace_glass_visible(App.net.BifoldRecord.bRepair);
        }

        private void door_type_changed(object sender, EventArgs e)
        {
            set_door_type_questions();
        }

        private void replace_glass_changed(object sender, EventArgs e)
        {
            SetGlassButton();
        }

        private void replace_glass_clicked(object sender, EventArgs e)
        {
            if (App.net.BifoldRecord.replace_glass == 1) // Replace glass?
            {
                int item_no = App.CurrentApp.BifoldRecord.item_number;

                App.net.GlassRecord = App.data.GetGlassByContractBifoldItemNo(App.CurrentApp.HeaderRecord.udi_cont, item_no);

                if (App.net.GlassRecord == null)
                {
                    App.net.table_init.CreateGlass();
                    App.CurrentApp.GlassRecord.item_number = App.CurrentApp.BifoldRecord.item_number;
                    App.CurrentApp.GlassRecord.parent_item = 2;
                    App.CurrentApp.loaded_item_number = App.CurrentApp.BifoldRecord.item_number;
                    App.CurrentApp.root_item_number = App.CurrentApp.BifoldRecord.item_number;
                    App.data.SaveHeader();
                }
                App.CurrentApp.CurrentItem = "glass";
                Navigation.PushAsync(new GlassItem(), false);
            }
        }

        private void gaskets_changed(object sender, EventArgs e)
        {
            gasket_text.IsVisible = SurveyFitterSharedLogic.gaskets_text_visible(App.CurrentApp.BifoldRecord.bRepair, App.net.BifoldRecord.gaskets);
        }

        private void handles_required_changed(object sender, EventArgs e)
        {
            handles_entry.IsVisible = SurveyFitterSharedLogic.handles_text_visible(App.CurrentApp.BifoldRecord.bRepair, App.CurrentApp.BifoldRecord.handles_req);
            set_handles_button_image();
        }

        private void handles_clicked(object sender, EventArgs e)
        {
            if (App.net.BifoldRecord.handles_req == 1)
            {
                App.net.drawing_type = "handles";

                if (App.net.BifoldRecord.bHandleDrawingComplete == true)
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

        private void threshold_changed (object sender, EventArgs e)
        {
            SurveyFitterSharedLogic.bifold_set_cill_image(cill_image);
        }

        private void OnTypeOfDoorsClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BifoldDoorType(), false);
        }

        private void addons_button_clicked(object sender, EventArgs e)
        {
            addon_width_edit.IsVisible =
                addon_height_edit.IsVisible = SurveyFitterSharedLogic.addon_dimensions_visible (App.CurrentApp.BifoldRecord.addons);
        }

        private void Signature_button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BifoldSignature(), false);
        }

        protected override string validate_drawings_and_pictures()
        {
            return cause_of_damage_area.photo_validation_error_string(App.CurrentApp.BifoldRecord.no_of_photos)
                 + (App.CurrentApp.BifoldRecord.no_of_pics == 0 ? "Drawings\n" : "");
        }

        protected override string validate_page()
        {
            return no_repair_reason_entry.validation_error_string("Reason cannot be repaired\n")
                 + (replace_glass_area.IsVisible ? replace_glass_button.validation_error_string("Replace glass\n")
                                                   + (App.CurrentApp.BifoldRecord.replace_glass == 1 && !App.net.BifoldRecord.glass_complete ? "Replace glass details\n" : "")
                                                 : "")
                 + (handles_area.IsVisible ? SurveyFitterSharedLogic.handles_validation_error_text(App.CurrentApp.BifoldRecord.handles_req, App.CurrentApp.BifoldRecord.handles_text, App.CurrentApp.BifoldRecord.bHandleDrawingComplete) : "")
                 + gaskets_button.validation_error_string("Gaskets\n")
                 + (gasket_text.IsVisible && App.CurrentApp.BifoldRecord.gaskets_text == "" ? "Gaskets text\n" : "")
                 + cause_of_damage_area.validation_error_string()
                 + door_type_picker.validation_error_string("Door type\n")
                 + overall_dimensions.validation_error_string("Overall width\n", "Overall height\n")
                 + internal_dimensions.validation_error_string("Internal width\n", "Internal width\n")
                 + addons_button.validation_error_string("Addons\n")
                 + addon_width_edit.validation_error_string("Addon width\n")
                 + addon_height_edit.validation_error_string("Addon height\n")
                 + WER_rating_picker.validation_error_string("WER Rating\n")
                 + opens_button.validation_error_string("Opens\n")
                 + trickle_vents_button.validation_error_string("Trickle vents\n")
                 + hardware_picker.validation_error_string("Hardware\n")
                 + knock_on_picker.validation_error_string("Knock on\n")
                 + handle_colour_picker.validation_error_string("Handles colour\n")
                 + external_colour_picker.validation_error_string("External colour\n")
                 + internal_colour_picker.validation_error_string("Internal colour\n")
                 + threshold_picker.validation_error_string("Threshold\n")
                 + cill_picker.validation_error_string("Cill\n")
                 // type of doors
                 + door_colour_picker.validation_error_string("Door colour\n")
                 + internal_door_colour_picker.validation_error_string("Internal door colour\n")
                 + glazing_options_picker.validation_error_string("Glazing options\n")
                 // Customer signature
                 + summary_pto_area.validation_error_string();
        }

        protected override void save_item(bool complete)
        {
            App.data.SaveBifold(complete);
        }

        protected override bool already_signed()
        {
            return false; //  App.CurrentApp.BifoldRecord.bDifferentFromOriginal;
        }

        private void OnNumChanged(object sender, EventArgs e)
        {

        }
    }
}