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
	public partial class LadderSend : ContentPage
	{
        StringBuilder postData = new StringBuilder(128000);
        int total_images;
        int current_image;
        string sendResponse = "";
        List<string> images_to_send = new List<string>();

        public LadderSend ()
		{
			InitializeComponent ();

            CreateImagesList();
            CreateLadderCheck();
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

        private void CreateLadderCheck()
        {

            XDocument srcTree = new XDocument(
            new XComment("This is a comment"),
            new XElement("User",
            new XElement("Serial", App.net.phone_serial),
            new XElement("UserCode", App.net.App_Settings.set_ownercode),

            new XElement("in_reasonable_condition", GetYesNo(App.CurrentApp.LadderRecord.in_reasonable_condition)),
            new XElement("rungs_missing_or_loose", GetYesNo(App.CurrentApp.LadderRecord.rungs_missing_or_loose)),
            new XElement("stiles_damaged_or_bent", GetYesNo(App.CurrentApp.LadderRecord.stiles_damaged_or_bent)),
            new XElement("any_cracks", GetYesNo(App.CurrentApp.LadderRecord.any_cracks)),
            new XElement("any_corrosion", GetYesNo(App.CurrentApp.LadderRecord.any_corrosion)),
            new XElement("rubber_plastic_feet", GetYesNo(App.CurrentApp.LadderRecord.rubber_plastic_feet)),
            new XElement("sharp_or_metal_splinters", GetYesNo(App.CurrentApp.LadderRecord.sharp_or_metal_splinters)),
            new XElement("painted_or_decorated", GetYesNo(App.CurrentApp.LadderRecord.painted_or_decorated)),
            new XElement("ladders_been_repaired", GetYesNo(App.CurrentApp.LadderRecord.ladders_been_repaired)),
            new XElement("comments", App.CurrentApp.LadderRecord.comments),
            new XElement("branch", App.CurrentApp.LadderRecord.branch),
            new XElement("ladnum", App.CurrentApp.LadderRecord.ladder_number),
            new XElement("registration", App.CurrentApp.LadderRecord.registration),

            new XElement("any_damage", GetYesNo(App.CurrentApp.LadderRecord.i_spare4)),

            new XElement("managers_name", App.CurrentApp.LadderRecord.managers_name),

            new XElement("rungs_dented", GetYesNo(App.CurrentApp.LadderRecord.rungs_dented)),
            new XElement("hooks_sit_properly", GetYesNo(App.CurrentApp.LadderRecord.hooks_sit_properly)),

            new XElement("fitsurname", App.CurrentApp.LadderRecord.fitter_surveyor_name),

            new XElement("signature1", App.CurrentApp.LadderRecord.signature_filename),
            new XElement("signature2", App.CurrentApp.LadderRecord.s_spare4),
            new XElement("D4CheckID", App.CurrentApp.LadderRecord.CheckID),

            new XElement("LadderType", App.CurrentApp.LadderRecord.s_spare5),

            new XElement("date_done", App.CurrentApp.LadderRecord.date_done)

            ));

            postData.Clear();

            postData.Append(srcTree.ToString());

            Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/WM7UploadLadders");
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
                if (sendResponse.Substring(0, 2) == "OK")
                {
                    if (sendResponse.Length > 2)
                    {
                        App.CurrentApp.LadderRecord.CheckID = sendResponse.Substring(2, 8);
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
                if (sendResponse == "OK")
                {
                    App.net.LadderRecord.bSent = true;
                    App.data.SaveLadderRecord();
                }
                else
                {

                }
                Device.BeginInvokeOnMainThread(CompleteDownload);
            }
        }

        private void CompleteDownload()
        {
            App.net.LadderRecord.bSent = true;
            App.data.SaveLadderRecord();
            //act_ind.IsRunning = false;
            if (sendResponse == "nointernet")
                complete_label.Text = "No Internet Connection";
            else
                Navigation.PopAsync(false);
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
                new XElement("Filename", App.CurrentApp.LadderRecord.CheckID + just_file_name),
                new XElement("D4CheckID", App.CurrentApp.LadderRecord.CheckID),
                new XElement("data", System.Convert.ToBase64String(data))));

                localpostData.Append(localTree.ToString());

                Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/SendLaddersImage");
                HttpHelper helper = new HttpHelper(thisuri, "POST",
                new KeyValuePair<string, string>("ContractFile", "<ContractFile>" + localpostData.ToString() + "</ContractFile>"));
                helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
                helper.Execute();
            }
        }

        private void CreateImagesList()
        {
            images_to_send.Add("Signatures/" + App.CurrentApp.LadderRecord.signature_filename);
            images_to_send.Add("Signatures/" + App.CurrentApp.LadderRecord.s_spare4);

            images_to_send.AddRange(App.files.GetFileList("Photos/", string.Format("{0:00000000}", App.CurrentApp.LadderRecord.RecID) + "_Lad*.*", "Photos/"));

            total_images = images_to_send.Count();
            current_image = 0;
        }
    }
}