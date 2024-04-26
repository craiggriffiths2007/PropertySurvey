using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetMessages : ContentPage
    {
        StringBuilder postData = new StringBuilder();
        bool bSending = true;
        string sendResponse = "";

        public GetMessages()
        {
            InitializeComponent();
            DownloadMessages();
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
            DeviceDisplay.KeepScreenOn = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            DeviceDisplay.KeepScreenOn = false;
        }

        public void DownloadMessages()
        {
            // Send PDA code and Branch code
            XDocument srcTree = new XDocument(
            new XComment("This is a comment"),
            new XElement("User",
            new XElement("PDAUser", App.net.App_Settings.set_ownercode),
            new XElement("Branch", App.net.App_Settings.set_branchcode),
            new XElement("Version", App.net.app_version),
            new XElement("imei", App.net.App_Settings.iemi),
            //new XElement("PhoneOS", App.net.app_platform),
            new XElement("PhoneOS", "Android"),
            new XElement("PhoneSerial", App.CurrentApp.phone_serial),
            new XElement("UserType", App.net.App_Settings.set_usertype)));

            postData.Append(srcTree.ToString());

            bSending = true;

            Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/GetMessages");
            HttpHelper helper = new HttpHelper(thisuri, "POST",
            new KeyValuePair<string, string>("ContractFile", "<JobData>" + postData.ToString() + "</JobData>"));
            helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
            helper.Execute();
        }

        private void CommandComplete(HttpResponseCompleteEventArgs e)
        {
            sendResponse = e.Response;

            int new_messages = 0;

            if ((sendResponse.Length > 0) && (sendResponse.Substring(0, 10) != "<Messages>"))
            {
                //DisplayAlert("Alert", "There was an error contacting the server, please try again later.", "OK");
                //Navigation.PopAsync(false);
            }
            else
            {
                try
                {
                    XDocument xml = XDocument.Parse(sendResponse);

                    foreach (var word in xml.Element("Messages").Elements())
                    {
                        if (word.Name == "Authority")
                        {
                            if ((string)word.Element("ladder_checks") == "1")
                                App.net.App_Settings.able_to_ladder_check = 1;
                            else
                                App.net.App_Settings.able_to_ladder_check = 0;
                            if ((string)word.Element("contract_comments") == "1")
                                App.net.App_Settings.able_to_send_comments = 1;
                            else
                                App.net.App_Settings.able_to_send_comments = 0;

                            App.data.SaveSettings();
                        }

                        if (word.Name == "Message")
                        {
                            string ID = (string)word.Element("ID");
                            string from = (string)word.Element("from");
                            string msgdate = (string)word.Element("msgdate");
                            string msgtime = (string)word.Element("msgtime");
                            string messagetext = (string)word.Element("messagetext");

                            App.net.MessageRecord = new Message_Text();

                            App.net.MessageRecord.ID = ID;
                            App.net.MessageRecord.from = from;
                            App.net.MessageRecord.message_date = msgdate;
                            if (messagetext == null)
                            {
                                App.net.MessageRecord.message_text = "";
                            }
                            else
                            {
                                App.net.MessageRecord.message_text = messagetext;
                            }

                            App.data.SaveMessage(App.net.MessageRecord);
                            new_messages++;
                        }

                        if (word.Name == "Image")
                        {
                            string Data = (string)word.Element("Data");
                            string MesID = (string)word.Element("MesID");
                            string ImNum = (string)word.Element("ImNum");

                            byte[] decoded = System.Convert.FromBase64String(Data);

                            App.files.SaveBinary("Photos/Message" + MesID + ImNum + ".jpg", decoded);
                        }

                    }
                    App.data.DeleteOldMessages();
                    App.net.App_Settings.new_mail = App.data.CountUnsentMessages();
                    App.data.SaveSettings();
                }
                catch (Exception ex)
                {
                    //DisplayAlert("Alert",e.ToString(),"OK");
                }
                //Navigation.PopAsync(false);
            }
            Device.BeginInvokeOnMainThread(CompleteDownload);

        }

        private void CompleteDownload()
        {
            Navigation.PopAsync(false);
        }
    }
}
