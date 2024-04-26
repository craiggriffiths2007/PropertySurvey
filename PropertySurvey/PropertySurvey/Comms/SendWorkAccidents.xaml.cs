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
    public partial class SendWorkAccidents : ContentPage
    {
        StringBuilder postData = new StringBuilder(128000);
        int total_images;
        int current_image;
        string sendResponse = "";
        List<string> images_to_send = new List<string>();

        public SendWorkAccidents()
        {
            InitializeComponent();

            if (App.CurrentApp.FAccidentsRecord.spare10 == "Nearmiss")
            {
                Title = "Send Near Miss";
            }

            act_ind.IsRunning = true;

            CreateImagesList();

            CreateMileage();
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

        private void CreateMileage()
        {
            XDocument srcTree = new XDocument(
            new XComment("This is a comment"),
            new XElement("User",
            new XElement("Serial", App.net.phone_serial),
            new XElement("UserCode", App.net.App_Settings.set_ownercode),

            new XElement("log_date", String.Format("{0:dd/MM/yyyy}", App.CurrentApp.FAccidentsRecord.date_time)),
            new XElement("acc_date", String.Format("{0:dd/MM/yyyy}", App.CurrentApp.FAccidentsRecord.date_happened)),
            new XElement("acc_time", String.Format("{0:HH:mm}", App.CurrentApp.FAccidentsRecord.time_happened)),

            new XElement("fname", App.CurrentApp.FAccidentsRecord.full_name),
            new XElement("add1", App.CurrentApp.FAccidentsRecord.add1),
            new XElement("add2", App.CurrentApp.FAccidentsRecord.add2),
            new XElement("add3", App.CurrentApp.FAccidentsRecord.add3),
            new XElement("pcode", App.CurrentApp.FAccidentsRecord.pcode),
            new XElement("log_full_name", App.CurrentApp.FAccidentsRecord.filer_full_name),
            new XElement("log_add1", App.CurrentApp.FAccidentsRecord.filer_add1),
            new XElement("log_add2", App.CurrentApp.FAccidentsRecord.filer_add2),
            new XElement("log_add3", App.CurrentApp.FAccidentsRecord.filer_add3),
            new XElement("log_pcode", App.CurrentApp.FAccidentsRecord.filer_pcode),
            new XElement("occupation", App.CurrentApp.FAccidentsRecord.occupation),
            new XElement("log_occupation", App.CurrentApp.FAccidentsRecord.filer_occupation),
            new XElement("how_happened", App.CurrentApp.FAccidentsRecord.how_did_accident_happen),
            new XElement("materials_used", App.CurrentApp.FAccidentsRecord.materials_used_in_treatment),
            new XElement("D4CheckID", App.CurrentApp.FAccidentsRecord.spare3),
            new XElement("sig_fname1", App.CurrentApp.FAccidentsRecord.spare1),
            new XElement("sig_fname2", App.CurrentApp.FAccidentsRecord.spare2),
            new XElement("where_happ_acc", App.CurrentApp.FAccidentsRecord.spare9),

            new XElement("AccorNear", App.CurrentApp.FAccidentsRecord.spare10),
            new XElement("what_happened", App.CurrentApp.FAccidentsRecord.spare11),
            new XElement("where_abouts", App.CurrentApp.FAccidentsRecord.spare12),
            new XElement("date_happened", App.CurrentApp.FAccidentsRecord.spare13),
            new XElement("anon_or_name", App.CurrentApp.FAccidentsRecord.spare14),
            new XElement("injuries", App.CurrentApp.FAccidentsRecord.spare4)));


            postData.Clear();

            postData.Append(srcTree.ToString());

            Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/WM7UploadFAccidents");
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
                if (sendResponse.Substring(0,2) == "OK")
                {
                    if(sendResponse.Length==10)
                    {
                        App.CurrentApp.FAccidentsRecord.spare3 = sendResponse.Substring(2,8);
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
                    App.net.FAccidentsRecord.bSent = true;
                }
                else
                {

                }
                Device.BeginInvokeOnMainThread(CompleteDownload);
            }
        }

        private void CompleteDownload()
        {
            App.net.FAccidentsRecord.bSent = true;
            App.data.SaveWorkAccident();
            act_ind.IsRunning = false;
            if (sendResponse == "nointernet")
            {
                complete_label.Text = "No Internet Connection";
            }
            else
            {
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
                new XElement("Filename", App.CurrentApp.FAccidentsRecord.spare3 + just_file_name),
                new XElement("D4CheckID", App.CurrentApp.FAccidentsRecord.spare3),
                new XElement("data", System.Convert.ToBase64String(data))));

                localpostData.Append(localTree.ToString());

                Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/SendFAccidentsImage");
                HttpHelper helper = new HttpHelper(thisuri, "POST",
                new KeyValuePair<string, string>("ContractFile", "<ContractFile>" + localpostData.ToString() + "</ContractFile>"));
                helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
                helper.Execute();
            }
        }

        private void CreateImagesList()
        {
            if (App.CurrentApp.FAccidentsRecord.spare10 != "Nearmiss")
            {
                images_to_send.Add(App.net.FAccidentsRecord.spare1);
                images_to_send.Add(App.net.FAccidentsRecord.spare2);
            }

            images_to_send.AddRange(App.files.GetFileList("Photos/", String.Format("8{0:0000000}", App.net.FAccidentsRecord.RecID) + "_FAcci*.*", "Photos/"));

            total_images = images_to_send.Count;

            current_image = 0;
        }
    }
}