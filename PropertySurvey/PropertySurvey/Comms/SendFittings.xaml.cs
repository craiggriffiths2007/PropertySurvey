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
    public partial class SendFittings : ContentPage
    {
        List<Header> headers = null;

        List<string> images_to_send = new List<string>();

        StringBuilder postData = new StringBuilder(128000);
        StringBuilder FittingImage = new StringBuilder(128000);

        string sendResponse = "";

        int total_surveys;
        int current_survey;

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

        public SendFittings()
        {
            InitializeComponent();

            headers = App.data.GetUnsentHeadersFittings();

            total_surveys = headers.Count();
            total_items_to_send = total_surveys;
            current_survey = 0;
            current_image = 0;
            if (headers.Count() > 0)
            {
                CreateImagesList();
                total_items_to_send += images_to_send.Count();
                if (total_images > 0)
                {
                    try
                    {
                        SendNextPicture();
                    }
                    catch (Exception e)
                    {
                        DisplayAlert("error sending", images_to_send[current_image], "OK");
                    }
                }
                else
                {
                    if (total_surveys > 0)
                    {
                        //SendNextSurvey();
                        SendNextFitting();
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

            foreach (var header in headers)
            {
                images_to_send.AddRange(App.files.GetFileList("Photos/", header.udi_cont + "*_f*.*", "Photos/"));
                images_to_send.AddRange(App.files.GetFileList("Photos/SS/", header.udi_cont + "*_f*.*", "Photos/SS/"));
                images_to_send.AddRange(App.files.GetFileList("Drawings/", header.udi_cont + "*_f*.*", "Drawings/"));
                images_to_send.AddRange(App.files.GetFileList("Signatures/", header.udi_cont + "*_f*.*", "Signatures/"));
                images_to_send.AddRange(App.files.GetFileList("Videos/", header.udi_cont + "*_f*.*", "Videos/"));

                images_to_send.AddRange(App.files.GetFileList("Photos/", header.udi_cont + "*_r*.*", "Photos/"));
                images_to_send.AddRange(App.files.GetFileList("Photos/SS/", header.udi_cont + "*_r*.*", "Photos/SS/"));
                images_to_send.AddRange(App.files.GetFileList("Drawings/", header.udi_cont + "*_r*.*", "Drawings/"));
                images_to_send.AddRange(App.files.GetFileList("Signatures/", header.udi_cont + "*_r*.*", "Signatures/"));
                images_to_send.AddRange(App.files.GetFileList("Videos/", header.udi_cont + "*_r*.*", "Videos/"));

                images_to_send.AddRange(App.files.GetFileList("Photos/", header.udi_cont + "*_s*.*", "Photos/"));
                images_to_send.AddRange(App.files.GetFileList("Photos/SS/", header.udi_cont + "*_s*.*", "Photos/SS/"));
                images_to_send.AddRange(App.files.GetFileList("Drawings/", header.udi_cont + "*_s*.*", "Drawings/"));
                images_to_send.AddRange(App.files.GetFileList("Signatures/", header.udi_cont + "*_s*.*", "Signatures/"));
                images_to_send.AddRange(App.files.GetFileList("Videos/", header.udi_cont + "*_s*.*", "Videos/"));
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
                    catch (Exception e)
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
                        //CreateSurvey();
                        // SendNextSurvey();

                        try
                        {
                            SendNextFitting();
                        }
                        catch (Exception e)
                        {
                            DisplayAlert("error sending", headers[current_survey].udi_cont, "OK");
                        }
                        
                        current_survey++;
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

        private void SendNextFitting()
        {
            string job_type = "";
            FittingImage = new StringBuilder(128000);

            if (headers[current_survey].iRecordType == 1 || headers[current_survey].iRecordType == 2)
            {
                switch (headers[current_survey].typeB)
                {
                    case "Remedial": AddRemedial(); job_type = "R"; break;
                    //case "Securing": AddSecuring(); AddGlass(); WriteLocking(); job_type = "I"; break; // obsolete
                    default: AddFitting(); job_type = "F"; break;
                }
            }

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(FittingImage.ToString());

            XDocument localTree = new XDocument(
            new XElement("ContractFile",
            new XElement("UserType", App.net.App_Settings.set_usertype),
            new XElement("JobType", job_type),
            new XElement("CreateEnd", "true"),
            new XElement("UserCode", App.net.App_Settings.set_ownercode),

            new XElement("ContractNumber", headers[current_survey].udi_cont),
            new XElement("Job", System.Convert.ToBase64String(buffer))));

            Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/SendJobFile");
            HttpHelper helper = new HttpHelper(thisuri, "POST",
            new KeyValuePair<string, string>("ContractFile", localTree.ToString()));
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

        private void AddRemedial()
        {
            Header item = headers[current_survey];

            FittingImage.Append("**HEADER**\n");

            FittingImage.Append("contract=    " + item.udi_cont + "\n");
            FittingImage.Append("rfault=      " + item.r_fault + "\n");

            switch (item.r_excess)
            {
                case 0: FittingImage.Append("rexcess=     \n"); break;
                case 1: FittingImage.Append("rexcess=     Yes\n"); break;
                case 2: FittingImage.Append("rexcess=     No\n"); break;
            }

            FittingImage.Append("rexcedit=    " + item.rexcedit + "\n");

            switch (item.r_comp)
            {
                case 0: FittingImage.Append("rcomp=       \n"); break;
                case 1: FittingImage.Append("rcomp=       Yes\n"); break;
                case 2: FittingImage.Append("rcomp=       No\n"); break;
            }

            FittingImage.Append("rnohours=    " + item.rno_hours + "\n");
            FittingImage.Append("rworktxt==   \n" + item.r_work_txt + "\n");

            if (item.bad_image_complete == true)
            {
                FittingImage.Append("rsignimage=  True\n");
            }
            else
            {
                FittingImage.Append("rsignimage=  False\n");
            }

            if (item.si_done == true)
            {
                FittingImage.Append("readdtxt1==  \n" + "QUALITY REPORT:" + item.rep_text + "  ADDITIONAL:" + item.readdtxt + "\n");
                FittingImage.Append("readdtxt2==  \n\n");
            }
            else
            {
                FittingImage.Append("readdtxt1==  \n" + item.readdtxt + "\n");
                FittingImage.Append("readdtxt2==  \n\n");
            }



            if (item.bad_image_complete == true)
            {
                FittingImage.Append("rsignimage=  True\n");
            }
            else
            {
                FittingImage.Append("rsignimage=  False\n");
            }

            FittingImage.Append("thedate=     " + item.f_sign_date + "\n");

            FittingImage.Append("fitter=      " + App.net.App_Settings.set_ownercode + "\n");

            FittingImage.Append("fname1=      " + item.fname1 + "\n");
            FittingImage.Append("fname2=      " + item.fname2 + "\n");
            FittingImage.Append("fname3=      " + item.fname3 + "\n");
            FittingImage.Append("fname4=      " + item.fname4 + "\n");
            FittingImage.Append("fname5=      " + item.fname5 + "\n");
            FittingImage.Append("fname6=      " + item.fname6 + "\n");
            FittingImage.Append("fname7=      " + item.fname7 + "\n");
            FittingImage.Append("fname8=      " + item.fname8 + "\n");
            // reason for work carried out
            FittingImage.Append("rfwco=       " + item.doc_l_compliant_reason + "\n");


        }

        private void AddFitting()
        {
            Header item = headers[current_survey];
            //string date_string = String.Format("{0:d}", App.CurrentApp.HeaderRecord.uspot_date);
            //date_string = date_string.Substring(0, 6) + String.Format("{0:d}", App.CurrentApp.HeaderRecord.uspot_date).Substring(8, 2);

            FittingImage.Append("**HEADER**\n");

            FittingImage.Append("contract=    " + item.udi_cont + "\n");
            FittingImage.Append("faddtxt1==   \n\n");
            FittingImage.Append("faddtxt2==   \n\n");

            FittingImage.Append("faddtxt2==   \n\n");

            FittingImage.Append("funfincode=  " + item.funfinished_code + "\n");
            FittingImage.Append("funfinoth=   \n");

            FittingImage.Append("freuntxt1==  \n" + item.freason_unfinished + "\n");
            FittingImage.Append("freuntxt2==  \n\n");

            FittingImage.Append("fpartreq1==  \n" + item.fparts_required + "\n");
            FittingImage.Append("fpartreq2==  \n\n");

            AddYesNoInt(item.bfitter_complete, "fjobfin=     ");

            FittingImage.Append("fname1=      " + item.fname1 + "\n");
            FittingImage.Append("fname2=      " + item.fname2 + "\n");
            FittingImage.Append("fname3=      " + item.fname3 + "\n");
            FittingImage.Append("fname4=      " + item.fname4 + "\n");
            FittingImage.Append("fname5=      " + item.fname5 + "\n");
            FittingImage.Append("fname6=      " + item.fname6 + "\n");
            FittingImage.Append("fname7=      " + item.fname7 + "\n");
            FittingImage.Append("fname8=      " + item.fname8 + "\n");

            AddYesNoInt(item.fbexcess_paid, "fexcess=     ");

            if (item.fbexcess_paid == 1)
            {
                FittingImage.Append("fexcessoth=  n/a\n");
            }
            else
            {
                FittingImage.Append("fexcessoth=  " + item.freason_excess_not_paid + "\n");
            }

            AddYesNoInt(item.fbmandate_signed, "fmand=       ");

            if (item.fbmandate_signed == 1)
            {
                FittingImage.Append("fmandoth=    n/a\n");
            }
            else
            {
                FittingImage.Append("fmandoth=    " + item.freason_mandate_not_signed + "\n");
            }

            //AddYesNoInt(item.fbexcess_paid, "fmand=       ");

            FittingImage.Append("ftimearr=    " + item.ftime_arrived.ToString().Substring(0, 5) + "\n");

            FittingImage.Append("ftimeleft=   " + item.ftime_left.ToString().Substring(0, 5) + "\n");

            AddYesNoInt(item.fbadditional_paid, "faddpaid=    ");

            FittingImage.Append("faddmuch=    " + item.fhow_mutch_additional_paid + "\n");

            FittingImage.Append("commtext1==   \n" + item.fitter_comments.Replace("\n", " ") + "\n");
            FittingImage.Append("commtext2==   \n\n"); // + item.fitter_comments + "\n");

            FittingImage.Append("wkcartxt1==   \n" + item.fitter_work.Replace("\n", " ") + "\n");
            FittingImage.Append("wkcartxt2==   \n\n"); // + item.fitter_comments + "\n");

            FittingImage.Append("parttext1==   \n" + item.parts_used.Replace("\n", " ") + "\n");
            FittingImage.Append("parttext2==   \n\n"); // + item.fitter_comments + "\n");

            FittingImage.Append("type=        " + item.typeA + "\n");

            FittingImage.Append("faddimage=   \n");
            FittingImage.Append("fmanimage=   \n" + item.fname3);
            FittingImage.Append("fsigimage=   \n" + item.fname4);

            FittingImage.Append("thedate=     " + String.Format("{0:dd/MM/yyyy}", item.fit_diary) + "\n");

            AddYesNoInt(item.ind, "InDam=       ");

            FittingImage.Append("InDamEd=     " + item.inevitable_damage + "\n");

            if (item.uspot_p2 == 1)
            {
                FittingImage.Append("IsReplace=   Yes\n");
            }
            else
            {
                FittingImage.Append("IsReplace=   No\n");
            }

            if (item.uspot_p1 == 1)
            {
                FittingImage.Append("IsLintel=    Yes\n");
            }
            else
            {
                FittingImage.Append("IsLintel=    No\n");
            }

            FittingImage.Append("compdate=    " + String.Format("{0:dd/MM/yyyy}", item.expiry) + "\n");

            if (item.i_spare3 == 1)
            {
                FittingImage.Append("hireeq=      Yes\n");
            }
            else
            {
                FittingImage.Append("hireeq=      No\n");
            }

            if (item.i_spare2 == 1)
            {
                item.s_spare3 = "Branch  -  " + item.s_spare3;
            }
            if (item.i_spare2 == 2)
            {
                item.s_spare3 = "Hired  -  " + item.s_spare3;
            }
            if (item.i_spare2 == 3)
            {
                item.s_spare3 = "None  -  " + item.s_spare3;
            }


            FittingImage.Append("hireeqtx=    " + item.s_spare3 + "\n");

            if (App.CurrentApp.HeaderRecord.door_type.Length > 0)
            {
                FittingImage.Append("door_type=   " + App.CurrentApp.HeaderRecord.door_type + "\n");
                FittingImage.Append("model_type=  " + App.CurrentApp.HeaderRecord.model_type + "\n");
                FittingImage.Append("serial_num=  " + App.CurrentApp.HeaderRecord.unique_serial + "\n");
                FittingImage.Append("door_size=   " + App.CurrentApp.HeaderRecord.door_size + "\n");
                FittingImage.Append("door_manuf=  " + App.CurrentApp.HeaderRecord.door_manufacturer + "\n");

                FittingImage.Append("op_type=     " + App.CurrentApp.HeaderRecord.powerered_operator_type + "\n");
                FittingImage.Append("op_manu=     " + App.CurrentApp.HeaderRecord.operator_manufacturer + "\n");

                FittingImage.Append("site_address=" + App.CurrentApp.HeaderRecord.site_address + "\n");
                FittingImage.Append("declare_by=  " + App.CurrentApp.HeaderRecord.decleration_by + "\n");
                FittingImage.Append("date=        " + App.CurrentApp.HeaderRecord.date + "\n");
                FittingImage.Append("behalf_of=   " + App.CurrentApp.HeaderRecord.on_behalf_of_person + "\n");
                FittingImage.Append("sig_filename=" + App.CurrentApp.HeaderRecord.s_spare1 + "\n");
                FittingImage.Append("sigcfilename=" + App.CurrentApp.HeaderRecord.s_spare2 + "\n");

                FittingImage.Append("cust_name=   " + App.CurrentApp.HeaderRecord.print_name + "\n");
                //Fitt
            }

            /*
            var query = from p in App.CurrentApp.DB.MotorSheet where p.udi_cont == item.udi_cont select p;
            foreach (var item in query)
            {
                if (App.CurrentApp.motor_sheet.s_spare1.Length > 0)
                {
                    FittingImage.Append("door_type=   " + App.CurrentApp.motor_sheet.door_type + "\n");
                    FittingImage.Append("model_type=  " + App.CurrentApp.motor_sheet.model_type + "\n");
                    FittingImage.Append("serial_num=  " + App.CurrentApp.motor_sheet.unique_serial + "\n");
                    FittingImage.Append("door_size=   " + App.CurrentApp.motor_sheet.door_size + "\n");
                    FittingImage.Append("door_manuf=  " + App.CurrentApp.motor_sheet.door_manufacturer + "\n");

                    FittingImage.Append("op_type=     " + App.CurrentApp.motor_sheet.powerered_operator_type + "\n");
                    FittingImage.Append("op_manu=     " + App.CurrentApp.motor_sheet.operator_manufacturer + "\n");

                    FittingImage.Append("site_address=" + App.CurrentApp.motor_sheet.site_address + "\n");
                    FittingImage.Append("declare_by=  " + App.CurrentApp.motor_sheet.decleration_by + "\n");
                    FittingImage.Append("date=        " + App.CurrentApp.motor_sheet.date + "\n");
                    FittingImage.Append("behalf_of=   " + App.CurrentApp.motor_sheet.on_behalf_of_person + "\n");
                    FittingImage.Append("sig_filename=" + App.CurrentApp.motor_sheet.s_spare1 + "\n");
                    FittingImage.Append("sigcfilename=" + App.CurrentApp.motor_sheet.s_spare2 + "\n");

                    FittingImage.Append("cust_name=   " + App.CurrentApp.motor_sheet.print_name + "\n");
                    FittingImage.Append("cust_date=   " + App.CurrentApp.motor_sheet.date_cust + "\n");
                }
            }
            */
            //item.expiry

            }

        public void SendNextPicture()
        {
            byte[] data;
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

            StringBuilder localpostData = new StringBuilder(128000);
            if (App.files.FileExists(this_image_filename) || (App.net.bCopyDatabaseFileToDownloads == true && this_image_filename == "PropertySurveySQLite.db3"))
            {
                if (this_image_filename == "PropertySurveySQLite.db3")
                    data = App.files.LoadBinaryFromDownloads(this_image_filename);
                else
                    data = App.files.LoadBinary(this_image_filename);

                XDocument localTree = new XDocument(
                new XElement("Image",
                new XElement("UserType", App.net.App_Settings.set_usertype),
                new XElement("JobType", "F"),
                new XElement("UserCode", App.net.App_Settings.set_ownercode),
                new XElement("LastImage", bLastImage),
                new XElement("Contract", "00000000"),
                new XElement("Filename", just_file_name.Replace("sAZIns", "fAZFit")),
                new XElement("data", App.net.bDoValidation ? System.Convert.ToBase64String(data) : "")));

                localpostData.Append(localTree.ToString());

                Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/SendJobImage");
                HttpHelper helper = new HttpHelper(thisuri, "POST",
                new KeyValuePair<string, string>("ContractFile", "<ContractFile>" + localpostData.ToString() + "</ContractFile>"));
                helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
                helper.Execute();
            }
        }
    }
}