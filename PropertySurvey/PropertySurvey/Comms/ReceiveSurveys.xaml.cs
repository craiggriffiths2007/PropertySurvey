using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Threading;
using System.Xml.Linq;
using System.IO;
using System.Globalization;
using Xamarin.Essentials;

using System.Text.Json;
using System.Net.Http;

namespace PropertySurvey
{
    public static class IntegerExtensions
    {
        public static int ParseInt(this string value)
        {
            int parsedInt;
            if (int.TryParse(value, out parsedInt))
                return parsedInt;

            return 0;
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceiveSurveys : ReceiveShared
    {
        string sendResponse = "";
        public String[] fileNames;

        StringBuilder postData = new StringBuilder();

        private ManualResetEvent _waitHandle = new ManualResetEvent(false);

        string[] image_filename;

        int download_stage = 0;

        HttpClient client;
        JsonSerializerOptions serializerOptions;


        public ReceiveSurveys()
        {
            InitializeComponent();

            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            GetSurveysJson();
            //Device.BeginInvokeOnMainThread(Start);
        }

        private async void Start()
        {
            await GetSurveysJson();
        }

        public class GetSurveysDTO
        {
            public string SurveyorCode { get; set; }

        }

        public class JobDTO
        {
            public int Id { get; set; }
            public int ContractId { get; set; }
            public string udi_cont { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            public string Name { get; set; }
            public string Add1 { get; set; }
            public string Add2 { get; set; }
            public string Add3 { get; set; }
            public string Postcode { get; set; }
            public string Phone1 { get; set; }
            public string Phone2 { get; set; }
            public string Phone3 { get; set; }
            public string DamageDesc { get; set; }
            public string Instructions { get; set; }
        }

        public async Task GetSurveysJson()
        {
            Uri uri = new Uri(string.Format(App.net.App_Settings.set_url + "/GetSurveyJobs", string.Empty));

            try
            {
                GetSurveysDTO send_record = new GetSurveysDTO();
                send_record.SurveyorCode = App.net.App_Settings.set_ownercode;

                string json = JsonSerializer.Serialize<GetSurveysDTO>(send_record, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string receive_content = await response.Content.ReadAsStringAsync();
                    List<JobDTO> receive_record = JsonSerializer.Deserialize<List<JobDTO>>(receive_content, serializerOptions);

                    foreach (var j in receive_record)
                    {
                        App.net.HeaderRecord = App.data.GetHeaderByContract(j.udi_cont);

                        if (App.net.HeaderRecord == null)
                        {
                            App.net.HeaderRecord = new Header();
                            App.net.table_init.CreateHeader(); 
                                                               
                            App.net.HeaderRecord.udi_cont = j.udi_cont;
                        }
                        else
                        {

                        }

                        App.net.HeaderRecord.iRecordType = 0;
                        App.net.HeaderRecord.udi_date = j.Date;
                        App.net.HeaderRecord.sn_name = "Insurer";
                        App.net.HeaderRecord.uc_name = j.Name;
                        App.net.HeaderRecord.uc_add1 = j.Add1;
                        App.net.HeaderRecord.uc_add2 = j.Add2;
                        App.net.HeaderRecord.uc_add3 = j.Add3;
                        App.net.HeaderRecord.uc_postcode = j.Postcode;

                        App.net.HeaderRecord.udi_jobtext = "";
                        App.net.HeaderRecord.udi_fin = j.Time;
                        App.net.HeaderRecord.uc_inceden = "03/11/2017";
                        App.net.HeaderRecord.COD_String = "Unknown";
                        App.net.HeaderRecord.udi_inst = j.Instructions;
                        App.net.HeaderRecord.policy_number = "001";
                        App.net.HeaderRecord.udi_start = j.Time;

                        App.data.AddSurveyHeader(App.net.HeaderRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.ToString(), "OK");
            }
            //Navigation.PopAsync();
        }


        public void GetSurveysXML()
        {
            XDocument srcTree = new XDocument(
            new XComment("This is a comment"),
            new XElement("User",
            new XElement("PDAUser", (App.net.receive_test_data ? "H1" : App.net.App_Settings.set_ownercode)),
            new XElement("Branch", "LUTN"),
            new XElement("PhoneSerial", "@" /*App.CurrentApp.cereal*/),
            new XElement("UserType", App.net.App_Settings.set_usertype)));

            postData.Append(srcTree.ToString());

            Uri thisuri = new Uri((App.net.receive_test_data? App.net.live_url: App.net.App_Settings.set_url) + "/WM7Communication/WM7DownloadSurveysMP");
            HttpHelper helper = new HttpHelper(thisuri, "POST",
            new KeyValuePair<string, string>("ContractFile", "<JobData>" + postData.ToString() + "</JobData>"));
            helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
            helper.Execute();
        }

        private async void CompleteDownload()
        {
            if ((sendResponse.Length > 18) && sendResponse.Contains("<WrongSerialNumber>")) 
            {
                await DisplayAlert("Serial number incorrect", "", "OK");
            }
            else
            {
                //if (sendResponse.Substring(0, 9) != "<Surveys>")
                //{
                //    await DisplayAlert("Alert", "Communication error", "OK");
                //}
            }
            if (sendResponse == "nointernet")
            {
                App.net.bCreateIndex = false;
                complete_label.Text = "no Internet Connection";
                complete_label.IsVisible = true;
                //DisplayAlert("No Internet Connection", "", "OK");
            }
            else
            {
                if(App.net.App_Settings.set_usertype == "SAT")
                {
                    Navigation.InsertPageBefore(new ReceiveFitting(), this);
                    await Navigation.PopAsync(false);
                }
                else
                {
                    Navigation.InsertPageBefore(new GetMessages(), this);
                    await Navigation.PopAsync(false);
                }
            }
            //Navigation.PopAsync(false);
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


        private void CommandComplete(HttpResponseCompleteEventArgs e)
        {
            
            //receive_label
            //receive_label.Text = receive_label.Text + "\nresponse";
            sendResponse = e.Response;

            if (true)
            {
                if (download_stage == 0)
                {
                    if ((sendResponse.Length > 8) && (sendResponse.Substring(0, 9) != "<Surveys>") && (sendResponse.Substring(0, 10) != "<Fittings>"))
                    {
                        /*
                        if (sendResponse.Substring(0, 19) == "<WrongSerialNumber>")
                        {
                            DisplayAlert("Alert", "You have been alerted", "OK");
                        }
                        else
                        {
                            DisplayAlert("Alert", "There was an error contacting the server, please try again later.", "OK");
                        }
                        Navigation.PopAsync(false);
                        */
                        //App.data.AddSurveyHeader(App.net.HeaderRecord);
                        Device.BeginInvokeOnMainThread(CompleteDownload);
                    }
                    else
                    {
                        if ((sendResponse.Length > 8) && sendResponse.Substring(0, 9) == "<Surveys>")
                        {
                            try
                            {
                                XDocument xml = XDocument.Parse(sendResponse);



                                foreach (var word in xml.Element("Surveys").Elements())
                                {
                                    string udi_cont = (string)word.Element("udi_cont");
                                    string uc_name = (string)word.Element("uc_name");
                                    string uc_postcode = (string)word.Element("uc_postcode");
                                    string udi_start = (string)word.Element("udi_start");
                                    string udi_fin = (string)word.Element("udi_fin");
                                    string add_long = (string)word.Element("add_long");
                                    string add1 = (string)word.Element("add1");
                                    string add2 = (string)word.Element("add2");
                                    string add3 = (string)word.Element("add3");
                                    string uc_h_phone = (string)word.Element("uc_h_phone");
                                    string udi_date = (string)word.Element("udi_date");
                                    string sn_name = (string)word.Element("sn_name");
                                    string uc_laname = (string)word.Element("uc_laname");
                                    string goaheadstr = (string)word.Element("goaheadstr");
                                    string uc_inceden = (string)word.Element("uc_inceden");
                                    string uc_desc = (string)word.Element("uc_desc");
                                    string udi_tlight = (string)word.Element("udi_tlight");
                                    string uc_excess = (string)word.Element("uc_excess");
                                    string udi_inst = (string)word.Element("udi_inst");
                                    string udi_jobtext = (string)word.Element("udi_jobtext");
                                    string udi_staff = (string)word.Element("udi_staff");
                                    string type = (string)word.Element("type");
                                    string uc_h_phone2 = (string)word.Element("uc_h_phone2");
                                    string uc_h_phone3 = (string)word.Element("uc_h_phone3");
                                    string policy_number = (string)word.Element("policy_number");
                                    string refmessage = (string)word.Element("refmessage");
                                    string COD_Code = (string)word.Element("COD_Code");
                                    string cover_instructions = (string)word.Element("cover_instructions");
                                    string truecomm = (string)word.Element("truecomm");
                                    string b_mrk = (string)word.Element("b_mrk");
                                    string add_phone_1 = (string)word.Element("add_phone_1");
                                    string add_phone_2 = (string)word.Element("add_phone_2");
                                    string ownquote = (string)word.Element("ownquote");
                                    string zur_com = (string)word.Element("zur_com");
                                    string ins_account_code = (string)word.Element("ins_account_code");
                                    string is_tenant = (string)word.Element("tenant");
                                    string jobblob = (string)word.Element("jobblob"); // previus survey
                                    string gtd_string = (string)word.Element("gfd_url");

                                    if (App.net.receive_test_data == true)
                                    {
                                        udi_cont = udi_cont.Replace("3", "0");

                                        string lastWord = uc_name.Split(' ').Last();
                                        string newWord = "";
                                        foreach (var c in lastWord)
                                        {
                                            newWord = newWord + "x";
                                        }
                                        uc_name = uc_name.Replace(lastWord, newWord);

                                        add_long = CensorString(add_long);
                                        add1 = CensorString(add1);
                                        add2 = CensorString(add2);
                                        add3 = CensorString(add3);
                                        sn_name = CensorString(sn_name);

                                        uc_postcode = CensorString(uc_postcode);
                                    }

                                    //if (ins_account_code.Contains("ZUR") || ins_account_code.Contains("DLG"))

                                    if (zur_com != null)
                                    {
                                        if (ins_account_code != null)
                                        {
                                            if (ins_account_code.Contains("ZUR") && zur_com == "True")
                                            {
                                                sn_name = sn_name + "  (comm)";
                                            }
                                        }
                                        else
                                        {
                                            if (sn_name.Contains("Zurich") && zur_com == "True")
                                            {
                                                sn_name = sn_name + "  (comm)";
                                            }
                                        }
                                    }

                                    App.net.HeaderRecord = App.data.GetHeaderByContract(udi_cont);

                                    if (App.net.HeaderRecord == null)
                                    {
                                        App.net.HeaderRecord = new Header();
                                        App.net.table_init.CreateHeader(); // Initialize
                                        //App.net.motor_sheet = new MotorSheet();
                                    }
                                    else
                                    {
                                        if (DateTime.Parse(udi_date) < DateTime.Today)
                                        {
                                            App.data.DeleteContract(udi_cont);
                                        }
                                    }
                                    CultureInfo provider = CultureInfo.InvariantCulture;

                                    if (is_tenant == "Yes")
                                    {
                                        App.net.HeaderRecord.uspot_p4 = 1;
                                    }
                                    else
                                    {
                                        App.net.HeaderRecord.uspot_p4 = 2;
                                    }


                                    if (ownquote == "Y")
                                    {
                                        App.net.HeaderRecord.ownquote = 1;
                                    }
                                    else
                                    {
                                        App.net.HeaderRecord.ownquote = 2;
                                    }

                                    if (App.net.HeaderRecord.typeA == "Spot check")
                                    {
                                        App.net.HeaderRecord.b_mrk = true;
                                    }

                                    if (ins_account_code != null)
                                    {
                                        App.net.HeaderRecord.fname1 = ins_account_code;
                                    }

                                    App.net.HeaderRecord.iRecordType = 0;

                                    App.net.HeaderRecord.fit_diary = DateTime.Today.ToString();
                                    if (uc_inceden.Length < 8)
                                        uc_inceden = "01/01/1998";

                                    App.net.HeaderRecord.uc_inceden = DateTime.Parse(uc_inceden).ToString();
                                    App.net.HeaderRecord.expiry = DateTime.Today.ToString();
                                    App.net.HeaderRecord.udi_start = DateTime.Parse(udi_start).ToString();
                                    udi_date = udi_date.Substring(0, 6) + "20" + udi_date.Substring(6, 2);

                                    //App.net.HeaderRecord.udi_date = DateTime.ParseExact(udi_date, format, provider);
                                    App.net.HeaderRecord.udi_date = DateTime.Parse(udi_date).ToString();
                                    App.net.HeaderRecord.udi_fin = DateTime.Parse(udi_fin).ToString();

                                    //App.net.HeaderRecord.udi_date = DateTime.Today;
                                    App.net.HeaderRecord.fmdate = DateTime.Today.ToString();
                                    App.net.HeaderRecord.ftimearr = DateTime.Today.ToString();
                                    App.net.HeaderRecord.ftimeleft = DateTime.Today.ToString();
                                    App.net.HeaderRecord.uspot_date = DateTime.Today.ToString();
                                    App.net.HeaderRecord.uspot_signeddate = DateTime.Today.ToString();
                                    App.net.HeaderRecord.f_sign_date = DateTime.Today.ToString();
                                    App.net.HeaderRecord.uc_excess = Convert.ToDouble(uc_excess);
                                    App.net.HeaderRecord.new_sspare7 = gtd_string;

                                    if (b_mrk == "Y")
                                    {
                                        App.net.HeaderRecord.b_mrk = true;
                                    }
                                    else
                                    {
                                        App.net.HeaderRecord.b_mrk = false;
                                    }

                                    App.net.HeaderRecord.udi_cont = udi_cont;
                                    App.net.HeaderRecord.uc_name = uc_name;
                                    App.net.HeaderRecord.udi_start = DateTime.Parse(udi_start).ToString();
                                    App.net.HeaderRecord.udi_fin = DateTime.Parse(udi_fin).ToString();

                                    App.net.HeaderRecord.uc_postcode = uc_postcode;
                                    App.net.HeaderRecord.add_long = add_long;
                                    App.net.HeaderRecord.uc_add1 = add1;
                                    App.net.HeaderRecord.uc_add2 = add2;
                                    App.net.HeaderRecord.uc_add3 = add3;
                                    App.net.HeaderRecord.uc_h_phone = uc_h_phone;

                                    //App.net.HeaderRecord.udi_date=
                                    //App.net.HeaderRecord.udi_date
                                    App.net.HeaderRecord.sn_name = sn_name;

                                    App.net.HeaderRecord.uc_laname = uc_laname;
                                    App.net.HeaderRecord.goaheadstr = goaheadstr;

                                    //App.net.HeaderRecord.udi_date=udi_date;
                                    App.net.HeaderRecord.uc_desc = uc_desc;
                                    App.net.HeaderRecord.udi_tlight = udi_tlight.ParseInt();
                                    if (App.net.HeaderRecord.udi_tlight > 2)
                                    {
                                        App.net.HeaderRecord.udi_tlight = 2;
                                    }
                                    if (App.net.HeaderRecord.udi_tlight < 0)
                                    {
                                        App.net.HeaderRecord.udi_tlight = 0;
                                    }
                                    //App.net.HeaderRecord.udi_tlight = 3;
                                    App.net.HeaderRecord.udi_inst = udi_inst;
                                    App.net.HeaderRecord.udi_jobtext = udi_jobtext;
                                    App.net.HeaderRecord.udi_staff = udi_staff;
                                    App.net.HeaderRecord.type = type;
                                    App.net.HeaderRecord.uc_h_phone2 = uc_h_phone2;
                                    App.net.HeaderRecord.uc_h_phone3 = uc_h_phone3;
                                    App.net.HeaderRecord.uc_h_phone3 = uc_h_phone3;
                                    App.net.HeaderRecord.refmessage = refmessage;
                                    App.net.HeaderRecord.COD_Code = COD_Code;

                                    if (jobblob != null)
                                    {
                                        //bool bFinished = false;

                                        byte[] byteArray = System.Convert.FromBase64String(jobblob);
                                        MemoryStream stream = new MemoryStream(byteArray);

                                        mrs_sr = new StreamReader(stream);

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
                                        } while (bFinished == false);
                                        */
                                        } while (!mrs_sr.EndOfStream) ;
                                    }

                                App.CurrentApp.CountItems();

                                    if (add_phone_1 == null)
                                    {
                                        App.net.HeaderRecord.add_phone_1 = "";
                                    }
                                    else
                                    {
                                        App.net.HeaderRecord.add_phone_1 = add_phone_1;
                                    }
                                    if (add_phone_2 == null)
                                    {
                                        App.net.HeaderRecord.add_phone_2 = "";
                                    }
                                    else
                                    {
                                        App.net.HeaderRecord.add_phone_2 = add_phone_2;
                                    }

                                    if (App.net.HeaderRecord.COD_Code.Length == 0)
                                    {
                                        App.net.HeaderRecord.COD_Code = "UK";
                                    }

                                    App.net.HeaderRecord.COD_String = "";

                                    if (App.net.HeaderRecord.COD_Code == "UK")
                                    {
                                        App.net.HeaderRecord.COD_String = "Unknown";
                                    }
                                    if (App.net.HeaderRecord.COD_Code == "TH")
                                    {
                                        App.net.HeaderRecord.COD_String = "Theft";
                                    }
                                    if (App.net.HeaderRecord.COD_Code == "ST")
                                    {
                                        App.net.HeaderRecord.COD_String = "Storm Damage";
                                    }
                                    if (App.net.HeaderRecord.COD_Code == "WT")
                                    {
                                        App.net.HeaderRecord.COD_String = "Wear + Tear";
                                    }
                                    if (App.net.HeaderRecord.COD_Code == "BW")
                                    {
                                        App.net.HeaderRecord.COD_String = "Bad Workmanship";
                                    }
                                    if (App.net.HeaderRecord.COD_Code == "CI")
                                    {
                                        App.net.HeaderRecord.COD_String = "Claim inconsistency";
                                    }
                                    if (App.net.HeaderRecord.COD_Code == "AD")
                                    {
                                        App.net.HeaderRecord.COD_String = "Accidental Damage";
                                    }

                                    if (App.net.HeaderRecord.COD_Code == "AL")
                                    {
                                        App.net.HeaderRecord.COD_String = "Loss of keys";
                                    }

                                    //		if( !strcmp( access->temp.COD_Code,  "AL" ) )
                                    //		{
                                    //			strcpy( access->temp.COD_String , "Accidental Loss" );
                                    //		}
                                    if (App.net.HeaderRecord.COD_Code == "FI")
                                    {
                                        App.net.HeaderRecord.COD_String = "Fire";
                                    }
                                    if (App.net.HeaderRecord.COD_Code == "FL")
                                    {
                                        App.net.HeaderRecord.COD_String = "Flood";
                                    }
                                    if (App.net.HeaderRecord.COD_Code == "GM")
                                    {
                                        App.net.HeaderRecord.COD_String = "Ground Movement";
                                    }
                                    if (App.net.HeaderRecord.COD_Code == "IM")
                                    {
                                        App.net.HeaderRecord.COD_String = "Impact";
                                    }
                                    if (App.net.HeaderRecord.COD_Code == "MD")
                                    {
                                        App.net.HeaderRecord.COD_String = "Malicious";
                                    }

                                    App.net.HeaderRecord.cover_instructions = cover_instructions;
                                    if (truecomm == "Y")
                                    {
                                        App.net.HeaderRecord.truecomm = true;
                                    }
                                    else
                                    {
                                        App.net.HeaderRecord.truecomm = false;
                                    }

                                    App.data.AddSurveyHeader(App.net.HeaderRecord);
                                }


                                download_stage = 2;
                                Device.BeginInvokeOnMainThread(CompleteDownload);

                                return;
                            }
                            catch (Exception)
                            {
                                //DisplayAlert("Alert", e1.ToString(), "OK");

                                Device.BeginInvokeOnMainThread(CompleteDownload);
                                //MessageBox.Show(e.ToString());
                            }
                        }
                    }
                }
            }
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

    }



}

