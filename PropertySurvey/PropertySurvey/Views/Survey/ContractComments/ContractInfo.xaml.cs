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
    public partial class ContractInfo : ContentPage
    {
        public ContractInfo()
        {
            InitializeComponent();

            BindingContext = App.net.App_Settings as app_settings;
        }

        private void OnContractTelephone(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ContractTelephone(), false);
        }
    }
}