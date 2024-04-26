using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        string response_string = "";


        public Login()
        {
            InitializeComponent();

            _vers.Text = App.net.app_version;
            //DisplayAlert(App.net.phone_serial, "a", "b"); 
        }

        private void DoLogin()
        {
            button2.IsEnabled = false;
            act_ind.IsRunning = true;

            XDocument srcTree = new XDocument(
            new XComment("This is a comment"),
            new XElement("User",
            new XElement("Username", username.Text),
            new XElement("SerialNumber", App.net.phone_serial)));

            Uri thisuri = new Uri("http://192.168.137.15:7293/" + "/WM7Communication/WM7ValidateMobilePhoneUser");
            HttpHelper helper = new HttpHelper(thisuri, "POST",
            new KeyValuePair<string, string>("ContractFile", srcTree.ToString()));
            helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
            helper.Execute();
        }

        private async void CommandComplete(HttpResponseCompleteEventArgs e)
        {
            string response_string = e.Response;

            if (response_string != null)
            {
                

                if(response_string.Length==16)
                {
                    App.net.app_version_update = response_string.Substring(6, 10);
                    if(DateTime.Parse(App.net.app_version_update)> DateTime.Parse(App.net.app_version))
                    {
                        App.net.update_available = true;
                    }
                }

                if (response_string.Substring(0, 3) != "Val")
                {
                    Device.BeginInvokeOnMainThread(handleNotFound);
                }
                else
                {
                    if (response_string.Length > 6)
                    {
                        //TheSettings.connect_password = response_string.Substring(6);
                        //TheSettings.last_connected_date = DateTime.Today;
                        //TheSettings.SaveCommSettings();
                    }
                    Device.BeginInvokeOnMainThread(GoToMainPage);
                }
            }
        }

        private void handleNotFound()
        {
            act_ind.IsRunning = false;
            button2.IsEnabled = true;
        }

        private async void GoToMainPage()
        {
            act_ind.IsRunning = false;
            Navigation.InsertPageBefore(new MainPage(), this);
            await Navigation.PopAsync(false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            serial.Text = App.net.phone_serial;

            DoLogin();
        }

        private void OnLogin(object sender, EventArgs e)
        {
            if(username.Text==App.net.App_Settings.set_ownercode)
            {
                GoToMainPage();
            }
            else
            {
                DoLogin();
            }
        }
    }
}