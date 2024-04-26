using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        bool bLoaded = false;

        public SettingsPage()
        {
            InitializeComponent();

            BindingContext = App.net.App_Settings as app_settings;

            serial_number.Text = App.net.phone_serial;
            //App.net.App_Settings.send_data_file = false;
        }

        protected override bool OnBackButtonPressed()
        {
            App.net.App_Settings.set_ownercode = App.net.App_Settings.set_ownercode.ToUpper();
            App.net.App_Settings.set_branchcode = App.net.App_Settings.set_branchcode.ToUpper();
            App.data.SaveSettings();

            return false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _vers.Text = App.net.app_version;

            SetButtons();
            bLoaded = true;
        }

        private void SetButtons()
        {
            if (App.net.App_Settings.set_usertype == "SUR")
            {
                Su.BackgroundColor = Color.FromHex("2281bc");
                Sa.BackgroundColor = Color.FromHex("d6d7d7");
                Fi.BackgroundColor = Color.FromHex("d6d7d7");
                App.net.App_Settings.set_ownercode = "H1";
                owner_code.Text = "H1";
            }
            if (App.net.App_Settings.set_usertype == "SAT")
            {
                Su.BackgroundColor = Color.FromHex("d6d7d7");
                Sa.BackgroundColor = Color.FromHex("2281bc");
                Fi.BackgroundColor = Color.FromHex("d6d7d7");
                App.net.App_Settings.set_ownercode = "CC1";
                owner_code.Text = "CC1";
            }
            if (App.net.App_Settings.set_usertype == "FIT")
            {
                Su.BackgroundColor = Color.FromHex("d6d7d7");
                Sa.BackgroundColor = Color.FromHex("d6d7d7");
                Fi.BackgroundColor = Color.FromHex("2281bc");
                App.net.App_Settings.set_ownercode = "H02";
                owner_code.Text = "H02";
            }
        }

        private void OnSurv(object sender, EventArgs e)
        {
            App.net.App_Settings.set_usertype = "SUR";
            SetButtons();
        }

        private void OnSat(object sender, EventArgs e)
        {
            App.net.App_Settings.set_usertype = "SAT";
            SetButtons();
        }

        private void OnFit(object sender, EventArgs e)
        {
            App.net.App_Settings.set_usertype = "FIT";
            SetButtons();
        }

        private void OnPrivacyPolicy(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new Info(), false);
            Device.OpenUri(new Uri("https://docs.google.com/document/d/1VG5xKZWyd_buiFfbvCUyHi4lmDfeZ9QhOvgmMPghYIg/edit"));
        }

        private void OnUpdateHistory(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UpdateHistory(), false);
        }

        private void OnPitchSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            if(bLoaded==true)
                DependencyService.Get<ITextToSpeech>().Speak("Testing 1 2 3");
        }

        private void OnSpeedSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (bLoaded == true)
                DependencyService.Get<ITextToSpeech>().Speak("Testing 1 2 3");
        }
    }
}