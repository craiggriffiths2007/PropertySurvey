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
	public partial class MileageSend : ContentPage
	{
        StringBuilder postData = new StringBuilder(128000);
        int total_images;
        int current_image;
        string sendResponse = "";
        List<string> images_to_send = new List<string>();

        public MileageSend()
        {
            InitializeComponent();

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
            new XElement("Serial", "@"),
            new XElement("UserCode", App.net.App_Settings.set_ownercode),
            new XElement("pcode_am", App.net.MileageRecord.start_postcode),
            new XElement("pcode_pm", App.net.MileageRecord.finish_postcode),
            new XElement("miles_am", App.net.MileageRecord.start_mileage),
            new XElement("miles_pm", App.net.MileageRecord.end_mileage),

            new XElement("registration", App.net.MileageRecord.registration),

            new XElement("op_pcode1", App.net.MileageRecord.op_postcode1),
            new XElement("op_pcode2", App.net.MileageRecord.op_postcode2),
            new XElement("op_pcode3", App.net.MileageRecord.op_postcode3),
            new XElement("op_pcode4", App.net.MileageRecord.op_postcode4),
            new XElement("op_pcode5", App.net.MileageRecord.op_postcode5),
            new XElement("op_pcode6", App.net.MileageRecord.op_postcode6),
            new XElement("op_pcode7", App.net.MileageRecord.op_postcode7),
            new XElement("op_pcode8", App.net.MileageRecord.op_postcode8),
            new XElement("op_pcode9", App.net.MileageRecord.op_postcode9),
            new XElement("op_pcode10", App.net.MileageRecord.op_postcode10),

            new XElement("op_time1", String.Format("{0:HH:mm}", App.net.MileageRecord.op_time1)),
            new XElement("op_time2", String.Format("{0:HH:mm}", App.net.MileageRecord.op_time2)),
            new XElement("op_time3", String.Format("{0:HH:mm}", App.net.MileageRecord.op_time3)),
            new XElement("op_time4", String.Format("{0:HH:mm}", App.net.MileageRecord.op_time4)),
            new XElement("op_time5", String.Format("{0:HH:mm}", App.net.MileageRecord.op_time5)),
            new XElement("op_time6", String.Format("{0:HH:mm}", App.net.MileageRecord.op_time6)),
            new XElement("op_time7", String.Format("{0:HH:mm}", App.net.MileageRecord.op_time7)),
            new XElement("op_time8", String.Format("{0:HH:mm}", App.net.MileageRecord.op_time8)),
            new XElement("op_time9", String.Format("{0:HH:mm}", App.net.MileageRecord.op_time9)),
            new XElement("op_time10", String.Format("{0:HH:mm}", App.net.MileageRecord.op_time10)),

            new XElement("signature_filename", App.net.MileageRecord.signature_filename.Replace("Signatures/", "")),

            new XElement("photo_am_filename", App.net.MileageRecord.new_sspare1.Replace("Photos/", "")),
            new XElement("photo_pm_filename", App.net.MileageRecord.new_sspare2.Replace("Photos/", "")),

            new XElement("comments", App.net.MileageRecord.comments),

            new XElement("miles_date", String.Format("{0:dd/MM/yy}", App.net.MileageRecord.sheet_date)),

            new XElement("toll_desc", App.net.MileageRecord.toll_charge_for),
            new XElement("toll_value", App.net.MileageRecord.toll_charge_ammount),

            new XElement("no_op", App.net.MileageRecord.no_of_other_places.ToString())));

            postData.Clear();

            postData.Append(srcTree.ToString());

            Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/WM7UploadMiles");
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
                if (sendResponse == "OK")
                {
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
                    App.net.MileageRecord.bSent = true;
                }
                else
                {
                    
                }
                Device.BeginInvokeOnMainThread(CompleteDownload);
            }
        }

        private void CompleteDownload()
        {
            App.net.MileageRecord.bSent = true;
            App.data.SaveMileage();
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
                new XElement("Filename", just_file_name),
                new XElement("data", System.Convert.ToBase64String(data))));

                localpostData.Append(localTree.ToString());

                Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/SendMileageImage");
                HttpHelper helper = new HttpHelper(thisuri, "POST",
                new KeyValuePair<string, string>("ContractFile", "<ContractFile>" + localpostData.ToString() + "</ContractFile>"));
                helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
                helper.Execute();
            }
        }

        private void CreateImagesList()
        {
            images_to_send.Add(App.net.MileageRecord.signature_filename);
            if(App.net.MileageRecord.new_sspare1!=null)
                images_to_send.Add(App.net.MileageRecord.new_sspare1);
            if(App.net.MileageRecord.new_sspare2!=null)
                images_to_send.Add(App.net.MileageRecord.new_sspare2);
            total_images = 3;

            current_image = 0;
        }
    }
}