using System;
using System.Collections.Generic;
using Xamarin.Forms;
using PropertySurvey;

namespace MartControls
{
    public enum t_leading_types { lt_diamond_lead, lt_georgian_lead, lt_georgian_bar, lt_back_to_back }

    public partial class SpecialGlass : StackLayout
    {
        public static readonly BindableProperty DiamondLeadCompleteProperty = BindableProperty.Create("DiamondLeadComplete", typeof(Boolean), typeof(SpecialGlass), false, BindingMode.OneWay);
        public static readonly BindableProperty GeorgianLeadCompleteProperty = BindableProperty.Create("GeorgianLeadComplete", typeof(Boolean), typeof(SpecialGlass), false, BindingMode.OneWay);
        public static readonly BindableProperty BackToBackCompleteProperty = BindableProperty.Create("BackToBackComplete", typeof(Boolean), typeof(SpecialGlass), false, BindingMode.OneWay);
        public static readonly BindableProperty GeorgianBarCompleteProperty = BindableProperty.Create("GeorgianBarComplete", typeof(Boolean), typeof(SpecialGlass), false, BindingMode.OneWay);

        public Boolean DiamondLeadComplete
        {
            get { return (Boolean)GetValue(DiamondLeadCompleteProperty); }
            set { SetValue(DiamondLeadCompleteProperty, value); }
        }

        public Boolean GeorgianLeadComplete
        {
            get { return (Boolean)GetValue(GeorgianLeadCompleteProperty); }
            set { SetValue(GeorgianLeadCompleteProperty, value); }
        }

        public Boolean BackToBackComplete
        {
            get { return (Boolean)GetValue(BackToBackCompleteProperty); }
            set { SetValue(BackToBackCompleteProperty, value); }
        }

        public Boolean GeorgianBarComplete
        {
            get { return (Boolean)GetValue(GeorgianBarCompleteProperty); }
            set { SetValue(GeorgianBarCompleteProperty, value); }
        }

        public SpecialGlass ()
		{
			InitializeComponent();
        }

        public string special_glass_binding
        {
            set { special_glass_picker.TextBinding = value; }
        }

        public string diamond_lead_complete_binding
        {
            set { this.SetBinding(DiamondLeadCompleteProperty, value); }
        }

        public string georgian_lead_complete_binding
        {
            set { this.SetBinding(GeorgianLeadCompleteProperty, value); }
        }

        public string back_to_back_complete_binding
        {
            set { this.SetBinding(BackToBackCompleteProperty, value); }
        }

        public string georgian_bar_complete_binding
        {
            set { this.SetBinding(GeorgianBarCompleteProperty, value); }
        }

        public void SetPickerItems(List<String> items)
        {
            special_glass_picker.SetPickerItems(items);
        }

        public void set_complete(bool bDiamondLeadComplete,
            bool bGeorgianLeadComplete,
            bool bBackToBackComplete,
            bool bGeorgianBarComplete)
        {
            DiamondLeadComplete = bDiamondLeadComplete;
            GeorgianLeadComplete = bGeorgianLeadComplete;
            BackToBackComplete = bBackToBackComplete;
            GeorgianBarComplete = bGeorgianBarComplete;
            set_special_glass_button();
        }

        private void set_special_glass_button_icon(bool complete)
        {
            special_glass_button.IsEnabled = true; // set_special_glass_button_icon is only called when user selects special glass. IsEnabled = false is set in the calling function when required

            if (complete)
                special_glass_button.ImageSource = "green_tick.png";
            else
                special_glass_button.ImageSource = "question.png";
        }

        public void set_special_glass_button()
        {
            switch (special_glass_picker.Text)
            {
                case "Diamond Leaded": set_special_glass_button_icon(DiamondLeadComplete); break;
                case "Georgian Leaded": set_special_glass_button_icon(GeorgianLeadComplete); break;
                case "Back to Back Spacer": set_special_glass_button_icon(BackToBackComplete); break;
                case "Georgian Bar": set_special_glass_button_icon(GeorgianBarComplete); break;
                default:
                    special_glass_button.ImageSource = "na.png";
                    special_glass_button.IsEnabled = false; break;
            }
        }

        private void special_glass_changed(object sender, EventArgs e)
        {
            set_special_glass_button();
        }

        private void special_glass_clicked(object sender, EventArgs e)
        {
            switch (special_glass_picker.Text)
            {
                case "Diamond Leaded": Navigation.PushAsync(new PropertySurvey.DiamindLeadInfo(), false); break;
                case "Georgian Leaded": Navigation.PushAsync(new PropertySurvey.GeorgianLeadInfo(), false); break;
                case "Back to Back Spacer": Navigation.PushAsync(new PropertySurvey.BackToBackInfo(), false); break;
                case "Georgian Bar": Navigation.PushAsync(new PropertySurvey.GeorgianBarInfo(), false); break;
            }
        }

        public string validation_error_string()
        {
            string result = "";

            if (this.IsVisible)
            {
                if (special_glass_picker.Text == null || special_glass_picker.Text.Length == 0)
                    result = result + "Special Glass\n";

                switch (special_glass_picker.Text)
                {
                    case "Diamond Leaded":
                        if (!DiamondLeadComplete)
                            result = result + "Diamond lead details\n";
                        break;

                    case "Georgian Leaded":
                        if (!GeorgianLeadComplete)
                            result = result + "Georgian lead details\n";
                        break;

                    case "Back to Back Spacer":
                        if (!BackToBackComplete)
                            result = result + "Back to Back Spacer\n";
                        break;

                    case "Georgian Bar":
                        if (!GeorgianBarComplete)
                            result = result + "Georgian bar details\n";
                        break;
                }
            }

            return result;
        }

        private void layout_changed(object sender, EventArgs e)
        {
            set_special_glass_button(); 
        }
    }
}