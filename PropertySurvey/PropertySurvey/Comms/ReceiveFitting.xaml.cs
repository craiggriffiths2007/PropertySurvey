using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceiveFitting : ReceiveShared
    {
        StringBuilder postData = new StringBuilder();

        string[] added_contracts = new string[50];
        int total_added_contracts = 0;

        int download_stage = 0; // 0 = fittings 1 = Images
        int current_fitting_image = 0;
        int total_fitting_images = 0;

        string sendResponse = "";

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

        public ReceiveFitting()
        {
            InitializeComponent();

            GetFitting();
        }

        public void GetFitting()
        {
            // Send PDA code and Branch code
            XDocument srcTree = new XDocument(
            new XComment("This is a comment"),
            new XElement("User",
            new XElement("PDAUser", App.net.receive_test_data ? "H03" : App.net.App_Settings.set_ownercode,
            new XElement("Branch", "*"), //App.net.App_Settings.set_branchcode),
            new XElement("PhoneSerial", "*"), //App.net.phone_serial),
            new XElement("UserType", App.net.App_Settings.set_usertype))));

            postData.Append(srcTree.ToString());

            //progressBar1.IsIndeterminate = true;

            Uri thisuri = new Uri((App.net.receive_test_data ? App.net.live_url : App.net.App_Settings.set_url) + "/WM7Communication/WM7DownloadFittingsMP");
            HttpHelper helper = new HttpHelper(thisuri, "POST",
            new KeyValuePair<string, string>("ContractFile", "<JobData>" + postData.ToString() + "</JobData>"));
            helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
            helper.Execute();
        }

        const int _downloadImageTimeoutInSeconds = 15;
        readonly HttpClient _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(_downloadImageTimeoutInSeconds) };



        async void DownloadImageAsync(string imageUrl)
        {
            try
            {
                using (var httpResponse = await _httpClient.GetAsync(imageUrl))
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        try // Fitting
                        {
                            //XDocument xml = XDocument.Parse(sendResponse);

                            byte[] decoded_image_data = await httpResponse.Content.ReadAsByteArrayAsync();

                            if (images_list[current_fitting_image].image_filename.Substring(9, 1) == "c")
                            {
                                string newfname = images_list[current_fitting_image].image_contract + "_cA" + images_list[current_fitting_image].image_filename.Substring(11);
                                //newfname = newfname.Replace(App.net.original_contract_number, App.net.HeaderRecord.udi_cont);
                                App.files.SaveBinary("Photos/" + newfname, decoded_image_data);
                            }
                            if (images_list[current_fitting_image].image_filename.Substring(9, 1) == "f")
                            {
                                string newfname = images_list[current_fitting_image].image_contract + "_fA" + images_list[current_fitting_image].image_filename.Substring(11);
                                //newfname = newfname.Replace(App.net.original_contract_number, App.net.HeaderRecord.udi_cont);
                                App.files.SaveBinary("Photos/" + newfname, decoded_image_data);
                            }
                            if (images_list[current_fitting_image].image_filename.Substring(9, 1) == "d")
                            {
                                string newfname = images_list[current_fitting_image].image_contract + "_dA" + images_list[current_fitting_image].image_filename.Substring(11);
                                //newfname = newfname.Replace(App.net.original_contract_number, App.net.HeaderRecord.udi_cont);
                                App.files.SaveBinary("Drawings/" + newfname, decoded_image_data);
                            }

                            current_fitting_image++;
                            if (current_fitting_image > total_fitting_images - 1)
                            {
                                Navigation.InsertPageBefore(new GetMessages(), this);
                                await Navigation.PopAsync(false);
                            }
                            else
                            {
                                GetFittingImage();
                            }
                        }
                        catch
                        {
                            await Navigation.PopAsync(false);
                        }

                        return;// null;//  await httpResponse.Content.ReadAsByteArrayAsync();
                    }
                    else
                    {
                        /*
                        complete_label.Text = "No Internet Connection";
                        //Url is Invalid
                        return;// null;
                        */
                        current_fitting_image++;
                        if (current_fitting_image > total_fitting_images - 1)
                        {
                            Navigation.InsertPageBefore(new GetMessages(), this);
                            await Navigation.PopAsync(false);
                        }
                        else
                        {
                            GetFittingImage();
                        }
                    }
                }
            }
            catch
            {
                //Handle Exception
                return;// null;
            }
        }

        public void GetFittingImage()
        {
            
            try
            {
                string fname = (App.net.receive_test_data ? App.net.live_url : App.net.App_Settings.set_url) + "/PPCImages/" + images_list[current_fitting_image].image_filename.Substring(2, 1) + "/" + images_list[current_fitting_image].image_filename.Substring(3, 2) + "/" + images_list[current_fitting_image].image_filename;
                DownloadImageAsync(fname);
            }
            catch (Exception e)
            {
                //DownloadImageAsync(fname);
            }
            sending_progress.ProgressTo((1.0f / total_fitting_images) * (float)current_fitting_image, 250, Easing.Linear);
            //return;
            
            /*
            int the_image = current_fitting_image + 1;

            // Send PDA code and Branch code
            XDocument srcTree = new XDocument(
            new XComment("This is a comment"),
            new XElement("User",
            new XElement("PDAUser", App.net.App_Settings.set_ownercode),
            new XElement("Branch", App.net.App_Settings.set_branchcode),
            new XElement("PhoneSerial", "@"),//App.net.cereal),
            new XElement("UserType", App.net.App_Settings.set_usertype),
            new XElement("image_filename", images_list[current_fitting_image].image_filename.ToString()),
            new XElement("image_path", images_list[current_fitting_image].image_path.ToString()),
            new XElement("image_contract", images_list[current_fitting_image].image_contract.ToString())));

            //filenam.Text = images_list[current_fitting_image].image_filename.ToString();

            int i;
            byte[] data;

            postData.Clear();
            postData.Append(srcTree.ToString());

            Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/WM7DownloadFittingImageB");
            HttpHelper helper = new HttpHelper(thisuri, "POST",
            new KeyValuePair<string, string>("ContractFile", "<JobData>" + postData.ToString() + "</JobData>"));
            helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
            helper.Execute();
            */
        }

        private void CommandComplete(HttpResponseCompleteEventArgs e)
        {
            sendResponse = e.Response;
            Device.BeginInvokeOnMainThread(CompleteDownload);
        }

        private void CommandComplete2(HttpResponseCompleteEventArgs e)
        {
            sendResponse = e.Response;
            //Device.BeginInvokeOnMainThread(CompleteDownload);
        }

        private string CensorString(string str)
        {
            string newWord = "";
            foreach (var c in str)
            {
                if (c != ' ')
                {
                    newWord = newWord + "x";
                }
                else
                {
                    newWord = newWord + " ";
                }
            }
            return newWord;
        }

        private void CompleteDownload()
        {
            if (download_stage == 0)
            {
                if ((sendResponse.Length > 0) && (sendResponse.Substring(0, 10) != "<Fittings>"))
                {
                    if (sendResponse == "nointernet")
                    {
                        complete_label.Text = "No Internet Connection";
                    }
                    else if (sendResponse.Substring(0, 19) == "<WrongSerialNumber>")
                    {
                        complete_label.Text = "Wrong Serial Number";
                    }
                    else
                        complete_label.Text = sendResponse;
                }
                else
                {
                    if (sendResponse.Substring(0, 10) == "<Fittings>")
                    {
                        try // Fitting
                        {
                            XDocument xml = XDocument.Parse(sendResponse);
                            bool bFound = false;

                            images_list = new List<ImageFiles>();

                            foreach (var word2 in xml.Element("Fittings").Elements("Images").Elements("Image"))
                            {
                                string image_filename = (string)word2.Element("image_filename");
                                string image_path = (string)word2.Element("image_path");
                                string image_contract = (string)word2.Element("image_contract");

                                byte[] decoded_image_filename = System.Convert.FromBase64String(image_filename);
                                byte[] decoded_image_path = System.Convert.FromBase64String(image_path);
                                byte[] decoded_image_contract = System.Convert.FromBase64String(image_contract);

                                string string_image_filename = System.Text.Encoding.UTF8.GetString(decoded_image_filename, 0, decoded_image_filename.Length);
                                string string_image_path = System.Text.Encoding.UTF8.GetString(decoded_image_path, 0, decoded_image_path.Length);
                                string string_image_contract = System.Text.Encoding.UTF8.GetString(decoded_image_contract, 0, decoded_image_contract.Length);

                                //for (int i = 0; i < total_added_contracts; i++)
                                //{
                                //    if (bFound2 == false)
                                //    {
                                //        if (string_image_contract.Substring(0, 8) == added_contracts[i])
                                //        {
                                //            bFound2 = true;
                                if (App.net.receive_test_data == true)
                                    string_image_contract = string_image_contract.Replace("3", "0");

                                images_list.Add(new ImageFiles(string_image_filename, string_image_path, string_image_contract));
                                //        }
                                //    }
                                //}

                                {
                                    StringBuilder localpostData2 = new StringBuilder(128000);

                                    XDocument localTree = new XDocument(
                                    new XElement("Image",
                                    new XElement("UserType", App.net.App_Settings.set_usertype),
                                    new XElement("JobType", "S"),
                                    new XElement("UserCode", App.net.App_Settings.set_ownercode),
                                    new XElement("LastImage", 0),
                                    new XElement("Contract", "00000000"),
                                    new XElement("Filename", string_image_filename),
                                    new XElement("data","0")));

                                    localpostData2.Append(localTree.ToString());

                                    Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/SendJobImage");
                                    HttpHelper helper = new HttpHelper(thisuri, "POST",
                                    new KeyValuePair<string, string>("ContractFile", "<ContractFile>" + localpostData2.ToString() + "</ContractFile>"));
                                    helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete2);
                                    helper.Execute();
                                }
                            }

                            total_fitting_images = images_list.Count();

                            foreach (var word in xml.Element("Fittings").Elements("Fitting"))
                            {
                                string Job = (string)word.Element("Job");
                                if (Job != null)
                                { }
                                byte[] decoded = System.Convert.FromBase64String(Job);

                                string full_string = System.Text.Encoding.UTF8.GetString(decoded);

                                Stream file_stream = new MemoryStream(decoded);

                                bFound = false;

                                mrs_sr = new StreamReader(file_stream);
                                string decoded_job = mrs_sr.ReadToEnd();
                                if (decoded_job != "")
                                { }
                                //mrs_sr = new StreamReader(file_stream);
                                file_stream.Seek(0, SeekOrigin.Begin);
                                string udi_cont = mrs_sr.ReadLine();

                                //var query = from p in App.CurrentApp.DB.Headers where p.udi_cont == udi_cont select p;
                                Header item = App.data.GetHeaderByContract(udi_cont);

                                if (false)// (item!=null)
                                {
                                    /*
                                    string typeA = mrs_sr.ReadLine();
                                    string typeB = mrs_sr.ReadLine();
                                    string fit_diary = mrs_sr.ReadLine();
                                    string fitters_instructions = mrs_sr.ReadLine();
                                    item.fitters_instructions = fitters_instructions;
                                    App.CurrentApp.DB.SubmitChanges();
                                    */

                                    if (item.bSent == true &&
                                        DateTime.Parse(item.udi_date) < DateTime.Today)
                                    {
                                        //App.CurrentApp.DeleteJob(item.udi_cont);
                                        bFound = false;
                                        //App.CurrentApp.DB.SubmitChanges();
                                    }
                                    else
                                    {
                                        App.CurrentApp.HeaderRecord = item;
                                        bFound = true;
                                    }
                                }

                                if (decoded.Length > 0)
                                {
                                    if (bFound == false)
                                    {
                                        App.CurrentApp.HeaderRecord = new Header();
                                        App.net.table_init.CreateHeader();
                                    }
                                    string typeA = mrs_sr.ReadLine();
                                    string typeB = mrs_sr.ReadLine();
                                    string fit_diary = mrs_sr.ReadLine();
                                    string fitters_instructions = mrs_sr.ReadLine();
                                    string fit_start = mrs_sr.ReadLine();
                                    string fit_fin = mrs_sr.ReadLine();
                                    string header_title = mrs_sr.ReadLine();

                                    if (header_title == "No Survey")
                                    {
                                        App.CurrentApp.HeaderRecord.bSurvey = false;
                                    }
                                    else
                                    {
                                        App.CurrentApp.HeaderRecord.bSurvey = true;
                                    }

                                    string udi_cont2 = mrs_sr.ReadLine();   // same as first one
                                    string uc_name = mrs_sr.ReadLine();
                                    string uc_postcode = mrs_sr.ReadLine();
                                    string udi_start = mrs_sr.ReadLine();
                                    string udi_fin = mrs_sr.ReadLine();
                                    string str_blank = mrs_sr.ReadLine();
                                    string add_long = mrs_sr.ReadLine();
                                    string uc_add1 = mrs_sr.ReadLine();
                                    string uc_add2 = mrs_sr.ReadLine();
                                    string uc_add3 = mrs_sr.ReadLine();
                                    string uc_h_phone = mrs_sr.ReadLine();
                                    string udi_date = mrs_sr.ReadLine();
                                    string sn_name = mrs_sr.ReadLine();
                                    string uc_laname = mrs_sr.ReadLine();
                                    string goaheadstr = mrs_sr.ReadLine();
                                    string uc_inceden = mrs_sr.ReadLine();
                                    string uc_desc = mrs_sr.ReadLine();
                                    string udi_tlight = mrs_sr.ReadLine();
                                    string si_bday1 = mrs_sr.ReadLine();
                                    string uc_excess = mrs_sr.ReadLine();
                                    string acc_code = mrs_sr.ReadLine();
                                    string str = mrs_sr.ReadLine();
                                    string b_super = mrs_sr.ReadLine();
                                    string uc_w_phone = mrs_sr.ReadLine();
                                    string uc_m_phone = mrs_sr.ReadLine();
                                    str = mrs_sr.ReadLine();
                                    str = mrs_sr.ReadLine();
                                    str = mrs_sr.ReadLine();
                                    str = mrs_sr.ReadLine();
                                    string udi_staff = mrs_sr.ReadLine();

                                    if (App.CurrentApp.HeaderRecord.bSurvey == true)
                                    {
                                        string imchup = mrs_sr.ReadLine();
                                        string card_cheq = mrs_sr.ReadLine();
                                        string expiry = mrs_sr.ReadLine();
                                        //string issue_no = mrs_sr.ReadLine();

                                        //string mop = mrs_sr.ReadLine();

                                        string surveyor_report = mrs_sr.ReadLine();
                                        surveyor_report = surveyor_report + " " + mrs_sr.ReadLine();
                                        surveyor_report = surveyor_report + " " + mrs_sr.ReadLine();
                                        string job_grade = mrs_sr.ReadLine();
                                        string acc_text = mrs_sr.ReadLine();
                                        str = mrs_sr.ReadLine();
                                        string summ_text = mrs_sr.ReadLine();
                                        string code_text = mrs_sr.ReadLine();
                                        summ_text = summ_text + mrs_sr.ReadLine();
                                        code_text = code_text + mrs_sr.ReadLine();
                                        string photo = mrs_sr.ReadLine();
                                        string booked = mrs_sr.ReadLine();
                                        string easy_park = mrs_sr.ReadLine();
                                        string access_rear = mrs_sr.ReadLine();
                                        string alarm_cont = mrs_sr.ReadLine();
                                        string ladder_req = mrs_sr.ReadLine();
                                        string height_res = mrs_sr.ReadLine();
                                        string obs_wires = mrs_sr.ReadLine();
                                        string sand_cemen = mrs_sr.ReadLine();
                                        string plaster = mrs_sr.ReadLine();
                                        string doorbell = mrs_sr.ReadLine();
                                        string loose_brick = mrs_sr.ReadLine();
                                        string genreq = mrs_sr.ReadLine();
                                        string architreq = mrs_sr.ReadLine();
                                        string acroreq = mrs_sr.ReadLine();
                                        string njs = mrs_sr.ReadLine();
                                        string nsn = mrs_sr.ReadLine();
                                        str = mrs_sr.ReadLine();
                                        string bWorkInside = mrs_sr.ReadLine();
                                        string inst_height = mrs_sr.ReadLine();
                                        string bBothHands = mrs_sr.ReadLine();
                                        string ground_surface = mrs_sr.ReadLine();
                                        string type_of_equipment = mrs_sr.ReadLine();
                                        string risks_and_dangers = mrs_sr.ReadLine();
                                        string loose_brick_text = mrs_sr.ReadLine();
                                        string obs_wires_text = mrs_sr.ReadLine();

                                        string stimea = mrs_sr.ReadLine();
                                        //str = mrs_sr.ReadLine();
                                        //type_of_equipment = "Scaffold";

                                        //string work_at_height = mrs_sr.ReadLine();
                                        //str = mrs_sr.ReadLine();
                                        string no_ladders = mrs_sr.ReadLine();
                                        string reason_excess_not_collected = mrs_sr.ReadLine();
                                        string uc_h_phone2 = mrs_sr.ReadLine();
                                        string uc_h_phone3 = mrs_sr.ReadLine();

                                        string items_above_roof = mrs_sr.ReadLine();
                                        string asbestos_visible = mrs_sr.ReadLine();
                                        string asvizex = mrs_sr.ReadLine();

                                        string esttimesite = mrs_sr.ReadLine();

                                        string subcontract = mrs_sr.ReadLine();
                                        string subcontract_text = mrs_sr.ReadLine();

                                        string ShopFrontWk = mrs_sr.ReadLine();

                                        string AccCode = mrs_sr.ReadLine();

                                        if (App.net.receive_test_data == true)
                                        {
                                            App.net.original_contract_number = udi_cont;
                                            udi_cont = udi_cont.Replace("3", "0");

                                            string lastWord = uc_name.Split(' ').Last();
                                            string newWord = "";
                                            foreach (var c in lastWord)
                                            {
                                                newWord = newWord + "x";
                                            }
                                            uc_name = uc_name.Replace(lastWord, newWord);

                                            add_long = CensorString(add_long);
                                            uc_add1 = CensorString(uc_add1);
                                            uc_add2 = CensorString(uc_add2);
                                            uc_add3 = CensorString(uc_add3);
                                            sn_name = CensorString(sn_name);

                                            uc_postcode = CensorString(uc_postcode);
                                        }


                                        App.CurrentApp.HeaderRecord.subcontracttext = subcontract_text;

                                        App.CurrentApp.HeaderRecord.survey_complete = 1;
                                        App.CurrentApp.HeaderRecord.reason_not_complete = "n/a";

                                        switch (subcontract)
                                        {
                                            case "": App.CurrentApp.HeaderRecord.b_subcontract = 0; break;
                                            case "Yes": App.CurrentApp.HeaderRecord.b_subcontract = 1; break;
                                            case "No": App.CurrentApp.HeaderRecord.b_subcontract = 2; break;
                                        }

                                        switch (ShopFrontWk)
                                        {
                                            case "": App.CurrentApp.HeaderRecord.shop_front_work = 0; break;
                                            case "Yes": App.CurrentApp.HeaderRecord.shop_front_work = 1; break;
                                            case "No": App.CurrentApp.HeaderRecord.shop_front_work = 2; break;
                                        }

                                        if (reason_excess_not_collected != "")
                                        {
                                            App.CurrentApp.HeaderRecord.bExcessCollected = 2;
                                        }
                                        else
                                        {
                                            App.CurrentApp.HeaderRecord.bExcessCollected = 1;
                                        }

                                        switch (esttimesite)
                                        {
                                            case "01:00": App.CurrentApp.HeaderRecord.time_to_complete = "1 hour"; break;
                                            case "01:30": App.CurrentApp.HeaderRecord.time_to_complete = "1 hours 30 minutes"; break;
                                            case "02:00": App.CurrentApp.HeaderRecord.time_to_complete = "2 hours"; break;
                                            case "02:30": App.CurrentApp.HeaderRecord.time_to_complete = "2 hours 30 minutes"; break;
                                            case "03:00": App.CurrentApp.HeaderRecord.time_to_complete = "3 hours"; break;
                                            case "03:30": App.CurrentApp.HeaderRecord.time_to_complete = "3 hours 30 minutes"; break;
                                            case "04:00": App.CurrentApp.HeaderRecord.time_to_complete = "4 hours"; break;
                                            case "04:30": App.CurrentApp.HeaderRecord.time_to_complete = "4 hours 30 minutes"; break;
                                            case "05:00": App.CurrentApp.HeaderRecord.time_to_complete = "5 hours"; break;
                                            case "05:30": App.CurrentApp.HeaderRecord.time_to_complete = "5 hours 30 minutes"; break;
                                            case "06:00": App.CurrentApp.HeaderRecord.time_to_complete = "6 hours"; break;
                                            case "06:30": App.CurrentApp.HeaderRecord.time_to_complete = "6 hours 30 minutes"; break;
                                            case "07:00": App.CurrentApp.HeaderRecord.time_to_complete = "7 hours"; break;
                                            case "07:30": App.CurrentApp.HeaderRecord.time_to_complete = "7 hours 30 minutes"; break;
                                            case "08:00": App.CurrentApp.HeaderRecord.time_to_complete = "8 hours"; break;
                                        }

                                        App.CurrentApp.HeaderRecord.card_cheq = card_cheq;

                                        //App.CurrentApp.HeaderRecord.expiry = DateTime.Parse(expiry);

                                        //if (App.CurrentApp.IsNumber(issue_no) == true)
                                        //{
                                        App.CurrentApp.HeaderRecord.issue_no = 0;
                                        //}

                                        App.CurrentApp.HeaderRecord.mop = "";
                                        App.CurrentApp.HeaderRecord.rep_text = surveyor_report;

                                        App.CurrentApp.HeaderRecord.job_grade = job_grade;

                                        /*
                                        if (job_grade == "None")
                                            App.CurrentApp.HeaderRecord.job_grade = 1;
                                        else
                                            App.CurrentApp.HeaderRecord.job_grade = Convert.ToInt32(job_grade);
                                        */

                                        App.CurrentApp.HeaderRecord.acc_text = acc_text;
                                        App.CurrentApp.HeaderRecord.summ_text = summ_text;
                                        App.CurrentApp.HeaderRecord.code_text = code_text;
                                        App.CurrentApp.HeaderRecord.photo = YNtoInt(photo);
                                        App.CurrentApp.HeaderRecord.booked = YNtoInt(booked);
                                        App.CurrentApp.HeaderRecord.easy_park = YNtoInt(easy_park);
                                        App.CurrentApp.HeaderRecord.access_rear = YNtoInt(access_rear);
                                        App.CurrentApp.HeaderRecord.alarm_cont = YNtoInt(alarm_cont);
                                        App.CurrentApp.HeaderRecord.ladder_req = YNtoInt(ladder_req);
                                        App.CurrentApp.HeaderRecord.height_res = YNtoInt(height_res);
                                        App.CurrentApp.HeaderRecord.obs_wires = YNtoInt(obs_wires);
                                        App.CurrentApp.HeaderRecord.sand_cemen = YNtoInt(sand_cemen);
                                        App.CurrentApp.HeaderRecord.plaster = YNtoInt(plaster);
                                        App.CurrentApp.HeaderRecord.doorbell = YNtoInt(doorbell);
                                        App.CurrentApp.HeaderRecord.loose_brick = YNtoInt(loose_brick);
                                        App.CurrentApp.HeaderRecord.genreq = YNtoInt(genreq);
                                        App.CurrentApp.HeaderRecord.architreq = YNtoInt(architreq);
                                        App.CurrentApp.HeaderRecord.acroreq = YNtoInt(acroreq);

                                        App.CurrentApp.HeaderRecord.njs = njs;
                                        App.CurrentApp.HeaderRecord.nsn = nsn;
                                        App.CurrentApp.HeaderRecord.bWorkInside = YNtoInt(bWorkInside);
                                        App.CurrentApp.HeaderRecord.inst_height = inst_height;
                                        App.CurrentApp.HeaderRecord.bBothHands = YNtoInt(bBothHands);
                                        App.CurrentApp.HeaderRecord.ground_surface = ground_surface;
                                        App.CurrentApp.HeaderRecord.type_of_equipment = type_of_equipment;
                                        App.CurrentApp.HeaderRecord.risks_and_dangers = risks_and_dangers;
                                        App.CurrentApp.HeaderRecord.loose_brick_text = loose_brick_text;
                                        App.CurrentApp.HeaderRecord.obs_wires_text = obs_wires_text;

                                        if (stimea.Length == 5)
                                        {
                                            App.CurrentApp.HeaderRecord.stimea = stimea;// DateTime.Parse(stimea);
                                        }
                                        else
                                        {
                                            App.CurrentApp.HeaderRecord.stimea = DateTime.Now.ToShortTimeString();
                                        }

                                        App.CurrentApp.HeaderRecord.inst_height = inst_height;

                                        if (inst_height.Length == 0)
                                        {
                                            App.CurrentApp.HeaderRecord.work_at_height = 2;
                                        }
                                        else
                                        {
                                            App.CurrentApp.HeaderRecord.work_at_height = 1;
                                        }

                                        App.CurrentApp.HeaderRecord.card_cheq = card_cheq;
                                        App.CurrentApp.HeaderRecord.mop = "";


                                        App.CurrentApp.HeaderRecord.no_ladders = YNtoInt(no_ladders);
                                        App.CurrentApp.HeaderRecord.reason_excess_not_collected = reason_excess_not_collected;
                                        /*
                                        if (reason_excess_not_collected.Length == 0)
                                        {
                                            App.CurrentApp.HeaderRecord.bExcessCollected = false;
                                        }
                                        else
                                        {
                                            App.CurrentApp.HeaderRecord.bExcessCollected = true;
                                        }
                                        */
                                        App.CurrentApp.HeaderRecord.uc_h_phone2 = uc_h_phone2;
                                        App.CurrentApp.HeaderRecord.uc_h_phone3 = uc_h_phone3;

                                        App.CurrentApp.HeaderRecord.items_above_roof = YNtoInt(items_above_roof);
                                        App.CurrentApp.HeaderRecord.asbestos_visible = YNtoInt(asbestos_visible);

                                        App.CurrentApp.HeaderRecord.asvizex = asvizex;

                                    }


                                    //App.CurrentApp.HeaderRecord.bDone = false;
                                    //App.CurrentApp.HeaderRecord.bSent = false;
                                    if (b_super == "Y")
                                    {
                                        App.CurrentApp.HeaderRecord.si_done = true;
                                    }
                                    else
                                    {
                                        App.CurrentApp.HeaderRecord.si_done = false;
                                    }



                                    App.CurrentApp.HeaderRecord.si_mpay = acc_code;

                                    App.CurrentApp.HeaderRecord.fit_diary = DateTime.Today.ToShortDateString();
                                    App.CurrentApp.HeaderRecord.uc_inceden = DateTime.Today.ToShortDateString();
                                    App.CurrentApp.HeaderRecord.expiry = DateTime.Today.ToShortDateString();
                                    App.CurrentApp.HeaderRecord.udi_start = DateTime.Today.ToShortDateString();
                                    //udi_date = udi_date.Substring(0, 6) + "20" + udi_date.Substring(6, 2);
                                    App.CurrentApp.HeaderRecord.udi_date = DateTime.Today.ToString();
                                    App.CurrentApp.HeaderRecord.udi_fin = DateTime.Today.ToShortDateString();
                                    App.CurrentApp.HeaderRecord.fmdate = DateTime.Today.ToShortDateString();
                                    App.CurrentApp.HeaderRecord.ftimearr = DateTime.Today.ToShortDateString();
                                    App.CurrentApp.HeaderRecord.ftimeleft = DateTime.Today.ToShortDateString();
                                    App.CurrentApp.HeaderRecord.uspot_date = DateTime.Today.ToShortDateString();
                                    App.CurrentApp.HeaderRecord.uspot_signeddate = DateTime.Today.ToShortDateString();
                                    App.CurrentApp.HeaderRecord.f_sign_date = DateTime.Today.ToShortDateString();
                                    //App.CurrentApp.HeaderRecord.uc_excess = Convert.ToDouble(uc_excess);

                                    App.CurrentApp.HeaderRecord.udi_cont = udi_cont;
                                    App.CurrentApp.HeaderRecord.typeA = typeA;
                                    App.CurrentApp.HeaderRecord.typeB = typeB;

                                    fit_diary = fit_diary.Substring(0, 6) + "20" + fit_diary.Substring(6, 2);

                                    App.CurrentApp.HeaderRecord.fit_diary = DateTime.Parse(fit_diary).ToString();// DateTime.Parse(fit_diary);
                                    App.CurrentApp.HeaderRecord.expiry = App.CurrentApp.HeaderRecord.fit_diary;
                                    App.CurrentApp.HeaderRecord.fitters_instructions = fitters_instructions;
                                    App.CurrentApp.HeaderRecord.fit_start = DateTime.Parse(fit_start).ToString();
                                    App.CurrentApp.HeaderRecord.fit_fin = DateTime.Parse(fit_fin).ToString();

                                    App.CurrentApp.HeaderRecord.uc_name = uc_name;
                                    App.CurrentApp.HeaderRecord.uc_postcode = uc_postcode;
                                    App.CurrentApp.HeaderRecord.udi_start = DateTime.Parse(udi_start).ToString();
                                    App.CurrentApp.HeaderRecord.udi_fin = DateTime.Parse(udi_fin).ToString();
                                    App.CurrentApp.HeaderRecord.add_long = add_long;
                                    App.CurrentApp.HeaderRecord.uc_add1 = uc_add1;
                                    App.CurrentApp.HeaderRecord.uc_add2 = uc_add2;
                                    App.CurrentApp.HeaderRecord.uc_add3 = uc_add3;
                                    App.CurrentApp.HeaderRecord.uc_h_phone = uc_h_phone;
                                    App.CurrentApp.HeaderRecord.uc_h_phone2 = uc_w_phone;
                                    App.CurrentApp.HeaderRecord.uc_h_phone3 = uc_m_phone;
                                    App.CurrentApp.HeaderRecord.udi_date = DateTime.Parse(udi_date).ToString();
                                    App.CurrentApp.HeaderRecord.sn_name = sn_name;
                                    App.CurrentApp.HeaderRecord.uc_laname = uc_laname;
                                    App.CurrentApp.HeaderRecord.goaheadstr = goaheadstr;
                                    if (uc_inceden.Length < 8)
                                        uc_inceden = "01/01/98";

                                    App.CurrentApp.HeaderRecord.uc_inceden = DateTime.Parse(uc_inceden).ToString();
                                    App.CurrentApp.HeaderRecord.uc_desc = uc_desc;

                                    App.CurrentApp.HeaderRecord.iRecordType = 1;

                                    App.CurrentApp.HeaderRecord.front_house_photos = 1;

                                    if ((typeA == "Fitting") || (typeA == "Unfinished") || (typeA == "Remedial") || (typeA == "Securing"))
                                    {
                                        App.CurrentApp.HeaderRecord.bDamTicked = true;

                                        switch (udi_tlight)
                                        {
                                            case "3":
                                                App.CurrentApp.HeaderRecord.udi_tlight = 0;
                                                break;
                                            case "2":
                                                App.CurrentApp.HeaderRecord.udi_tlight = 1;
                                                break;
                                            case "1":
                                                App.CurrentApp.HeaderRecord.udi_tlight = 2;
                                                break;
                                        }

                                        App.CurrentApp.HeaderRecord.si_bday1 = si_bday1;
                                        App.CurrentApp.HeaderRecord.uc_excess = Convert.ToDouble(uc_excess);
                                        App.CurrentApp.HeaderRecord.udi_staff = udi_staff;

                                        //if ((imchup == "None") || (imchup == "NONE"))
                                        App.CurrentApp.HeaderRecord.imchup = 0;
                                        //else
                                        //  App.CurrentApp.HeaderRecord.imchup = Convert.ToInt32( imchup);

                                        /*
                                        if (bFound == true)
                                        {
                                            App.CurrentApp.DB.Headers.DeleteOnSubmit(App.CurrentApp.HeaderRecord);
                                        }
                                        if (true)//(bFound == false)
                                        {
                                            App.CurrentApp.DB.Headers.InsertOnSubmit(App.CurrentApp.HeaderRecord);
                                            App.CurrentApp.DB.SubmitChanges();
                                        }
                                        */
                                        if (bFound == false)
                                        {
                                            App.CurrentApp.total_panels = 0;
                                            App.CurrentApp.total_alum = 0;
                                            App.CurrentApp.total_comp = 0;
                                            App.CurrentApp.total_cons = 0;
                                            App.CurrentApp.total_garage = 0;
                                            App.CurrentApp.total_glass = 0;
                                            App.CurrentApp.total_items = 0;
                                            App.CurrentApp.total_lock = 0;
                                            App.CurrentApp.total_timber = 0;
                                            App.CurrentApp.total_upvc = 0;
                                            App.CurrentApp.total_bifold = 0;
                                            //App.CurrentApp.total_ = 0;

                                            LoadMRSItems();
                                        }

                                        App.CurrentApp.CountItems();

                                        bFound = false;
                                        //var query2 = from p in App.CurrentApp.DB.Headers where p.udi_cont == App.CurrentApp.HeaderRecord.udi_cont select p;
                                        Header header = App.data.GetHeaderByContract(App.CurrentApp.HeaderRecord.udi_cont);
                                        if (header != null)
                                        {
                                            bFound = true;

                                            if (item.iRecordType == 0 && item.bSent == false)
                                            {

                                            }
                                            else
                                            {
                                                if (item.fit_diary != App.CurrentApp.HeaderRecord.fit_diary &&
                                                    DateTime.Parse(item.fit_diary) < DateTime.Today) // So that if the job is for tomorow then todays wont get deleted.
                                                {
                                                    App.data.DeleteContract(App.CurrentApp.HeaderRecord.udi_cont, false); // keep pictures
                                                    
                                                    App.data.SaveHeader();
                                                    if (total_added_contracts < 49)
                                                    {
                                                        if (udi_cont.Length > 7)
                                                        {
                                                            added_contracts[total_added_contracts] = App.CurrentApp.HeaderRecord.udi_cont;
                                                            total_added_contracts++;
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        if (bFound == false)
                                        {
                                            App.data.SaveHeader();
                                            if (total_added_contracts < 49)
                                            {
                                                if (udi_cont.Length > 7)
                                                {
                                                    added_contracts[total_added_contracts] = App.CurrentApp.HeaderRecord.udi_cont;
                                                    total_added_contracts++;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        App.CurrentApp.HeaderRecord.iRecordType = 1;

                                        Header header = App.data.GetHeaderByContract(App.CurrentApp.HeaderRecord.udi_cont);
                                        if (header == null)
                                        {
                                            App.data.SaveHeader();
                                        }
                                    }
                                }
                                else
                                {

                                }
                            }

                            if (total_fitting_images > 0)
                            {
                                download_stage = 1;
                                GetFittingImage();
                            }
                            else
                            {
                                Navigation.InsertPageBefore(new GetMessages(), this);
                                Navigation.PopAsync(false);
                            }

                        }
                        catch (Exception e)
                        {
                            //MessageBox.Show(e.ToString());
                        }
                    }
                }
            }
            else
            {
                if (download_stage == 1)
                {

                    if ((sendResponse.Length > 0) && (sendResponse.Substring(0, 10) != "<Fittings>"))
                    {
                        if (sendResponse.Substring(0, 19) == "<WrongSerialNumber>")
                        {
                            //MessageBox.Show("Your serial number is incorrect.");
                        }
                        else
                        {
                            //MessageBox.Show("There was an error contacting the server, please try again later.");
                        }
                        Navigation.PopAsync(false);
                        //NavigationService.GoBack();
                    }
                    else
                    {
                        if (sendResponse.Substring(0, 10) == "<Fittings>")
                        {
                            try // Fitting
                            {
                                XDocument xml = XDocument.Parse(sendResponse);
                                bool bFound = false;
                                try
                                {
                                    foreach (var word in xml.Element("Fittings").Elements("Image"))
                                    {
                                        String tempstring = ((String)word.Element("image_data"));

                                        byte[] decoded_image_data = System.Convert.FromBase64CharArray(tempstring.ToCharArray(), 0, tempstring.Length);

                                        if (images_list[current_fitting_image].image_filename.Substring(9, 1) == "c")
                                        {
                                            string newfname = images_list[current_fitting_image].image_contract + "_cA" + images_list[current_fitting_image].image_filename.Substring(11);
                                            App.files.SaveBinary("Photos/" + newfname, decoded_image_data);
                                        }
                                        if (images_list[current_fitting_image].image_filename.Substring(9, 1) == "f")
                                        {
                                            string newfname = images_list[current_fitting_image].image_contract + "_fA" + images_list[current_fitting_image].image_filename.Substring(11);
                                            App.files.SaveBinary("Photos/" + newfname, decoded_image_data);
                                        }
                                        if (images_list[current_fitting_image].image_filename.Substring(9, 1) == "d")
                                        {
                                            string newfname = images_list[current_fitting_image].image_contract + "_dA" + images_list[current_fitting_image].image_filename.Substring(11);
                                            App.files.SaveBinary("Drawings/" + newfname, decoded_image_data);
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    current_fitting_image--;
                                }

                                current_fitting_image++;
                                if (current_fitting_image > total_fitting_images - 1)
                                {
                                    Navigation.InsertPageBefore(new GetMessages(), this);
                                    Navigation.PopAsync(false);
                                }
                                else
                                {
                                    GetFittingImage();
                                }
                            }
                            catch (Exception e)
                            {
                                Navigation.PopAsync(false);
                            }
                        }
                    }
                }
            }
        }

        public void LoadMRSItems()
        {
            //bool bFinished = false;

            next_line = mrs_sr.ReadLine();

            do
            {
                switch (next_line)
                {
                    case "**PSALU**": load_aluminium(); break;
                    case "**PSBIFOLD**": load_bifold(); break;
                    case "**PSCOMPDOOR**": load_composite(); break;
                    case "**PSCONS**": load_conservatory(); break;
                    case "**PSGAR**": load_garage(); break;
                    case "**PSGLAS**": load_glass(); break;
                    case "**PSGREEN**": load_greenhouse(); break;
                    case "**LPSLOCKS**": load_lock(); break;
                    case "**PSPAN**": load_panel(); break;
                    case "**PSTIMB**": load_timber(); break;
                    case "**PSUPVC**": load_upvc(); break;
                    default:
                        next_line = mrs_sr.ReadLine(); // We didn't understand the item - probably extra stuff on end of header, skip to next line
                        break;
                }
                /*
            do
            {
                if (bFinished == false)
                {
                    string next_item = mrs_sr.ReadLine();
                    switch (next_item)
                    {
                        case "**PANEL**": LoadMRSPanel(); break;
                        case "**ALU**": LoadMRSAlum(); break;
                        case "**UPVC**": LoadMRSUPVC(); break;
                        case "**CONS**": LoadMRSCons(); break;
                        case "**GARAGE**": LoadMRSGarage(); break;
                        case "**TIMBER**": LoadMRSTimber(); break;
                        case "**GLASS**": LoadMRSGlass(); break;
                        case "**Lock**": LoadMRSLock(); break;
                        case "**Comp**": LoadMRSComp(); break;
                        case "**GREEN**": LoadMRSGreen(); break;
                        case "**BIFOLD**": LoadMRSBifold(); break;
                        default: if (mrs_sr.EndOfStream == true) { bFinished = true; }; break;
                    }
                }
                    */
            } while (!mrs_sr.EndOfStream);
        }

    }
}