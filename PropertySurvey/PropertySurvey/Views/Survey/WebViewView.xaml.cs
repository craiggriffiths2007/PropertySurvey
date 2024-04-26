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
    public partial class WebViewView : ContentPage
    {
        public WebViewView()
        {
            InitializeComponent();
            //web.Source = App.net.web_string;
            var browser = new WebView();
            browser.Source = App.net.web_string;
            Content = browser;
        }


    }
}