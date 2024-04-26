using PropertySurvey;
using System;
using Xamarin.Forms;

namespace MartControls
{
    public static class cause_of_damage_logic
    {
        public static bool reason_different_visible(string reason)
        {
            return reason != null && reason.Length > 0 && App.net.HeaderRecord.COD_String != "Unknown" && App.net.HeaderRecord.COD_String != reason && reason != "Domestic";
        }

        public static bool point_of_entry_visible(string reason)
        {
            return reason == "Theft";
        }
    }

    public partial class CauseOfDamage : StackLayout
    {
        public CauseOfDamage()
        {
            InitializeComponent();

            cause_of_damage_picker.SetPickerItems(App.net.cod_list);

            type_of_lock_picker.SetPickerItems(App.net.locks_required_poe);

            ss_req.BindingContext = App.net.HeaderRecord as Header;
            //set_reason_different();
        }

        public string reason_different_binding { set { reason_different.SetBinding(Editor.TextProperty, value); } }

        public string cause_of_damage_binding
        {
            set { cause_of_damage_picker.SetBinding (EditPicker.TextProperty, value); }
        }

        public string point_of_entry_binding
        {
            set { point_of_entry_button.button_binding = value; }
        }

        public string was_it_locked_button_binding
        {
            set { was_it_locked_button.button_binding = value; }
        }

        public string type_of_lock_picker_binding
        {
            set { type_of_lock_picker.SetBinding(EditPicker.TextProperty, value); }
        }

        public string validation_error_string()
        {
            if (this.IsVisible)
                return cause_of_damage_picker.validation_error_string("Cause of damage\n")
                     + (damage_different_area.IsVisible && (reason_different.Text == null || reason_different.Text == "") ? "Why is cause different to contract\n" : "")
                     + (ss_area.IsVisible && App.CurrentApp.HeaderRecord.ss_bIsSecuritySurvey == 0 ? "Security survey\n" : "")
                     + (ss_area.IsVisible && App.CurrentApp.HeaderRecord.ss_bIsSecuritySurvey == 1 && !App.CurrentApp.HeaderRecord.bSSTicked ? "Security survey detail\n" : "")
                     + (point_of_entry_area.IsVisible ?  point_of_entry_button.validation_error_string("Entry gained through this item\n")
                                                       + was_it_locked_button.validation_error_string("Was it locked\n")
                                                       + type_of_lock_picker.validation_error_string("What locks were on this item\n")
                                                      : "");
            else
                return "";
        }

        public string photo_validation_error_string(int num_photos)
        {
            if (cod_warning_message.IsVisible && num_photos < 10)
                return "At least 10 photographs\n";
            else if (num_photos < 1)
                return "Photograph\n";
            else
                return "";
        }

        private void set_cod_warning_visible()
        {
            cod_warning_message.IsVisible = (cause_of_damage_picker.Text == "Wear + Tear" || cause_of_damage_picker.Text == "Bad Workmanship") /* && App.net.DoRep() */ ;
        }

        private void set_point_of_entry_visible()
        {
            point_of_entry_area.IsVisible = cause_of_damage_logic.point_of_entry_visible(cause_of_damage_picker.Text);
        }

        private void set_reason_different_security_survey()
        {
            if (cause_of_damage_logic.reason_different_visible(cause_of_damage_picker.Text))
            //if (cause_of_damage_picker.Text != null && cause_of_damage_picker.Text.Length > 0 && App.net.HeaderRecord.COD_String != "Unknown" && App.net.HeaderRecord.COD_String != cause_of_damage_picker.Text)
            {
                damage_different_area.IsVisible = true;

                var fs = new FormattedString();
                fs.Spans.Add(new Span { Text = "Please explain why the cause of damage is "});
                fs.Spans.Add(new Span { Text = cause_of_damage_picker.Text,  ForegroundColor = Color.Black, });
                fs.Spans.Add(new Span { Text = " as the contract is " });
                fs.Spans.Add(new Span { Text = App.net.HeaderRecord.COD_String, ForegroundColor = Color.Black, });
                fs.Spans.Add(new Span { Text = "." });
                diff_label.FormattedText = fs;
            }
            else
                damage_different_area.IsVisible = false;

            if (cause_of_damage_picker.Text == "Wear + Tear" || cause_of_damage_picker.Text == "Bad Workmanship")
                App.net.HeaderRecord.funfinoth = cause_of_damage_picker.Text;
            else
                App.net.HeaderRecord.funfinoth = "";

            if (cause_of_damage_picker.Text != null && cause_of_damage_picker.Text == "Theft" && App.CurrentApp.HeaderRecord.readditimage)
            {
                ss_area.IsVisible = true;
                ss_req.LabelText = "As this is " + App.CurrentApp.HeaderRecord.sn_name + " and Theft you must ask the customer if they require a security survey. Does the customer require you to carry out a security survey?";
                SetSSButton();
            }
            else
                ss_area.IsVisible = false;
        }

        public void unfocus()
        {
            // adds it to the report
            App.net.HeaderRecord.rep_text = App.net.HeaderRecord.rep_text + " " + reason_different.Text + ".";
        }

        public void cause_of_damage_changed (object sender, EventArgs e)
        {
            set_cod_warning_visible();
            set_point_of_entry_visible();
            set_reason_different_security_survey();
        }

        private void SetSSButton()
        {
            if (App.CurrentApp.HeaderRecord.ss_bIsSecuritySurvey == 1)
                if (App.CurrentApp.HeaderRecord.bSSTicked == false)
                    ss_button.ImageSource = "question.png";
                else
                    ss_button.ImageSource = "green_tick.png";
            else
                ss_button.ImageSource = "na.png";
        }

        private void OnSSButton(object sender, EventArgs e)
        {
            if (App.CurrentApp.HeaderRecord.ss_bIsSecuritySurvey == 1)
            {
                App.CurrentApp.HeaderRecord.bSSTicked = true;
                SetSSButton();
                Navigation.PushAsync(new SecuritySurveyMessage(), false);
            }
        }

        private void selection_changed(object sender, EventArgs e)
        {
            SetSSButton();
        }

        private void layout_changed(object sender, EventArgs e)
        {
            SetSSButton();
        }
    }
}