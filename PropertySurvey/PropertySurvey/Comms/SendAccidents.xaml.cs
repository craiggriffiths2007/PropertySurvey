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
    public partial class SendAccidents : ContentPage
    {
        List<Accident_sheet> accidents = null;

        List<string> images_to_send = new List<string>();

        StringBuilder postData = new StringBuilder(128000);
        StringBuilder SurveyImage = new StringBuilder(128000);
        StringBuilder FittingImage = new StringBuilder(128000);

        string sendResponse = "";

        string AccidentID = "";

        string parts_compiled = "";

        int total_accidents;
        int current_accident;

        int current_image = 0;
        int total_images = 0;

        int total_items_to_send = 0;
        int current_item_sending = 0;

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

        public SendAccidents()
        {
            InitializeComponent();

            accidents = App.data.GetUnsentAccidents();

            total_accidents = accidents.Count();
            total_items_to_send = total_accidents;
            current_accident = 0;
            current_image = 0;
            if (accidents.Count() > 0)
            {
                CreateImagesList();
                total_items_to_send += images_to_send.Count();

                if (total_accidents > 0)
                {
                    //SendNextSurvey();
                    SendNextAccident();
                }
                else
                {
                    if (total_images > 0)
                    {
                        SendNextPicture();
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(CompleteDownload);
                    }
                }
            }
            else
                Device.BeginInvokeOnMainThread(CompleteDownload);
        }

        private void CreateImagesList()
        {
            images_to_send = new List<string>();

            foreach (var accident in accidents)
            {
                images_to_send.AddRange(App.files.GetFileList("Photos/", String.Format("9{0:0000000}", accident.RecID) + "_photo_*.*", "Photos/"));
                images_to_send.AddRange(App.files.GetFileList("Drawings/", String.Format("9{0:0000000}", accident.RecID)+ "*.*", "Drawings/"));
                images_to_send.AddRange(App.files.GetFileList("Signatures/", String.Format("9{0:0000000}", accident.RecID) + "*.*", "Signatures/"));
            }

            total_images = images_to_send.Count();
            current_image = 0;
        }

        private void CommandComplete(HttpResponseCompleteEventArgs e)
        {
            sendResponse = e.Response;

            Device.BeginInvokeOnMainThread(CompleteDownload);
        }

        private void CompleteDownload()
        {
            if (total_accidents > 0)
            {
                if (current_accident < total_accidents)
                {
                    if (true)//(current_accident > 0)
                    {
                        if (sendResponse.Substring(0, 4) == "Sent")
                        {
                            AccidentID = sendResponse.Substring(12, 8);
                            App.net.AccidentRecord = accidents[current_accident];
                            App.net.AccidentRecord.bSent = true;
                            App.data.SaveVehicleAccident();
                        }
                    }
                    SendNextPicture();
                    //CreateSurvey();
                    // SendNextSurvey();
                    //SendNextAccident();
                    current_accident++;
                    current_item_sending++;
                    sending_progress.ProgressTo((1.0f / total_items_to_send) * (float)current_item_sending, 250, Easing.Linear);
                }
                else
                {
                    if (current_image < total_images)
                    {
                        SendNextPicture();
                        current_image++;
                        current_item_sending++;
                        sending_progress.ProgressTo((1.0f / total_items_to_send) * (float)current_item_sending, 250, Easing.Linear);
                    }
                    else
                        Navigation.PopAsync(false);
                }
            }
            else
                Navigation.PopAsync(false);
        }


        private void SendNextAccident()
        {
            FittingImage = new StringBuilder(128000);

            AddAccident();

            Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/SendAccident");
            HttpHelper helper = new HttpHelper(thisuri, "POST",
            new KeyValuePair<string, string>("ContractFile", "<ContractFile>" + FittingImage.ToString() + "</ContractFile>"));
            helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
            helper.Execute();
        }

        private void AddYesNo(bool add_field, string name_field)
        {
            if (add_field == true)
            {
                FittingImage.Append(name_field + "Y\n");
            }
            else
            {
                FittingImage.Append(name_field + "N\n");
            }
        }

        private void AddYesNoInt(int add_field, string name_field)
        {
            if (add_field == 1)
            {
                FittingImage.Append(name_field + "Yes\n");
            }
            else
            {
                FittingImage.Append(name_field + "No\n");
            }
        }

        private void AddAccident()
        {
            Accident_sheet item = accidents[current_accident];
            //string date_string = String.Format("{0:d}", App.CurrentApp.HeaderRecord.uspot_date);
            //date_string = date_string.Substring(0, 6) + String.Format("{0:d}", App.CurrentApp.HeaderRecord.uspot_date).Substring(8, 2);

            XDocument srcTree = new XDocument(
            new XComment("This is a comment"),
            new XElement("Accident",
            new XElement("rec_id", item.RecID.ToString()),
            new XElement("pda_code", App.net.App_Settings.set_ownercode),
            new XElement("date_time_date", item.acc_date),
            new XElement("date_time_time", item.acc_time),
            new XElement("brief", item.brief.ToString()),
            new XElement("d_bPolice", item.d_bPolice.ToString()),
            new XElement("d_officers_name", item.d_officers_name.ToString()),
            new XElement("d_officers_number", item.d_officers_number.ToString()),
            new XElement("d_station", item.d_station.ToString()),
            new XElement("d_place", item.d_place.ToString()),
            new XElement("d_speed", item.d_speed.ToString()),
            new XElement("d_weather", item.d_weather.ToString()),
            new XElement("d_description", item.d_description.ToString()),
            new XElement("d_sign_date", item.d_sign_date.ToString()),
            new XElement("y_make", item.y_make.ToString()),
            new XElement("y_model", item.y_model.ToString()),
            new XElement("y_reg", item.y_reg.ToString()),
            new XElement("y_used_for", item.y_used_for.ToString()),
            new XElement("y_driver_full_name", item.y_driver_full_name.ToString()),
            new XElement("y_driver_dob", item.y_driver_dob.ToString()),
            new XElement("y_address1", item.y_address1.ToString()),
            new XElement("y_address2", item.y_address2.ToString()),
            new XElement("y_address3", item.y_address3.ToString()),
            new XElement("y_pcode", item.y_pcode.ToString()),
            new XElement("y_occupation", item.y_occupation.ToString()),
            new XElement("y_years_employed", item.y_years_employed.ToString()),
            new XElement("y_months_employed", item.y_months_employed.ToString()),
            new XElement("y_any_other_accidents", item.y_any_other_accidents.ToString()),
            new XElement("y_infirmity", item.y_infirmity.ToString()),
            new XElement("y_prosecution", item.y_prosecution.ToString()),
            new XElement("y_vehicle_damage", item.y_vehicle_damage.ToString()),
            new XElement("y_driveable", item.y_driveable.ToString()),
            new XElement("y_damage_to_property", item.y_damage_to_property.ToString()),
            new XElement("y_injuries_sustained", item.y_injuries_sustained.ToString()),
            new XElement("t_name", item.t_name.ToString()),
            new XElement("t_add1", item.t_add1.ToString()),
            new XElement("t_add2", item.t_add2.ToString()),
            new XElement("t_add3", item.t_add3.ToString()),
            new XElement("t_pcode", item.t_pcode.ToString()),
            new XElement("t_make", item.t_make.ToString()),
            new XElement("t_reg", item.t_reg.ToString()),
            new XElement("t_model", item.t_model.ToString()),
            new XElement("t_insurer", item.t_insurer.ToString()),
            new XElement("t_policy_no", item.t_policy_no.ToString()),
            new XElement("t_telnum", item.t_telnum.ToString()),
            new XElement("p_name", item.p_name.ToString()),
            new XElement("p_add1", item.p_add1.ToString()),
            new XElement("p_add2", item.p_add2.ToString()),
            new XElement("p_add3", item.p_add3.ToString()),
            new XElement("p_pcode", item.p_pcode.ToString()),
            new XElement("p_wittel", item.p_wittel.ToString()),
            new XElement("v_reg", item.v_reg.ToString()),
            new XElement("no_other_people", item.no_of_other_people.ToString()),
            new XElement("v_model", item.v_model.ToString())));

            FittingImage.Append(srcTree.ToString());
        }

        public void SendNextPicture()
        {
            byte[] data;
            string bLastImage = "no";

            string this_image_filename = images_to_send[current_image];
            string just_file_name;

            just_file_name = this_image_filename.Replace("Photos/", "");
            just_file_name = just_file_name.Replace("Drawings/", "");
            just_file_name = just_file_name.Replace("Signatures/", "");
            just_file_name = just_file_name.Replace("Videos/", "");

            just_file_name = just_file_name.Replace(String.Format("9{0:0000000}", accidents[0].RecID) + "_", "");

            just_file_name = just_file_name.Replace("dAU000","drawing_");

            if (current_image == total_images)
                bLastImage = "yes";

            StringBuilder localpostData = new StringBuilder(128000);
            if (App.files.FileExists(this_image_filename))
            {
                data = App.files.LoadBinary(this_image_filename);

                XDocument localTree = new XDocument(
                new XElement("Image",
                new XElement("UserType", App.net.App_Settings.set_usertype),
                new XElement("JobType", "F"),
                new XElement("UserCode", App.net.App_Settings.set_ownercode),
                new XElement("LastImage", bLastImage),
                new XElement("AccidentID", AccidentID),
                new XElement("Filename", just_file_name),
                new XElement("data", App.net.bDoValidation ? System.Convert.ToBase64String(data) : "")));

                localpostData.Append(localTree.ToString());

                Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/SendAccidentImage");
                HttpHelper helper = new HttpHelper(thisuri, "POST",
                new KeyValuePair<string, string>("ContractFile", "<ContractFile>" + localpostData.ToString() + "</ContractFile>"));
                helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
                helper.Execute();
            }
        }
        
        private string YesNoBool(bool yorn)
        {
            if (yorn == true)
                return "Yes";
            else
                return "No";
        }
        private string YesNoString(int yorn)
        {
            switch (yorn)
            {
                case 0: return "";
                case 1: return "Yes";
                case 2: return "No";
                default: return "";
            }
        }

        private string PetMagnetString(int yorn)
        {
            switch (yorn)
            {
                case 0: return "";
                case 1: return "None";
                case 2: return "Microchip";
                case 3: return "Magnetic";
                default: return "";
            }
        }


        private string InOutString(int yorn)
        {
            switch (yorn)
            {
                case 0: return "";
                case 1: return "IN";
                case 2: return "OUT";
                case 3: return "FIXED";
                default: return "";
            }
        }
        private string GetLockType(int iType)
        {
            switch (iType)
            {
                case 1: return "Deadlock"; break;
                case 2: return "Hook"; break;
                case 3: return "Latch"; break;
                case 4: return "Cam"; break;
                case 5: return "Mushroom"; break;
                default: return ""; break;
            }
        }
    }
}