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
    public partial class SpecialRequirements : ContentPage
    {
        public SpecialRequirements()
        {
            InitializeComponent();

            BindingContext = App.net.HeaderRecord as Header;

            if (App.net.HeaderRecord.iRecordType > 0)
                page1.IsEnabled = false;
        }
        
        protected override bool OnBackButtonPressed()
        {
            if (App.net.HeaderRecord.iRecordType == 0)
            {
                if (App.CurrentApp.HeaderRecord.doorbell == 0 ||
                    App.CurrentApp.HeaderRecord.alarm_cont == 0 ||
                    App.CurrentApp.HeaderRecord.acroreq == 0 ||
                    App.CurrentApp.HeaderRecord.sand_cemen == 0 ||
                    //App.CurrentApp.HeaderRecord.plaster == 0 ||
                    App.CurrentApp.HeaderRecord.genreq == 0 ||
                    //App.CurrentApp.HeaderRecord.ladder_req == 0 ||
                    App.CurrentApp.HeaderRecord.architreq == 0 ||
                    //App.CurrentApp.HeaderRecord.height_res == 0 ||
                    App.CurrentApp.HeaderRecord.acc_text.Length == 0)
                {
                    App.CurrentApp.HeaderRecord.bSRFin = false;
                }
                else
                {
                    App.CurrentApp.HeaderRecord.bSRFin = true;
                }
            }
            Navigation.PopAsync(false);
            //this.
            //RaiseBackButtonPressed();
            return true;
        }
    }
}


