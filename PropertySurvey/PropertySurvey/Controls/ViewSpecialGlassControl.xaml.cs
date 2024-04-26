using System;
using Xamarin.Forms;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

namespace MartControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewSpecialGlassControl : StackLayout
    {
        public PropertySurvey.t_current_item parent_item { get; set; }
        public string LabelText { set { the_label.Text = value; } }

        public ViewSpecialGlassControl()
        {
            InitializeComponent();
        }

        public string special_glass_binding
        {
            set { the_text.SetBinding(Label.TextProperty, value); }
        }

        private void layout_changed(object sender, EventArgs e)
        {
            switch (the_text.Text)
            {
                case "Diamond Leaded":
                case "Georgian Leaded":
                case "Back to Back Spacer":
                case "Georgian Bar": view_button.IsVisible = true;  break;
                default:
                    view_button.IsVisible = false; break;
            }
        }

        private void view_clicked(object sender, EventArgs e)
        {
            switch (the_text.Text)
            {
                case "Diamond Leaded": Navigation.PushAsync(new PropertySurvey.ViewLeaded(parent_item, t_leading_types.lt_diamond_lead), false); break;
                case "Georgian Leaded": Navigation.PushAsync(new PropertySurvey.ViewLeaded(parent_item, t_leading_types.lt_georgian_lead), false); break;
                case "Back to Back Spacer": Navigation.PushAsync(new PropertySurvey.ViewLeaded(parent_item, t_leading_types.lt_back_to_back), false); break;
                case "Georgian Bar": Navigation.PushAsync(new PropertySurvey.ViewLeaded(parent_item, t_leading_types.lt_georgian_bar), false); break;
            }
        }
    }
}