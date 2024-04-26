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
    public partial class ContractTelephone : ContentPage
    {
        public ContractTelephone()
        {
            InitializeComponent();

            BindingContext = App.net.App_Settings as app_settings;
        }

        private void OnCallHome(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:" + App.net.App_Settings.ContractHPhone));
        }

        private void OnCallWork(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:" + App.net.App_Settings.ContractWPhone));
        }

        private void OnCallMobile(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:" + App.net.App_Settings.ContractMPhone));
        }

        private void OnCallAdd1(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:" + App.net.App_Settings.ContractAddPhone1));
        }

        private void OnCallAdd2(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:" + App.net.App_Settings.ContractAddPhone2));
        }
    }
}