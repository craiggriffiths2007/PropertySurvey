using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using System.Text.Json;
using System.Net.Http;
using static PropertySurvey.SendSurveys;
using System.Text.Json.Serialization;


namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendSurveys : ContentPage
    {
        List<Header> headers = null;

        List<string> images_to_send = new List<string>();

        StringBuilder postData = new StringBuilder(128000);
        StringBuilder SurveyImage = new StringBuilder(128000);

        string sendResponse = "";

        string parts_compiled = "";

        int total_surveys;
        int current_survey;

        int current_image = 0;
        int total_images = 0;

        int total_items_to_send = 0;
        int current_item_sending = 0;

        string job_type = "";
        string summaries_compiled = "";

        string last_filename = "";
        int current_file_position = 0;

        byte[] data;

        HttpClient client;
        JsonSerializerOptions serializerOptions;

        public SendSurveys()
        {
            InitializeComponent();

            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            headers = App.data.GetUnsentHeadersSurveys();

            total_surveys = headers.Count();
            total_items_to_send = total_surveys;
            current_survey = 0;
            current_image = 0;
            if (headers.Count() > 0)
            {
                CreateImagesList();
                total_items_to_send += images_to_send.Count();

                try
                {

                    SendImagesJson();
                    SendSurveyJson();
                }
                catch (Exception e)
                {
                    DisplayAlert("error sending", e.ToString(), "OK");
                }                
                

            }
             
       
        }

        public async Task SendImagesJson()
        {

            foreach (var p in images_to_send)
            {
                Uri uri = new Uri(string.Format(App.net.App_Settings.set_url + "/SendSurveyImage", string.Empty));

                ImageDTO image_dto = new ImageDTO();

                string just_file_name = p.Replace("Photos/", "");
                just_file_name = just_file_name.Replace("Drawings/", "");
                just_file_name = just_file_name.Replace("Signatures/", "");
                just_file_name = just_file_name.Replace("Videos/", "");

                if (App.files.FileExists(p))
                {
                    data = App.files.LoadBinary(p);
                }
                image_dto.Filename = just_file_name;
                image_dto.Data = System.Convert.ToBase64String(data);

                string json = JsonSerializer.Serialize<ImageDTO>(image_dto, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string receive_content = await response.Content.ReadAsStringAsync();
                    OKRecordDTO receive_record = JsonSerializer.Deserialize<OKRecordDTO>(receive_content, serializerOptions);

                    //p.Id = receive_record.DBId;
                    //App.data.SavePanel(p);
                }
            }

            MessageImages.Text = "Images Sent";

        }
        public async Task SendSurveyJson()
        {
            Uri uri = new Uri(string.Format(App.net.App_Settings.set_url + "/SendSurveyHeader", string.Empty));

            try
            {
                string json = JsonSerializer.Serialize<Header>(headers[current_survey], serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string receive_content = await response.Content.ReadAsStringAsync();
                    OKRecordDTO receive_record = JsonSerializer.Deserialize<OKRecordDTO>(receive_content, serializerOptions);
                    headers[current_survey].bSent = true;
                    headers[current_survey].Id = receive_record.DBId;
                    App.net.HeaderRecord = headers[current_survey];
                    
                    App.data.SaveHeader();

                    List<PanelTable> panels = App.data.GetPanelsByContract(headers[current_survey].udi_cont);
                    MessageLabel.Text = "Panel";
                    foreach (var p in panels)
                    {
                        p.HeaderId = headers[current_survey].Id;

                        uri = new Uri(string.Format(App.net.App_Settings.set_url + "/SendSurveyPanel", string.Empty));

                        json = JsonSerializer.Serialize<PanelTable>(p, serializerOptions);
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        response = null;
                        response = await client.PostAsync(uri, content);

                        if (response.IsSuccessStatusCode)
                        {
                            receive_content = await response.Content.ReadAsStringAsync();
                            receive_record = JsonSerializer.Deserialize<OKRecordDTO>(receive_content, serializerOptions);

                            p.Id = receive_record.DBId;
                            App.data.SavePanel(p);
                        }
                    }

                    List<AlumTable> alums = App.data.GetAlumsByContract(headers[current_survey].udi_cont);

                    foreach (var p in alums)
                    {
                        p.HeaderId = headers[current_survey].Id;

                        uri = new Uri(string.Format(App.net.App_Settings.set_url + "/SendSurveyPanel", string.Empty));

                        json = JsonSerializer.Serialize<AlumTable>(p, serializerOptions);
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        response = null;
                        response = await client.PostAsync(uri, content);

                        if (response.IsSuccessStatusCode)
                        {
                            receive_content = await response.Content.ReadAsStringAsync();
                            receive_record = JsonSerializer.Deserialize<OKRecordDTO>(receive_content, serializerOptions);

                            p.Id = receive_record.DBId;
                            App.data.SaveAlum(p);
                        }
                    }

                    List<BifoldTable> bifold = App.data.GetBifoldsByContract(headers[current_survey].udi_cont);

                    foreach (var p in bifold)
                    {
                        p.HeaderId = headers[current_survey].Id;

                        uri = new Uri(string.Format(App.net.App_Settings.set_url + "/SendSurveyBifold", string.Empty));

                        json = JsonSerializer.Serialize<BifoldTable>(p, serializerOptions);
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        response = null;
                        response = await client.PostAsync(uri, content);

                        if (response.IsSuccessStatusCode)
                        {
                            receive_content = await response.Content.ReadAsStringAsync();
                            receive_record = JsonSerializer.Deserialize<OKRecordDTO>(receive_content, serializerOptions);

                            p.Id = receive_record.DBId;
                            App.data.SaveBifold(p);
                        }
                    }

                    List<CompositeTable> comp = App.data.GetCompByContract(headers[current_survey].udi_cont);

                    foreach (var p in comp)
                    {
                        p.HeaderId = headers[current_survey].Id;

                        uri = new Uri(string.Format(App.net.App_Settings.set_url + "/SendSurveyComp", string.Empty));

                        json = JsonSerializer.Serialize<CompositeTable>(p, serializerOptions);
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        response = null;
                        response = await client.PostAsync(uri, content);

                        if (response.IsSuccessStatusCode)
                        {
                            receive_content = await response.Content.ReadAsStringAsync();
                            receive_record = JsonSerializer.Deserialize<OKRecordDTO>(receive_content, serializerOptions);

                            p.Id = receive_record.DBId;
                            App.data.SaveComp(p);
                        }
                    }

                    List<ConsTable> cons = App.data.GetConssByContract(headers[current_survey].udi_cont);

                    foreach (var p in cons)
                    {
                        p.HeaderId = headers[current_survey].Id;

                        uri = new Uri(string.Format(App.net.App_Settings.set_url + "/SendSurveyCons", string.Empty));

                        json = JsonSerializer.Serialize<ConsTable>(p, serializerOptions);
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        response = null;
                        response = await client.PostAsync(uri, content);

                        if (response.IsSuccessStatusCode)
                        {
                            receive_content = await response.Content.ReadAsStringAsync();
                            receive_record = JsonSerializer.Deserialize<OKRecordDTO>(receive_content, serializerOptions);

                            p.Id = receive_record.DBId;
                            App.data.SaveCons(p);
                        }
                    }

                    List<GarageTable> gar = App.data.GetGaragesByContract(headers[current_survey].udi_cont);

                    foreach (var p in gar)
                    {
                        p.HeaderId = headers[current_survey].Id;

                        uri = new Uri(string.Format(App.net.App_Settings.set_url + "/SendSurveyGar", string.Empty));

                        json = JsonSerializer.Serialize<GarageTable>(p, serializerOptions);
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        response = null;
                        response = await client.PostAsync(uri, content);

                        if (response.IsSuccessStatusCode)
                        {
                            receive_content = await response.Content.ReadAsStringAsync();
                            receive_record = JsonSerializer.Deserialize<OKRecordDTO>(receive_content, serializerOptions);

                            p.Id = receive_record.DBId;
                            App.data.SaveGarage(p);
                        }
                    }

                    List<GlassTable> glass = App.data.GetGlasssByContract(headers[current_survey].udi_cont);

                    foreach (var p in glass)
                    {
                        p.HeaderId = headers[current_survey].Id;

                        uri = new Uri(string.Format(App.net.App_Settings.set_url + "/SendSurveyGlass", string.Empty));

                        json = JsonSerializer.Serialize<GlassTable>(p, serializerOptions);
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        response = null;
                        response = await client.PostAsync(uri, content);

                        if (response.IsSuccessStatusCode)
                        {
                            receive_content = await response.Content.ReadAsStringAsync();
                            receive_record = JsonSerializer.Deserialize<OKRecordDTO>(receive_content, serializerOptions);

                            p.Id = receive_record.DBId;
                            App.data.SaveGlass(p);
                        }
                    }

                    List<GreenTable> green = App.data.GetGreensByContract(headers[current_survey].udi_cont);

                    foreach (var p in green)
                    {
                        p.HeaderId = headers[current_survey].Id;

                        uri = new Uri(string.Format(App.net.App_Settings.set_url + "/SendSurveyGreen", string.Empty));

                        json = JsonSerializer.Serialize<GreenTable>(p, serializerOptions);
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        response = null;
                        response = await client.PostAsync(uri, content);

                        if (response.IsSuccessStatusCode)
                        {
                            receive_content = await response.Content.ReadAsStringAsync();
                            receive_record = JsonSerializer.Deserialize<OKRecordDTO>(receive_content, serializerOptions);

                            p.Id = receive_record.DBId;
                            App.data.SaveGreen(p);
                        }
                    }

                    List<LockingTable> locking = App.data.GetLockByContract(headers[current_survey].udi_cont);

                    foreach (var p in locking)
                    {
                        p.HeaderId = headers[current_survey].Id;

                        uri = new Uri(string.Format(App.net.App_Settings.set_url + "/SendSurveyLock", string.Empty));

                        json = JsonSerializer.Serialize<LockingTable>(p, serializerOptions);
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        response = null;
                        response = await client.PostAsync(uri, content);

                        if (response.IsSuccessStatusCode)
                        {
                            receive_content = await response.Content.ReadAsStringAsync();
                            receive_record = JsonSerializer.Deserialize<OKRecordDTO>(receive_content, serializerOptions);

                            p.Id = receive_record.DBId;
                            App.data.SaveLocking(p);
                        }
                    }
                    List<TimberTable> timb = App.data.GetTimberByContract(headers[current_survey].udi_cont);

                    foreach (var p in timb)
                    {
                        p.HeaderId = headers[current_survey].Id;

                        uri = new Uri(string.Format(App.net.App_Settings.set_url + "/SendSurveyTimber", string.Empty));

                        json = JsonSerializer.Serialize<TimberTable>(p, serializerOptions);
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        response = null;
                        response = await client.PostAsync(uri, content);

                        if (response.IsSuccessStatusCode)
                        {
                            receive_content = await response.Content.ReadAsStringAsync();
                            receive_record = JsonSerializer.Deserialize<OKRecordDTO>(receive_content, serializerOptions);

                            p.Id = receive_record.DBId;
                            App.data.SaveTimber(p);
                        }
                    }

                    List<UPVCTable> upvc = App.data.GetUPVCsByContract(headers[current_survey].udi_cont);

                    foreach (var p in upvc)
                    {
                        p.HeaderId = headers[current_survey].Id;

                        uri = new Uri(string.Format(App.net.App_Settings.set_url + "/SendSurveyUPVC", string.Empty));

                        json = JsonSerializer.Serialize<UPVCTable>(p, serializerOptions);
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                        response = null;
                        response = await client.PostAsync(uri, content);

                        if (response.IsSuccessStatusCode)
                        {
                            receive_content = await response.Content.ReadAsStringAsync();
                            receive_record = JsonSerializer.Deserialize<OKRecordDTO>(receive_content, serializerOptions);

                            p.Id = receive_record.DBId;
                            App.data.SaveUPVC(p);
                        }
                    }

                    

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }
            //await Navigation.PopAsync();
            //Device.BeginInvokeOnMainThread(CompleteDownload2);
            act_ind.IsRunning = false;
            MessageLabel.Text = "Complete";
            CompleteButton.IsVisible = true;
        }


        private void CompleteDownload2()
        {

        }


        private void OnCompleteClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        public void SendNextSurvey()
        {
            StringBuilder localpostData = new StringBuilder(128000);

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(SurveyImage.ToString());

            Header item = headers[current_survey];

            XDocument localTree = new XDocument(
            new XElement("ContractFile",
            new XElement("UserType", App.net.App_Settings.set_usertype),
            new XElement("JobType", job_type),
            new XElement("CreateEnd", "true"),
            new XElement("UserCode", App.net.App_Settings.set_ownercode),
            new XElement("UserType", App.net.App_Settings.set_usertype),
            new XElement("ContractNumber", headers[current_survey].udi_cont),
            new XElement("Job", System.Convert.ToBase64String(buffer))));


            if (item.ss_no_of_photos > 0)
            {
                string str_sec_surv = new XDocument(new XElement("SecSurveyIn",
                       new XElement("contract", item.udi_cont),
                       new XElement("nowindows", item.ss_nowindows.ToString()),

                       new XElement("nodoors", item.ss_nodoors.ToString()),
                       new XElement("gencondition", item.ss_gencondition),
                       new XElement("gencondition_other", item.ss_gencondition_other),
                       new XElement("matwindows", item.ss_matwindows),
                       new XElement("matwindows_other", item.ss_matwindows_other),
                       new XElement("matdoors", item.ss_matdoors),
                       new XElement("matdoors_other", item.ss_matdoors_other),
                       new XElement("lockwindows", item.ss_lockwindows),
                       new XElement("lockwindows_other", item.ss_lockwindows_other),
                       new XElement("lockdoors", item.ss_lockdoors),
                       new XElement("lockdoors_other", item.ss_lockdoors_other),
                       new XElement("add_window_security", item.ss_add_window_security.ToString()),
                       new XElement("location_windows_other", item.ss_location_windows_other),
                       new XElement("secwindows_other", item.ss_secwindows_other),
                       new XElement("add_door_security", item.ss_add_door_security.ToString()),
                       new XElement("location_doors_other", item.ss_location_doors_other),
                       new XElement("secdoors_other", item.ss_secdoors_other),
                       new XElement("time_required", item.ss_time_required),
                       new XElement("no_of_photos", item.ss_no_of_photos.ToString()))).ToString();

                localTree.Element("ContractFile").Add(new XElement("SecSurvey", str_sec_surv));
            }

            localpostData.Append(localTree.ToString());

            Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/SendJobFile");
            HttpHelper helper = new HttpHelper(thisuri, "POST",
            new KeyValuePair<string, string>("ContractFile", localpostData.ToString()));
            helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
            helper.Execute();
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

        private void CreateImagesList()
        {
            images_to_send = new List<string>();

            foreach (var header in headers)
            {
                images_to_send.AddRange(App.files.GetFileList("Photos/", header.udi_cont + "*.jpg", "Photos/"));
                //images_to_send.AddRange(App.files.GetFileListSegments("Photos/", header.udi_cont + "*.mp4", "Photos/"));
                images_to_send.AddRange(App.files.GetFileList("Photos/SS/", header.udi_cont + "*.*", "Photos/SS/"));
                images_to_send.AddRange(App.files.GetFileList("Drawings/", header.udi_cont + "*.*", "Drawings/"));
                images_to_send.AddRange(App.files.GetFileList("Signatures/", header.udi_cont + "*.*", "Signatures/"));
                images_to_send.AddRange(App.files.GetFileList("Videos/", header.udi_cont + "*.*", "Videos/"));
            }

            if (false)//(App.net.App_Settings.send_data_file == true)
            {
                images_to_send.Add("PropertySurveySQLite.db3");
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
            if (total_surveys > 0)
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
                    current_item_sending++;
                    sending_progress.ProgressTo((1.0f / total_items_to_send) * (float)current_item_sending, 250, Easing.Linear);
                }
                else
                {
                    if (current_survey > 0)
                    {
                        if (sendResponse == "<done>")
                        {
                            App.net.HeaderRecord = headers[current_survey - 1];
                            App.net.HeaderRecord.bSent = true;
                            App.data.SaveHeader();
                        }
                    }
                    if (current_survey < total_surveys)
                    {
                        try
                        {
                            CreateSurvey();
                        }
                        catch (Exception)
                        {
                            DisplayAlert("error making", headers[current_survey].udi_cont, "OK");
                        }
                        try
                        {
                            SendNextSurvey();
                        }
                        catch (Exception)
                        {
                            DisplayAlert("error sending", headers[current_survey].udi_cont, "OK");
                        }
                        current_survey++;
                        current_item_sending++;
                        sending_progress.ProgressTo((1.0f / total_items_to_send) * (float)current_item_sending, 250, Easing.Linear);
                    }
                    else
                    {
                        if (App.net.App_Settings.set_usertype == "SAT")
                        {
                            Navigation.InsertPageBefore(new SendFittings(), this);
                            Navigation.PopAsync(false);
                        }
                        else
                        {
                            Navigation.PopAsync(false);
                        }
                    }

                }
            }
            else
            {
                if (App.net.App_Settings.set_usertype == "SAT")
                {
                    Navigation.InsertPageBefore(new SendFittings(), this);
                    Navigation.PopAsync(false);
                }
                else
                {
                    Navigation.PopAsync(false);
                }
            }
        }

        private XDocument MakeSecuritySurvey()
        {
            Header item = headers[current_survey];

            return new XDocument(
                   new XElement("SecSurveyIn",
                   new XElement("contract", item.udi_cont),
                   new XElement("nowindows", item.ss_nowindows.ToString()),

                   new XElement("nodoors", item.ss_nodoors.ToString()),
                   new XElement("gencondition", item.ss_gencondition),
                   new XElement("gencondition_other", item.ss_gencondition_other),
                   new XElement("matwindows", item.ss_matwindows),
                   new XElement("matwindows_other", item.ss_matwindows_other),
                   new XElement("matdoors", item.ss_matdoors),
                   new XElement("matdoors_other", item.ss_matdoors_other),
                   new XElement("lockwindows", item.ss_lockwindows),
                   new XElement("lockwindows_other", item.ss_lockwindows_other),
                   new XElement("lockdoors", item.ss_lockdoors),
                   new XElement("lockdoors_other", item.ss_lockdoors_other),
                   new XElement("add_window_security", item.ss_add_window_security.ToString()),
                   new XElement("location_windows_other", item.ss_location_windows_other),
                   new XElement("secwindows_other", item.ss_secwindows_other),
                   new XElement("add_door_security", item.ss_add_door_security.ToString()),
                   new XElement("location_doors_other", item.ss_location_doors_other),
                   new XElement("secdoors_other", item.ss_secdoors_other),
                   new XElement("time_required", item.ss_time_required),
                   new XElement("no_of_photos", item.ss_no_of_photos.ToString())));
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
            just_file_name = just_file_name.Replace("Drawings/", "");
            just_file_name = just_file_name.Replace("Signatures/", "");
            just_file_name = just_file_name.Replace("Videos/", "");

            if (current_image == total_images)
                bLastImage = "yes";

            try
            {
                StringBuilder localpostData = new StringBuilder(128000);
                if (App.files.FileExists(this_image_filename) || (App.net.bCopyDatabaseFileToDownloads == true && this_image_filename == "PropertySurveySQLite.db3"))
                {
                    if (this_image_filename == "PropertySurveySQLite.db3")
                        data = App.files.LoadBinaryFromDownloads(this_image_filename);
                    else
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
                    }
                    last_filename = just_file_name;

                    XDocument localTree = new XDocument(
                    new XElement("Image",
                    new XElement("UserType", App.net.App_Settings.set_usertype),
                    new XElement("JobType", "S"),
                    new XElement("UserCode", App.net.App_Settings.set_ownercode),
                    new XElement("LastImage", bLastImage),
                    new XElement("Contract", "00000000"),
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

        private void AddSpotCheck()
        {
            string date_string = String.Format("{0:d}", headers[current_survey].uspot_date);
            date_string = date_string.Substring(0, 6) + String.Format("{0:d}", headers[current_survey].uspot_date).Substring(8, 2);
            SurveyImage.Append("**SPOTHEADER**\n");

            SurveyImage.Append("udi_cont=    " + headers[current_survey].udi_cont + "\n");
            SurveyImage.Append("uspotfitter= " + headers[current_survey].uspot_fitter + "\n");
            SurveyImage.Append("uspottrainee=" + headers[current_survey].uspot_trainee + "\n");
            SurveyImage.Append("uspotdate=   " + date_string + "\n");

            Add_YN("pdaRepair=   ", headers[current_survey].uspot_repair);
            Add_YN("pdaRpr_Arivd=", headers[current_survey].uspot_repair_arrived);
            Add_YN("pdaRpr_StUp= ", headers[current_survey].uspot_repair_setup);
            Add_YN("pdaRpr_Going=", headers[current_survey].uspot_repair_ongoing);
            Add_YN("pdaRpr_Cmplt=", headers[current_survey].uspot_repair_completed);
            Add_YN("pdaReplace=  ", headers[current_survey].uspot_replace);
            Add_YN("pdaRplc_Ariv=", headers[current_survey].uspot_replace_arrived);
            Add_YN("pdaRplc_StUp=", headers[current_survey].uspot_replace_setup);
            Add_YN("pdaRplc_UnRm=", headers[current_survey].uspot_replace_unitmoved);
            Add_YN("pdaRplc_Fitt=", headers[current_survey].uspot_replace_fit);
            Add_YN("pdaRplc_Cmpl=", headers[current_survey].uspot_replace_completed);
            Add_YN("pdaWrk_Door= ", headers[current_survey].uspot_rev_door);
            Add_YN("pdaWrk_Win=  ", headers[current_survey].uspot_rev_window);
            Add_YN("pdaWrk_Gar=  ", headers[current_survey].uspot_rev_garagedoor);
            Add_YN("pdaWrk_Glas= ", headers[current_survey].uspot_rev_glass);
            Add_YN("pdaWrk_Loc=  ", headers[current_survey].uspot_rev_locks);
            Add_YN("pdaWrk_Oth=  ", headers[current_survey].uspot_rev_other);
            Add_YN("pdaMat_uPVC= ", headers[current_survey].uspot_revb_upvc);
            Add_YN("pdaMat_Ali=  ", headers[current_survey].uspot_revb_ali);
            Add_YN("pdaMat_Tim=  ", headers[current_survey].uspot_revb_timber);
            Add_YN("pdaMat_Oth=  ", headers[current_survey].uspot_revb_other);

            SurveyImage.Append("pdaAs_Appear=" + headers[current_survey].uspot_appearence.ToString() + "\n");
            SurveyImage.Append("pdaAs_AppTxt=" + headers[current_survey].uspot_appearence_improvements + "\n");
            SurveyImage.Append("pdaAs_Qual=  " + headers[current_survey].uspot_qualityofworks.ToString() + "\n");
            SurveyImage.Append("pdaAs_QalTxt=" + headers[current_survey].uspot_qualityofworks_improvements + "\n");
            SurveyImage.Append("pdaAs_Cust=  " + headers[current_survey].uspot_customersatisfaction.ToString() + "\n");
            SurveyImage.Append("pdaAs_CuTxt= " + headers[current_survey].uspot_customersatisfaction_improvements + "\n");
            SurveyImage.Append("pdaObserv=   " + headers[current_survey].uspot_otherobservations + "\n");
            SurveyImage.Append("fname1=      " + headers[current_survey].name1 + "\n");
            add_YesNo_nonzero("uniform1=    ", headers[current_survey].safety_boots_worn1);
            SurveyImage.Append("uniform1s=   " + headers[current_survey].safety_boots_worn1_s + "\n");
            add_YesNo_nonzero("uniform2=    ", headers[current_survey].safety_boots_worn1);
            SurveyImage.Append("uniform2s=   " + headers[current_survey].safety_boots_worn1_s + "\n");
            add_YesNo_nonzero("sftyboswrn1= ", headers[current_survey].safety_boots_worn1);
            SurveyImage.Append("sftyboswrn1s=" + headers[current_survey].safety_boots_worn1_s + "\n");
            add_YesNo_nonzero("sftyglowrn1= ", headers[current_survey].safety_gloves_worn1);
            SurveyImage.Append("sftyglowrn1s=" + headers[current_survey].safety_gloves_worn1_s + "\n");
            add_YesNo_nonzero("sftygoowrn1= ", headers[current_survey].safety_googles_worn1);
            SurveyImage.Append("sftygoowrn1s=" + headers[current_survey].safety_googles_worn1_s + "\n");
            add_YesNo_nonzero("sftyhelwrn1= ", headers[current_survey].safety_helmet_worn1);
            SurveyImage.Append("sftyhelwrn1s=" + headers[current_survey].safety_helmet_worn1_s + "\n");
            add_YesNo_nonzero("wrstgdswrn1= ", headers[current_survey].wristguards_worn1);
            SurveyImage.Append("wrstgdswrn1s=" + headers[current_survey].wristguards_worn1_s + "\n");
            add_YesNo_nonzero("idcrdavail1= ", headers[current_survey].safety_boots_worn1);
            SurveyImage.Append("idcrdavail1s=" + headers[current_survey].name1 + "\n");
            SurveyImage.Append("fname2=      " + headers[current_survey].name1 + "\n");
            add_YesNo_nonzero("sftyboswrn2= ", headers[current_survey].safety_boots_worn2);
            SurveyImage.Append("sftyboswrn2s=" + headers[current_survey].safety_boots_worn2_s + "\n");
            add_YesNo_nonzero("sftyglowrn2= ", headers[current_survey].safety_gloves_worn2);
            SurveyImage.Append("sftyglowrn2s=" + headers[current_survey].safety_gloves_worn2_s + "\n");
            add_YesNo_nonzero("sftygoowrn2= ", headers[current_survey].safety_googles_worn2);
            SurveyImage.Append("sftygoowrn2s=" + headers[current_survey].safety_googles_worn2_s + "\n");
            add_YesNo_nonzero("sftyhelwrn2= ", headers[current_survey].safety_helmet_worn2);
            SurveyImage.Append("sftyhelwrn2s=" + headers[current_survey].safety_helmet_worn2_s + "\n");
            add_YesNo_nonzero("wrstgdswrn2= ", headers[current_survey].wristguards_worn2);
            SurveyImage.Append("sftyhelwrn2s=" + headers[current_survey].wristguards_worn2_s + "\n");
            add_YesNo_nonzero("idcrdavail2= ", headers[current_survey].id_card_available2);
            SurveyImage.Append("idcrdavail2s=" + headers[current_survey].id_card_available2_s + "\n");
            add_YesNo_nonzero("chemstocor=  ", headers[current_survey].chemicals_stored_correctly);
            SurveyImage.Append("chemstocors= " + headers[current_survey].chemicals_stored_correctly_s + "\n");
            add_YesNo_nonzero("sheavail=    ", headers[current_survey].are_sheets_available);
            SurveyImage.Append("sheavails=   " + headers[current_survey].are_sheets_available_s + "\n");
            add_YesNo_nonzero("abocheck=    ", headers[current_survey].area_above_been_checked);
            SurveyImage.Append("abochecks=   " + headers[current_survey].area_above_been_checked_s + "\n");
            add_YesNo_nonzero("obscheck=    ", headers[current_survey].obstructions_checked);
            SurveyImage.Append("obschecks=   " + headers[current_survey].obstructions_checked_s + "\n");
            add_YesNo_nonzero("lintelok=    ", headers[current_survey].lintel_ok);
            SurveyImage.Append("linteloks=   " + headers[current_survey].lintel_ok_s + "\n");
            add_YesNo_nonzero("ladsec=      ", headers[current_survey].ladders_secure);
            SurveyImage.Append("ladsecs=     " + headers[current_survey].ladders_secure_s + "\n");
            add_YesNo_nonzero("safhei=      ", headers[current_survey].safe_work_at_height);
            SurveyImage.Append("safheis=     " + headers[current_survey].safe_work_at_height_s + "\n");
            add_YesNo_nonzero("conlad=      ", headers[current_survey].condition_of_ladders);
            SurveyImage.Append("conlads=     " + headers[current_survey].condition_of_ladders_s + "\n");
            add_YesNo_nonzero("toolset=     ", headers[current_survey].tools_set_out_safely);
            SurveyImage.Append("toolsets=    " + headers[current_survey].tools_set_out_safely_s + "\n");
            add_YesNo_nonzero("fireex=      ", headers[current_survey].fire_extinguisher_on_van);
            SurveyImage.Append("fireexs=     " + headers[current_survey].fire_extinguisher_on_van_s + "\n");
            add_YesNo_nonzero("firstaid=    ", headers[current_survey].first_aid_kit_on_van);
            SurveyImage.Append("firstaids=   " + headers[current_survey].first_aid_kit_on_van_s + "\n");
            add_YesNo_nonzero("eletes=      ", headers[current_survey].electrical_equipment_tested);
            SurveyImage.Append("eletess=     " + headers[current_survey].electrical_equipment_tested_s + "\n");
            SurveyImage.Append("comments=    " + headers[current_survey].comments + "\n");
        }

        private void CreateSurvey()
        {
            SurveyImage = new StringBuilder(128000);

            if (headers[current_survey].b_mrk == true)
            {
                AddSpotCheck();
                job_type = "K";
            }
            else
            {
                CompileParts();

                AddHeader();
                AddGlass();
                AddGreen();
                AddUPVC();
                AddAlum();
                WriteTimber();
                WriteCons();
                WritePanels();
                WriteGarage();
                WriteComp();
                WriteLocking();
                AddBifold();

                if (headers[current_survey].type == "Complaint")
                    job_type = "C";
                else
                    job_type = "S";
            }

        }

        private void Add_YN(string name_field, bool state)
        {
            if (state)
                SurveyImage.Append(name_field + "Y\n");
            else
                SurveyImage.Append(name_field + "N\n");
        }

        private void add_YN_nonzero(string name_field, int state)
        {
            switch (state)
            {
                case 1: SurveyImage.Append(name_field + "Y\n"); break;
                case 2: SurveyImage.Append(name_field + "N\n"); break;
            }
        }

        private void add_YesNo_nonzero(string name_field, int state) // 0=Don't send, 1=Yes, 2=No
        {
            switch (state)
            {
                case 1: SurveyImage.Append(name_field + "Yes\n"); break;
                case 2: SurveyImage.Append(name_field + "No\n"); break;
            }
        }

        private void add_NoYes(string name_field, int state) // 0=No, 1=Yes
        {
            switch (state)
            {
                case 0: SurveyImage.Append(name_field + "No\n"); break;
                case 1: SurveyImage.Append(name_field + "Yes\n"); break;
            }
        }

        private void add_via_button_list(string name_field, int state, List<String> button_list)
        {
            if (state > 0 && state < button_list.Count)
                SurveyImage.Append(name_field + button_list [state] + "\n");
        }

        private string YesNoBool(bool yorn)
        {
            if (yorn == true)
                return "Yes";
            else
                return "No";
        }

        private void add_repair_or_replace(string name_field, bool bRepair)
        {
            if (bRepair)
                SurveyImage.Append(name_field + "Repair\n");
            else
                SurveyImage.Append(name_field + "Replace\n");
        }

        private string GetLockType(int iType)
        {
            switch (iType)
            {
                case 1: return "Deadlock";
                case 2: return "Hook";
                case 3: return "Latch";
                case 4: return "Cam";
                case 5: return "Mushroom";
                default: return "";
            }
        }

        private void add_repudiate_info(string cause_of_damage)
        {
            if ((cause_of_damage == "Wear + Tear" || cause_of_damage == "Bad Workmanship") && !headers[current_survey].sn_name.Contains("MA Assist Ltd"))
            {
                if (headers[current_survey].i_spare2 == 0)
                {
                    SurveyImage.Append("repudiate=   No\n");
                    SurveyImage.Append("wt_explain=  " + headers[current_survey].s_spare1 + "\n");
                    SurveyImage.Append("wt_otherinfo=" + headers[current_survey].s_spare2 + "\n");
                    SurveyImage.Append("norepreas=   " + headers[current_survey].s_spare3 + "\n");
                }
                else
                {
                    SurveyImage.Append("repudiate=   Yes\n");
                    SurveyImage.Append("wt_explain=  " + headers[current_survey].s_spare1 + "\n");
                    SurveyImage.Append("wt_otherinfo=" + headers[current_survey].s_spare2 + "\n");
                }
            }
        }

       private void CompileParts()
        {
            summaries_compiled = "";
            int current_summary_num = 0;

            parts_compiled = "";
            foreach (var item in App.data.GetGlasssByContract(headers[current_survey].udi_cont, true))
            {
                parts_compiled = parts_compiled + "Glass - \n" + item.item_number.ToString();
                parts_compiled = parts_compiled + item.parts_to_order + "\n";
                if (item.parent_item == 0)
                {
                    summaries_compiled += ". " + (++current_summary_num).ToString() + ". " + item.long_comments + ". ";
                }
            }
            foreach (var item in App.data.GetGreensByContract(headers[current_survey].udi_cont, true))
            {
                parts_compiled = parts_compiled + "Greenhouse - \n" + item.item_number.ToString();
                parts_compiled = parts_compiled + item.parts_to_order + "\n";
                summaries_compiled += ". " + (++current_summary_num).ToString() + ". " + item.summary + ". ";
            }
            foreach (var item in App.data.GetUPVCsByContract(headers[current_survey].udi_cont, true))
            {
                parts_compiled = parts_compiled + "UPVC - \n" + item.item_number.ToString();
                parts_compiled = parts_compiled + item.parts_to_order + "\n";
                summaries_compiled += ". " + (++current_summary_num).ToString() + ". " + item.long_comments + ". ";
            }
            foreach (var item in App.data.GetAlumsByContract(headers[current_survey].udi_cont, true))
            {
                parts_compiled = parts_compiled + "Alum - \n" + item.item_number.ToString();
                parts_compiled = parts_compiled + item.parts_to_order + "\n";
                summaries_compiled += ". " + (++current_summary_num).ToString() + ". " + item.long_comments + ". ";
            }
            foreach (var item in App.data.GetTimberByContract(headers[current_survey].udi_cont, true))
            {
                parts_compiled = parts_compiled + "Timber - \n" + item.item_number.ToString();
                parts_compiled = parts_compiled + item.parts_to_order + "\n";
                summaries_compiled += ". " + (++current_summary_num).ToString() + ". " + item.long_timber_comments + ". ";
            }
            foreach (var item in App.data.GetConssByContract(headers[current_survey].udi_cont, true))
            {
                parts_compiled = parts_compiled + "Cons - \n" + item.item_number.ToString();
                parts_compiled = parts_compiled + item.parts_to_order + "\n";
                summaries_compiled += ". " + (++current_summary_num).ToString() + ". " + item.long_comments + ". ";
            }
            foreach (var item in App.data.GetPanelsByContract(headers[current_survey].udi_cont, true))
            {
                parts_compiled = parts_compiled + "Panel - \n" + item.item_number.ToString();
                parts_compiled = parts_compiled + item.parts_to_order + "\n";
                if (item.upvc_item_number==0 && item.alum_item_number==0)
                {
                    summaries_compiled += ". " + (++current_summary_num).ToString() + ". " + item.long_sptext + ". ";
                }
            }
            foreach (var item in App.data.GetGaragesByContract(headers[current_survey].udi_cont, true))
            {
                parts_compiled = parts_compiled + "Garage - \n" + item.item_number.ToString();
                parts_compiled = parts_compiled + item.parts_to_order + "\n";
                summaries_compiled += ". " + (++current_summary_num).ToString() + ". " + item.long_comments + ". ";
            }
            foreach (var item in App.data.GetCompByContract(headers[current_survey].udi_cont, true))
            {
                parts_compiled = parts_compiled + "Composite - \n" + item.item_number.ToString();
                parts_compiled = parts_compiled + item.parts_to_order + "\n";
                summaries_compiled += ". " + (++current_summary_num).ToString() + ". " + item.comments + ". ";
            }
            foreach (var item in App.data.GetBifoldsByContract(headers[current_survey].udi_cont, true))
            {
                parts_compiled = parts_compiled + "Bifolding - \n" + item.item_number.ToString();
                parts_compiled = parts_compiled + item.parts_to_order + "\n";
                summaries_compiled += ". " + (++current_summary_num).ToString() + ". " + item.comments + ". ";
            }
        }
        private void AddHeader()
        {
            Header rec = headers[current_survey];
            SurveyImage.Append("**HEADER**\n");
            SurveyImage.Append("udi_cont=    " + rec.udi_cont + "\n");
            SurveyImage.Append("uc_name=     " + rec.uc_name + "\n");
            SurveyImage.Append("uc_postcod=  " + rec.uc_postcode + "\n");
            SurveyImage.Append("udi_start=   " + rec.udi_start.ToString().Substring(11, 5) + "\n");
            SurveyImage.Append("udi_fin=     " + rec.udi_fin.ToString().Substring(11, 5) + "\n");
            SurveyImage.Append("si_done=     " + YesNoBool(rec.si_done) + "\n");
            SurveyImage.Append("uc_add1=     " + rec.add_long.ToString() + "\n");
            SurveyImage.Append("uc_add2=     " + rec.uc_add2.ToString() + "\n");
            SurveyImage.Append("uc_add3=     " + rec.uc_add3.ToString() + "\n");
            SurveyImage.Append("uc_h_phone=  " + rec.uc_h_phone.ToString() + "\n");
            SurveyImage.Append("udi_date=    " + rec.udi_date.ToString().Substring(0, 6) + rec.udi_date.ToString().Substring(8, 2) + "\n");
            SurveyImage.Append("sn_name=     " + rec.sn_name.ToString() + "\n");
            SurveyImage.Append("uc_laname=   " + rec.uc_laname.ToString() + "\n");
            SurveyImage.Append("uc_goahead=  " + rec.goaheadstr.ToString() + "\n");
            SurveyImage.Append("uc_inceden=  " + rec.uc_inceden.ToString().Substring(0, 6) + rec.uc_inceden.ToString().Substring(8, 2) + "\n");
            SurveyImage.Append("uc_descr==    \n" + rec.uc_desc.ToString() + "\n");

            switch (rec.udi_tlight)
            {
                case 2: SurveyImage.Append("udi_tlight=  3" + "\n"); break;
                case 1: SurveyImage.Append("udi_tlight=  2" + "\n"); break;
                case 0: SurveyImage.Append("udi_tlight=  1" + "\n"); break;
            }

            SurveyImage.Append("sAutoTimeA=  " + rec.new_sspare4 + "\n");
            SurveyImage.Append("uc_excess=   " + rec.uc_excess.ToString() + "\n");
            SurveyImage.Append("si_numitem=  " + rec.si_numitem.ToString() + "\n");
            SurveyImage.Append("udi_jobtex=  " + rec.udi_jobtext.ToString() + "\n");
            SurveyImage.Append("udi_inst=    " + rec.udi_inst.ToString() + "\n");
            SurveyImage.Append("udi_staff=   " + rec.udi_staff.ToString() + "\n");
            SurveyImage.Append("cardcheq=    " + rec.card_cheq + "\n");
            SurveyImage.Append("expiry=      " + rec.expiry.ToString().Substring(0, 6) + rec.expiry.ToString().Substring(8, 2) + "\n");
            SurveyImage.Append("issueno=     " + rec.issue_no.ToString() + "\n");

            if (rec.bExcessCollected == 1)
                SurveyImage.Append("paych=       " + rec.mop + "\n");

            if (rec.survey_complete == 2)
                rec.rep_text = rec.rep_text + ".  " + rec.reason_not_complete;

            rec.rep_text = rec.rep_text.Replace("\n", " ");

            SurveyImage.Append("reptext1==   \n" + rec.rep_text.ToString() + "\n" + parts_compiled);
            SurveyImage.Append("acctext1==   \n" + rec.acc_text.ToString() + "\n");
            SurveyImage.Append("instruct==   \n" + rec.udi_inst.ToString() + "\n");
            SurveyImage.Append("summtext1==  \n" + rec.summ_text.ToString() + summaries_compiled + "\n");
            SurveyImage.Append("codetext1==  \n" + rec.code_text.ToString() + "\n");
            add_YesNo_nonzero("photo=       ", rec.photo);
            add_YesNo_nonzero("booked1=     ", rec.booked);
            add_YesNo_nonzero("easypark=    ", rec.easy_park);
            add_YesNo_nonzero("accesrear=   ", rec.access_rear);
            add_YesNo_nonzero("alarmcont=   ", rec.alarm_cont);
            add_YesNo_nonzero("ladderreq=   ", rec.ladder_req);
            add_YesNo_nonzero("heightres=   ", rec.height_res);
            add_YesNo_nonzero("obswires=    ", rec.obs_wires);
            add_YesNo_nonzero("sandcemen=   ", rec.sand_cemen);
            add_YesNo_nonzero("plaster=     ", rec.plaster);
            add_YesNo_nonzero("doorbell=    ", rec.doorbell);
            add_YesNo_nonzero("loosebric=   ", rec.loose_brick);
            add_YesNo_nonzero("generreq=    ", rec.genreq);
            add_YesNo_nonzero("architreq=   ", rec.architreq);
            add_YesNo_nonzero("acroreq=     ", rec.acroreq);
            SurveyImage.Append("njs=         " + rec.njs + "\n");
            SurveyImage.Append("nsn=         " + rec.nsn + "\n");
            SurveyImage.Append("PocketPC=    Yes\n");

            if (rec.work_at_height == 1)
            {
                add_YesNo_nonzero("workinside=  ", rec.bWorkInside);
                SurveyImage.Append("instheight=  " + rec.inst_height + "\n");
                SurveyImage.Append("bothhands=   \n");
                SurveyImage.Append("gndsurface== \n" + rec.ground_surface + "\n");
                SurveyImage.Append("equiptype=   " + rec.type_of_equipment + "\n");
            }

            if (rec.requiring_load_bearing_jacks == true)
                SurveyImage.Append("riskndanger==\nLOAD BEARING BAY JACKS MUST BE USED! - " + rec.type_of_equipment + "\n");
            else
                SurveyImage.Append("riskndanger==\n" + rec.type_of_equipment + "\n");

            SurveyImage.Append("LooseBrick=  " + rec.loose_brick_text + "\n");
            SurveyImage.Append("ObstWires=   " + rec.obs_wires_text + "\n");
            SurveyImage.Append("stimearr=    " + rec.stimea + "\n");
            SurveyImage.Append("sTimeLeft=   " + rec.new_sspare1 + "\n");

            if (rec.bExcessCollected == 2)
                SurveyImage.Append("ExcessReasNo=" + rec.reason_excess_not_collected + "\n");
            else
                SurveyImage.Append("ExcessReasNo=\n");

            add_YesNo_nonzero("NoLadders=   ", rec.no_ladders);
            SurveyImage.Append("uc_w_phone=  " + rec.uc_h_phone2 + "\n");
            SurveyImage.Append("uc_m_phone=  " + rec.uc_h_phone3 + "\n");
            add_YesNo_nonzero("AboveRoof=   ", rec.items_above_roof);
            add_YesNo_nonzero("AsbestosVis= ", rec.asbestos_visible);
            SurveyImage.Append("AsbestosExp= " + rec.asvizex + "\n");
            add_YesNo_nonzero("ShopFrontWk= ", rec.shop_front_work);
            SurveyImage.Append("InfInsurer=  " + rec.messagetoinsurer + "\n");
            add_YesNo_nonzero("Subcontract= ", rec.b_subcontract);

            if (rec.b_subcontract == 1)
                SurveyImage.Append("SubContText= " + rec.subcontracttext + "\n");
            else
                SurveyImage.Append("SubContText= \n");

            if (rec.truecommconf == true)
                SurveyImage.Append("TrueComm=    Yes\n");
            else
                SurveyImage.Append("TrueComm=    No\n");

            if (rec.booked == 2)
                SurveyImage.Append("ReasNoBD1=   " + rec.reason_not_booked_in + "\n");
            else
                SurveyImage.Append("ReasNoBD1=   \n");

            SurveyImage.Append("EstTimOnSite=" + rec.time_to_complete + "\n");
            add_YesNo_nonzero("acrosboy=    ", rec.acrosboy);

            if (rec.parking_at_rear == 1)
                SurveyImage.Append("RearPark=    Yes\n");
            else
                SurveyImage.Append("RearPark=    No\n");

            if (rec.work_on_public_footpath == 1)
                SurveyImage.Append("Footpath=    Yes\n");
            else
                SurveyImage.Append("Footpath=    No\n");

            SurveyImage.Append("Barriers=    " + rec.fbunfinother + "\n");
            add_YesNo_nonzero("pc_scaff=    ", rec.no_of_fitters);

            switch (rec.lintel_present)
            {
                case 0: SurveyImage.Append("lint_pres=   \n"); break;
                case 1: SurveyImage.Append("lint_pres=   na\n"); break;
                case 2: SurveyImage.Append("lint_pres=   Yes\n"); break;
                case 3: SurveyImage.Append("lint_pres=   No\n"); break;
            }

            SurveyImage.Append("lint_pres_ex=" + rec.lintel_present_text + "\n");
            SurveyImage.Append("jobgrade=    " + rec.job_grade + "\n");
        }

        private void AddAlum()
        {
            foreach (var aluminium_item in App.data.GetAlumsByContract(headers[current_survey].udi_cont))
            {
                if (aluminium_item.isComplete == 1)
                {
                    SurveyImage.Append("**Alu**\n");
                    SurveyImage.Append("contract=    " + aluminium_item.udi_cont + "\n");
                    SurveyImage.Append("itemcode=    " + aluminium_item.item_number + "\n");
                    SurveyImage.Append("lead_comment=" + aluminium_item.lead_comments.Replace("\n", " ") + "\n");
                    add_YesNo_nonzero("urcdosdam=   ", aluminium_item.cosmetic_damage);
                    SurveyImage.Append("uraddlock=   " + aluminium_item.additional_locks + "\n");
                    add_via_button_list("urgasket=    ", aluminium_item.gaskets, SurveyFitterButtonLists.shared_gasket_button_list);
                    SurveyImage.Append("urgasedit=   " + aluminium_item.gaskets_text + "\n");
                    add_YesNo_nonzero("urhandles=   ", aluminium_item.handles_req);
                    SurveyImage.Append("urhandedit=  " + aluminium_item.handles_text + "\n");
                    SurveyImage.Append("parts_order= " + aluminium_item.parts_to_order.Replace("\n", " ") + "\n");
                    add_YesNo_nonzero("urreppan=    ", aluminium_item.replace_panel);
                    SurveyImage.Append("adamch=      " + aluminium_item.cause_of_damage + "\n");
                    SurveyImage.Append("aitemch=     " + aluminium_item.type + "\n");
                    add_via_button_list("asecch=      ", aluminium_item.section_type, SurveyFitterButtonLists.aluminium_section_type_list);
                    add_YesNo_nonzero("atimbsub=    ", aluminium_item.new_timber_sub_frame);
                    SurveyImage.Append("asubdedit=   " + aluminium_item.sub_frame_depth + "\n");
                    SurveyImage.Append("atimszwedit= " + aluminium_item.item_frame_width + "\n");
                    add_repair_or_replace("arrch=       ", aluminium_item.bRepair);
                    add_YesNo_nonzero("col_copy=    ", aluminium_item.collect_and_copy);
                    add_via_button_list("temp_door=   ", aluminium_item.temporary, SurveyFitterButtonLists.shared_temporary_button_list);
                    add_via_button_list("Glazed=      ", aluminium_item.glazed, SurveyFitterButtonLists.aluminium_glazed_button_list);
                    add_via_button_list("BeadType=    ", aluminium_item.bead_type, SurveyFitterButtonLists.aluminium_bead_type_list);
                    SurveyImage.Append("OSWidth=     " + aluminium_item.outer_section_width + "\n");
                    SurveyImage.Append("OSHeight=    " + aluminium_item.outer_section_height + "\n");
                    SurveyImage.Append("atimszhedit= " + aluminium_item.item_frame_height + "\n");
                    SurveyImage.Append("abrkszwedit= " + aluminium_item.brick_width + "\n");
                    SurveyImage.Append("abrkszhedit= " + aluminium_item.brick_height + "\n");
                    SurveyImage.Append("aintwedit=   " + aluminium_item.internal_width + "\n");
                    SurveyImage.Append("ainthedit=   " + aluminium_item.internal_height + "\n");
                    add_YesNo_nonzero("acill=       ", aluminium_item.cill);
                    add_YesNo_nonzero("adrip=       ", aluminium_item.drip);
                    add_via_button_list("anvent=      ", aluminium_item.night_vent, SurveyFitterButtonLists.aluminium_night_vent_list);
                    SurveyImage.Append("amidtype=    " + aluminium_item.midrail_type + "\n");
                    SurveyImage.Append("aitemcol=    " + aluminium_item.item_color + "\n");
                    SurveyImage.Append("alock=       " + aluminium_item.locking_type + "\n");
                    SurveyImage.Append("aletter=     " + aluminium_item.letter_box + "\n");
                    add_via_button_list("aopens=      ", aluminium_item.opens, SurveyFitterButtonLists.shared_in_out_button_list);
                    SurveyImage.Append("ahcolour=    " + aluminium_item.handle_color + "\n");
                    SurveyImage.Append("aspacthic=   " + aluminium_item.spacer_thickness + "\n");
                    SurveyImage.Append("aspaccolo=   " + aluminium_item.spacer_color + "\n");
                    SurveyImage.Append("aglatype=    " + aluminium_item.glass_type + "\n");
                    SurveyImage.Append("aglapatt=    " + aluminium_item.glass_pattern + "\n");
                    SurveyImage.Append("ASPEGLAS=    " + aluminium_item.special_glass + "\n");
                    SurveyImage.Append("ansfc=       " + aluminium_item.sub_frame_color + "\n");
                    add_via_button_list("anfty=       ", aluminium_item.frame_type, SurveyFitterButtonLists.aluminium_frame_type_list);
                    if (aluminium_item.is_a_flat == 1)
                        SurveyImage.Append("asptext==    \n" + aluminium_item.long_comments.Replace("\n", " ") + " THUMB TURN LOCKS MUST BE USED\n");
                    else
                        SurveyImage.Append("asptext==    \n" + aluminium_item.long_comments.Replace("\n", " ") + "\n");
                    if (aluminium_item.special_glass != "None")
                    {
                        SurveyImage.Append("sizeA=       " + aluminium_item.lead_sizeA + "\n");
                        SurveyImage.Append("sizeB=       " + aluminium_item.lead_sizeB + "\n");
                        SurveyImage.Append("sizeC=       " + aluminium_item.lead_sizeC + "\n");
                        SurveyImage.Append("sizeD=       " + aluminium_item.lead_sizeD + "\n");
                        SurveyImage.Append("LeadWidth=   " + aluminium_item.lead_CWidthf.ToString() + "\n");
                        SurveyImage.Append("LeadHeight=  " + aluminium_item.lead_CHeightf.ToString() + "\n");
                    }

                    add_via_button_list("AntiRattle=  ", aluminium_item.lead_anti_rattle, georgian_bar_logic.anti_rattle);

                    if (aluminium_item.special_glass == "Georgian Bar")
                        SurveyImage.Append("GeorgBar=    " + aluminium_item.lead_thickness + "\n");
                    else if (aluminium_item.special_glass != "None")
                        SurveyImage.Append("LeadThick=   " + aluminium_item.lead_thickness + "\n");

                    SurveyImage.Append("LeadSingDoub=" + aluminium_item.lead_sod + "\n");

                    if (aluminium_item.special_glass != "Georgian Bar" && aluminium_item.special_glass != "None")
                        SurveyImage.Append("TypeOfLead=  " + aluminium_item.lead_type + "\n");

                    SurveyImage.Append("LetBoxPos=   " + aluminium_item.letter_box_pos + "\n");
                    SurveyImage.Append("PetFlap=     " + aluminium_item.pet_flap + "\n");
                    SurveyImage.Append("PetType=     " + aluminium_item.pet_type + "\n");

                    if (aluminium_item.bNewLockingMech == 1)
                    {
                        if (aluminium_item.type == "Door" ||
                            aluminium_item.type == "French Doors" ||
                            aluminium_item.type == "Combi Frame" ||
                            aluminium_item.type == "Porch" ||
                            aluminium_item.type == "Window" ||
                            aluminium_item.type == "Bay Window")
                        {
                            SurveyImage.Append("NoOfLocks=   " + aluminium_item.l_num.ToString() + "\n");
                            SurveyImage.Append("LockDist1=   " + aluminium_item.l_size1 + "\n");
                            SurveyImage.Append("LockDist2=   " + aluminium_item.l_size2 + "\n");
                            SurveyImage.Append("LockDistA=   " + aluminium_item.l_sizeA + "\n");
                            SurveyImage.Append("LockDistB=   " + aluminium_item.l_sizeB + "\n");
                            SurveyImage.Append("LockDistC=   " + aluminium_item.l_sizeC + "\n");
                            SurveyImage.Append("LockDistD=   " + aluminium_item.l_sizeD + "\n");
                            SurveyImage.Append("LockDistE=   " + aluminium_item.l_sizeE + "\n");
                            SurveyImage.Append("LockDistF=   " + aluminium_item.l_sizeF + "\n");
                            SurveyImage.Append("LockDistG=   " + aluminium_item.l_sizeG + "\n");
                            SurveyImage.Append("LockType1=   " + GetLockType(aluminium_item.l_itype1) + "\n");
                            SurveyImage.Append("LockType2=   " + GetLockType(aluminium_item.l_itype2) + "\n");
                            SurveyImage.Append("LockType3=   " + GetLockType(aluminium_item.l_itype3) + "\n");
                            SurveyImage.Append("LockType4=   " + GetLockType(aluminium_item.l_itype4) + "\n");
                            SurveyImage.Append("LockType5=   " + GetLockType(aluminium_item.l_itype5) + "\n");
                            SurveyImage.Append("LockType6=   " + GetLockType(aluminium_item.l_itype6) + "\n");
                            SurveyImage.Append("LockType7=   " + GetLockType(aluminium_item.l_itype7) + "\n");
                            SurveyImage.Append("PicDist1=    " + (aluminium_item.l_fpos1 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist2=    " + (aluminium_item.l_fpos2 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist3=    " + (aluminium_item.l_fpos3 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist4=    " + (aluminium_item.l_fpos4 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist5=    " + (aluminium_item.l_fpos5 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist6=    " + (aluminium_item.l_fpos6 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist7=    " + (aluminium_item.l_fpos7 / 2.0).ToString() + "\n");
                            SurveyImage.Append("MechDist=    " + (aluminium_item.lock_position / 2.0).ToString() + "\n");
                            add_NoYes ("LeftBolt=    ", aluminium_item.left_bolt);
                            add_NoYes ("RightBolt=   ", aluminium_item.right_bolt);
                            add_via_button_list("Gearbox=     ", aluminium_item.GearBox, window_lock_logic.gearbox_list);
                            SurveyImage.Append("LockMake=    " + aluminium_item.lock_make + "\n");
                            SurveyImage.Append("LockCodes=   " + aluminium_item.lock_codes + "\n");
                        }
                    }

                    SurveyImage.Append("mrheight=    " + aluminium_item.midrail_height + "\n");
                    SurveyImage.Append("qdocl=       " + aluminium_item.docl + "\n");
                    SurveyImage.Append("ReplacReas=  " + aluminium_item.replace_reason + "\n");
                    SurveyImage.Append("WhyNoRepair= " + aluminium_item.replace_explain + "\n");
                    SurveyImage.Append("RoomLocation=" + aluminium_item.room_location + "\n");
                    add_via_button_list("LPHandles=   ", aluminium_item.LPHandles, SurveyFitterButtonLists.shared_handle_operation_list);
                    //add_via_button_list("SlidePos=    ", aluminium_item.slide_position, SurveyFitterButtonLists.shared_inside_outside_button_list);
                    SurveyImage.Append("Threshold=   " + aluminium_item.threshold_type + "\n");

                    if (aluminium_item.bDifferentFromOriginal == true)
                    {
                        SurveyImage.Append("origdiff=    Y\n");
                        SurveyImage.Append("changedto=   " + aluminium_item.ChangeItemTo + "\n");
                    }
                    else
                        SurveyImage.Append("origdiff=    N\n");

                    SurveyImage.Append("PrintName=   " + aluminium_item.print_name + "\n");

                    if (aluminium_item.bFencer == true)
                    {
                        if (aluminium_item.FecerRating == "UNKNOWN")
                            SurveyImage.Append("FencerRate= C\n");
                        else
                            SurveyImage.Append("FencerRate= " + aluminium_item.FecerRating + "\n");
                    }
                    else
                        SurveyImage.Append("FencerRate= NO\n");

                    add_via_button_list("petmagnet=   ", aluminium_item.pet_magnetic, MartControls.pet_flap_logic.magnetic_list);
                    add_YesNo_nonzero("consubfrme=  ", aluminium_item.cill_on_subframe);
                    add_via_button_list("consubfrty=  ", aluminium_item.cill_type, SurveyFitterButtonLists.aluminium_cill_type_list);
                    add_repudiate_info(aluminium_item.cause_of_damage);
                    add_YN_nonzero("isflat=      ", aluminium_item.is_a_flat);
                    SurveyImage.Append("pointenter=  " + aluminium_item.point_of_entry + "\n");
                    add_YN_nonzero("waslocked=   ", aluminium_item.was_it_locked);
                    SurveyImage.Append("lockrequ=    " + aluminium_item.type_of_lockng_system_required + "\n");
                    SurveyImage.Append("bbspacerwid= " + aluminium_item.back_to_back_spacer_width + "\n");
                    SurveyImage.Append("bboveralwid= " + aluminium_item.back_to_back_spacer_height + "\n");
                    add_YN_nonzero("replaceGlass=", aluminium_item.replace_glass);
                }
            }
        }

        private void AddBifold()
        {
            foreach (var bifold_item in App.data.GetBifoldsByContract(headers[current_survey].udi_cont))
            {
                if (bifold_item.isComplete == 1)
                {
                    SurveyImage.Append("**Bifold**\n");
                    SurveyImage.Append("contract=    " + bifold_item.udi_cont + "\n");
                    SurveyImage.Append("itemcode=    " + bifold_item.item_number.ToString() + "\n");
                    SurveyImage.Append("cosdam=      " + bifold_item.cause_of_damage + "\n");
                    SurveyImage.Append("doortype=    " + bifold_item.door_type + "\n");
                    SurveyImage.Append("hardware=    " + bifold_item.hardware + "\n");
                    SurveyImage.Append("colint=      " + bifold_item.color_internal + "\n");
                    SurveyImage.Append("colext=      " + bifold_item.color_external + "\n");
                    SurveyImage.Append("cilltype=    " + bifold_item.threshold_type + "\n");
                    SurveyImage.Append("numdoors=    " + bifold_item.number_of_doors.ToString() + "\n");
                    SurveyImage.Append("numdoorstxt= " + bifold_item.number_of_doors_text + "\n");
                    SurveyImage.Append("glazeopts=   " + bifold_item.glazing_options + "\n");
                    SurveyImage.Append("owidth=      " + bifold_item.overall_width + "\n");
                    SurveyImage.Append("oheight=     " + bifold_item.overall_height + "\n");
                    SurveyImage.Append("iwidth=      " + bifold_item.internal_width + "\n");
                    SurveyImage.Append("iheight=     " + bifold_item.internal_height + "\n");
                    SurveyImage.Append("doorcolor=   " + bifold_item.colour_of_doors + "\n");
                    SurveyImage.Append("intcolor=    " + bifold_item.internal_door_colour + "\n");
                    SurveyImage.Append("handcolor=   " + bifold_item.handle_colour + "\n");
                    SurveyImage.Append("cillreq=     " + bifold_item.cill_type + "\n");
                    SurveyImage.Append("knockon=     " + bifold_item.knock_on + "\n");
                    SurveyImage.Append("comments=    " + bifold_item.comments.Replace("\n", " ") + "\n");
                    SurveyImage.Append("parts_order= " + bifold_item.parts_to_order.Replace("\n", " ") + "\n");
                    add_via_button_list("opens=       ", bifold_item.opens, SurveyFitterButtonLists.shared_in_out_button_list);
                    add_YN_nonzero("tvents=      ", bifold_item.trickle_vents);
                    add_repudiate_info(bifold_item.cause_of_damage);
                    add_repair_or_replace("reppepl=     ", bifold_item.bRepair);
                    add_YN_nonzero("replaceGlass=", bifold_item.replace_glass);
                    SurveyImage.Append("reasNoRepair=" + bifold_item.reason_not_repaired + "\n");
                    add_YN_nonzero("handles_req= ", bifold_item.handles_req);
                    SurveyImage.Append("handles_text=" + bifold_item.handles_text + "\n");
                    Add_YN("fensa=       ", bifold_item.fensa);
                    SurveyImage.Append ("WER_rating=  " + bifold_item.WER_rating + "\n");
                    add_via_button_list("gaskets=     ", bifold_item.gaskets, SurveyFitterButtonLists.shared_gasket_button_list);
                    SurveyImage.Append ("gaskets_text=" + bifold_item.gaskets_text + "\n");
                    add_YesNo_nonzero  ("addons=      ", bifold_item.addons);
                    SurveyImage.Append ("addon_width= " + bifold_item.addon_width + "\n");
                    SurveyImage.Append ("addon_height=" + bifold_item.addon_height + "\n");
                }
            }
        }

        private void WriteComp()
        {
            foreach (var composite_item in App.data.GetCompByContract(headers[current_survey].udi_cont))
            {
                if (composite_item.isComplete == 1)
                {
                    SurveyImage.Append("**Comp**\n");
                    SurveyImage.Append("contract=    " + composite_item.udi_cont + "\n");
                    SurveyImage.Append("itemcode=    " + composite_item.item_number + "\n");
                    SurveyImage.Append("CosDam=      " + composite_item.cause_of_damage + "\n");
                    SurveyImage.Append("doormake=    " + composite_item.door_make + "\n");
                    SurveyImage.Append("lead_comment=" + composite_item.lead_comments.Replace("\n", " ") + "\n");
                    SurveyImage.Append("parts_order= " + composite_item.parts_to_order.Replace("\n", " ") + "\n");
                    add_via_button_list("lock=        ", composite_item.is_lock, SurveyFitterButtonLists.composite_is_lock_button_list);
                    add_via_button_list("opens=       ", composite_item.opens, SurveyFitterButtonLists.shared_in_out_button_list);
                    SurveyImage.Append("tvents=      " + composite_item.trickle_vents + "\n");
                    SurveyImage.Append("fcolins=     " + composite_item.frame_colour_inside + "\n");
                    SurveyImage.Append("fcolouts=    " + composite_item.frame_colour_outside + "\n");
                    SurveyImage.Append("dcolins=     " + composite_item.door_colour_inside + "\n");
                    SurveyImage.Append("dcolouts=    " + composite_item.door_colour_outside + "\n");
                    SurveyImage.Append("doordes=     " + composite_item.door_design + "\n");
                    SurveyImage.Append("glassdes=    " + composite_item.glass_design + "\n");
                    SurveyImage.Append("intwidth=    " + composite_item.internal_width + "\n");
                    SurveyImage.Append("intheight=   " + composite_item.internal_height + "\n");

                    SurveyImage.Append("brwidth=     " + composite_item.brick_width + "\n");
                    SurveyImage.Append("brheight=    " + composite_item.brick_height + "\n");
                    add_via_button_list("lphandles=   ", composite_item.lever_pad_handles, SurveyFitterButtonLists.shared_handle_operation_list);

                    add_YesNo_nonzero("addons=      ", composite_item.addons);
                    if (composite_item.addons == 1)
                    {
                        SurveyImage.Append("addonsw=     " + composite_item.addons_width + "\n");
                        SurveyImage.Append("addonsh=     " + composite_item.addons_height + "\n");
                    }

                    add_via_button_list("HingedOn=    ", composite_item.hinged_on, SurveyFitterButtonLists.composite_hinged_on_button_list);
                    SurveyImage.Append("gpattern=    " + composite_item.glass_pattern + "\n");
                    SurveyImage.Append("gtype=       " + composite_item.glass_type + "\n");
                    SurveyImage.Append("spthick=     " + composite_item.spacer_thickness + "\n");
                    SurveyImage.Append("spcolour=    " + composite_item.spacer_colour + "\n");

                    if (composite_item.bDifferentFromOriginal == true)
                    {
                        SurveyImage.Append("origdiff=    Y\n");
                        SurveyImage.Append("changedto=   " + composite_item.ChangeItemTo + "\n");
                    }
                    else
                        SurveyImage.Append("origdiff=    N\n");

                    SurveyImage.Append("PrintName=   " + composite_item.print_name + "\n");
                    add_via_button_list("ptype=       ", composite_item.profile_type, SurveyFitterButtonLists.shortened_profile_type_button_list);
                    SurveyImage.Append("rlocation=   " + composite_item.room_location + "\n");
                    SurveyImage.Append("lockother=   " + composite_item.lock_other_text + "\n");
                    SurveyImage.Append("specialglass=" + composite_item.special_glass + "\n");
                    SurveyImage.Append("docl=        " + composite_item.docl + "\n");
                    SurveyImage.Append("letbox=      " + composite_item.letteredit + "\n");
                    SurveyImage.Append("letboxpos=   " + composite_item.letter_box_pos + "\n");
                    SurveyImage.Append("petflap=     " + composite_item.pet_flap + "\n");
                    SurveyImage.Append("pettype=     " + composite_item.pet_type + "\n");
                    add_via_button_list("petmagnet=   ", composite_item.pet_magnetic, MartControls.pet_flap_logic.magnetic_list);
                    add_via_button_list("glaze=       ", composite_item.glaze, SurveyFitterButtonLists.shared_internal_external_button_list);
                    SurveyImage.Append("singordouble=" + composite_item.lead_sod + "\n");
                    SurveyImage.Append("leadthick=   " + composite_item.lead_thickness + "\n");
                    SurveyImage.Append("typeoflead=  " + composite_item.lead_type + "\n");
                    add_via_button_list("antirattle=  ", composite_item.lead_anti_rattle, georgian_bar_logic.anti_rattle);
                    if (composite_item.is_a_flat == 1)
                        SurveyImage.Append("comments=    " + composite_item.comments.Replace("\n", " ") + " THUMB TURN LOCKS MUST BE USED\n");
                    else
                        SurveyImage.Append("comments=    " + composite_item.comments.Replace("\n", " ") + "\n");
                    SurveyImage.Append("LeadWidth=   " + composite_item.lead_CWidthf.ToString() + "\n");
                    SurveyImage.Append("LeadHeight=  " + composite_item.lead_CHeightf.ToString() + "\n");
                    SurveyImage.Append("cills=       " + composite_item.cills + "\n");
                    SurveyImage.Append("ttype=       " + composite_item.threshold_type + "\n");
                    SurveyImage.Append("hcolour=     " + composite_item.handle_colour + "\n");
                    SurveyImage.Append("DoorWood=    " + composite_item.door_wood + "\n");

                    if (SurveyFitterSharedLogic.reason_not_repaired_visible(composite_item.bRepair))
                        SurveyImage.Append("repl_reas=   " + composite_item.reason_not_repaired + "\n");

                    add_repudiate_info(composite_item.cause_of_damage);
                    add_YN_nonzero("isflat=      ", composite_item.is_a_flat);
                    SurveyImage.Append("pointenter=  " + composite_item.point_of_entry + "\n");
                    add_YN_nonzero("waslocked=   ", composite_item.was_it_locked);
                    SurveyImage.Append("lockrequ=    " + composite_item.type_of_lockng_system_required + "\n");
                    add_repair_or_replace("Reparepl=    ", composite_item.bRepair);
                    add_YN_nonzero("replaceGlass=", composite_item.replace_glass);
                    add_YN_nonzero("handles_req= ", composite_item.handles_req);
                    SurveyImage.Append("handles_text=" + composite_item.handles_text + "\n");
                    Add_YN("fensa=       ", composite_item.fensa);
                    SurveyImage.Append("WER_rating=  " + composite_item.WER_rating + "\n");
                    add_via_button_list("gaskets=     ", composite_item.gaskets, SurveyFitterButtonLists.shared_gasket_button_list);
                    SurveyImage.Append("gaskets_text=" + composite_item.gaskets_text + "\n");
                    add_YN_nonzero ("fire_door=   ", composite_item.fire_door);
                    add_YN_nonzero ("headdrip=    ", composite_item.head_drip);
                }
            }
        }

        private void WriteCons()
        {
            foreach (var conservatory_item in App.data.GetConssByContract(headers[current_survey].udi_cont))
            {
                if (conservatory_item.isComplete == 1)
                {
                    SurveyImage.Append("**Cons**\n");

                    SurveyImage.Append("contract=    " + conservatory_item.udi_cont + "\n");
                    SurveyImage.Append("itemcode=    " + conservatory_item.item_number + "\n");
                    SurveyImage.Append("cdamch=      " + conservatory_item.cause_of_damage + "\n");
                    SurveyImage.Append("ctyperoof=   " + conservatory_item.type + "\n");
                    SurveyImage.Append("parts_order= " + conservatory_item.parts_to_order.Replace("\n", " ") + "\n");
                    add_via_button_list("cmattype=    ", conservatory_item.material_type, SurveyFitterButtonLists.conservatory_material_type_list);
                    SurveyImage.Append("csizea=      " + conservatory_item.sizeA + "\n");
                    SurveyImage.Append("csizeb=      " + conservatory_item.sizeB + "\n");
                    SurveyImage.Append("csizec=      " + conservatory_item.sizeC + "\n");
                    SurveyImage.Append("csized=      " + conservatory_item.sizeD + "\n");
                    SurveyImage.Append("csizee=      " + conservatory_item.sizeE + "\n");
                    SurveyImage.Append("csizef=      " + conservatory_item.sizeF + "\n");
                    SurveyImage.Append("csizeg=      " + conservatory_item.sizeG + "\n");
                    SurveyImage.Append("cangle1=     " + conservatory_item.angle1 + "\n");
                    SurveyImage.Append("cangle2=     " + conservatory_item.angle2 + "\n");
                    SurveyImage.Append("cangle3=     " + conservatory_item.angle3 + "\n");
                    SurveyImage.Append("cangle4=     " + conservatory_item.angle4 + "\n");
                    SurveyImage.Append("cpitchhedi=  " + conservatory_item.pitch_height + "\n");
                    SurveyImage.Append("cpsecedit=   " + conservatory_item.profile_section_size + "\n");

                    int cur_sheet = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        int num_of = 0;
                        switch (i)
                        {
                            case 0: num_of = conservatory_item.roof_sheets_quantity_1; break;
                            case 1: num_of = conservatory_item.roof_sheets_quantity_2; break;
                            case 2: num_of = conservatory_item.roof_sheets_quantity_3; break;
                            case 3: num_of = conservatory_item.roof_sheets_quantity_4; break;
                            case 4: num_of = conservatory_item.roof_sheets_quantity_5; break;
                            case 5: num_of = conservatory_item.roof_sheets_quantity_6; break;
                            case 6: num_of = conservatory_item.roof_sheets_quantity_7; break;
                            case 7: num_of = conservatory_item.roof_sheets_quantity_8; break;
                            case 8: num_of = conservatory_item.roof_sheets_quantity_9; break;
                            case 9: num_of = conservatory_item.roof_sheets_quantity_10; break;
                        }
                        for (int j = 0; j < num_of; j++)
                        {
                            string w = "";
                            string h = "";

                            switch (i)
                            {
                                case 0: w = conservatory_item.sheet_width_1; h = conservatory_item.sheet_height_1; break;
                                case 1: w = conservatory_item.sheet_width_2; h = conservatory_item.sheet_height_2; break;
                                case 2: w = conservatory_item.sheet_width_3; h = conservatory_item.sheet_height_3; break;
                                case 3: w = conservatory_item.sheet_width_4; h = conservatory_item.sheet_height_4; break;
                                case 4: w = conservatory_item.sheet_width_5; h = conservatory_item.sheet_height_5; break;
                                case 5: w = conservatory_item.sheet_width_6; h = conservatory_item.sheet_height_6; break;
                                case 6: w = conservatory_item.sheet_width_7; h = conservatory_item.sheet_height_7; break;
                                case 7: w = conservatory_item.sheet_width_8; h = conservatory_item.sheet_height_8; break;
                                case 8: w = conservatory_item.sheet_width_9; h = conservatory_item.sheet_height_9; break;
                                case 9: w = conservatory_item.sheet_width_10; h = conservatory_item.sheet_height_10; break;
                            }

                            string wst = "cwid" + (cur_sheet + 1).ToString() + "edit=   " + w + "\n";
                            string hst = "chei" + (cur_sheet + 1).ToString() + "edit=   " + h + "\n";

                            SurveyImage.Append(wst);
                            SurveyImage.Append(hst);

                            cur_sheet = cur_sheet + 1;

                        }
                    }

                    SurveyImage.Append("crglazth=    " + conservatory_item.roof_glazing_thickness + "\n");
                    SurveyImage.Append("ccolour=     " + conservatory_item.color + "\n");
                    SurveyImage.Append("crcolour=    " + conservatory_item.roof_color + "\n");
                    add_YesNo_nonzero("cfirring=    ", conservatory_item.new_firrings_req);
                    add_YesNo_nonzero("cgutter=     ", conservatory_item.new_gutters_req);
                    SurveyImage.Append("csptext==    \n" + conservatory_item.long_comments.Replace("\n", " ") + "\n");
                    SurveyImage.Append("ridge_length=" + conservatory_item.ridge_length + "\n");
                    SurveyImage.Append("FluteSize=   " + conservatory_item.flute_size + "\n");

                    if (conservatory_item.bDifferentFromOriginal == true)
                    {
                        SurveyImage.Append("origdiff=    Y\n");
                        SurveyImage.Append("changedto=   " + conservatory_item.ChangeItemTo + "\n");
                    }
                    else
                        SurveyImage.Append("origdiff=    N\n");

                    SurveyImage.Append("PrintName=   " + conservatory_item.print_name + "\n");
                    SurveyImage.Append("WallPos=     " + conservatory_item.wall_pos + "\n");
                    SurveyImage.Append("PitchDegree= " + conservatory_item.pitch_degree + "\n");
                    add_YesNo_nonzero("bDrawOnly=   ", conservatory_item.bDrawingsOnly);
                    add_YesNo_nonzero("spars_line=  ", conservatory_item.spars_line_up);
                    add_repudiate_info(conservatory_item.cause_of_damage);
                    SurveyImage.Append("pointenter=  " + conservatory_item.point_of_entry + "\n");
                    add_YN_nonzero("waslocked=   ", conservatory_item.was_it_locked);
                    add_repair_or_replace("replacOrRepr=", conservatory_item.bRepair);
                    SurveyImage.Append("lockrequ=    " + conservatory_item.type_of_lockng_system_required + "\n");
                    add_YN_nonzero("replaceGlass=", conservatory_item.replace_glass);
                    SurveyImage.Append("reasNoRepair=" + conservatory_item.reason_not_repaired + "\n");
                    Add_YN("fensa=       ", conservatory_item.fensa);
                    SurveyImage.Append("WER_rating=  " + conservatory_item.WER_rating + "\n");
                    SurveyImage.Append("OverallLen=  " + conservatory_item.overall_length_of_sheet + "\n");
                }
            }
        }

        private void WriteGarage()
        {
            foreach (var garage_item in App.data.GetGaragesByContract(headers[current_survey].udi_cont))
            {
                if (garage_item.isComplete == 1)
                {
                    SurveyImage.Append("**Garage**\n");
                    SurveyImage.Append("contract=    " + garage_item.udi_cont + "\n");
                    SurveyImage.Append("itemcode=    " + garage_item.item_number + "\n");
                    SurveyImage.Append("edamedit=    " + garage_item.cause_of_damage + "\n");
                    add_via_button_list("efixtype=    ", garage_item.frame_fix_type, SurveyFitterButtonLists.garage_frame_fix_type_list);
                    add_via_button_list("eintimsub=   ", garage_item.door_fits_into, SurveyFitterButtonLists.garage_subframe_colour_list);

                    if (garage_item.door_fits_into != 2)
                        add_via_button_list("enewtimsub=  ", garage_item.new_subframe_req, SurveyFitterButtonLists.garage_timber_subframe_list);
                    else
                        add_via_button_list("enewtimsub=  ", garage_item.new_subframe_req, SurveyFitterButtonLists.garage_metal_subframe_list);

                    SurveyImage.Append("eactwedit=   " + garage_item.actual_door_width + "\n");
                    SurveyImage.Append("eacthedit=   " + garage_item.actual_door_height + "\n");
                    SurveyImage.Append("ecolour=     " + garage_item.color + "\n");
                    SurveyImage.Append("efinish=     " + garage_item.finish + "\n");
                    SurveyImage.Append("eopentype=   " + garage_item.opening_type + "\n");
                    SurveyImage.Append("parts_order= " + garage_item.parts_to_order.Replace("\n", " ") + "\n");
                    add_YesNo_nonzero("eelecpower=  ", garage_item.power_points);
                    add_YesNo_nonzero("eelecoper=   ", garage_item.electric_door);
                    SurveyImage.Append("esptext==    \n" + garage_item.long_comments.Replace("\n", " ") + "\n");
                    //SurveyImage.Append("etmsecedit=  " + garage_item.timber_section_size + "\n");
                    add_YesNo_nonzero("enobsin=     ", garage_item.obstruction_inside_b);

                    if (garage_item.obstruction_inside_b == 1)
                        SurveyImage.Append("enobsiedit=  " + garage_item.obstruction_inside + "\n");

                    add_YesNo_nonzero("enobsout=    ", garage_item.obstruction_outside_b);

                    if (garage_item.obstruction_outside_b == 1)
                        SurveyImage.Append("enobsoedit=  " + garage_item.obstruction_outside + "\n");

                    SurveyImage.Append("entypeg=     " + garage_item.type_of_garage + "\n");
                    SurveyImage.Append("enneor=      " + garage_item.new_electric_operator_req + "\n");
                    SurveyImage.Append("enintfh=     " + garage_item.side_size_A + "\n");
                    SurveyImage.Append("enlintd=     " + garage_item.side_size_B + "\n");
                    SurveyImage.Append("enbrh=       " + garage_item.side_size_C + "\n");
                    SurveyImage.Append("enlngg=      " + garage_item.side_size_D + "\n");
                    SurveyImage.Append("enftch=      " + garage_item.side_size_E + "\n");
                    SurveyImage.Append("enftchm=     " + garage_item.side_size_F + "\n");
                    SurveyImage.Append("sizeG=       " + garage_item.side_size_G + "\n");
                    SurveyImage.Append("enstsa=      " + garage_item.side_timber_1 + "\n");
                    SurveyImage.Append("enstsb=      " + garage_item.side_timber_2 + "\n");
                    SurveyImage.Append("enintts=     " + garage_item.plan_size_A + "\n");
                    SurveyImage.Append("enbos=       " + garage_item.plan_size_B + "\n");
                    SurveyImage.Append("enrsa=       " + garage_item.plan_size_C1 + "\n");
                    SurveyImage.Append("enrsb=       " + garage_item.plan_size_C2 + "\n");
                    SurveyImage.Append("enrdts=      " + garage_item.plan_size_D + "\n");
                    SurveyImage.Append("enptsa=      " + garage_item.plan_timber_1 + "\n");
                    SurveyImage.Append("enptsb=      " + garage_item.plan_timber_2 + "\n");

                    SurveyImage.Append("rdoortype=   " + garage_item.roller_door_type + "\n");
                    SurveyImage.Append("rboxtype=    " + garage_item.roller_box_type + "\n");
                    add_YesNo_nonzero("colmatch=    ", garage_item.colour_match_roll_box);
                    add_YesNo_nonzero("ElecDoor=    ", garage_item.electric_door);
                    add_YesNo_nonzero("HandleOuts=  ", garage_item.handle_outside);
                    add_YesNo_nonzero("OtherAccess= ", garage_item.other_access);
                    add_YesNo_nonzero("SafeRelease= ", garage_item.need_safety_release);
                    add_via_button_list("openingdir=  ", garage_item.opening_direction, SurveyFitterButtonLists.garage_opening_direction_list);
                    add_YesNo_nonzero("Walls=       ", garage_item.insulated);
                    add_YesNo_nonzero("DoorStuck=   ", garage_item.door_stuck_shut);
                    add_via_button_list("MotorPos=    ", garage_item.motor_position, SurveyFitterButtonLists.garage_motor_position_list);

                    if (garage_item.bDifferentFromOriginal == true)
                    {
                        SurveyImage.Append("origdiff=    Y\n");
                        SurveyImage.Append("changedto=   " + garage_item.ChangeItemTo + "\n");
                    }
                    else
                        SurveyImage.Append("origdiff=    N\n");

                    SurveyImage.Append("PrintName=   " + garage_item.print_name + "\n");
                    //SurveyImage.Append("OGSize=      " + garage_item.over_guide_size + "\n");
                    //SurveyImage.Append("TrackH=      " + garage_item.track_height + "\n");
                    add_YesNo_nonzero("Perimeter=   ", garage_item.door_within_perimeter);
                    add_repudiate_info(garage_item.cause_of_damage);
                    SurveyImage.Append("WireType=    " + garage_item.wire_type + "\n");
                    add_YesNo_nonzero("WithinMeter= ", garage_item.socket_within_1m);
                    //add_YesNo_nonzero("nearhouse=   ", garage_item.added_to_otherrisks);
                    //SurveyImage.Append("other_place= " + garage_item.room_location + "\n");
                    SurveyImage.Append("pointenter=  " + garage_item.point_of_entry + "\n");
                    add_YN_nonzero("waslocked=   ", garage_item.was_it_locked);
                    SurveyImage.Append("lockrequ=    " + garage_item.type_of_lockng_system_required + "\n");
                    SurveyImage.Append("other_place= " + garage_item.where_is_garage + "\n");
                }
            }
        }

        private void AddGlass()
        {
            foreach (var glass_item in App.data.GetGlasssByContract(headers[current_survey].udi_cont))
            {
                if (glass_item.isComplete == 1)
                {
                    SurveyImage.Append("**Glass**\n");
                    SurveyImage.Append("contract=    " + glass_item.udi_cont + "\n");
                    SurveyImage.Append("itemcode=    " + glass_item.item_number.ToString() + "\n");
                    SurveyImage.Append("gdamch=      " + glass_item.cause_of_damage + "\n");
                    SurveyImage.Append("gglatype=    " + glass_item.glass_type + "\n");
                    SurveyImage.Append("gglapatt=    " + glass_item.glass_pattern + "\n");
                    SurveyImage.Append("gspaccolo=   " + glass_item.spacer_color + "\n");
                    SurveyImage.Append("gspacthic=   " + glass_item.spacer_thickness + "\n");
                    SurveyImage.Append("gspeglas=    " + glass_item.special_glass + "\n");
                    SurveyImage.Append("gsptext==    \n" + glass_item.long_comments.Replace("\n", " ") + "\n");
                    add_YesNo_nonzero("gstepint=    ", glass_item.stepped_unit);
                    SurveyImage.Append("gstepwedit=  " + glass_item.int_width + "\n");
                    SurveyImage.Append("gstephedit=  " + glass_item.int_height + "\n");
                    SurveyImage.Append("parts_order= " + glass_item.parts_to_order.Replace("\n", " ") + "\n");
                    add_via_button_list("gsingdoub=   ", glass_item.single_or_double, SurveyFitterButtonLists.shortened_single_or_double_list);
                    add_YesNo_nonzero("col_copy=    ", glass_item.collect_and_copy);
                    add_via_button_list("temp_door=   ", glass_item.temporary, SurveyFitterButtonLists.shared_temporary_button_list);
                    SurveyImage.Append("sizeA=       " + glass_item.sizeA + "\n");
                    SurveyImage.Append("sizeB=       " + glass_item.sizeB + "\n");
                    SurveyImage.Append("sizeC=       " + glass_item.sizeC + "\n");
                    SurveyImage.Append("sizeD=       " + glass_item.sizeD + "\n");
                    SurveyImage.Append("LeadWidth=   " + glass_item.lead_CWidthf.ToString() + "\n");
                    SurveyImage.Append("LeadHeight=  " + glass_item.lead_CHeightf.ToString() + "\n");
                    add_via_button_list("AntiRattle=  ", glass_item.lead_anti_rattle, georgian_bar_logic.anti_rattle);
                    SurveyImage.Append("LeadSingDoub=" + glass_item.lead_sod + "\n");

                    if (glass_item.special_glass == "Georgian Bar")
                        SurveyImage.Append("GeorgBar=    " + glass_item.lead_thickness + "\n");
                    else if (glass_item.special_glass != "None")
                        SurveyImage.Append("LeadThick=   " + glass_item.lead_thickness + "\n");

                    if (glass_item.special_glass != "Georgian Bar" && glass_item.special_glass != "None")
                        SurveyImage.Append("TypeOfLead=  " + glass_item.lead_type + "\n");

                    SurveyImage.Append("qdocl=       " + glass_item.docl + "\n");
                    add_YesNo_nonzero("Trim30mill=  ", glass_item.gb_trim);
                    SurveyImage.Append("RoomLocation=" + glass_item.room_location + "\n");

                    if (glass_item.bDifferentFromOriginal == true)
                    {
                        SurveyImage.Append("origdiff=    Y\n");
                        SurveyImage.Append("changedto=   " + glass_item.ChangeItemTo + "\n");
                    }
                    else
                        SurveyImage.Append("origdiff=    N\n");

                    SurveyImage.Append("PrintName=   " + glass_item.print_name + "\n");
                    SurveyImage.Append("GlassIn=     " + glass_item.ProductInto + "\n");
                    SurveyImage.Append("JointType=   " + glass_item.glazing_type + "\n");
                    SurveyImage.Append("nounits=     " + glass_item.units_required.ToString() + "\n");
                    SurveyImage.Append("gexwedit=    " + glass_item.glass_width + "\n");
                    SurveyImage.Append("gexhedit=    " + glass_item.glass_height + "\n");
                    SurveyImage.Append("gexwedit2=   " + glass_item.glass_width2 + "\n");
                    SurveyImage.Append("gexhedit2=   " + glass_item.glass_height2 + "\n");
                    SurveyImage.Append("gexwedit3=   " + glass_item.glass_width3 + "\n");
                    SurveyImage.Append("gexhedit3=   " + glass_item.glass_height3 + "\n");
                    SurveyImage.Append("gexwedit4=   " + glass_item.glass_width4 + "\n");
                    SurveyImage.Append("gexhedit4=   " + glass_item.glass_height4 + "\n");
                    SurveyImage.Append("gexwedit5=   " + glass_item.glass_width5 + "\n");
                    SurveyImage.Append("gexhedit5=   " + glass_item.glass_height5 + "\n");
                    SurveyImage.Append("gexwedit6=   " + glass_item.glass_width6 + "\n");
                    SurveyImage.Append("gexhedit6=   " + glass_item.glass_height6 + "\n");
                    SurveyImage.Append("gexwedit7=   " + glass_item.glass_width7 + "\n");
                    SurveyImage.Append("gexhedit7=   " + glass_item.glass_height7 + "\n");
                    SurveyImage.Append("gexwedit8=   " + glass_item.glass_width8 + "\n");
                    SurveyImage.Append("gexhedit8=   " + glass_item.glass_height8 + "\n");
                    SurveyImage.Append("TapeorGas=   " + glass_item.TapeorGasket + "\n");
                    SurveyImage.Append("lead_comment=" + glass_item.lead_comments + "\n");
                    SurveyImage.Append("over_thick=  " + glass_item.docl_old + "\n");
                    add_via_button_list("glazed_int=  ", glass_item.glaze, SurveyFitterButtonLists.glass_glaze_button_list);
                    add_repudiate_info(glass_item.cause_of_damage);
                    SurveyImage.Append("pointenter=  " + glass_item.point_of_entry + "\n");
                    add_YN_nonzero("waslocked=   ", glass_item.was_it_locked);
                    SurveyImage.Append("lockrequ=    " + glass_item.type_of_lockng_system_required + "\n");
                    SurveyImage.Append("bbspacerwid= " + glass_item.back_to_back_spacer_width + "\n");
                    SurveyImage.Append("bboveralwid= " + glass_item.back_to_back_spacer_height + "\n");
                    SurveyImage.Append("combi=       " + glass_item.parent_item.ToString() + "\n");

                }
            }
        }

        private void AddGreen()
        {
            foreach (var greenhouse_item in App.data.GetGreensByContract(headers[current_survey].udi_cont))
            {
                if (greenhouse_item.isComplete == 1)
                {
                    bool bNonStandard = false;
                    SurveyImage.Append("**Green**\n");
                    SurveyImage.Append("contract=    " + greenhouse_item.udi_cont + "\n");
                    SurveyImage.Append("itemcode=    " + greenhouse_item.item_number.ToString() + "\n");
                    SurveyImage.Append("cosdam=      " + greenhouse_item.cause_of_damage + "\n");
                    SurveyImage.Append("repreas=     " + greenhouse_item.rep_reason + "\n");
                    SurveyImage.Append("mattype=     " + greenhouse_item.material_type + "\n");
                    SurveyImage.Append("colour=      " + greenhouse_item.colour + "\n");
                    SurveyImage.Append("glazetype=   " + greenhouse_item.glaze_type + "\n");
                    if (greenhouse_item.base_size.Length > 4)
                        if (greenhouse_item.base_size.Substring(0, 3) == "Non")
                            bNonStandard = true;

                    SurveyImage.Append("basesize=    " + greenhouse_item.base_size + "\n");

                    if (bNonStandard == true)
                    {
                        SurveyImage.Append("basewidth=   " + greenhouse_item.base_size_x + " mm\n");
                        SurveyImage.Append("baseheight=  " + greenhouse_item.base_size_y + " mm\n");
                    }
                    else
                    {
                        SurveyImage.Append("basewidth=   " + greenhouse_item.base_size_x + " ft\n");
                        SurveyImage.Append("baseheight=  " + greenhouse_item.base_size_y + " ft\n");
                    }

                    SurveyImage.Append("typeofglass= " + greenhouse_item.type_of_glass + "\n");
                    SurveyImage.Append("dopentype=   " + greenhouse_item.door_opening_type + "\n");
                    SurveyImage.Append("wopentype=   " + greenhouse_item.window_opening_type + "\n");
                    SurveyImage.Append("overheight=  " + greenhouse_item.overall_height + "\n");
                    SurveyImage.Append("summary=     " + greenhouse_item.summary.Replace("\n", " ") + "\n");
                    SurveyImage.Append("parts_order= " + greenhouse_item.parts_to_order.Replace("\n", " ") + "\n");
                    add_repudiate_info(greenhouse_item.cause_of_damage);
                    SurveyImage.Append("pointenter=  " + greenhouse_item.point_of_entry + "\n");
                    add_YN_nonzero("waslocked=   ", greenhouse_item.was_it_locked);
                    SurveyImage.Append("lockrequ=    " + greenhouse_item.type_of_lockng_system_required + "\n");
                    add_YN_nonzero("replaceGlass=", greenhouse_item.replace_glass);
                    add_via_button_list("replacOrRepr=", greenhouse_item.repair_or_replace, SurveyFitterButtonLists.shared_repair_replace_list);
                }
            }
        }

        private void WriteLocking()
        {
            foreach (var locking_item in App.data.GetLockByContract(headers[current_survey].udi_cont))
            {
                if (locking_item.isComplete == 1)
                {
                    SurveyImage.Append("**LOCK**\n");
                    SurveyImage.Append("contract=    " + locking_item.udi_cont + "\n");
                    SurveyImage.Append("itemcode=    " + locking_item.item_number + "\n");
                    SurveyImage.Append("LockMake=    " + locking_item.locking_make + "\n");
                    SurveyImage.Append("LockCodes=   " + locking_item.locking_codes + "\n");
                    SurveyImage.Append("LockColour=  " + locking_item.lock_colour + "\n");
                    SurveyImage.Append("ItemType=    " + locking_item.item + "\n");
                    SurveyImage.Append("LockComments=" + locking_item.comments.Replace("\n", " ") + locking_item.long_comments.Replace("\n", " ") + "\n");
                    SurveyImage.Append("PageNum=     " + locking_item.pagenum + "\n");
                    SurveyImage.Append("ldamch=      " + locking_item.cause_of_damage + "\n");
                    SurveyImage.Append("SuppBroc=    " + locking_item.COD_Code + "\n");
                    //SurveyImage.Append("comp_date=   " + locking_item.new_sspare3 + "\n");
                    SurveyImage.Append("parts_order= " + locking_item.parts_to_order.Replace("\n", " ") + "\n");

                    if (locking_item.bDoorComplete == true ||
                        locking_item.bWindowComplete == true)
                    {
                        if (locking_item.item == "Door" ||
                            locking_item.item == "French Doors" ||
                            locking_item.item == "Combi Frame" ||
                            locking_item.item == "Porch" ||
                            locking_item.item == "Window" ||
                            locking_item.item == "Bay Window")
                        {
                            SurveyImage.Append("NoOfLocks=   " + locking_item.l_num.ToString() + "\n");
                            SurveyImage.Append("LockDist1=   " + locking_item.l_size1 + "\n");
                            SurveyImage.Append("LockDist2=   " + locking_item.l_size2 + "\n");
                            SurveyImage.Append("LockDistA=   " + locking_item.l_sizeA + "\n");
                            SurveyImage.Append("LockDistB=   " + locking_item.l_sizeB + "\n");
                            SurveyImage.Append("LockDistC=   " + locking_item.l_sizeC + "\n");
                            SurveyImage.Append("LockDistD=   " + locking_item.l_sizeD + "\n");
                            SurveyImage.Append("LockDistE=   " + locking_item.l_sizeE + "\n");
                            SurveyImage.Append("LockDistF=   " + locking_item.l_sizeF + "\n");
                            SurveyImage.Append("LockDistG=   " + locking_item.l_sizeG + "\n");
                            SurveyImage.Append("LockType1=   " + GetLockType(locking_item.l_itype1) + "\n");
                            SurveyImage.Append("LockType2=   " + GetLockType(locking_item.l_itype2) + "\n");
                            SurveyImage.Append("LockType3=   " + GetLockType(locking_item.l_itype3) + "\n");
                            SurveyImage.Append("LockType4=   " + GetLockType(locking_item.l_itype4) + "\n");
                            SurveyImage.Append("LockType5=   " + GetLockType(locking_item.l_itype5) + "\n");
                            SurveyImage.Append("LockType6=   " + GetLockType(locking_item.l_itype6) + "\n");
                            SurveyImage.Append("LockType7=   " + GetLockType(locking_item.l_itype7) + "\n");
                            SurveyImage.Append("PicDist1=    " + (locking_item.l_fpos1 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist2=    " + (locking_item.l_fpos2 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist3=    " + (locking_item.l_fpos3 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist4=    " + (locking_item.l_fpos4 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist5=    " + (locking_item.l_fpos5 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist6=    " + (locking_item.l_fpos6 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist7=    " + (locking_item.l_fpos7 / 2.0).ToString() + "\n");
                            SurveyImage.Append("MechDist=    " + (locking_item.lock_position / 2.0).ToString() + "\n");
                            add_NoYes ("LeftBolt=    ", locking_item.left_bolt);
                            add_NoYes ("RightBolt=   ", locking_item.right_bolt);
                            add_via_button_list("Gearbox=     ", locking_item.GearBox, window_lock_logic.gearbox_list);
                        }
                    }

                    add_repudiate_info(locking_item.cause_of_damage);
                    SurveyImage.Append("pointenter=  " + locking_item.point_of_entry + "\n");
                    add_YN_nonzero("waslocked=   ", locking_item.was_it_locked);
                    SurveyImage.Append("lockrequ=    " + locking_item.type_of_lockng_system_required + "\n");
                }
            }
        }

        private void WritePanels()
        {
            foreach (var panel_item in App.data.GetPanelsByContract(headers[current_survey].udi_cont))
            {
                if (panel_item.isComplete == 1)
                {
                    SurveyImage.Append("**Panel**\n");
                    SurveyImage.Append("contract=    " + panel_item.udi_cont + "\n");

                    if (panel_item.alum_item_number > 0)
                        SurveyImage.Append("itemcode=    " + panel_item.alum_item_number + "\n");
                    else if (panel_item.upvc_item_number > 0)
                        SurveyImage.Append("itemcode=    " + panel_item.upvc_item_number + "\n");
                    else
                        SurveyImage.Append("itemcode=    " + panel_item.item_number + "\n");

                    Add_YN("combi=       ", panel_item.alum_item_number > 0 || panel_item.upvc_item_number > 0);
                    SurveyImage.Append("pdamch=      " + panel_item.cause_of_damage + "\n");
                    SurveyImage.Append("pknock=      " + panel_item.knockedit + "\n");
                    SurveyImage.Append("pknocol=     " + panel_item.knocoledit + "\n");
                    SurveyImage.Append("pletter=     " + panel_item.letteredit + "\n");
                    SurveyImage.Append("LetBoxPos=   " + panel_item.letter_box_pos + "\n");
                    SurveyImage.Append("pwedit=      " + panel_item.wedit + "\n");
                    SurveyImage.Append("phedit=      " + panel_item.hedit + "\n");
                    SurveyImage.Append("ptypeedit=   " + panel_item.typeedit + "\n");
                    SurveyImage.Append("pthick=      " + panel_item.thickedit + "\n");
                    SurveyImage.Append("pcol=        " + panel_item.coledit + "\n");
                    SurveyImage.Append("pbackg=      " + panel_item.backgedit + "\n");
                    //SurveyImage.Append("comp_date=   " + panel_item.new_sspare3 + "\n");
                    SurveyImage.Append("parts_order= " + panel_item.parts_to_order.Replace("\n", " ") + "\n");
                    //add_YesNo_nonzero("ptonc=       ", panel_item.tonc);
                    SurveyImage.Append("pgltext=     " + panel_item.gltext + "\n");
                    SurveyImage.Append("pspaccolo=   " + panel_item.spaccoloedit + "\n");
                    SurveyImage.Append("psptext=     " + panel_item.long_sptext + "\n");
                    SurveyImage.Append("PetFlap=     " + panel_item.pet_flap + "\n");
                    SurveyImage.Append("PetType=     " + panel_item.pet_type + "\n");
                    SurveyImage.Append("RoomLocation=" + panel_item.room_location + "\n");

                    if (panel_item.bDifferentFromOriginal == true)
                    {
                        SurveyImage.Append("origdiff=    Y\n");
                        SurveyImage.Append("changedto=   " + panel_item.ChangeItemTo + "\n");
                    }
                    else
                        SurveyImage.Append("origdiff=    N\n");

                    SurveyImage.Append("Printname=   " + panel_item.print_name + "\n");
                    add_via_button_list("petmagnet=   ", panel_item.pet_magnetic, MartControls.pet_flap_logic.magnetic_list);
                    SurveyImage.Append("upvcitemno=  " + panel_item.upvc_item_number.ToString() + "\n");
                    SurveyImage.Append("alumitemno=  " + panel_item.alum_item_number.ToString() + "\n");
                    add_repudiate_info(panel_item.cause_of_damage);
                    SurveyImage.Append("pointenter=  " + panel_item.point_of_entry + "\n");
                    add_YN_nonzero("waslocked=   ", panel_item.was_it_locked);
                    SurveyImage.Append("lockrequ=    " + panel_item.type_of_lockng_system_required + "\n");
                }
            }
        }

        private void WriteTimber()
        {
            foreach (var timber_item in App.data.GetTimberByContract(headers[current_survey].udi_cont))
            {
                if (timber_item.isComplete == 1)
                {
                    SurveyImage.Append ("**Timber**\n");
                    SurveyImage.Append ("contrac=     " + timber_item.udi_cont + "\n");
                    SurveyImage.Append ("itemcod=     " + timber_item.item_number.ToString() + "\n");
                    SurveyImage.Append ("lead_comment=" + timber_item.lead_comments.Replace("\n", " ") + "\n");
                    SurveyImage.Append ("uraddlo=     " + timber_item.additional_locks + "\n");
                    SurveyImage.Append ("parts_order= " + timber_item.parts_to_order.Replace("\n", " ") + "\n");
                    add_repair_or_replace("trrch=       ", timber_item.bRepair);
                    add_YesNo_nonzero  ("urcosda=     ",  timber_item.cosmetic_damage);
                    add_YesNo_nonzero  ("col_copy=    ",  timber_item.collect_and_copy);
                    add_via_button_list("temp_door=   ",  timber_item.temporary, SurveyFitterButtonLists.shared_temporary_button_list);
                    add_YesNo_nonzero  ("preglazed_d= ",  timber_item.pre_glazed_door);
                    add_via_button_list("urgaske=     ",  timber_item.gaskets, SurveyFitterButtonLists.shared_gasket_button_list);
                    SurveyImage.Append ("urgased=     " + timber_item.gaskets_text + "\n");
                    add_YesNo_nonzero  ("urhandl=     ",  timber_item.handles_req);
                    SurveyImage.Append ("urhande=     " + timber_item.handles_text + "\n");
                    SurveyImage.Append ("tdamch=      " + timber_item.cause_of_damage + "\n");
                    SurveyImage.Append ("titemch=     " + timber_item.timber_item + "\n");
                    SurveyImage.Append ("twoodty=     " + timber_item.timber_wood + "\n");
                    add_YesNo_nonzero  ("tframe=      ",  timber_item.timber_new_frame_req);
                    SurveyImage.Append ("tfwood=      " + timber_item.timber_frame_wood + "\n");
                    SurveyImage.Append ("tbrkswe=     " + timber_item.brick_width + "\n");
                    SurveyImage.Append ("tbrkshe=     " + timber_item.brick_height + "\n");
                    SurveyImage.Append ("tintwed=     " + timber_item.internal_width + "\n");
                    SurveyImage.Append ("tinthed=     " + timber_item.internal_height + "\n");
                    add_YesNo_nonzero  ("trepfra=     ",  timber_item.repair_frame);
                    SurveyImage.Append ("tdrthke=     " + timber_item.door_thickness + "\n");
                    SurveyImage.Append ("tdrwedi=     " + timber_item.door_width + "\n");
                    SurveyImage.Append ("tdrhedi=     " + timber_item.door_height + "\n");
                    add_via_button_list("topens=      ",  timber_item.opens, SurveyFitterButtonLists.shared_in_out_button_list);
                    add_YesNo_nonzero  ("tsasher=     ",  timber_item.new_sash_required);
                    add_YesNo_nonzero  ("wbareq=      ",  timber_item.weather_bar);
                    add_YesNo_nonzero  ("theaddr=     ",  timber_item.head_drip);
                    SurveyImage.Append ("tcill=       " + timber_item.cills + "\n");
                    add_YesNo_nonzero  ("tdraugh=     ",  timber_item.draught_strip);
                    add_YesNo_nonzero  ("tthresh=     ",  timber_item.thresher);
                    add_via_button_list("tsingdo=     ",  timber_item.single_double, SurveyFitterButtonLists.shortened_single_or_double_list);
                    SurveyImage.Append("ttrickL=     " + timber_item.trickle_vents + "\n");
                    SurveyImage.Append("tlock=       " + timber_item.locks + "\n");
                    SurveyImage.Append("thardco=     " + timber_item.hardware_color + "\n");
                    SurveyImage.Append("tspacth=     " + timber_item.spacer_thickness + "\n");
                    SurveyImage.Append("tspacco=     " + timber_item.spacer_color + "\n");
                    SurveyImage.Append("tglatyp=     " + timber_item.glass_type + "\n");
                    SurveyImage.Append("tglapat=     " + timber_item.glass_pattern + "\n");
                    SurveyImage.Append("tspegla=     " + timber_item.special_glass + "\n");
                    SurveyImage.Append("tnstano=     Other\n");
                    add_YesNo_nonzero("tnbeadty=    ", timber_item.beading_type); // Fire rated door
                    add_via_button_list("AntiRattle=  ", timber_item.lead_anti_rattle, georgian_bar_logic.anti_rattle);
                    add_YN_nonzero    ("isflat=      ", timber_item.is_a_flat);
                    SurveyImage.Append("qdocl=       " + timber_item.docl + "\n");
                    SurveyImage.Append("LetBox=      " + timber_item.letter_box + "\n");
                    SurveyImage.Append("PetFlap=     " + timber_item.pet_flap + "\n");
                    SurveyImage.Append("Pettype=     " + timber_item.pet_type + "\n");

                    SurveyImage.Append("Pettype=     " + timber_item.pet_type + "\n");
                    SurveyImage.Append("Pettype=     " + timber_item.pet_type + "\n");
                    SurveyImage.Append("Pettype=     " + timber_item.pet_type + "\n");
                    SurveyImage.Append("Pettype=     " + timber_item.pet_type + "\n");
                    SurveyImage.Append("Pettype=     " + timber_item.pet_type + "\n");
                    SurveyImage.Append("Pettype=     " + timber_item.pet_type + "\n");
                    SurveyImage.Append("Pettype=     " + timber_item.pet_type + "\n");
                    SurveyImage.Append("Pettype=     " + timber_item.pet_type + "\n");
                    SurveyImage.Append("Pettype=     " + timber_item.pet_type + "\n");
                    SurveyImage.Append("Pettype=     " + timber_item.pet_type + "\n");
                    SurveyImage.Append("Pettype=     " + timber_item.pet_type + "\n");
                    SurveyImage.Append("Pettype=     " + timber_item.pet_type + "\n");

                    if (timber_item.is_a_flat == 1)
                        SurveyImage.Append("tsptext=     " + timber_item.long_timber_comments.Replace("\n", " ") + " THUMB TURN LOCKS MUST BE USED\n");
                    else
                        SurveyImage.Append("tsptext=     " + timber_item.long_timber_comments.Replace("\n", " ") + "\n");

                    SurveyImage.Append("tdcolou=     " + timber_item.door_color + "\n");
                    SurveyImage.Append("tfcolou=     " + timber_item.frame_color + "\n");
                    SurveyImage.Append("tdcolouro=   " + timber_item.door_color_out + "\n");
                    SurveyImage.Append("tfcolouro=   " + timber_item.frame_color_out + "\n");
                    SurveyImage.Append("tdcolcode=   " + timber_item.door_color_code + "\n");
                    SurveyImage.Append("tdcolcodeo=  " + timber_item.door_color_code_out + "\n");
                    SurveyImage.Append("tfcolcode=   " + timber_item.frame_color_code + "\n");
                    SurveyImage.Append("tfcolcodeo=  " + timber_item.frame_color_code_out + "\n");
                    SurveyImage.Append("RoomLocation=" + timber_item.room_location + "\n");
                    SurveyImage.Append("ReplacReas=  " + timber_item.replace_reason + "\n");
                    SurveyImage.Append("WhyNoRepair= " + timber_item.replace_explain + "\n");
                    add_YesNo_nonzero("DocLComp=    ", timber_item.doc_l_compliant);

                    if (timber_item.special_glass == "Georgian Bar")
                        SurveyImage.Append("GeorgBar=    " + timber_item.lead_thickness + "\n");
                    else if (timber_item.special_glass != "None")
                        SurveyImage.Append("LeadThick=   " + timber_item.lead_thickness + "\n");

                    SurveyImage.Append("LeadSingDoub=" + timber_item.lead_sod + "\n");

                    if (timber_item.special_glass == "Georgian Bar")
                        SurveyImage.Append("TypeOfLead=  \n");
                    else
                    {
                        if (timber_item.special_glass == "None")
                            SurveyImage.Append("TypeOfLead=  \n");
                        else
                            SurveyImage.Append("TypeOfLead=  " + timber_item.lead_type + "\n");
                    }

                    SurveyImage.Append("hinge_type=  " + timber_item.hinge_type + "\n");

                    if (timber_item.special_glass != "None")
                    {
                        SurveyImage.Append("sizeA=       " + timber_item.lead_sizeA.ToString() + "\n");
                        SurveyImage.Append("sizeB=       " + timber_item.lead_sizeB.ToString() + "\n");
                        SurveyImage.Append("sizeC=       " + timber_item.lead_sizeC.ToString() + "\n");
                        SurveyImage.Append("sizeD=       " + timber_item.lead_sizeD.ToString() + "\n");
                        SurveyImage.Append("LeadWidth=   " + timber_item.lead_CWidthf.ToString() + "\n");
                        SurveyImage.Append("LeadHeight=  " + timber_item.lead_CHeightf.ToString() + "\n");
                    }

                    if (timber_item.bNewLockingMech == 1)
                    {
                        if (timber_item.timber_item == "Door" ||
                            timber_item.timber_item == "French Doors" ||
                            timber_item.timber_item == "Combi Frame" ||
                            timber_item.timber_item == "Porch" ||
                            timber_item.timber_item == "Window" ||
                            timber_item.timber_item == "Bay Window")
                        {
                            SurveyImage.Append("NoOfLocks=   " + timber_item.l_num.ToString() + "\n");
                            SurveyImage.Append("LockDist1=   " + timber_item.l_size1 + "\n");
                            SurveyImage.Append("LockDist2=   " + timber_item.l_size2 + "\n");
                            SurveyImage.Append("LockDistA=   " + timber_item.l_sizeA + "\n");
                            SurveyImage.Append("LockDistB=   " + timber_item.l_sizeB + "\n");
                            SurveyImage.Append("LockDistC=   " + timber_item.l_sizeC + "\n");
                            SurveyImage.Append("LockDistD=   " + timber_item.l_sizeD + "\n");
                            SurveyImage.Append("LockDistE=   " + timber_item.l_sizeE + "\n");
                            SurveyImage.Append("LockDistF=   " + timber_item.l_sizeF + "\n");
                            SurveyImage.Append("LockDistG=   " + timber_item.l_sizeG + "\n");
                            SurveyImage.Append("LockType1=   " + GetLockType(timber_item.l_itype1) + "\n");
                            SurveyImage.Append("LockType2=   " + GetLockType(timber_item.l_itype2) + "\n");
                            SurveyImage.Append("LockType3=   " + GetLockType(timber_item.l_itype3) + "\n");
                            SurveyImage.Append("LockType4=   " + GetLockType(timber_item.l_itype4) + "\n");
                            SurveyImage.Append("LockType5=   " + GetLockType(timber_item.l_itype5) + "\n");
                            SurveyImage.Append("LockType6=   " + GetLockType(timber_item.l_itype6) + "\n");
                            SurveyImage.Append("LockType7=   " + GetLockType(timber_item.l_itype7) + "\n");
                            SurveyImage.Append("PicDist1=    " + (timber_item.l_fpos1 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist2=    " + (timber_item.l_fpos2 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist3=    " + (timber_item.l_fpos3 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist4=    " + (timber_item.l_fpos4 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist5=    " + (timber_item.l_fpos5 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist6=    " + (timber_item.l_fpos6 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist7=    " + (timber_item.l_fpos7 / 2.0).ToString() + "\n");
                            SurveyImage.Append("MechDist=    " + (timber_item.lock_position / 2.0).ToString() + "\n");
                            add_NoYes ("LeftBolt=    ", timber_item.left_bolt);
                            add_NoYes ("RightBolt=   ", timber_item.right_bolt);
                            add_via_button_list("Gearbox=     ", timber_item.GearBox, window_lock_logic.gearbox_list);
                            SurveyImage.Append("LockMake=    " + timber_item.lock_make + "\n");
                            SurveyImage.Append("LockCodes=   " + timber_item.lock_codes + "\n");
                        }
                    }

                    if (timber_item.doc_l_compliant == 2)
                        SurveyImage.Append("DocLCompreas=\n");
                    else
                        SurveyImage.Append("DocLCompreas=" + timber_item.doc_l_compliant_reason + "\n");

                    add_via_button_list("SlidePos=    ", timber_item.slide_position, SurveyFitterButtonLists.shared_inside_outside_button_list);
                    add_via_button_list("timglazed=   ", timber_item.timber_glazed, SurveyFitterButtonLists.timber_glazed_button_list);

                    if (timber_item.bDifferentFromOriginal == true)
                    {
                        SurveyImage.Append("origdiff=    Y\n");
                        SurveyImage.Append("changedto=   " + timber_item.ChangeItemTo + "\n");
                    }
                    else
                        SurveyImage.Append("origdiff=    N\n");

                    SurveyImage.Append("PrintName=   " + timber_item.print_name + "\n");

                    if (timber_item.standard_sizes == "Non standard")
                        SurveyImage.Append("ReasNonStand=" + timber_item.reasonnonstandard + "\n");

                    if (timber_item.Fensa == true)
                    {
                        if (timber_item.WER_rating == "UNKNOWN")
                            SurveyImage.Append("FencerRate=  C\n");
                        else
                            SurveyImage.Append("FencerRate=  " + timber_item.WER_rating + "\n");
                    }
                    else
                        SurveyImage.Append("FencerRate=  NO\n");

                    SurveyImage.Append("LetBoxPos=   " + timber_item.letter_box_pos + "\n");
                    add_via_button_list("petmagnet=   ", timber_item.pet_magnetic, MartControls.pet_flap_logic.magnetic_list);
                    SurveyImage.Append("mouldings=   " + timber_item.moulding + "\n");
                    add_repudiate_info(timber_item.cause_of_damage);
                    add_YesNo_nonzero("fire_door=   ", timber_item.beading_type);
                    SurveyImage.Append("pointenter=  " + timber_item.point_of_entry + "\n");
                    add_YN_nonzero("waslocked=   ", timber_item.was_it_locked);
                    SurveyImage.Append("lockrequ=    " + timber_item.type_of_lockng_system_required + "\n");
                    SurveyImage.Append("bbspacerwid= " + timber_item.back_to_back_spacer_width + "\n");
                    SurveyImage.Append("bboveralwid= " + timber_item.back_to_back_spacer_height + "\n");
                    add_YN_nonzero("replaceGlass=", timber_item.replace_glass);
                }
            }
        }

        private void AddUPVC()
        {
            foreach (var upvc_item in App.data.GetUPVCsByContract(headers[current_survey].udi_cont))
            {
                if (upvc_item.isComplete == 1)
                {
                    SurveyImage.Append("**UPVC**\n");
                    SurveyImage.Append("contract=    " + upvc_item.udi_cont + "\n");
                    SurveyImage.Append("itemcode=    " + upvc_item.item_number + "\n");
                    SurveyImage.Append("lead_comment=" + upvc_item.lead_comments.Replace("\n", " ") + "\n");
                    add_YesNo_nonzero("urcdosdam=   ", upvc_item.cosmetic_damage);
                    SurveyImage.Append("uraddedit=   " + upvc_item.additional_locks + "\n");
                    SurveyImage.Append("parts_order= " + upvc_item.parts_to_order.Replace("\n", " ") + "\n");
                    add_via_button_list("urgasket=    ", upvc_item.gaskets, SurveyFitterButtonLists.shared_gasket_button_list);
                    SurveyImage.Append("urgasedit=   " + upvc_item.gaskets_text + "\n");
                    add_YesNo_nonzero("urhandles=   ", upvc_item.handles_req);
                    SurveyImage.Append("urhandedit=  " + upvc_item.handles_text + "\n");
                    add_repair_or_replace("urrch=       ", upvc_item.bRepair);
                    add_YesNo_nonzero("urreppan=    ", upvc_item.replace_panel);
                    SurveyImage.Append("uitemch=     " + upvc_item.upvc_item + "\n");
                    SurveyImage.Append("udamch=      " + upvc_item.cause_of_damage + "\n");
                    SurveyImage.Append("ucolour=     " + upvc_item.colour + "\n");
                    SurveyImage.Append("usill=       " + upvc_item.cills + "\n");
                    SurveyImage.Append("uoutsedit=   " + upvc_item.outer_section_size + "\n");
                    SurveyImage.Append("uintwedit=   " + upvc_item.internal_width + "\n");
                    SurveyImage.Append("uinthedit=   " + upvc_item.internal_height + "\n");
                    SurveyImage.Append("uextwedit=   " + upvc_item.brick_width + "\n");
                    SurveyImage.Append("uexthedit=   " + upvc_item.brick_height + "\n");
                    add_YesNo_nonzero("umidrail=    ", upvc_item.midrail);
                    add_YesNo_nonzero("uaddons=     ", upvc_item.addons);
                    SurveyImage.Append("uaddwedit=   " + upvc_item.addon_width + "\n");
                    SurveyImage.Append("uaddhedit=   " + upvc_item.addon_height + "\n");
                    add_YesNo_nonzero("uheaddrip=   ", upvc_item.head_drip);
                    SurveyImage.Append("uhcolour=    " + upvc_item.handle_colour + "\n");
                    SurveyImage.Append("ulock=       " + upvc_item.locking_type + "\n");
                    SurveyImage.Append("uletter=     " + upvc_item.letter_box + "\n");
                    SurveyImage.Append("ubead=       " + upvc_item.bead_type + "\n");
                    add_via_button_list("uopens=      ", upvc_item.opens, SurveyFitterButtonLists.upvc_opens_list);
                    add_YesNo_nonzero("col_copy=    ", upvc_item.collect_and_copy);
                    add_via_button_list("temp_door=   ", upvc_item.temporary, SurveyFitterButtonLists.shared_temporary_button_list);
                    add_via_button_list("uglaze=      ", upvc_item.glaze, SurveyFitterButtonLists.shared_internal_external_button_list);
                    add_via_button_list("utrickle=    ", upvc_item.trickle_vents, SurveyFitterButtonLists.upvc_trickle_vents_button_list);
                    SurveyImage.Append("uspacthic=   " + upvc_item.spacer_thickness + "\n");
                    SurveyImage.Append("uspaccolo=   " + upvc_item.spacer_colour + "\n");
                    SurveyImage.Append("uglatype=    " + upvc_item.glass_type + "\n");
                    SurveyImage.Append("uglapatt=    " + upvc_item.glass_pattern + "\n");
                    SurveyImage.Append("uspeglas=    " + upvc_item.special_glass + "\n");
                    add_via_button_list("usingdoub=   ", upvc_item.double_tripple, SurveyFitterButtonLists.upvc_double_or_triple_list);
                    add_via_button_list("uintextlk=   ", upvc_item.internal_lock, SurveyFitterButtonLists.upvc_lock_type_button_list);
                    if (upvc_item.is_a_flat == 1)
                        SurveyImage.Append("usptext==    \n" + upvc_item.long_comments.Replace("\n", " ") + " THUMB TURN LOCKS MUST BE USED\n");
                    else
                        SurveyImage.Append("usptext==    \n" + upvc_item.long_comments.Replace("\n", " ") + "\n");

                    if (upvc_item.special_glass != "None")
                    {
                        SurveyImage.Append("sizeA=       " + upvc_item.lead_sizeA + "\n");
                        SurveyImage.Append("sizeB=       " + upvc_item.lead_sizeB + "\n");
                        SurveyImage.Append("sizeC=       " + upvc_item.lead_sizeC + "\n");
                        SurveyImage.Append("sizeD=       " + upvc_item.lead_sizeD + "\n");
                        SurveyImage.Append("LeadWidth=   " + upvc_item.lead_CWidthf.ToString() + "\n");
                        SurveyImage.Append("LeadHeight=  " + upvc_item.lead_CHeightf.ToString() + "\n");
                    }

                    add_via_button_list("AntiRattle=  ", upvc_item.lead_anti_rattle, georgian_bar_logic.anti_rattle);

                    if (upvc_item.special_glass == "Georgian Bar")
                        SurveyImage.Append("GeorgBar=    " + upvc_item.lead_thickness + "\n");
                    else if (upvc_item.special_glass != "None")
                        SurveyImage.Append("LeadThick=   " + upvc_item.lead_thickness + "\n");

                    SurveyImage.Append("LeadSingDoub=" + upvc_item.lead_sod + "\n");

                    if (upvc_item.special_glass == "Georgian Bar")
                        SurveyImage.Append("TypeOfLead=  \n");
                    else
                    {
                        if (upvc_item.special_glass == "None")
                            SurveyImage.Append("TypeOfLead=  \n");
                        else
                            SurveyImage.Append("TypeOfLead=  " + upvc_item.lead_type + "\n");
                    }

                    SurveyImage.Append("LetBoxPos=   " + upvc_item.letter_box_pos + "\n");
                    SurveyImage.Append("PetFlap=     " + upvc_item.pet_flap + "\n");
                    SurveyImage.Append("PetType=     " + upvc_item.pet_type + "\n");
                    add_via_button_list("petmagnet=   ", upvc_item.pet_magnetic, MartControls.pet_flap_logic.magnetic_list);
                    SurveyImage.Append("HingeCol=    " + upvc_item.hinge_colour + "\n");
                    add_via_button_list("ProfileType= ", upvc_item.profile_type, SurveyFitterButtonLists.shortened_profile_type_button_list);

                    if (upvc_item.bNewLockingMech == 1)
                    {
                        if (upvc_item.upvc_item == "Door" ||
                            upvc_item.upvc_item == "French Doors" ||
                            upvc_item.upvc_item == "Combi Frame" ||
                            upvc_item.upvc_item == "Porch" ||
                            upvc_item.upvc_item == "Window" ||
                            upvc_item.upvc_item == "Bay Window")
                        {
                            SurveyImage.Append("NoOfLocks=   " + upvc_item.l_num.ToString() + "\n");
                            SurveyImage.Append("LockDist1=   " + upvc_item.l_size1 + "\n");
                            SurveyImage.Append("LockDist2=   " + upvc_item.l_size2 + "\n");
                            SurveyImage.Append("LockDistA=   " + upvc_item.l_sizeA + "\n");
                            SurveyImage.Append("LockDistB=   " + upvc_item.l_sizeB + "\n");
                            SurveyImage.Append("LockDistC=   " + upvc_item.l_sizeC + "\n");
                            SurveyImage.Append("LockDistD=   " + upvc_item.l_sizeD + "\n");
                            SurveyImage.Append("LockDistE=   " + upvc_item.l_sizeE + "\n");
                            SurveyImage.Append("LockDistF=   " + upvc_item.l_sizeF + "\n");
                            SurveyImage.Append("LockDistG=   " + upvc_item.l_sizeG + "\n");
                            SurveyImage.Append("LockType1=   " + GetLockType(upvc_item.l_itype1) + "\n");
                            SurveyImage.Append("LockType2=   " + GetLockType(upvc_item.l_itype2) + "\n");
                            SurveyImage.Append("LockType3=   " + GetLockType(upvc_item.l_itype3) + "\n");
                            SurveyImage.Append("LockType4=   " + GetLockType(upvc_item.l_itype4) + "\n");
                            SurveyImage.Append("LockType5=   " + GetLockType(upvc_item.l_itype5) + "\n");
                            SurveyImage.Append("LockType6=   " + GetLockType(upvc_item.l_itype6) + "\n");
                            SurveyImage.Append("LockType7=   " + GetLockType(upvc_item.l_itype7) + "\n");
                            SurveyImage.Append("PicDist1=    " + (upvc_item.l_fpos1 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist2=    " + (upvc_item.l_fpos2 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist3=    " + (upvc_item.l_fpos3 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist4=    " + (upvc_item.l_fpos4 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist5=    " + (upvc_item.l_fpos5 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist6=    " + (upvc_item.l_fpos6 / 2.0).ToString() + "\n");
                            SurveyImage.Append("PicDist7=    " + (upvc_item.l_fpos7 / 2.0).ToString() + "\n");
                            SurveyImage.Append("MechDist=    " + (upvc_item.lock_position / 2.0).ToString() + "\n");
                            add_NoYes ("LeftBolt=    ", upvc_item.left_bolt);
                            add_NoYes ("RightBolt=   ", upvc_item.right_bolt);
                            add_via_button_list("Gearbox=     ", upvc_item.GearBox, window_lock_logic.gearbox_list);
                            SurveyImage.Append("LockMake=    " + upvc_item.lock_make + "\n");
                            SurveyImage.Append("LockCodes=   " + upvc_item.lock_codes + "\n");
                        }
                    }

                    SurveyImage.Append("mrheight=    " + upvc_item.midrail_height + "\n");
                    SurveyImage.Append("qdocl=       " + upvc_item.docl + "\n");
                    SurveyImage.Append("ReplacReas=  " + upvc_item.replace_reason + "\n");
                    SurveyImage.Append("WhyNoRepair= " + upvc_item.replace_explain + "\n");
                    SurveyImage.Append("FrameDepthIO=" + upvc_item.frame_depth + "\n");
                    SurveyImage.Append("RoomLocation=" + upvc_item.room_location + "\n");
                    add_via_button_list("LPHandles=   ", upvc_item.LPHandles, SurveyFitterButtonLists.shared_handle_operation_list);
                    add_via_button_list("SlidePos=    ", upvc_item.slide_position, SurveyFitterButtonLists.shared_inside_outside_button_list);
                    SurveyImage.Append("Threshold=   " + upvc_item.threshold_type + "\n");

                    if (upvc_item.bDifferentFromOriginal == true)
                    {
                        SurveyImage.Append("origdiff=    Y\n");
                        SurveyImage.Append("changedto=   " + upvc_item.ChangeItemTo + "\n");
                    }

                    SurveyImage.Append("PrintName=   " + upvc_item.print_name + "\n");

                    if (upvc_item.fensa == true)
                    {
                        if (upvc_item.WER_Rating == "UNKNOWN")
                            SurveyImage.Append("FencerRate= C\n");
                        else
                            SurveyImage.Append("FencerRate= " + upvc_item.WER_Rating + "\n");
                    }
                    else
                        SurveyImage.Append("FencerRate=  NO\n");

                    add_repudiate_info(upvc_item.cause_of_damage);
                    add_YN_nonzero("isflat=      ", upvc_item.is_a_flat);
                    SurveyImage.Append("pointenter=  " + upvc_item.point_of_entry + "\n");
                    add_YN_nonzero("waslocked=   ", upvc_item.was_it_locked);
                    SurveyImage.Append("lockrequ=    " + upvc_item.type_of_lockng_system_required + "\n");
                    SurveyImage.Append("bbspacerwid= " + upvc_item.back_to_back_spacer_width + "\n");
                    SurveyImage.Append("bboveralwid= " + upvc_item.back_to_back_spacer_height + "\n");
                    add_YN_nonzero("replaceGlass=", upvc_item.replace_glass);
                    SurveyImage.Append("URADDLOCK=   " + upvc_item.additional_locks + "\n");
                }
            }
        }


    }
}