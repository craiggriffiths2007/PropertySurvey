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
    public partial class Excess : ContentPage
    {
        public Excess()
        {
            InitializeComponent();
            List<string> methods = new List<string>() { "", "Cheque", "Visa", "Master Card", "Switch", "Postal order" };
            List<string> exreas = new List<string>() { "", "Not dele", "ins'd not present", "Not going ahead yet", "Already collected", "No Excess" };

            PayMeth.SetPickerItems(methods);
            NoPayReas.SetPickerItems(exreas);

            BindingContext = App.net.HeaderRecord as Header;

            SetActive();

            if (App.net.HeaderRecord.iRecordType > 0)
                page1.IsEnabled = false;
        }

        void SetActive()
        {
            if (App.net.HeaderRecord.bExcessCollected == 1)
            {
                PayMeth.IsVisible = true;
                NoPayReas.IsVisible = false;
                //pay_meth.IsEnabled = true;
                //reas_no.IsEnabled = false;
            }
            else
            {
                if (App.net.HeaderRecord.bExcessCollected == 2)
                {
                    PayMeth.IsVisible = false;
                    NoPayReas.IsVisible = true;
                    //pay_meth.IsEnabled = false;
                    //reas_no.IsEnabled = true;
                }
                else
                {
                    PayMeth.IsVisible = false;
                    NoPayReas.IsVisible = false;
                    //pay_meth.IsEnabled = false;
                    //reas_no.IsEnabled = false;
                }
            }

        }

        private void OnButton(object sender, EventArgs e)
        {
            SetActive();
        }

        protected override bool OnBackButtonPressed()
        {
            if (App.net.HeaderRecord.iRecordType == 0)
            {
                if (App.CurrentApp.HeaderRecord.bExcessCollected == 0 || (App.CurrentApp.HeaderRecord.bExcessCollected == 1 && App.CurrentApp.HeaderRecord.mop.Length == 0) ||
                (App.CurrentApp.HeaderRecord.bExcessCollected == 2 && App.CurrentApp.HeaderRecord.reason_excess_not_collected.Length == 0))
                {
                    App.CurrentApp.HeaderRecord.bMOPFin = false;
                }
                else
                {
                    App.CurrentApp.HeaderRecord.bMOPFin = true;
                }
            }
            Navigation.PopAsync(false);
            //this.
            //RaiseBackButtonPressed();
            return true;
        }
    }
}

