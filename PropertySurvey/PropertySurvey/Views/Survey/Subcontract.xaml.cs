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
    public partial class Subcontract : ContentPage
    {
        public Subcontract()
        {
            InitializeComponent();
            BindingContext = App.net.HeaderRecord as Header;
            SetSubconVisible();
            if (App.net.HeaderRecord.iRecordType > 0)
                page1.IsEnabled = false;
        }

        private void SetSubconVisible()
        {
            if (App.net.HeaderRecord.b_subcontract == 1)
            {
                subcon_text.IsVisible = true;
                subcon_text.Focus();
            }
            else
                subcon_text.IsVisible = false;
        }

        private void OnButton(object sender, EventArgs e)
        {
            SetSubconVisible();
        }

        protected override bool OnBackButtonPressed()
        {
            if (App.net.HeaderRecord.iRecordType == 0)
            {
                if (App.CurrentApp.HeaderRecord.b_subcontract == 0 ||
                    (App.CurrentApp.HeaderRecord.b_subcontract == 1 &&
                    App.CurrentApp.HeaderRecord.subcontracttext.Length == 0))
                {
                    App.CurrentApp.HeaderRecord.bSubFin = false;
                }
                else
                {
                    App.CurrentApp.HeaderRecord.bSubFin = true;
                }
            }
            Navigation.PopAsync(false);

            return true;
        }

    }
}