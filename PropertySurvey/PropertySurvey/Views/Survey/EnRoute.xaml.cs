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
	public partial class EnRoute : ContentPage
	{
        StringBuilder postData = new StringBuilder();
        string sendResponse = "";

        public EnRoute ()
		{
			InitializeComponent ();

            if (App.net.HeaderRecord.iRecordType == 0)
            {
                job_times.Text = App.net.HeaderRecord.udi_start.Substring(App.net.HeaderRecord.udi_start.Length - 8, 5) + " - " + App.net.HeaderRecord.udi_fin.Substring(App.net.HeaderRecord.udi_fin.Length - 8, 5);
            }
            else
            {
                job_times.Text = App.net.HeaderRecord.fit_start.Substring(App.net.HeaderRecord.udi_start.Length - 8, 5) + " - " + App.net.HeaderRecord.fit_fin.Substring(App.net.HeaderRecord.udi_fin.Length - 8, 5);
            }
            SetMessagesVisible();

        }

        private void SetMessagesVisible()
        {
            if (App.net.HeaderRecord.si_inum == null)
                App.net.HeaderRecord.si_inum = "";
            if (App.net.HeaderRecord.si_inum != "")
            {
                yes_button.IsEnabled = false;
                no_button.IsEnabled = false;
            }
            else
            {

            }

            if (App.net.HeaderRecord.si_inum == "1") // Customer informed
            {
                cust_informed.IsVisible = true;
            }

            if (App.net.HeaderRecord.si_inum == "2") // contact branch
            {
                if (App.net.HeaderRecord.iRecordType == 0)
                {
                    contact_customer.IsVisible = true;
                }
                else
                {
                    contact_branch.IsVisible = true;
                }
            }
        }

        private void OnYes(object sender, EventArgs e)
        {
            //App.net.HeaderRecord.si_inum = "1";
            //App.net.HeaderRecord.si_cnum = DateTime.Today.ToShortTimeString();

            //SetMessagesVisible();
            act_ind.IsRunning = true;

            XDocument srcTree = new XDocument(
            new XComment("This is a comment"),
            new XElement("User",
            new XElement("Serial", App.net.phone_serial),
            new XElement("UserCode", App.net.App_Settings.set_ownercode),
            new XElement("JobType", App.net.HeaderRecord.iRecordType.ToString()),
            new XElement("Contract", App.net.HeaderRecord.udi_cont)

            ));

            int i;
            byte[] data;

            postData.Clear();

            postData.Append(srcTree.ToString());

            Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/InformCustomerOnRoute");
            HttpHelper helper = new HttpHelper(thisuri, "POST",
            new KeyValuePair<string, string>("ContractFile", "<JobData>" + postData.ToString() + "</JobData>"));
            helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
            helper.Execute();
        }

        private void CommandComplete(HttpResponseCompleteEventArgs e)
        {
            sendResponse = e.Response;
            Device.BeginInvokeOnMainThread(CompleteDownload);
        }

        private void CompleteDownload()
        {
            act_ind.IsRunning = false;
            if ((sendResponse.Length > 0) && (sendResponse.Substring(0, 2) == "OK"))
            {
                App.net.HeaderRecord.si_inum = "1";
                App.net.HeaderRecord.si_cnum = DateTime.Today.ToShortTimeString();

                SetMessagesVisible();
            }
            else
            {
                App.net.HeaderRecord.si_inum = "0";
                error_server.IsVisible = true;
            }
        }

        private void OnNo(object sender, EventArgs e)
        {
            App.net.HeaderRecord.si_inum = "1";
            App.net.HeaderRecord.si_cnum = DateTime.Today.ToShortTimeString();

            SetMessagesVisible();
        }
    }
}