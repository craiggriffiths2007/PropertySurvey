using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    public static class StringExtensions
    {
        public static string Replace(this string originalString, string oldValue, string newValue, StringComparison comparisonType)
        {
            int startIndex = 0;
            while (true)
            {
                startIndex = originalString.IndexOf(oldValue, startIndex, comparisonType);
                if (startIndex == -1)
                    break;

                originalString = originalString.Substring(0, startIndex) + newValue + originalString.Substring(startIndex + oldValue.Length);

                startIndex += newValue.Length;
            }
            return originalString;
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Damage : ContentPage
    {
        public Damage()
        {
            InitializeComponent();

            BindingContext = App.net.HeaderRecord as Header;

            SetNextEnabled();

            if (App.net.HeaderRecord.iRecordType > 0)
            {
                Title = "Damage";
                confirm_tick.IsVisible = false;
            }
            else
            {
                Title = App.net.HeaderRecord.COD_String;
            }

            if (App.CurrentApp.DoRep() == true && App.net.HeaderRecord.uc_laname != "Non" /* or non deli*/)
            {
                repude.Text = "Yes";
            }
            else
            {
                repude.Text = "No";
            }
            //App.net.HeaderRecord.stimea = DateTime.Now.ToShortTimeString();

            if (App.net.HeaderRecord.bDamTicked == true)
            {
                confirm_tick.IsEnabled = false;
                next_button.IsVisible = false;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (App.net.HeaderRecord.b_mrk == true)
            {
                Navigation.InsertPageBefore(new SpotCheckHeader(), this);
                Navigation.PopAsync(false);
            }
            else
            {
                Navigation.InsertPageBefore(new PropertySurveyItemPage(), this);
                Navigation.PopAsync(false);
            }

            return true;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void SetNextEnabled()
        {
            if (App.net.HeaderRecord.bDamTicked == true)
            {
                next_button.IsEnabled = true;
            }
            else
            {
                next_button.IsEnabled = false;
            }
        }


        private async void OnNext(object sender, EventArgs e)
        {
            if (App.net.HeaderRecord.b_mrk == true)
            {
                Navigation.InsertPageBefore(new SpotCheckHeader(), this);
                await Navigation.PopAsync(false);

            }
            else
            {
                if (App.net.HeaderRecord.iRecordType == 1 || App.net.HeaderRecord.iRecordType == 2)
                {
                    /*
                    switch (App.net.HeaderRecord.typeB)
                    {
                        //case "Remedial": NavigationService.Navigate(new Uri("/Fitter/Remedial/RemedialHeader.xaml", UriKind.Relative)); break;
                        case "Securing": NavigationService.Navigate(new Uri("/Fitter/FitterWarning.xaml", UriKind.Relative)); break;

                        //NavigationService.Navigate(new Uri("/Fitter/Securing/SecuringHeader.xaml", UriKind.Relative)); break;
                        default: NavigationService.Navigate(new Uri("/Fitter/FitterHeaderPage.xaml", UriKind.Relative)); break;
                    }
                    */
                    //Navigation.InsertPageBefore(new FitterHeader(), this);
                    await Navigation.PopAsync(false);
                }
                else
                {

                    if (false) // (App.net.HeaderRecord.type == "Complaint")
                    {
                        //    NavigationService.Navigate(new Uri("/Surveyor/ComplaintHeader.xaml", UriKind.Relative));
                    }
                    else
                    {
                        Navigation.InsertPageBefore(new PropertySurveyItemPage(), this);
                        await Navigation.PopAsync(false);
                    }

                }
            }
        }

        private void TickButtonLabel_Clicked(object sender, EventArgs e)
        {
            SetNextEnabled();
        }

        private void OnSpeak(object sender, EventArgs e)
        {
            DependencyService.Get<ITextToSpeech>().Speak(App.net.HeaderRecord.COD_String + "..." + App.CurrentApp.HeaderRecord.uc_desc.Trim().Replace(App.net.HeaderRecord.COD_String, "", StringComparison.CurrentCultureIgnoreCase)/* + ". The fucking dicks" */);
        }
    }
}