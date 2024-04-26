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
    public partial class RemReport : ContentPage
    {
        public RemReport()
        {
            InitializeComponent();

            BindingContext = App.net.HeaderRecord as Header;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            fitter_comments.Focus();
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
            App.data.SaveHeader();
            return false;
        }
    }
}