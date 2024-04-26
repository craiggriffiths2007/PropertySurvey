using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendPictures : ContentPage
    {
        List<string> images_to_send = new List<string>();

        StringBuilder postData = new StringBuilder(128000);
        StringBuilder SurveyImage = new StringBuilder(128000);

        string sendResponse = "";

        int current_image = 0;
        int total_images = 0;

        string last_filename = "";
        int current_file_position = 0;

        byte[] data;

        public SendPictures()
        {
            InitializeComponent();

            CreateImagesList();
            SendNextPicture();
        }


        private void CreateImagesList()
        {
            images_to_send = new List<string>();

            images_to_send.AddRange(App.files.GetFileList("PhotosSend/", "*.jpg", "PhotosSend/"));

            total_images = images_to_send.Count();
            current_image = 0;
        }


        private void CommandComplete(HttpResponseCompleteEventArgs e)
        {
            sendResponse = e.Response;

            Device.BeginInvokeOnMainThread(CompleteDownload);
        }

        public void SendNextPicture()
        {
            byte[] send_data = new byte[200000];
            string bLastImage = "no";

            string this_image_filename = images_to_send[current_image];
            string just_file_name;

            if (App.net.bCopyDatabaseFileToDownloads == true)
            {

            }

            just_file_name = this_image_filename.Replace("Photos/", "");
            just_file_name = just_file_name.Replace("PhotosSend/", "");
            just_file_name = just_file_name.Replace("Drawings/", "");
            just_file_name = just_file_name.Replace("Signatures/", "");
            just_file_name = just_file_name.Replace("Videos/", "");

            if (current_image == total_images)
                bLastImage = "yes";

            try
            {
                StringBuilder localpostData = new StringBuilder(128000);
                if (App.files.FileExists(this_image_filename))
                {

                    if (just_file_name.Substring(just_file_name.Length - 3, 3) == "mp4")
                    {
                        if (last_filename == just_file_name)
                            current_file_position += 200000;
                        else
                        {
                            current_file_position = 0;
                            //data = App.files.LoadBinaryRange(this_image_filename, current_file_position, 200000);
                            data = App.files.LoadBinary(this_image_filename);
                        }

                        int length_to_copy = 200000;
                        if (current_file_position + 200000 > data.Length)
                        {
                            length_to_copy = data.Length - (current_file_position + 200000);
                        }
                        Array.Copy(data, current_file_position, send_data, 0, length_to_copy);
                    }
                    else
                    {
                        send_data = App.files.LoadBinary(this_image_filename);
                    }
                    //data = App.files.LoadBinary(this_image_filename);

                    last_filename = just_file_name;

                    XDocument localTree = new XDocument(
                    new XElement("Image",
                    new XElement("UserType", "@"),
                    new XElement("JobType", "@"),
                    new XElement("UserCode", App.net.App_Settings.set_ownercode),
                    new XElement("LastImage", bLastImage),
                    new XElement("PhoneSerial", "@" /*App.CurrentApp.cereal*/),
                    new XElement("Branch", "@"), 
                    new XElement("Contract", just_file_name.Substring(0,8)),
                    new XElement("Filename", just_file_name),
                    new XElement("data", App.net.bDoValidation ? System.Convert.ToBase64String(send_data) : "")));

                    localpostData.Append(localTree.ToString());

                    Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/SendJobImage");
                    HttpHelper helper = new HttpHelper(thisuri, "POST",
                    new KeyValuePair<string, string>("ContractFile", "<ContractFile>" + localpostData.ToString() + "</ContractFile>"));
                    helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
                    helper.Execute();
                }
            }
            catch (Exception)
            {

            }
        }

        private void CompleteDownload()
        {
            if (current_image < total_images)
            {
                try
                {
                    SendNextPicture();
                }
                catch (Exception)
                {
                    DisplayAlert("error sending", images_to_send[current_image], "OK");
                }
                //SendNextPicture();
                current_image++;
            }
            else
            {
                Navigation.PopAsync(false);
            }
        }
    }
}