using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Report : ContentPage
    {
        public Report()
        {
            InitializeComponent();

            BindingContext = App.net.HeaderRecord as Header;

            if (App.net.HeaderRecord.iRecordType > 0)
            {
                page1.IsEnabled = false;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(App.net.HeaderRecord.iRecordType==0)
            {
                report_area.Focus();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (App.CurrentApp.HeaderRecord.rep_text.Length == 0)
            {
                App.CurrentApp.HeaderRecord.bRepFin = false;
            }
            else
            {
                App.CurrentApp.HeaderRecord.bRepFin = true;
            }
            Navigation.PopAsync(false);

            return true;
        }

        private void OnSpeak(object sender, EventArgs e)
        {
            DependencyService.Get<ITextToSpeech>().Speak(App.CurrentApp.HeaderRecord.rep_text);
        }
    }
}
