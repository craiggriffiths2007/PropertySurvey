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
    public partial class ToolCheckSend : ContentPage
    {
        StringBuilder postData = new StringBuilder(128000);
        int total_images;
        int current_image;
        string sendResponse = "";
        List<string> images_to_send = new List<string>();

        public ToolCheckSend()
        {
            InitializeComponent();

            CreateImagesList();
            CreateToolCheck();
        }

        private string GetYesNo(int val)
        {
            string ret = "";
            switch (val)
            {
                case 0: ret = "No"; break;
                case 1: ret = "Yes"; break;
                case 2: ret = "No"; break;
                case 3: ret = "n/a"; break;
            }
            return ret;
        }

        private void CreateToolCheck()
        {

            XDocument srcTree = new XDocument(
            new XComment("This is a comment"),
            new XElement("User",
            new XElement("Serial", App.net.phone_serial),
            new XElement("UserCode", App.net.App_Settings.set_ownercode),

            new XElement("files_a", GetYesNo(App.CurrentApp.ToolsRecord.files_a)),
            new XElement("pliers_a", GetYesNo(App.CurrentApp.ToolsRecord.pliers_a)),
            new XElement("chisels_a", GetYesNo(App.CurrentApp.ToolsRecord.chisels_a)),
            new XElement("pincers_a", GetYesNo(App.CurrentApp.ToolsRecord.pincers_a)),
            new XElement("scraper_a", GetYesNo(App.CurrentApp.ToolsRecord.scraper_a)),
            new XElement("hacksaw_a", GetYesNo(App.CurrentApp.ToolsRecord.hacksaw_a)),
            new XElement("crowbar_a", GetYesNo(App.CurrentApp.ToolsRecord.crowbar_a)),
            new XElement("handsaw_a", GetYesNo(App.CurrentApp.ToolsRecord.handsaw_a)),
            new XElement("molegrips_a", GetYesNo(App.CurrentApp.ToolsRecord.molegrips_a)),
            new XElement("sidecutters_a", GetYesNo(App.CurrentApp.ToolsRecord.sidecutters_a)),
            new XElement("hammer_a", GetYesNo(App.CurrentApp.ToolsRecord.hammer_a)),
            new XElement("spiritlevel_a", GetYesNo(App.CurrentApp.ToolsRecord.spiritlevel_a)),
            new XElement("screwdrivers_a", GetYesNo(App.CurrentApp.ToolsRecord.screwdrivers_a)),
            new XElement("bolsterchisel_a", GetYesNo(App.CurrentApp.ToolsRecord.bolsterchisel_a)),
            new XElement("setsquare_a", GetYesNo(App.CurrentApp.ToolsRecord.setsquare_a)),
            new XElement("stanleyknife_a", GetYesNo(App.CurrentApp.ToolsRecord.stanleyknife_a)),
            new XElement("clubhammer_a", GetYesNo(App.CurrentApp.ToolsRecord.clubhammer_a)),
            new XElement("tapemeasure_a", GetYesNo(App.CurrentApp.ToolsRecord.tapemeasure_a)),
            new XElement("slidingbevel_a", GetYesNo(App.CurrentApp.ToolsRecord.slidingbevel_a)),
            new XElement("glazingshovel_a", GetYesNo(App.CurrentApp.ToolsRecord.glazingshovel_a)),
            new XElement("pointingtrowel_a", GetYesNo(App.CurrentApp.ToolsRecord.pointingtrowel_a)),
            new XElement("setofallenkeys_a", GetYesNo(App.CurrentApp.ToolsRecord.setofallenkeys_a)),
            new XElement("adjustablespanner_a", GetYesNo(App.CurrentApp.ToolsRecord.adjustablespanner_a)),
            new XElement("puttyknife_a", GetYesNo(App.CurrentApp.ToolsRecord.puttyknife_a)),
            new XElement("socketset_a", GetYesNo(App.CurrentApp.ToolsRecord.socketset_a)),
            new XElement("copingsaw_a", GetYesNo(App.CurrentApp.ToolsRecord.copingsaw_a)),
            new XElement("augerbitsjoin_a", GetYesNo(App.CurrentApp.ToolsRecord.augerbitsjoin_a)),
            new XElement("nailpunchjoin_a", GetYesNo(App.CurrentApp.ToolsRecord.nailpunchjoin_a)),
            new XElement("puttyknifejoin_a", GetYesNo(App.CurrentApp.ToolsRecord.puttyknifejoin_a)),
            new XElement("socketsetjoin_a", GetYesNo(App.CurrentApp.ToolsRecord.socketsetjoin_a)),
            new XElement("copingsawjoin_a", GetYesNo(App.CurrentApp.ToolsRecord.copingsawjoin_a)),
            new XElement("rivetgunjoin_a", GetYesNo(App.CurrentApp.ToolsRecord.rivetgunjoin_a)),

            new XElement("files_f", GetYesNo(App.CurrentApp.ToolsRecord.files_f)),
            new XElement("pliers_f", GetYesNo(App.CurrentApp.ToolsRecord.pliers_f)),
            new XElement("chisels_f", GetYesNo(App.CurrentApp.ToolsRecord.chisels_f)),
            new XElement("pincers_f", GetYesNo(App.CurrentApp.ToolsRecord.pincers_f)),
            new XElement("scraper_f", GetYesNo(App.CurrentApp.ToolsRecord.scraper_f)),
            new XElement("hacksaw_f", GetYesNo(App.CurrentApp.ToolsRecord.hacksaw_f)),
            new XElement("crowbar_f", GetYesNo(App.CurrentApp.ToolsRecord.crowbar_f)),
            new XElement("handsaw_f", GetYesNo(App.CurrentApp.ToolsRecord.handsaw_f)),
            new XElement("molegrips_f", GetYesNo(App.CurrentApp.ToolsRecord.molegrips_f)),
            new XElement("sidecutters_f", GetYesNo(App.CurrentApp.ToolsRecord.sidecutters_f)),
            new XElement("hammer_f", GetYesNo(App.CurrentApp.ToolsRecord.hammer_f)),
            new XElement("spiritlevel_f", GetYesNo(App.CurrentApp.ToolsRecord.spiritlevel_f)),
            new XElement("screwdrivers_f", GetYesNo(App.CurrentApp.ToolsRecord.screwdrivers_f)),
            new XElement("bolsterchisel_f", GetYesNo(App.CurrentApp.ToolsRecord.bolsterchisel_f)),
            new XElement("setsquare_f", GetYesNo(App.CurrentApp.ToolsRecord.setsquare_f)),
            new XElement("stanleyknife_f", GetYesNo(App.CurrentApp.ToolsRecord.stanleyknife_f)),
            new XElement("clubhammer_f", GetYesNo(App.CurrentApp.ToolsRecord.clubhammer_f)),
            new XElement("tapemeasure_f", GetYesNo(App.CurrentApp.ToolsRecord.tapemeasure_f)),
            new XElement("slidingbevel_f", GetYesNo(App.CurrentApp.ToolsRecord.slidingbevel_f)),
            new XElement("glazingshovel_f", GetYesNo(App.CurrentApp.ToolsRecord.glazingshovel_f)),
            new XElement("pointingtrowel_f", GetYesNo(App.CurrentApp.ToolsRecord.pointingtrowel_f)),
            new XElement("setofallenkeys_f", GetYesNo(App.CurrentApp.ToolsRecord.setofallenkeys_f)),
            new XElement("adjustablespanner_f", GetYesNo(App.CurrentApp.ToolsRecord.adjustablespanner_f)),
            new XElement("puttyknife_f", GetYesNo(App.CurrentApp.ToolsRecord.puttyknife_f)),
            new XElement("socketset_f", GetYesNo(App.CurrentApp.ToolsRecord.socketset_f)),
            new XElement("copingsaw_f", GetYesNo(App.CurrentApp.ToolsRecord.copingsaw_f)),
            new XElement("augerbitsjoin_f", GetYesNo(App.CurrentApp.ToolsRecord.augerbitsjoin_f)),
            new XElement("nailpunchjoin_f", GetYesNo(App.CurrentApp.ToolsRecord.nailpunchjoin_f)),
            new XElement("puttyknifejoin_f", GetYesNo(App.CurrentApp.ToolsRecord.puttyknifejoin_f)),
            new XElement("socketsetjoin_f", GetYesNo(App.CurrentApp.ToolsRecord.socketsetjoin_f)),
            new XElement("copingsawjoin_f", GetYesNo(App.CurrentApp.ToolsRecord.copingsawjoin_f)),
            new XElement("rivetgunjoin_f", GetYesNo(App.CurrentApp.ToolsRecord.rivetgunjoin_f)),

            new XElement("date_done", App.CurrentApp.ToolsRecord.date_done),
            new XElement("signature_filename", App.CurrentApp.ToolsRecord.signature_filename),
            new XElement("signature_filename2", App.CurrentApp.ToolsRecord.signature_filename2),
            new XElement("signature_printed", App.CurrentApp.ToolsRecord.signature_printed),
            new XElement("signature_printed2", App.CurrentApp.ToolsRecord.signature_printed2),
            new XElement("registration", App.CurrentApp.ToolsRecord.registration),
            new XElement("D4CheckID", App.CurrentApp.ToolsRecord.CheckID),
            new XElement("branch", App.CurrentApp.ToolsRecord.branch),
            new XElement("photo_filename", App.CurrentApp.ToolsRecord.photo_filename),
            new XElement("user_code", App.CurrentApp.App_Settings.set_ownercode)
            ));

            postData.Clear();

            postData.Append(srcTree.ToString());

            Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/WM7UploadToolCheck");
            HttpHelper helper = new HttpHelper(thisuri, "POST",
            new KeyValuePair<string, string>("ContractFile", "<JobData>" + postData.ToString() + "</JobData>"));
            helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
            helper.Execute();
        }

        private void CommandComplete(HttpResponseCompleteEventArgs e)
        {
            sendResponse = e.Response;

            if (current_image < total_images)
            {
                if (sendResponse.Length > 1 && sendResponse.Substring(0, 2) == "OK")
                {
                    if (sendResponse.Length > 2)
                    {
                        App.CurrentApp.ToolsRecord.CheckID = sendResponse.Substring(2, 8);
                        App.data.SaveToolsRecord();
                    }
                    sending_progress.ProgressTo((1.0f / total_images) * (float)current_image, 250, Easing.Linear);
                    SendNextPicture();
                }
                else
                {
                    Device.BeginInvokeOnMainThread(CompleteDownload);
                }
            }
            else
            {
                if (sendResponse.Length>1 && sendResponse.Substring(0,2) == "OK")
                {
                    App.net.ToolsRecord.bSent = true;
                    App.data.SaveToolsRecord();
                }
                else
                {

                }
                Device.BeginInvokeOnMainThread(CompleteDownload);
            }
        }

        private void CompleteDownload()
        {
            App.net.ToolsRecord.bSent = true;
            App.data.SaveToolsRecord();
            //act_ind.IsRunning = false;
            if (sendResponse == "nointernet")
                complete_label.Text = "No Internet Connection";
            else
            {
                Navigation.PopAsync(false);
                Navigation.PopAsync(false);
            }
        }

        public void SendNextPicture()
        {
            byte[] data;

            string this_image_filename = images_to_send[current_image];
            string just_file_name;

            just_file_name = this_image_filename.Replace("Photos/", "");
            just_file_name = just_file_name.Replace("Drawings/", "");
            just_file_name = just_file_name.Replace("Signatures/", "");
            just_file_name = just_file_name.Replace("Videos/", "");

            current_image++;

            StringBuilder localpostData = new StringBuilder(128000);
            if (App.files.FileExists(this_image_filename))
            {
                data = App.files.LoadBinary(this_image_filename);

                XDocument localTree = new XDocument(
                new XElement("Image",
                new XElement("UserType", App.net.App_Settings.set_usertype),
                new XElement("JobType", "F"),
                new XElement("UserCode", App.net.App_Settings.set_ownercode),
                new XElement("Filename", just_file_name),
                new XElement("D4CheckID", App.CurrentApp.ToolsRecord.CheckID),
                new XElement("data", System.Convert.ToBase64String(data))));

                localpostData.Append(localTree.ToString());

                Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/SendToolCheckImage");
                HttpHelper helper = new HttpHelper(thisuri, "POST",
                new KeyValuePair<string, string>("ContractFile", "<ContractFile>" + localpostData.ToString() + "</ContractFile>"));
                helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
                helper.Execute();
            }
        }

        private void CreateImagesList()
        {
            images_to_send.Add("Signatures/" + App.CurrentApp.ToolsRecord.signature_filename);
            images_to_send.Add("Signatures/" + App.CurrentApp.ToolsRecord.signature_filename2);

            images_to_send.AddRange(App.files.GetFileList("Photos/", "T"+string.Format("{0:0000000}", App.CurrentApp.ToolsRecord.RecID) + "*.*", "Photos/"));

            total_images = images_to_send.Count();
            current_image = 0;
        }
    }
}