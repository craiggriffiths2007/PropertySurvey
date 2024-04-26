using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrawingTextPage : ContentPage
    {
        public DrawingTextPage()
        {
            InitializeComponent();
            TextVertical.IsToggled = App.net.App_Settings.vertical_text;
        }

        protected override bool OnBackButtonPressed()
        {
            App.net.App_Settings.vertical_text = TextVertical.IsToggled;
            App.data.SaveSettings();
            base.OnBackButtonPressed();
            return false;
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            App.net.App_Settings.vertical_text = TextVertical.IsToggled;
            App.data.SaveSettings();
            App.net.TextEntryText = ((Button)sender).Text;
            Navigation.PopAsync(false);
        }

        private void degree_button_click(object sender, EventArgs e)
        {
            App.net.App_Settings.vertical_text = TextVertical.IsToggled;
            App.data.SaveSettings();
            App.net.TextEntryText = CurrentText.Text + "°";
            Navigation.PopAsync(false);
        }

        private void insert_text_button_click(object sender, EventArgs e)
        {
            App.net.App_Settings.vertical_text = TextVertical.IsToggled;
            App.data.SaveSettings();
            App.net.TextEntryText = CurrentText.Text;
            Navigation.PopAsync(false);
        }
    }
}